namespace WebApplication_API_version2.DTO
{
    public class PatchPersonDTO
    {
        public string? Name { get; set; } // Nullable, allowing the client to omit this field.
        public decimal? Salary { get; set; } // Nullable, allowing the client to omit this field.
        public long? MarketCapital { get; set; } // Nullable, allowing the client to omit this field.
    }
}
