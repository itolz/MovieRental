namespace MovieRental.Price
{
    public interface IPriceCalculator
    {
        double CalculatePrice(Rental.Rental rental);
    }
}
