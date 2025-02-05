using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityREST;

public class CheckpointsHandler : BaseHandler
{
    public static void StartRun(Action<WebResult<StartGameResponse>> onRunStart)
    {
        string[] args = Args($"gameType={GameType}");

        RestApiManager.Instance.PostRequest("startRun", null, onRunStart, args);
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
    public List<CheckpointData> checkpoints = new();
}

[Serializable]
public class CheckpointData
{
    public int distance;
    public DateTime date;
}

public class StartGameResponse
{
    public StartDetails data;
}

public class StartDetails
{
    public DateTime startDate;
}