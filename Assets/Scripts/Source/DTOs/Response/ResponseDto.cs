namespace Source.DTOs
{
    public class ResponseDto<T>
    {
        public int statusCode;
        public string success;
        public string error; 
        public T data;
    }
}