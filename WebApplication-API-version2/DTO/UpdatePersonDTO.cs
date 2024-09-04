using System.ComponentModel.DataAnnotations;

namespace WebApplication_API_version2.DTO
{
    public class UpdatePersonDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Name character must be minimum 3")]
        [MaxLength(10, ErrorMessage = "Name character cannot be over maximum 10")]
        public string Name { get; set; }

        [Required]
        [Range(1000,100000)]
        public decimal Salary { get; set; }

        [Required]
        [Range(1000, 10000000)]
        public long MarketCapital { get; set; }
    }
}
