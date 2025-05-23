
namespace MovieRental.PaymentProviders
{
    public class FailPaymentProvider : IPaymentProvider
    {
        public Task<bool> Pay(double price)
        {
            throw new NotImplementedException();
        }
    }
}
