using Source.DTOs.Request;
using System;

namespace UI.DTOs
{
    public class JWTPayloadDto : RegisterDto
    {
        public DateTime lastLogin;
        public string score;
        public string distance;
        public long iat;
        public long exp;
    }
}