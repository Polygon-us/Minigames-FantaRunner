using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private GameObject usernamePanel;
    [SerializeField] private GameObject startButton;

    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private Button sendButton;
    
    private void Start()
    {
        if (NakamaConnection.Instance.HasRegistered())
        {
            HideUsernamePanel();
        }
        else
        {
            ShowUsernamePanel();
        }
        
        sendButton.onClick.AddListener(OnSendUsername);
    }
    
    private void OnSendUsername()
    {
        if (usernameInputField.text.Length == 0)
            return;
        
        NakamaConnection.Instance.SetUsername(usernameInputField.text);
        
        HideUsernamePanel();
    }
    
    private void ShowUsernamePanel()
    {
        usernamePanel.SetActive(true);
        startButton.gameObject.SetActive(false);
    }
    
    private void HideUsernamePanel()
    {
        usernamePanel.SetActive(false);
        startButton.SetActive(true);
    }
}
