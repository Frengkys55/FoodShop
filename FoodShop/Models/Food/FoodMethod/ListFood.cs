using FoodShop.Models.Misc.Database;
using FoodShop.Models.Misc.Database.Procedures;
namespace FoodShop.Models.Food
{
    public partial class Food
    {
        public async Task<ICollection<Food>> List()
        {
            try
            {
                return await new GenericRead<Food>().Read("LISTFOODS", _connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
