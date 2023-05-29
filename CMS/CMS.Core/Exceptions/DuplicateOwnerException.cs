namespace CMS.Core.Exceptions
{
    public class DuplicateOwnerException : Exception
    {
        public DuplicateOwnerException() : base("Owner Is Exist")
        {
        }
    }
}
