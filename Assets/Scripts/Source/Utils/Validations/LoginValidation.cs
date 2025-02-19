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

            if (!InputValidator.IsValidEmail(loginDto.email))
            {
                return ResultResponse<LoginDto>.Failure(
                    "Invalid email format or email is too long.",
                    "EMAIL_INVALID"
                );
            }

            if (!InputValidator.IsValidPassword(loginDto.password))
            {
                return ResultResponse<LoginDto>.Failure(
                    "password must be at least 6 characters long.",
                    "PASSWORD_TOO_SHORT"
                );
            }

            return ResultResponse<LoginDto>.Success(loginDto, "Validation successful.");
        }
    }
}