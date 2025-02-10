using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.Views
{
    public class RegisterMenu : MonoBehaviour
    {
        [SerializeField] private Button sendButton;

        [SerializeField] private TMP_InputField usernameInputField;

        private RegisterHandler _registerHandler;

        private void Start()
        {
            sendButton.onClick.AddListener(OnRegister);

            _registerHandler = new RegisterHandler();
        }

        private void OnRegister()
        {
            if (usernameInputField.text.Length == 0)
                return;

            // RegisterHandler.Register(usernameInputField.text);
        }
    }
}