using System;
using System.Linq;
using Source.DTOs.Response;
using UnityEngine;

[CreateAssetMenu(fileName = "UserModelSO", menuName = "Models/UserModelSO")]
public class UserModel : ScriptableObject
{
    public GameType gameType;
    public UserInfo userInfo;

    public int distance = 0;

    public void Clear()
    {
        userInfo = new UserInfo();
        distance = 0;
    }

    public void SetData(LoginDetailsDto data)
    {
        userInfo = new UserInfo
        {
            username = data.username,
            fullName = data.fullName,
            address = data.address,
            idCard = data.idCard,
            email = data.email,
            phone = data.phone,
            city = data.city
        };

        var leaderboard = data.leaderboard.FirstOrDefault(entry => gameType.ToString() == entry.gameType);

        if (leaderboard == null)
            return;

        distance = leaderboard.distance;
    }
}

public enum GameType
{
    endless_runner
}

[Serializable]
public class UserInfo
{
    public string username;
    public string fullName;
    public string address;
    public string idCard;
    public string email;
    public string phone;
    public string city;

    public bool AllFilled()
    {
        return !string.IsNullOrEmpty(username) &&
               !string.IsNullOrEmpty(fullName) &&
               !string.IsNullOrEmpty(address) &&
               !string.IsNullOrEmpty(idCard) &&
               !string.IsNullOrEmpty(email) &&
               !string.IsNullOrEmpty(phone) &&
               !string.IsNullOrEmpty(city);
    }
}