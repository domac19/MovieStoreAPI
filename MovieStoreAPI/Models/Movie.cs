namespace MovieStoreAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public double Duration { get; set; }
        public DateTime StartOfFilming { get; set; }
        public DateTime EndOfFilming { get; set; }
        public string Genre { get; set; }
    }
}
