using System.Collections.Generic;

namespace Grimoire_Csharp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public List<Rating> Ratings { get; set; }
        public double AverageRating { get; set; }
    }

    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Grade { get; set; }
    }
}