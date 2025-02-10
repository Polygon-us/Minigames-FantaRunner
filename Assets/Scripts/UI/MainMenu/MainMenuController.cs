using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private LoginMenu loginPanel;
        [SerializeField] private RegisterMenu registerPanel;
        [SerializeField] private MainMenu mainMenuPanel;

        private GameObject _currentMenu;

        private void Start()
        {
            ShowLogin();
        }

        private void ShowPanel(GameObject panel)
        {
            _currentMenu?.SetActive(false);
            _currentMenu = panel;
            _currentMenu?.SetActive(true);
        }

        private void ShowLogin()
        {
            ShowPanel(loginPanel.gameObject);
        }

        private void ShowRegister()
        {
            ShowPanel(registerPanel.gameObject);
        }

        private void ShowMainMenu()
        {
            ShowPanel(mainMenuPanel.gameObject);
        }
    }
}