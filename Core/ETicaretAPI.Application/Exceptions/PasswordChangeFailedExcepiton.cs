namespace ETicaretAPI.Application.Exceptions
{
    public class PasswordChangeFailedExcepiton : Exception
    {
        public PasswordChangeFailedExcepiton() : base("Şifre güncellenirken bir sorun oluştu.")
        {
        }

        public PasswordChangeFailedExcepiton(string? message) : base(message)
        {
        }

        public PasswordChangeFailedExcepiton(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
