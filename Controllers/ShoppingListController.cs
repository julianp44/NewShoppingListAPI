using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using ShoppingListStoreApi.Services;

namespace ShoppingListStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController : ControllerBase
{
    private readonly ShoppingListService _shoppingListService;

    public ShoppingListController(ShoppingListService shoppingListService) =>
        _shoppingListService = shoppingListService;

    [HttpGet]
    public async Task<List<ShoppingList>> Get()
    {
      return  await _shoppingListService.GetAsync();
    }
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ShoppingList>> Get(string id)
    {
        var shoppingList = await _shoppingListService.GetAsync(id);

        if (shoppingList is null)
        {
            return NotFound();
        }

        return shoppingList;
    }
 
    [HttpPost]
    public async Task<IActionResult> Post(ShoppingList newShoppingList)
    {
        await _shoppingListService.CreateAsync(newShoppingList);

        return CreatedAtAction(nameof(Get), new { id = newShoppingList.Id }, newShoppingList);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, ShoppingList updatedShoppingList)
    {
        var shoppingList = await _shoppingListService.GetAsync(id);

        if (shoppingList is null)
        {
            return NotFound();
        }

        updatedShoppingList.Id = shoppingList.Id;

        await _shoppingListService.UpdateAsync(id, updatedShoppingList);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var shoppingList = await _shoppingListService.GetAsync(id);

        if (shoppingList is null)
        {
            return NotFound();
        }

        await _shoppingListService.RemoveAsync(id);

        return NoContent();
    }
}