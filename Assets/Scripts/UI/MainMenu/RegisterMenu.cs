using Source.Utils.Validations;
using Source.Utils.Responses;
using Source.DTOs.Request;
using Source.Handlers;
using UnityEngine.UI;
using UnityEngine;
using UnityREST;
using System;
using TMPro;

namespace UI.Views
{
    public class RegisterMenu : ViewBase
    {
        [SerializeField] private Button sendButton;
        [SerializeField] private Button loginButton;

        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField idCardInputField;
        [SerializeField] private TMP_InputField phoneInputField;

        public Action GoToLogin;

        private void Start()
        {
            sendButton.onClick.AddListener(OnRegister);
            loginButton.onClick.AddListener(() => GoToLogin?.Invoke());
        }

        private void OnRegister()
        {
            RegisterDto registerDto = new RegisterDto
            {
                fullName = nameInputField.text,
                username = usernameInputField.text,
                email = emailInputField.text,
                password = passwordInputField.text,
                idCard = idCardInputField.text,
                phone = phoneInputField.text,
                acceptedTerms = true
            };

            ResultResponse<RegisterDto> validate = RegisterValidation.Validate(registerDto);

            if (!validate.IsSuccess)
            {
                // TODO: Show error
                Debug.Log(validate.ErrorMessage);
                return;
            }

            RegisterHandler.Register(validate.Data, OnRegisterResponse);
        }

        private void OnRegisterResponse(WebResult<object> response)
        {
        }
    }
}