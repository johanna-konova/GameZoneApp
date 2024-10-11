using GameZoneApp.Infrastructure.Data.Models;

namespace GameZoneApp.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static IEnumerable<Genre> GenerateGenres()
        {
            var genres = new HashSet<Genre>();

            var genre = new Genre { Id = 1, Name = "Action" };
            genres.Add(genre);

            genre = new Genre { Id = 2, Name = "Adventure" };
            genres.Add(genre);

            genre = new Genre { Id = 3, Name = "Fighting" };
            genres.Add(genre);

            genre = new Genre { Id = 4, Name = "Sports" };
            genres.Add(genre);

            genre = new Genre { Id = 5, Name = "Racing" };
            genres.Add(genre);

            genre = new Genre { Id = 6, Name = "Strategy" };
            genres.Add(genre);

            return genres;
        }
    }
}
