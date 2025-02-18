using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenu : ViewBase
    {
        [SerializeField] private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(OnStartGame);
        }
        
        private void OnStartGame()
        {
            SceneManager.LoadScene("Main");
        }
    }
}