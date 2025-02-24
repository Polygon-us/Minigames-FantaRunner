using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.DTOs;
using UnityREST;
using System;

namespace Source.Handlers
{
    public class RankingHandler : BaseHandler
    {
        private const int Limit = 5;
        private const int Offset = 0;

        public void GetRanking(Action<WebResult<ResponseDto<RankingResponseDto>>> onRanking = null)
        {
            (string, string)[] args =
            {
                ("gameType", GameType), 
                ("limit", Limit.ToString()), 
                ("offset", Offset.ToString()),
                ("username", SaveUserInfo.username)
            };

            RestApiManager.Instance.GetRequest("listLeaderboard", onRanking, args);
        }

        public void PostRanking(RankingDto rankingDto, Action<WebResult<object>> onRanking = null)
        {
            RestApiManager.Instance.PatchRequest("updateLeaderboard", rankingDto, onRanking);
        }
    }
}