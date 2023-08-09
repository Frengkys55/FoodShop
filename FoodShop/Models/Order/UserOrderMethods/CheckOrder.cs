using FoodShop.Models.Misc.Database;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Data;

namespace FoodShop.Models.Order
{
    public partial class UserOrder
    {
        public async Task<bool> CheckOrder(Guid OrderId)
        {
			try
			{
				var additionalData = new List<KeyValuePair<string, dynamic>>()
				{
					new KeyValuePair<string, dynamic>("OrderGuid", OrderId)
				};

				var result = await new Misc.Database.Procedures.Executor(_connectionString).Execute<DBNull, DbResult>("CHECKORDER", null, null, additionalData);
				if (Convert.ToBoolean(result.ToList()[0].Result))
				{
					return true;
				}
				else
					return false;
			}
			catch (Exception)
			{
				throw;
			};
        }
    }
}
