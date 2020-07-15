namespace Appendesk
{
    public class Response
    {
        public Response()
        {
        }
        public Response(int code,string message,ResponseLevel responseLevel)
        {
            Code = code;
            Message = message;
            ResponseLevel = responseLevel;
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public ResponseLevel ResponseLevel { get; set; }
    }
}
