using UnityEngine.EventSystems;
using UnityEngine;
using UI.Views;

namespace UI.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private LoginMenu loginPanel;
        [SerializeField] private RegisterMenu registerPanel;
        [SerializeField] private MainMenu mainMenuPanel;

        private ViewBase _currentMenu;

        private void Start()
        {
            // loginPanel.gameObject.SetActive(false);
            registerPanel.gameObject.SetActive(false);
            mainMenuPanel.gameObject.SetActive(false);
            
            ShowLogin();
        }

        private void ShowPanel(ViewBase panel)
        {
            _currentMenu?.gameObject.SetActive(false);
            _currentMenu = panel;
            _currentMenu.gameObject.SetActive(true);
            
            eventSystem.SetSelectedGameObject(_currentMenu.FirstSelected);
        }

        private void ShowLogin()
        {
            ShowPanel(loginPanel);
        }

        private void ShowRegister()
        {
            ShowPanel(registerPanel);
        }

        private void ShowMainMenu()
        {
            ShowPanel(mainMenuPanel);
        }
    }
}