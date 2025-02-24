using System;

namespace UI.DTOs
{
    [Serializable]
    public class SaveUserInfoDto
    {
        public string username;
        public string email;
        public int score;
        // public string password;
    }
}