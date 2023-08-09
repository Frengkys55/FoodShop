using FoodShop.Models.Misc.Database;
using Microsoft.Identity.Client;
using System.Data;
using System.Diagnostics;

namespace FoodShop.Models.Order
{
    public partial class UserOrder
    {
        public async Task<Guid> CreateNewOrder(UserOrder user)
        {
			try
			{
				var ignoreProperty = new List<string>();
				ignoreProperty.Add(nameof(user.OrderGuid));

				var result = await new Misc.Database.Procedures.Executor(_connectionString).Execute<UserOrder, DbResult>("CREATENEWORDER", user, ignoreProperty);

				return Guid.Parse(result.ToArray()[0].Result);
			}
			catch (Exception )
			{

				throw;
			}
        }
    }
}
