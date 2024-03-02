using testingweb.Models;

namespace testingweb.Services
{
    public class PaymentGatewayService
    {
        public bool ProcessPayment(Order order)
        {
            if (order != null)
            {
                order.OrderDate= DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
