using System.Collections.Generic;
using Source.DTOs.Response;
using Source.DTOs.Request;
using UnityREST;
using System;

namespace Source.Handlers
{
    public class SessionHandler : BaseHandler
    {
        public static void StartRun(Action<WebResult<SessionResponseDto>> result)
        {
            string[] args = Args($"gameType={GameType}");

            RestApiManager.Instance.PostRequest("startRun", null, result, args);
        }
         
        public static void SendCheckpoints(CheckpointTimeline checkpointTimeline)
        {
            string[] args = Args($"gameType={GameType}");

            RestApiManager.Instance.PostRequest<object>("endRun", checkpointTimeline, args: args);
        }
    }

    [Serializable]
    public class CheckpointTimeline
    {
        public List<CheckpointDto> metadata = new();
    }
}