namespace TestPipeline.Models
{
    public record User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Enabled { get; set; }

        public int ClientId { get; set; }

    }
}
