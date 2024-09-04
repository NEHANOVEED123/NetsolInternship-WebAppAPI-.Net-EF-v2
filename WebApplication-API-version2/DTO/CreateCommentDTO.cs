using WebApplication_API_version2.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_API_version2.DTO
{
    public class CreateCommentDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name character must be minimum 3")]
        [MaxLength(10, ErrorMessage = "Name character cannot be over maximum 10")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Name character must be minimum 3")]
        [MaxLength(100, ErrorMessage = "Name character cannot be over maximum 100")]
        public string Content { get; set; } = string.Empty;
    }
}
