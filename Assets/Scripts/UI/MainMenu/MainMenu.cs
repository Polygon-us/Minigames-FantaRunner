using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        void Start()
        {
            startButton.onClick.AddListener(OnStartGame);
        }
        
        private void OnStartGame()
        {
            SceneManager.LoadScene("Main");
        }
    }
}