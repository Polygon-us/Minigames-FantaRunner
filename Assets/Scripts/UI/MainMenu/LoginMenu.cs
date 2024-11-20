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

    private void OnEnable()
    {
        NakamaConnection.OnLoginCompleted += OnLoginCompleted;
    }

    private void OnDisable()
    {
        NakamaConnection.OnLoginCompleted -= OnLoginCompleted;
    }

    private void Start()
    {
        sendButton.onClick.AddListener(() => OnSendUsername().Forget());
        logoutButton.onClick.AddListener(OnLogout);
        startButton.onClick.AddListener(OnStartGame);
        
        if (NakamaConnection.Instance.HasRegistered())
            NakamaConnection.Instance.Connect().Forget();
        else
            ShowUsernamePanel();
    }

    private void OnLoginCompleted(bool wasSuccess)
    {
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
    
    private async UniTaskVoid OnSendUsername()
    {
        if (usernameInputField.text.Length == 0)
            return;
        
        NakamaConnection.Instance.SetUsername(usernameInputField.text);
        
        await NakamaConnection.Instance.Connect();
        
        HideUsernamePanel();
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
