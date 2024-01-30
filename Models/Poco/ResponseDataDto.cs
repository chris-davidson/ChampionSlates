namespace Models.Poco
{
    public class ResponseDataDto<T>
    {
        public T? Data { get; set; }
        public ResponseDto? Response { get; set; }
    }

    public class ResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
