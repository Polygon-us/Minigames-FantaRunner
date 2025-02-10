namespace Source.Utils.Validations
{
    public static class RegisterValidation
    {
        public static bool Validate(AuthenticationByEmailDto authenticationByEmailDto)
        {
            if (authenticationByEmailDto == null)
            {
                return ResultResponse<AuthenticationByEmailDto>.Failure("DTO cannot be null.", "DTO_NULL");
            }

            if (!InputValidator.IsValidEmail(authenticationByEmailDto.Email))
            {
                return ResultResponse<AuthenticationByEmailDto>.Failure(
                    "Invalid email format or email is too long.",
                    "EMAIL_INVALID"
                );
            }

            if (!InputValidator.IsValidPassword(authenticationByEmailDto.Password))
            {
                return ResultResponse<AuthenticationByEmailDto>.Failure(
                    "password must be at least 6 characters long.",
                    "PASSWORD_TOO_SHORT"
                );
            }

            return ResultResponse<AuthenticationByEmailDto>.Success(authenticationByEmailDto, "Validation successful.");
        }
    }
}