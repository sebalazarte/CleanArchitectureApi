using Microsoft.AspNetCore.Http;

namespace Domain.DTO
{
    public class ResponseDTO<T>
    {
        public MetaDTO Meta { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }

        public void SetMeta(HttpRequest request)
        {
            Meta = new MetaDTO(request.Method, request.Path);
        }
    }

    public class MetaDTO
    {
        public string Method { get; set; }
        public string Operation { get; set; }

        public MetaDTO() { }
        public MetaDTO(string method, string operation)
        {
            Method = method;
            Operation = operation;
        }
    }
}
