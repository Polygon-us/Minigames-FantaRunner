using System;
using UnityREST;

public class LoginHandler : BaseHandler
{
    public static void Login(string email, Action<WebResult<LoginResponse>> onLogin = null)
    {
        var loginData = new LoginPayload
        {
            email = email,
        };
        
        RestApiManager.Instance.PostRequest("login", loginData, onLogin);
    }
}

public class LoginPayload
{
    public string email;
}

public class LoginResponse
{
    public int statusCode;
    public string success;
    public LoginDetails data;
}

public class LoginDetails
{
    public string authorization;
    public string username;
    public string fullName;
    public string address;
    public string idCard;
    public string email;
    public string phone;
    public string city;
    public MyLeaderboard[] leaderboard;
}

public class MyLeaderboard
{
    public string gameType;
    public int coins;
    public int distance;
}