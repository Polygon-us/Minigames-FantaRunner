using Core.Functions.Authentication.Handler;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private GameObject usernamePanel;
    [SerializeField] private GameObject startPanel;

    [SerializeField] private Button sendButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button logoutButton;

    [SerializeField] private TMP_InputField usernameInputField;

    private AuthenticationHandler _authenticationHandler;
    
    private void Start()
    {
        sendButton.onClick.AddListener(OnSendUsername);
        logoutButton.onClick.AddListener(OnLogout);
        startButton.onClick.AddListener(OnStartGame);

        _authenticationHandler = new AuthenticationHandler();
        
        Connect().Forget();
    }

    private async UniTaskVoid Connect()
    {
        // _authenticationHandler.AuthenticationEmail()
        if (!NakamaConnection.Instance.HasRegistered())
        {
            ShowUsernamePanel();
            return;
        }

        bool wasSuccess = await NakamaConnection.Instance.Connect();

        if (wasSuccess)
        {
            HideUsernamePanel();
        }
        else
        {
            NakamaConnection.Instance.DeleteUsername();

            ShowUsernamePanel();
        }
    }

    private void OnSendUsername()
    {
        if (usernameInputField.text.Length == 0)
            return;

        NakamaConnection.Instance.SetUsername(usernameInputField.text);

        Connect().Forget();
    }

    private void OnLogout()
    {
        NakamaConnection.Instance.Disconnect().Forget();

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