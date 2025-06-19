using Source.DTOs.Request;
using Source.Globals;
using Source.DTOs;
using UnityREST;
using System;

namespace Source.Handlers
{
    public class MetricsHandler : BaseHandler
    {
        public static void CodeClicked(ClickCodeDto clickCodeDto, Action<WebResult<ResponseDto<object>>> onClickSent = null)
        {
            RestApiManager.Instance.PostRequest(Endpoints.clickCode, clickCodeDto, onClickSent);
        }
    }
}