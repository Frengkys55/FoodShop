using FoodShop.Models.Misc.Database;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Data;

namespace FoodShop.Models.Order
{
    public partial class UserOrder
    {
        public async Task<IEnumerable<OrderDetailsExtended>> GetListOrder(Guid order)
        {
			try
			{
				var parameters = new Collection<KeyValuePair<string, string>>()
				{
					new KeyValuePair<string, string>("UserGuid", order.ToString())
				};

				var result = await new Misc.Database.GenericRead<OrderDetailsExtended>().Read("GETUSERORDER", _connectionString, parameters);
				return result;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
