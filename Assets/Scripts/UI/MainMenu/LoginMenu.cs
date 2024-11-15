using System;
using UnityEngine;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private GameObject usernamePanel;
    [SerializeField] private GameObject startButton;

    private void Awake()
    {
        if (NakamaConnection.Instance.HasRegistered())
        {
            usernamePanel.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            usernamePanel.SetActive(true);
            startButton.SetActive(false);
        }
    }
}
