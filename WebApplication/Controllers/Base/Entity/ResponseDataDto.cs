namespace WebApplication.Controllers.Base.Entity
{
    public class ResponseDataDto<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}