using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Movie
{
    public class MovieFeatures : IMovieFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        public MovieFeatures(MovieRentalDbContext movieRentalDb)
        {
            _movieRentalDb = movieRentalDb;
        }

        public Movie Save(Movie movie)
        {
            _movieRentalDb.Movies.Add(movie);
            _movieRentalDb.SaveChanges();
            return movie;
        }

        // TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
        //Since it is only intended to only read data, it should be called with AsNoTracking()
        //also, as the Movies table can increase in size, it is better to use pagination
        //Instead of returning movies as List, it is better to return IEnumerable<Movie> for performance and separation of concerns
        public IEnumerable<Movie> GetAll()
        {
            return _movieRentalDb.Movies.AsNoTracking();
        }

        private List<Movie> GetAllPaginated(int skip = 0, int take = 1000)
        {
            return _movieRentalDb.Movies
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
