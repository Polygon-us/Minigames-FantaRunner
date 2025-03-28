using UnityREST;
using System;
using Source.DTOs;
using Source.DTOs.Request;
using Source.DTOs.Response;

namespace Source.Handlers
{
    public class LoginHandler : BaseHandler
    {
        public static void Login(LoginDto loginDto, Action<WebResult<ResponseDto<LoginResponseDto>>> onLogin = null)
        {
            RestApiManager.Instance.PostRequest("login", loginDto, onLogin);
        }
    }
}