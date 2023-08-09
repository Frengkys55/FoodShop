using FoodShop.Models.Food;
using FoodShop.Models.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Controllers.API
{
    [Route("api/Food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        IConfiguration configuration;

        public FoodController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Food>> GetFoods()
        {
            var food = new Food(configuration.GetConnectionString("Default")!);
            return await food.List();
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Food>> SearchFood([FromQuery] string name, [FromQuery] int page)
        {
            try
            {
                if (page > 0)
                {
                    return await new Food(configuration.GetConnectionString("default")!).Search(name, page);
                }
                else
                {
                    return await new Food(configuration.GetConnectionString("default")!).Search(name);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    
        
    }
}
