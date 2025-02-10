using UnityREST;
using System;

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

