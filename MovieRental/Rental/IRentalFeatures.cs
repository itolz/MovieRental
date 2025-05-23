namespace MovieRental.Rental;

public interface IRentalFeatures
{
    public Task<Rental> RentMovie(Rental rental);
    IEnumerable<Rental> GetRentalsByCustomerName(string customerName);
}