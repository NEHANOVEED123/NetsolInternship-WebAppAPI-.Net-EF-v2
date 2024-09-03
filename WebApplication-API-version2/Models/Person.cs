using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API_version2.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public DateOnly? JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        [Column (TypeName ="decimal(9,2)")]
        public long MarketCapital { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
