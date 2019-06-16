namespace FoodDeliveryServer.Entities
{
    public interface IProduct
    {
        string Name { get; set; }

        decimal Price { get; set; }
    }
}
