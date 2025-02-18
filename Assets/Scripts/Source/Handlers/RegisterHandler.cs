using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.DTOs;
using UnityREST;
using System;

namespace Source.Handlers
{
    public class RegisterHandler : BaseHandler
    {
        public static void Register(RegisterDto registerDto, Action<WebResult<ResponseDto<RegisterResponseDto>>> onRegister = null)
        {
            RestApiManager.Instance.PostRequest("register", registerDto, onRegister);
        }

        public static void UpdateUser(string city, string address, string id, Action<WebResult<ResponseDto<object>>> onUpdate = null)
        {
            var payload = new UpdatePayload
            {
                address = address,
                idCard = id,
                city = city
            };

            RestApiManager.Instance.PatchRequest("update", payload, onUpdate);
        }
    }
    
    public class UpdatePayload
    {
        public string address;
        public string idCard;
        public string city;
    }
}
