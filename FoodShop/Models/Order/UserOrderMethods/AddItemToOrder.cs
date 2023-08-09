using FoodShop.Models.Misc.Database;
using System.Data;

namespace FoodShop.Models.Order
{
    public partial class UserOrder
    {
        public async Task<bool> AddItemToOrder(Guid OrderId, OrderDetails food)
        {
			try
			{
				var ignoreProperty = new List<string>();
				ignoreProperty.Add(nameof(food.PriceTotal));
				ignoreProperty.Add(nameof(food.UserGuid));
				ignoreProperty.Add(nameof(food.Guid));

                var additionalData = new List<KeyValuePair<string, dynamic>>
                {
                    new KeyValuePair<string, dynamic>("OrderId", OrderId)
                };
                var result = await new Misc.Database.Procedures.Executor(_connectionString).Execute<OrderDetails, DbResult>("ADDORDER", food, ignoreProperty, additionalData);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }
    }
}
