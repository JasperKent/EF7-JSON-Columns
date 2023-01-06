namespace JsonColumns.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Fillarea Shape { get; set; }
    }
}
