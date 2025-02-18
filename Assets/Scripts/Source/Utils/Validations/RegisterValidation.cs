using Source.DTOs.Request;
using Source.Utils.Responses;

namespace Source.Utils.Validations
{
    public static class RegisterValidation
    {
        public static ResultResponse<RegisterDto> Validate(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return ResultResponse<RegisterDto>.Failure("DTO cannot be null.", "DTO_NULL");
            }

            if (!InputValidator.IsValidEmail(registerDto.email))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "Invalid email format or email is too long.",
                    "EMAIL_INVALID"
                );
            }

            if (!InputValidator.IsValidPassword(registerDto.password))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "password must be at least 6 characters long.",
                    "PASSWORD_TOO_SHORT"
                );
            }

            if (!InputValidator.IsValidName(registerDto.fullName))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "name must be at least 3 characters long.",
                    "NAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidName(registerDto.username))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "username must be at least 3 characters long.",
                    "USERNAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidPhoneNumber(registerDto.phone))
            {
                return ResultResponse<RegisterDto>.Failure(
                    "phone number must be a valid phone number.",
                    "PHONE_INVALID"
                );
            }

            return ResultResponse<RegisterDto>.Success(registerDto, "Validation successful.");
        }
    }
}