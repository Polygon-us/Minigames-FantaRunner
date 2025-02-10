using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.Views
{
    public class LoginMenu : MonoBehaviour
    {
        [SerializeField] private Button sendButton;
        [SerializeField] private Button logoutButton;

        [SerializeField] private TMP_InputField usernameInputField;

        private LoginHandler _loginHandler;
        private RegisterHandler _registerHandler;
        // private SessionHandler _sessionHandler;

        private void Start()
        {
            sendButton.onClick.AddListener(OnSendUsername);
            logoutButton.onClick.AddListener(OnLogout);


            _loginHandler = new LoginHandler();
            _registerHandler = new RegisterHandler();
            // _sessionHandler = new SessionHandler();
        }

        private void OnSendUsername()
        {
            if (usernameInputField.text.Length == 0)
                return;

            // RegisterHandler.Register(usernameInputField.text);
        }

        private void OnLogout()
        {
            // await _sessionHandler.SessionLogout();
        }
    }
}