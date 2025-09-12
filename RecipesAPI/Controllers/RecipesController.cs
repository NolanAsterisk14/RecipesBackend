using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesAPI.Data;
using RecipesAPI.Models;

namespace RecipesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesAPIContext _context;

    public RecipesController(RecipesAPIContext context)
    {
        _context = context;
    }

    //CRUD operation requests

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetRecipes()
    {
        return Ok(await _context.Recipes.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipe(int id)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }

    [HttpPost]
    public async Task<ActionResult<Recipe>> AddRecipe(Recipe newRecipe)
    {
        if (newRecipe == null)
        {
            return BadRequest();
        }

        _context.Recipes.Add(newRecipe);

        return await TrySaveChangesAsync(
            async () => await _context.SaveChangesAsync(),
            newRecipe,
            r => CreatedAtAction(nameof(GetRecipe), new { id = r.Id }, r)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(int id, Recipe updatedRecipe)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        recipe.Title = updatedRecipe.Title;
        recipe.Description = updatedRecipe.Description;
        recipe.Ingredients = updatedRecipe.Ingredients;
        recipe.Steps = updatedRecipe.Steps;
        recipe.Notes = updatedRecipe.Notes;

        _context.Update(recipe);

        return await TrySaveChangesAsync(async () => await _context.SaveChangesAsync());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        _context.Recipes.Remove(recipe);

        return await TrySaveChangesAsync(async () => await _context.SaveChangesAsync());
    }

    //Helper methods

    private async Task<ActionResult> TrySaveChangesAsync(Func<Task> action)
    {
        try
        {
            await action();
            return NoContent();
        }
        catch (Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            Console.WriteLine($"Exception occurred: {e.Message}");
            return BadRequest(e.Message);
        }
    }

    private async Task<ActionResult<T>> TrySaveChangesAsync<T>(Func<Task> action, T result, Func<T, ActionResult<T>> onSuccess)
    {
        try
        {
            await action();
            return onSuccess(result);
        }
        catch (Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            Console.WriteLine($"Exception occurred: {e.Message}");
            return BadRequest(e.Message);
        }
    }
}