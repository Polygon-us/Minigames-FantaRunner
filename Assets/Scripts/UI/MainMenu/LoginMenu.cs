using Source.Utils.Validations;
using Source.Utils.Responses;
using Source.DTOs.Response;
using Source.DTOs.Request;
using Source.Handlers;
using UnityEngine.UI;
using Source.Popups;
using Med.SafeValue;
using UnityEngine;
using Source.DTOs;
using UnityREST;
using UI.DTOs;
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
        
        protected override void OnCreation()
        {
            sendButton.onClick.AddListener(OnSendLogin);
            registerButton.onClick.AddListener(() => GoToRegister?.Invoke());

            ToggableButtons.Add(registerButton);
            ToggableButtons.Add(sendButton);
            
            SetPrefills();
        }

        public override void OnShow()
        {
        }

        public override void OnHide()
        {
        }

        private void OnSendLogin()
        {
            ToggleButtons(false);
            
            LoginDto loginDto = new LoginDto
            {
                email = usernameInputField.text,
                password = passwordInputField.text
            };

            ResultResponse<LoginDto> validate = LoginValidation.Validate(loginDto);

            if (!validate.IsSuccess)
            {
                ConfirmationPopUp.Instance.Open(validate.ErrorMessage);
                ToggleButtons(true);
                return;
            }

            LoginHandler.Login(validate.Data, OnLoginResponse);
        }

        private void OnLoginResponse(WebResult<ResponseDto<LoginResponseDto>> response)
        {
            if (response.result.HasError())
            {
                ConfirmationPopUp.Instance.Open(response.data.error);
                ToggleButtons(true);
                return;
            }
             
            SaveInfoToPrefs();
            
            RestApiManager.Instance.SetAuthToken(response.data.data.authorization);
            
            OnLoginSuccess?.Invoke();
        }

        private void SetPrefills()
        {
            UserInfoDto userInfoDto = PlayerSaves.DecryptClass<UserInfoDto>(UserInfoKey);
            
            if (userInfoDto == null)
                return;
            
            usernameInputField.text = userInfoDto.username;
            passwordInputField.text = userInfoDto.password;
        }
        
        private void SaveInfoToPrefs()
        {
            UserInfoDto userInfoDto = new UserInfoDto
            {
                username = usernameInputField.text,
                password = passwordInputField.text
            };

            PlayerSaves.EncryptClass(userInfoDto, UserInfoKey);
        }
    }
}