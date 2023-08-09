using System.Collections.ObjectModel;

namespace FoodShop.Models.Food
{
    public partial class Food
    {
        public async Task<IEnumerable<Food>> Search(string foodName, int page = 0, int maxSize = 5)
        {
			try
			{
				var parameter = new Collection<KeyValuePair<string, string>>();
				parameter.Add(new KeyValuePair<string, string>("Name", foodName));
				var result = await new Misc.Database.GenericRead<Food>().Read("SEARCHFOOD", _connectionString, parameter);

				var paginated = new Collection<Food>();

				if (result.Count > maxSize)
				{
					int startNumber = (page == 0) ? 0 : ((page - 1) * maxSize + 1) - 1 ;
					int endNumber = startNumber + maxSize;

					for (int i = startNumber; i < result.Count; i++)
					{
						paginated.Add(result.ToList()[i]);
						if (i == result.Count || startNumber == endNumber)
						{
							break;
						}
					}
					return paginated;

				}
				else
					return result;

            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
