using FoodShop.Models.Misc.Database;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Data;

namespace FoodShop.Models.Order
{
    public partial class UserOrder
    {
        public async Task<bool> PaymentCompleteOrder(Guid OrderId)
        {
			try
			{

				var additionalData = new List<KeyValuePair<string, dynamic>>()
				{
					new KeyValuePair<string, dynamic>("OrderGuid", OrderId)
				};

				var result = await new Misc.Database.Procedures.Executor(_connectionString).Execute<DBNull, DbResult>("PAYORDERCOMPLETION", null, null, additionalData);
				return true;
			}
			catch (Exception)
			{
				return false;
			};
        }
    }
}
