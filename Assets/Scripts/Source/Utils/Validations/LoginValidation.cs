using Source.Utils.Responses;
using Source.DTOs.Request;

namespace Source.Utils.Validations
{
    public static class LoginValidation
    {
        public static ResultResponse<LoginDto> Validate(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return ResultResponse<LoginDto>.Failure("DTO cannot be null.", "DTO_NULL");
            }
            
            Satinize(loginDto);

            if (!InputValidator.IsValidEmail(loginDto.email))
            {
                return ResultResponse<LoginDto>.Failure(
                    "El correo no es válido.",
                    "EMAIL_INVALID"
                );
            }

            return ResultResponse<LoginDto>.Success(loginDto, "Validation successful.");
        }

        private static void Satinize(LoginDto loginDto)
        {
            loginDto.email = loginDto.email.Trim();
        }
    }
}