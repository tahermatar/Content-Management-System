using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم التصنيف")]
        public string? Name { get; set; }
    }
}
