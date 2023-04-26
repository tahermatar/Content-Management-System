using CMS.Core.Enums;

namespace CMS.Data.Models
{
    public class ContentChangeLog
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public ContentType Type { get; set; }
        public DateTime ChangeAt { get; set; }
        public ContentStatus Old { get; set; }
        public ContentStatus New { get; set; }
    }
}
