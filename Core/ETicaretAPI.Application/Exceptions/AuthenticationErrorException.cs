namespace ETicaretAPI.Application.Exceptions
{
    public class AuthenticationErrorException: Exception
    {
        public AuthenticationErrorException() : base("Kimlik doğrulama hatası.")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
