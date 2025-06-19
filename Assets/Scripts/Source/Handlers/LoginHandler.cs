using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.Globals;
using Source.DTOs;
using UnityREST;
using System;

namespace Source.Handlers
{
    public class LoginHandler : BaseHandler
    {
        public static void Login(LoginDto loginDto, Action<WebResult<ResponseDto<LoginResponseDto>>> onLogin = null)
        {
            RestApiManager.Instance.PostRequest(Endpoints.login, loginDto, onLogin);
        }
    }
}