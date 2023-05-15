namespace CMS.Core.Exceptions
{
    public class DuplicateEmailOrPhoneException : Exception
    {
        public DuplicateEmailOrPhoneException() : base("Duplicate Email Or Phone")
        {
        }
    }
}
