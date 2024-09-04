using WebApplication_API_version2.Models;

namespace WebApplication_API_version2.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? PersonId { get; set; }

    }
}
