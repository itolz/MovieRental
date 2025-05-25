namespace MovieRental.Rental;

public interface IRentalFeatures
{
    public Task<Rental> Save(Rental rental);
    IEnumerable<Rental> GetRentalsByCustomerName(string customerName);
}