namespace Source.DTOs.Response
{
    public class LoginResponseDto
    {
        public string authorization;
        public string username;
        public string fullName;
        public string address;
        public string idCard;
        public string email;
        public string phone;
        public string city;
        public LeaderboardResponseDto[] leaderboard;
    }
}