using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ShoppingListStoreApi.Services;

public class ShoppingListService
{
    private readonly IMongoCollection<ShoppingList> _shoppingListCollection;

    public ShoppingListService(
        IOptions<ShoppingListDatabaseSettings> shoppingListDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            shoppingListDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            shoppingListDatabaseSettings.Value.DatabaseName);

        _shoppingListCollection = mongoDatabase.GetCollection<ShoppingList>(
            shoppingListDatabaseSettings.Value.ShoppingListCollectionName);
    }

    public async Task<List<ShoppingList>> GetAsync() =>
        await _shoppingListCollection.Find(_ => true).ToListAsync(); // Returns all documents in the collection matching the provided search criteria.

    public async Task<ShoppingList?> GetAsync(string id) =>
        await _shoppingListCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); // Returns all documents in the collection matching the provided search criteria.

    public async Task CreateAsync(ShoppingList newShoppingList) =>
        await _shoppingListCollection.InsertOneAsync(newShoppingList); // Inserts the provided object as a new document in the collection.

    public async Task UpdateAsync(string id, ShoppingList updatedShoppingList) =>
        await _shoppingListCollection.ReplaceOneAsync(x => x.Id == id, updatedShoppingList); // Replaces the single document matching the provided search criteria with the provided object.

    public async Task RemoveAsync(string id) =>
        await _shoppingListCollection.DeleteOneAsync(x => x.Id == id); //Deletes a single document matching the provided search criteria.
}