namespace SalonNamjestaja.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode= statusCode;
            Message = message ?? GetDefaultMessageStatusCone(statusCode);
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageStatusCone(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made a bad request.",
                401 => "You are not authorized.",
                404 => "Resource not found.",
                409 => "A resource with that ID already exists.",
                _ => null
            };
        }
    }
}
