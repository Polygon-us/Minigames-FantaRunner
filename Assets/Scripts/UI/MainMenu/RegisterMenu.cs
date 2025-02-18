using Source.Utils.Validations;
using Source.Utils.Responses;
using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.Handlers;
using UnityEngine.UI;
using Source.Popups;
using UnityEngine;
using Source.DTOs;
using UnityREST;
using System;
using TMPro;

namespace UI.Views
{
    public class RegisterMenu : ViewBase
    {
        private const string SuccessMessage = "Registrado correctamente";

        [SerializeField] private Button sendButton;
        [SerializeField] private Button loginButton;

        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField idCardInputField;
        [SerializeField] private TMP_InputField phoneInputField;

        public Action GoToLogin;
        public Action OnRegisterSuccess;

        protected override void OnCreation()
        {
            sendButton.onClick.AddListener(OnRegister);
            loginButton.onClick.AddListener(() => GoToLogin?.Invoke());
            
            Buttons.Add(sendButton);
            Buttons.Add(loginButton);
        }

        public override void OnShow()
        {
        }

        public override void OnHide()
        {
        }

        private void OnRegister()
        {
            ToggleButtons(false);

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
                ConfirmationPopUp.Instance.Open(validate.ErrorMessage);
                ToggleButtons(true);
                return;
            }

            RegisterHandler.Register(validate.Data, OnRegisterResponse);
        }

        private void OnRegisterResponse(WebResult<ResponseDto<RegisterResponseDto>> response)
        {
            sendButton.interactable = true;

            if (response.result.HasError())
            {
                ConfirmationPopUp.Instance.Open(response.data.error);
                ToggleButtons(true);
            }
            else
            {
                BaseHandler.SaveInfoToPrefs(usernameInputField.text, emailInputField.text, passwordInputField.text);
                
                RestApiManager.Instance.SetAuthToken(response.data.data.authorization);

                ConfirmationPopUp.Instance.Open(SuccessMessage, OnRegisterSuccess);
            }
        }
    }
}