namespace FoodShop.Models.Order
{
    public class OrderDetailsExtended : OrderDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
