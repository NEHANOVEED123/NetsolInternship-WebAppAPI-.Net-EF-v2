namespace WebApplication_API_version2.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public string Content { get; set; }= string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? PersonId { get; set; }
        public Person? Person { get; set; }// FK   ---- Navigation property like Person.Name
    }
}
