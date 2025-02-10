using UnityEngine;

namespace Source.Handlers
{
    public class BaseHandler
    {
        private UserModel userModel;
        protected UserModel UserModel => userModel ??= Resources.Load<UserModel>("UserModelSO");

        protected const string GameType = "endless_runner";

        protected static string[] Args(params string[] args) => args;
    }
}