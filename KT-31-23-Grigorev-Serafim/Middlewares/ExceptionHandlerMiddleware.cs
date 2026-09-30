using System.Net;




namespace KT_31_23_Grigorev_Serafim.Middlewares
{
    public class ExceptionHandlerMiddleware
    {

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;


        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception exception)
            {

                _logger.LogError("Exception", exception);

                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";

                var responseModel = new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = exception.Message
                };

                switch (exception)
                {

                    case ArgumentException argEx:
                        httpResponse.StatusCode = (int)HttpStatusCode.BadRequest; // 400 Bad Request
                        responseModel.Errors = new List<string> { argEx.Message };
                        break;

                    case KeyNotFoundException keyEx:
                        httpResponse.StatusCode = (int)HttpStatusCode.NotFound; // 404 Not Found
                        responseModel.Errors = new List<string> { keyEx.Message };
                        break;

                    default:
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                        responseModel.Errors = new List<string> { exception.InnerException?.Message ?? exception.Message };
                        break;

                }

                await httpResponse.WriteAsJsonAsync(responseModel);

            }

        }

    }



    public class ResponseModel<T>
    {
        
        public bool Succeeded { get; set; }
        
        public string Message { get; set; }
        
        public List<string> Errors { get; set; }
        
        public T Data { get; set; }

        
        public ResponseModel()
        {
        }

        
        public ResponseModel(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        
        public ResponseModel(string message)
        {
            Succeeded = true;
            Message = message;
        }

    }

}
