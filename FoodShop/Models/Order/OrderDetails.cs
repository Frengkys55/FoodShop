namespace FoodShop.Models.Order
{
    /// <summary>
    /// Containing the details of the ordered food
    /// </summary>
    public class OrderDetails
    {
        public Guid Guid { get; set; }
        public Guid? UserGuid { get; set; }
        public Guid FoodGuid { get; set; }
        public int Quantity { get; set; }
        public int PriceTotal { get; set; }
    }
}
