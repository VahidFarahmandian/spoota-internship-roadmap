using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testingweb.Models;
using testingweb.Services;

namespace testingweb.test
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void ProcessOrderAndPayment_Successful()
        {
            // Arrange
            var order = new Order();
            var orderProcessingService = new OrderProcessingService();
            var paymentGatewayService = new PaymentGatewayService();
            var integrationService = new IntegrationService(orderProcessingService, paymentGatewayService);

            // Act
            var result = integrationService.ProcessOrderAndPayment(order);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessOrderAndPayment_Failed_OrderProcessing()
        {
            // Arrange
            var order = new Order();
            var orderProcessingService = new OrderProcessingService();
            var paymentGatewayService = new PaymentGatewayService();
            var integrationService = new IntegrationService(orderProcessingService, paymentGatewayService);

            // Act
            // Simulate a scenario where order processing fails
            order = null; // Set order to null to simulate failure
            var result = integrationService.ProcessOrderAndPayment(order);

            // Assert
            Assert.IsFalse(result);
        }

       
    }
}
