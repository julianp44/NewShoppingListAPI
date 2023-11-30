namespace BookStoreApi.Models;

public class ShoppingListDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ShippingListCollectionName { get; set; } = null!;
}