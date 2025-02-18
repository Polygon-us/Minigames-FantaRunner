using Source.Utils.Validations;
using Source.Utils.Responses;
using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.Handlers;
using UnityEngine.UI;
using UnityEngine;
using UnityREST;
using System;
using TMPro;

namespace UI.Views
{
    public class LoginMenu : ViewBase
    {
        [SerializeField] private Button registerButton;
        [SerializeField] private Button sendButton;

        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;

        public Action OnLoginSuccess;
        public Action GoToRegister;

        private void Start()
        {
            sendButton.onClick.AddListener(OnSendLogin);
            registerButton.onClick.AddListener(() => GoToRegister?.Invoke());
        }

        private void OnSendLogin()
        {
            LoginDto loginDto = new LoginDto
            {
                email = usernameInputField.text,
                password = passwordInputField.text
            };

            ResultResponse<LoginDto> validate = LoginValidation.Validate(loginDto);

            if (!validate.IsSuccess)
            {
                // TODO: Show error
                Debug.Log(validate.ErrorMessage);
                return;
            }

            LoginHandler.Login(validate.Data, OnLoginResponse);
        }

        private void OnLoginResponse(WebResult<LoginResponseDto> response)
        {
            if (response.result.HasError())
            {
                // TODO: Show error
                Debug.Log(response.result.ResponseText);
                return;
            }

            OnLoginSuccess?.Invoke();
        }
    }
}