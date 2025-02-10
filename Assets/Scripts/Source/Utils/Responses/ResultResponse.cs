using System;

namespace Source.Utils.Responses
{
    public class ResultResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public DateTime Timestamp { get; set; }
        public string AuditMessage { get; set; }

        public static implicit operator T (ResultResponse<T> result)
        {
            return result.Data;
        }

        private ResultResponse(T data, string auditMessage = null)
        {
            Data = data;
            ErrorMessage = null;
            Timestamp = DateTime.Now;
            AuditMessage = auditMessage;
        }

        private ResultResponse(string errorMessage, string auditMessage = null)
        {
            Data = default(T);
            ErrorMessage = errorMessage;
            Timestamp = DateTime.Now;
            AuditMessage = auditMessage;
        }

        public static ResultResponse<T> Success(T data, string auditMessage = null)
        {
            return new ResultResponse<T>(data, auditMessage);
        }

        public static ResultResponse<T> Failure(string errorMessage, string auditMessage = null)
        {
            return new ResultResponse<T>(errorMessage, auditMessage);
        }
    }
}