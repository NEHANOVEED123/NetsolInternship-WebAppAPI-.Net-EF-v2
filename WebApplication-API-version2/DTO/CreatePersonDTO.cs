namespace WebApplication_API_version2.DTO
{
    public class CreatePersonDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public long MarketCapital { get; set; }

    }
}
