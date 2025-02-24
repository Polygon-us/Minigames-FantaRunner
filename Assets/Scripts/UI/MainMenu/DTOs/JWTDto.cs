using System;
using Source.DTOs.Request;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace UI.DTOs
{
    public class JWTPayloadDto : RegisterDto
    {
        public DateTime lastLogin;
        public int score;
        public long iat;
        public long exp;
    }
}