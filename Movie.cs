namespace MiniDataManager.Models
{
    // Modellklass
    // Looks like a movie model with properties for Title, Genre, Year, and Price.
    public class Movie
    {
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}


