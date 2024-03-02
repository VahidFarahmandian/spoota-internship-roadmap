using testingweb.Models;

namespace testingweb.Services
{
    public class OrderProcessingService
    {
        public bool ProcessOrder(Order order)
        {
            if (order != null)
            {
                order.IsProcessed= true;
                if (order.TotalAmount > 10)
                {
                    order.TotalAmount = order.TotalAmount - 10; 
                   
                }
                return true;
            } 
            return false;
        }
    }
}
