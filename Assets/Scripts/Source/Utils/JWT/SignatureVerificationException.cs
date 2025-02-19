using System;

namespace Source.Utils.JWT
{
    namespace JWT
    {
        public class SignatureVerificationException : Exception
        {
            public SignatureVerificationException(string message)
                : base(message)
            {
            }
        }
    }
}