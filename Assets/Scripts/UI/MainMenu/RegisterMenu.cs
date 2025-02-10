using Source.Utils.Validations;
using Source.Utils.Responses;
using Source.DTOs.Request;
using Source.Handlers;
using UnityEngine.UI;
using UnityEngine;
using UnityREST;
using TMPro;

namespace UI.Views
{
    public class RegisterMenu : MonoBehaviour
    {
        [SerializeField] private Button sendButton;

        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField lastNameInputField;
        [SerializeField] private TMP_InputField idCardInputField;
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField phoneInputField;

        private void Start()
        {
            sendButton.onClick.AddListener(OnRegister);
        }

        private void OnRegister()
        {
            RegisterDto registerDto = new RegisterDto
            {
                name = nameInputField.text,
                lastName = lastNameInputField.text,
                idCard = idCardInputField.text,
                email = usernameInputField.text,
                password = passwordInputField.text,
                phone = phoneInputField.text
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