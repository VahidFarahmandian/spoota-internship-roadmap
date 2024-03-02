using testingweb.Models;

namespace testingweb.Services
{
    public class IntegrationService
    {
        private readonly OrderProcessingService _orderProcessingService;
        private readonly PaymentGatewayService _paymentGatewayService;

        public IntegrationService(OrderProcessingService orderProcessingService, PaymentGatewayService paymentGatewayService)
        {
            _orderProcessingService = orderProcessingService;
            _paymentGatewayService = paymentGatewayService;
        }

        public bool ProcessOrderAndPayment(Order order)
        {
            var orderProcessed = _orderProcessingService.ProcessOrder(order);
            var paymentProcessed = _paymentGatewayService.ProcessPayment(order);

            return orderProcessed && paymentProcessed;
        }
    
}
}
