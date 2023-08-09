namespace FoodShop.Models.Order
{

    /// <summary>
    /// Containing information about the user that creates the order
    /// </summary>
    public partial class UserOrder
    {
        string _connectionString = string.Empty;
        
        public Guid UserGuid { get; set; }

        public Guid OrderGuid { get; set; }


        public UserOrder()
        {

        }

        public UserOrder(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
