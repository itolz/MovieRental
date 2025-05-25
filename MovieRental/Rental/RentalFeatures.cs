using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;
using MovieRental.Price;
using System.Transactions;

namespace MovieRental.Rental
{
    public class RentalFeatures : IRentalFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        private readonly IEnumerable<IPaymentProvider> _paymentProviders;
        private readonly IPriceCalculator _priceCalculator;
        public RentalFeatures(MovieRentalDbContext movieRentalDb, IEnumerable<IPaymentProvider> paymentProviders, IPriceCalculator priceCalculator)
        {
            _movieRentalDb = movieRentalDb;
            _paymentProviders = paymentProviders;
            _priceCalculator = priceCalculator;
        }

        public async Task<Rental> Save(Rental rental)
        {
            //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
                var processResult = await PaymentProcess(rental);

                if (processResult)
                {
                    _movieRentalDb.Rentals.Add(rental);
                    await _movieRentalDb.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Payment failed");
                }
            //}
            
            return rental;
        }

        public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
        {
            return _movieRentalDb.Rentals
                 .Include(r => r.Movie)
                 .Include(r => r.Customer)
                 .Where(r => r.Customer != null && r.Customer.CustomerName == customerName)
                 .ToList();
        }

        private Task<bool> PaymentProcess(Rental rental)
        {
            var paymentProvider = _paymentProviders.FirstOrDefault(p => p.GetType().Name.ToLower() == $"{rental.PaymentMethod}Provider".ToLower());

            if (paymentProvider == null)
            {
                throw new Exception($"Payment provider '{rental.PaymentMethod}' not found.");
            }

            var price = _priceCalculator.CalculatePrice(rental);
            return paymentProvider.Pay(price);
        }
    }
}
