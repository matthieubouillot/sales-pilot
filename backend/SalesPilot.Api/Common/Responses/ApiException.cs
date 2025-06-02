namespace SalesPilot.Api.Common.Errors
{
    /// <summary>
    /// Exception personnalisée pour gestion centralisée des erreurs API
    /// </summary>
    public class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}