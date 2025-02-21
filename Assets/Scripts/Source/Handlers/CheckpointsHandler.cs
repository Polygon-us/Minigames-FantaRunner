using System.Collections.Generic;
using Source.DTOs.Request;
using System;

namespace Source.Handlers
{
    public class CheckpointsHandler : BaseHandler
    {
        public static void StartRun()
        {
            string[] args = Args($"gameType={GameType}");

            RestApiManager.Instance.PostRequest<object>("startRun", null, null, args);
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
        public List<CheckpointDto> checkpoints = new();
    }
}