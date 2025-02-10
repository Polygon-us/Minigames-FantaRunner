using UnityREST;
using System;

public class RankingHandler : BaseHandler
{
    private const int Limit = 5;
    private const int Offset = 0;
    

    public void GetRanking(Action<WebResult<RankingListResponse>> onRanking = null)
    {
        string[] args = Args($"gameType={GameType}", $"limit={Limit}", $"offset={Offset}", $"username={UserModel.userInfo.username}");
        
        RestApiManager.Instance.GetRequest("listLeaderboard", onRanking ,args);
    }

    public void PostRanking(int coins, int distance, Action<WebResult<object>> onRanking = null)
    {
        var rankingData = new RankingPayload
        {
            coins = coins,
            distance = distance
        };

        RestApiManager.Instance.PatchRequest("updateLeaderboard", rankingData, onRanking);
    }
}


public class RankingPayload
{
    public int coins;
    public int distance;
}

public class RankingListResponse
{
    public int statusCode;
    public string success;
    public RankingData data;
}

public class RankingData
{
    public PlayerRanking player;
    public PlayerRanking[] global;
}

public class PlayerRanking
{
    public string username;
    public int coins;
    public int distance;
    public int rank;
}