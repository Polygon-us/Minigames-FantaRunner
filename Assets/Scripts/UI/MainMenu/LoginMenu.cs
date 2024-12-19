using Core.Functions.Authentication.Handler;
using Core.Functions.Register.Domain.DTOs;
using Core.Functions.Register.Handler;
using Core.Functions.Session.Domain.DTOs;
using Core.Functions.Session.Handler;
using Core.Utils.Responses;
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

    private AuthenticationHandler _authenticationHandler;
    private RegisterHandler _registerHandler;
    private SessionHandler _sessionHandler;
    
    private void Start()
    {
        sendButton.onClick.AddListener(OnSendUsername);
        logoutButton.onClick.AddListener(OnLogout);
        startButton.onClick.AddListener(OnStartGame);

        _authenticationHandler = new AuthenticationHandler();
        _registerHandler = new RegisterHandler();
        _sessionHandler = new SessionHandler();
        
        Connect().Forget();
    }

    private async UniTaskVoid Connect()
    {
        ResultResponse<SessionDto> response = await _authenticationHandler.AuthenticationDevice();
        
        if (!response.IsSuccess)
        {
            ShowUsernamePanel();
            return;
        }

        HideUsernamePanel();
    }

    private async void OnSendUsername()
    {
        if (usernameInputField.text.Length == 0)
            return;

        RegisterByDeviceDto dto = new RegisterByDeviceDto()
        {
            username = usernameInputField.text,
        };

        await _registerHandler.RegisterByDevice(dto);
        
        Connect().Forget();
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