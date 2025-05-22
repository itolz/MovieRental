using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

		//TODO: make me async :(
		public async Task<Rental> Save(Rental rental)
		{
			_movieRentalDb.Rentals.Add(rental);
			await _movieRentalDb.SaveChangesAsync().ConfigureAwait(false);
			return rental;
		}

		public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
        {
            return _movieRentalDb.Rentals
            .Where(r => r.CustomerName == customerName).ToList();
        }
	}
}
