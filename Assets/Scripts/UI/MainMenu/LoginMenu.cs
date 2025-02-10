using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private GameObject usernamePanel;
    [SerializeField] private GameObject startPanel;

    [SerializeField] private Button sendButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button logoutButton;

    [SerializeField] private TMP_InputField usernameInputField;

    private LoginHandler _loginHandler;
    private RegisterHandler _registerHandler;
    // private SessionHandler _sessionHandler;
    
    private void Start()
    {
        sendButton.onClick.AddListener(OnSendUsername);
        logoutButton.onClick.AddListener(OnLogout);
        startButton.onClick.AddListener(OnStartGame);

        _loginHandler = new LoginHandler();
        _registerHandler = new RegisterHandler();
        // _sessionHandler = new SessionHandler();
        
        ShowUsernamePanel();
    }
    
    private void OnSendUsername()
    {
        if (usernameInputField.text.Length == 0)
            return;
        
        RegisterHandler.Register(usernameInputField.text);
    }

    private async void OnLogout()
    {
        await _sessionHandler.SessionLogout();

        ShowUsernamePanel();
    }

    private void OnStartGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void ShowUsernamePanel()
    {
        usernamePanel.SetActive(true);
        startPanel.SetActive(false);
    }

    private void HideUsernamePanel()
    {
        usernamePanel.SetActive(false);
        startPanel.SetActive(true);
    }
}