
namespace MovieRental.Price
{
    public class PriceCalculator : IPriceCalculator 
    {
        public double CalculatePrice(Rental.Rental rental)
        {
            return rental.Price * rental.DaysRented;
        }
    }
}
