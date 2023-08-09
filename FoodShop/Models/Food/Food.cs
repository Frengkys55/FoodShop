namespace FoodShop.Models.Food
{
    public partial class Food
    {
        string _connectionString = string.Empty;

        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }

        public Food() { }
        public Food(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
