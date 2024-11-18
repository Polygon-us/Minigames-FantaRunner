using Cysharp.Threading.Tasks;
using Nakama;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NakamaConnection : MonoBehaviour
{
    [SerializeField] private string url = "https://73c2-8-242-214-187.ngrok-free.app";
    [SerializeField] private string serverKey = "defaultkey";

    [Space] [SerializeField] private string leaderboardId = "trash_dash";

    [Space] [SerializeField] private string storageValueJson = "{\"Progreso\":{\"monedas\":13,\"color\":\"FFFFFF\"}}";

    //private const string SessionPrefName = "nakama.Session";
    private const string DeviceIdentifierPrefName = "nakama.deviceUniqueIdentifier";
    private const string UsernamePrefName = "nakama.username";

    private IClient Client;
    private ISession Session;
    //private ISocket Socket;

    private static NakamaConnection _instance;

    public static NakamaConnection Instance
    {
        get
        {
            if (_instance)
                return _instance;

            _instance = FindFirstObjectByType<NakamaConnection>();

            DontDestroyOnLoad(_instance.gameObject);

            return _instance;
        }
    }

    public bool HasRegistered()
    {
        return PlayerPrefs.HasKey(UsernamePrefName);
    }

    public void SetUsername(string username)
    {
        PlayerPrefs.SetString(UsernamePrefName, username);
    }

    public static string GetUsername()
    {
        return PlayerPrefs.GetString(UsernamePrefName);
    }

#if UNITY_EDITOR
    [ContextMenu("Delete Username")]
    public void DeleteUsername()
    {
        PlayerPrefs.DeleteKey(UsernamePrefName);
    }
#endif

    public async UniTask Connect()
    {
        Debug.Log("Connect");

        Client = new Client(new Uri(url), serverKey);

        //CacheSessionSearch(SessionPrefName);

        await AuthenticateSessionIfNull();

        // Socket = Client.NewSocket();
        // await Socket.ConnectAsync(Session, true);

        Debug.Log("Connect finish");
    }

    private async UniTask AuthenticateSessionIfNull()
    {
        Debug.Log("AuthenticateSessionIfNull");

        Session ??= await Client.AuthenticateDeviceAsync(GetDeviceIdentifier(), username: GetUsername());

        Debug.Log("AuthenticateSessionIfNull finish");
    }

    private string GetUserID()
    {
        return Session.UserId;
    }
    
    private static string GetDeviceIdentifier()
    {
        Debug.Log("GetDeviceIdentifier");
        
        if (PlayerPrefs.HasKey(DeviceIdentifierPrefName))
        {
            return PlayerPrefs.GetString(DeviceIdentifierPrefName);
        }

        string deviceId = SystemInfo.deviceUniqueIdentifier;

        if (deviceId == SystemInfo.unsupportedIdentifier)
        {
            deviceId = Guid.NewGuid().ToString();
        }

        PlayerPrefs.SetString(DeviceIdentifierPrefName, deviceId);

        return deviceId;
    }

    public async UniTask SendLeaderboard(int score)
    {
        Debug.Log("SendLeaderboard");

        await Client.WriteLeaderboardRecordAsync(Session, leaderboardId, score);

        Debug.Log("SendLeaderboard finish");
    }

    public async UniTask<List<IApiLeaderboardRecord>> GetLeaderboard()
    {
        Debug.Log("GetLeaderboard");

        IApiLeaderboardRecordList leaderboardRecords = await Client.ListLeaderboardRecordsAsync(Session, leaderboardId, limit: 10);

        Debug.Log("GetLeaderboard finish");

        return leaderboardRecords.Records.ToList();
    }

    public async Task<IApiLeaderboardRecord> GetPlayerLeaderboard()
    {
        Debug.Log("GetPlayerLeaderboard");

        IApiLeaderboardRecordList leaderboardRecords = await Client.ListLeaderboardRecordsAsync(Session, leaderboardId, new[] { GetUserID() }, limit: 1);

        Debug.Log("GetPlayerLeaderboard finish");

        return leaderboardRecords.OwnerRecords.FirstOrDefault(record => record.OwnerId == GetUserID());
    }

    [ContextMenu("Test Send Storage")]
    public async UniTaskVoid SendStorage()
    {
        Debug.Log("SendStorage");

        WriteStorageObject[] objects = new[]
        {
            new WriteStorageObject
            {
                Collection = "test_progress",
                Key = "test_data",
                Value = "Test data",
                PermissionRead = 2, // 2 = Public (others can read if needed)
                PermissionWrite = 1 // 1 = Private (only the owner can write)
            },
            new WriteStorageObject
            {
                Collection = "player_progress",
                Key = "progress_data",
                Value = storageValueJson,
                PermissionRead = 2, // 2 = Public (others can read if needed)
                PermissionWrite = 1 // 1 = Private (only the owner can write)
            }
        };

        await Client.WriteStorageObjectsAsync(Session, objects);

        Debug.Log("SendStorage finish");
    }
}