namespace MovieRental.Movie;

public interface IMovieFeatures
{
	Movie Save(Movie movie);
	IEnumerable<Movie> GetAll();
}