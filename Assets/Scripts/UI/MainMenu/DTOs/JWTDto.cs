using System;
using Source.DTOs.Request;

namespace UI.DTOs
{
    public class JWTPayloadDto : RegisterDto
    {
        public DateTime lastLogin;
        public long iat;
        public long exp;
    }
}