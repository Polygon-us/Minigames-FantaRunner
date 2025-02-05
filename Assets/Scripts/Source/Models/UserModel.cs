using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UserModelSO", menuName = "Models/UserModelSO")]
public class UserModel : ScriptableObject
{
    public UserInfo userInfo;

    public int distance = 0;

    public void Clear()
    {
        userInfo = new UserInfo();
        distance = 0;
    }

    public void SetData(LoginDetails data)
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

        var leaderboard = data.leaderboard.FirstOrDefault(entry => GameConfiguration.GameType.ToString() == entry.gameType);

        if (leaderboard == null)
            return;

        distance = leaderboard.distance;
    }
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