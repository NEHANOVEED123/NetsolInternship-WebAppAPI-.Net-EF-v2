namespace WebApplication_API_version2.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly? JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        public long MarketCapital { get; set; }
        public List<CommentDTO> Comments { get; set; }

    }
}
