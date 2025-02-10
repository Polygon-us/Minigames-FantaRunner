namespace Source.DTOs.Response
{
    public class RankingResponseDto
    {
        public int statusCode;
        public string success;
        public RankingData data;

        public class RankingData
        {
            public PlayerRankingResponseDto player;
            public PlayerRankingResponseDto[] global;
        }
    }
}