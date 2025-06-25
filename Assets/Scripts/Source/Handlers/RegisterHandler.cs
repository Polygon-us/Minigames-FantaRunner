using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.Globals;
using Source.DTOs;
using UnityREST;
using System;

namespace Source.Handlers
{
    public class RegisterHandler : BaseHandler
    {
        public static void Register(RegisterDto registerDto, Action<WebResult<ResponseDto<RegisterResponseDto>>> onRegister = null)
        {
            RestApiManager.Instance.PostRequest(Endpoints.register, registerDto, onRegister);
        }
    }
}
