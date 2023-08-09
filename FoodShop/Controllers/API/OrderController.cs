using FoodShop.Models.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections;

namespace FoodShop.Controllers.API
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IConfiguration configuration;
        public OrderController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDetailsExtended>> GetOrders([FromQuery] Guid id) {
            try
            {
                
                return await new UserOrder(configuration.GetConnectionString("Default")).GetListOrder(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("new")]
        public async Task<UserOrder> CreateNewOrder()
        {
            UserOrder userOrder = new UserOrder(configuration.GetConnectionString("Default")!);
            userOrder.UserGuid = Guid.NewGuid();

            userOrder.OrderGuid = await userOrder.CreateNewOrder(userOrder);

            return userOrder;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewItem([FromQuery] Guid foodGuid, [FromQuery] Guid orderGuid, [FromQuery] int quantity)
        {
            OrderDetails details = new OrderDetails()
            {
                Guid = Guid.NewGuid(),
                FoodGuid = foodGuid,
                Quantity = quantity
            };

            try
            {
                var result = await new UserOrder(configuration.GetConnectionString("Default")!).AddItemToOrder(orderGuid, details);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("completepayment")] 
        public async Task<IActionResult> CompleteOrderPayment([FromQuery] Guid id)
        {
            try
            {
                var result = await new UserOrder(configuration.GetConnectionString("Default")!).PaymentCompleteOrder(id);
                return Ok(1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Check if an order has been paid
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        [HttpGet("check")]
        public async Task<IActionResult> CheckOrderStatus([FromQuery] Guid id)
        {
            try
            {
                var result = await new UserOrder(configuration.GetConnectionString("Default")!).CheckOrder(id);
                if (result)
                {
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
