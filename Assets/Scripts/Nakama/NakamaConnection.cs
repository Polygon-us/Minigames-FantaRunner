using Cysharp.Threading.Tasks;
using Nakama;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NakamaConnection : MonoBehaviour
{
    [SerializeField] private GameEnvironment gameEnvironment;

    [SerializeField] private string serverKey = "defaultkey";

    [Space] [SerializeField] private string leaderboardId = "trash_dash";

    //private const string SessionPrefName = "nakama.Session";
    private const string DeviceIdentifierPrefName = "nakama.deviceUniqueIdentifier";
    private const string UsernamePrefName = "nakama.username";

    private IClient Client;
    private ISession Session;

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

    [ContextMenu("Delete Username")]
    public void DeleteUsername()
    {
        PlayerPrefs.DeleteKey(UsernamePrefName);
    }

    public async UniTask<bool> Connect()
    {
        Debug.Log("Connect");

        try
        {
            string domain = gameEnvironment.GetPathByType();
            string key = gameEnvironment.GetKey();

            Client = new Client(new Uri(domain), key, UnityWebRequestAdapter.Instance);
            Client.Logger = new UnityLogger();

            await AuthenticateSessionIfNull();
        }
        catch (ApiResponseException e)
        {
            Debug.LogException(e);
            return false;
        }

        Debug.Log("Connect finish");

        return true;
    }

    public async UniTaskVoid Disconnect()
    {
        Debug.Log("Disconnect");

        try
        {
            await Client.SessionLogoutAsync(Session);
        }
        catch (ApiResponseException e)
        {
            Debug.LogException(e);
            throw;
        }

        Debug.Log("Disconnect finish");

        DeleteUsername();

        Session = null;
        Client = null;
    }

    private async UniTask AuthenticateSessionIfNull()
    {
        Debug.Log("AuthenticateSessionIfNull");

        string username = GetUsername();
        Debug.Log($"new Username: {username}");

        try
        {
            Session = await Client.AuthenticateDeviceAsync(GetDeviceIdentifier(), username);
        }
        catch (ApiResponseException e)
        {
            Debug.LogException(e);
            throw;
        }

        Debug.Log($"Username: {Session.Username}");

        Debug.Log("AuthenticateSessionIfNull finish");
    }

    private string GetUserID()
    {
        return Session.UserId;
    }

    private static string GetDeviceIdentifier()
    {
        Debug.Log("GetDeviceIdentifier");

        string deviceId;

        if (PlayerPrefs.HasKey(DeviceIdentifierPrefName))
        {
            deviceId = PlayerPrefs.GetString(DeviceIdentifierPrefName);

            print($"Device ID: {deviceId}");

            return deviceId;
        }

        deviceId = SystemInfo.deviceUniqueIdentifier;

        if (deviceId == SystemInfo.unsupportedIdentifier)
        {
            deviceId = Guid.NewGuid().ToString();
        }

        PlayerPrefs.SetString(DeviceIdentifierPrefName, deviceId);

        print($"Device ID: {deviceId}");

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

        IApiLeaderboardRecordList leaderboardRecords =
            await Client.ListLeaderboardRecordsAsync(Session, leaderboardId, limit: 10);

        Debug.Log("GetLeaderboard finish");

        return leaderboardRecords.Records.ToList();
    }

    public async Task<IApiLeaderboardRecord> GetPlayerLeaderboard()
    {
        Debug.Log("GetPlayerLeaderboard");

        IApiLeaderboardRecordList leaderboardRecords =
            await Client.ListLeaderboardRecordsAroundOwnerAsync(Session, leaderboardId, GetUserID());

        Debug.Log("GetPlayerLeaderboard finish");

        return leaderboardRecords.Records.FirstOrDefault(record => record.OwnerId == GetUserID());
    }
}