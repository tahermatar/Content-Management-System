namespace CMS.Core.Exceptions
{
    public class EmailHaveBeenUsedException : Exception
    {
        public EmailHaveBeenUsedException() : base("Email Have Been Used")
        {
        }
    }
}
