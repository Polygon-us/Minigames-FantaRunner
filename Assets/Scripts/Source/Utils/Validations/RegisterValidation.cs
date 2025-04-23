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
            
            Satinize(registerDto);

            if (!InputValidator.IsValidEmail(registerDto.email))
            {
                return ResultResponse<RegisterDto>.Failure
                (
                    "El correo no es válido.",
                    "EMAIL_INVALID"
                );
            }

            if (!InputValidator.IsValidPassword(registerDto.password))
            {
                return ResultResponse<RegisterDto>.Failure
                (
                    "Contraseña debe tener al menos 6 caracteres.",
                    "PASSWORD_TOO_SHORT"
                );
            }

            if (registerDto.password != registerDto.confirmPassword)
            {
                return ResultResponse<RegisterDto>.Failure
                (
                    "Las contraseñas deben coincidir.",
                    "DIFFERENT PASSWORDS"
                );
            }

            if (!InputValidator.IsValidName(registerDto.fullName))
            {
                return ResultResponse<RegisterDto>.Failure
                (
                    "Nombre debe tener al menos 3 caracteres.",
                    "NAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidName(registerDto.username))
            {
                return ResultResponse<RegisterDto>.Failure
                (
                    "Nombre de usuario debe tener al menos 3 caracteres.",
                    "USERNAME_TOO_SHORT"
                );
            }
            
            if (!InputValidator.IsValidPhoneNumber(registerDto.phone))
            {
                return ResultResponse<RegisterDto>.Failure
                (
                    "El número de celular debe ser válido.",
                    "PHONE_INVALID"
                );
            }

            return ResultResponse<RegisterDto>.Success(registerDto, "Validation successful.");
        }
        
        private static void Satinize(RegisterDto registerDto)
        {
            registerDto.email = registerDto.email.Trim();
            registerDto.password = registerDto.password.Trim();
            registerDto.confirmPassword = registerDto.confirmPassword.Trim();
            registerDto.fullName = registerDto.fullName.Trim();
            registerDto.username = registerDto.username.Trim();
            registerDto.phone = registerDto.phone.Trim();
        }
    }
}