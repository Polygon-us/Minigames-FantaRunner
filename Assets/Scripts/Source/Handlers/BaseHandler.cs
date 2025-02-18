using Med.SafeValue;
using UI.DTOs;

namespace Source.Handlers
{
    public class BaseHandler
    {
        private const string UserInfoKey = "UserInfo";

        protected const string GameType = "fanta_runner";

        public static SaveUserInfoDto SaveUserInfo => PlayerSaves.DecryptClass<SaveUserInfoDto>(UserInfoKey);
     
        protected static string[] Args(params string[] args) => args;
        
        public static void SaveInfoToPrefs(string username, string email, string password)
        {
            SaveUserInfoDto saveUserInfoDto = new SaveUserInfoDto
            {
                username = username,
                email = email,
                password = password
            };

            PlayerSaves.EncryptClass(saveUserInfoDto, UserInfoKey);
        }
    }
}