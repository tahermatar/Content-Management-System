using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [Display(Name = "اسم التصنيف")]
        public string? Name { get; set; }
    }
}
