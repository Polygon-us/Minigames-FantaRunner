using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenu : ViewBase
    {
        [SerializeField] private Button startButton;

        protected override void OnCreation()
        {
            startButton.onClick.AddListener(OnStartGame);
        }

        public override void OnShow()
        {
        }

        public override void OnHide()
        {
        }
        
        private void OnStartGame()
        {
            SceneManager.LoadScene("Main");
        }
    }
}