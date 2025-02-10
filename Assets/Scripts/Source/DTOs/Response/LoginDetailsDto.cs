namespace Source.DTOs.Response
{
    public class LoginDetailsDto
    {
        public string authorization;
        public string username;
        public string fullName;
        public string address;
        public string idCard;
        public string email;
        public string phone;
        public string city;
        public MyLeaderboard[] leaderboard;
    }
}