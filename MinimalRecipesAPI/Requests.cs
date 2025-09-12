using Microsoft.EntityFrameworkCore;
using MinimalRecipesAPI.Data;
using MinimalRecipesAPI.Models;

public static class Requests
{
    //Request methods
    public static async Task<IResult> GetRecipes(MinimalRecipesAPIContext db)
    {
        return Results.Ok(await db.Recipes.ToListAsync());
    }

    public static async Task<IResult> GetRecipe(int id, MinimalRecipesAPIContext db)
    {
        var recipe = await db.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(recipe);
    }

    public static async Task<IResult> AddRecipe(Recipe newRecipe, MinimalRecipesAPIContext db)
    {
        if (newRecipe == null)
        {
            return Results.BadRequest();
        }

        db.Recipes.Add(newRecipe);
        return await TrySaveChangesAsync(
            async () => await db.SaveChangesAsync(),
            newRecipe,
            r => Results.Created($"api/recipes/{r.Id}", r)
        );
    }

    public static async Task<IResult> UpdateRecipe(int id, Recipe updatedRecipe, MinimalRecipesAPIContext db)
    {
        var recipe = await db.Recipes.FirstOrDefaultAsync(x => x.Id == id);
        if (recipe == null)
        {
            return Results.NotFound();
        }

        recipe.Title = updatedRecipe.Title;
        recipe.Description = updatedRecipe.Description;
        recipe.Ingredients = updatedRecipe.Ingredients;
        recipe.Steps = updatedRecipe.Steps;
        recipe.Notes = updatedRecipe.Notes;

        db.Update(recipe);

        return await TrySaveChangesAsync(async () => await db.SaveChangesAsync());
    }

    public static async Task<IResult> DeleteRecipe(int id, MinimalRecipesAPIContext db)
    {
        var recipe = await db.Recipes.FirstOrDefaultAsync(x => x.Id == id);
        if (recipe == null)
        {
            return Results.NotFound();
        }

        db.Recipes.Remove(recipe);

        return await TrySaveChangesAsync(async () => await db.SaveChangesAsync());
    }

    //Helper methods
    private static async Task<IResult> TrySaveChangesAsync(Func<Task> action)
    {
        try
        {
            await action();
            return Results.NoContent();
        }
        catch (Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            Console.WriteLine($"Exception occurred: {e.Message}");
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> TrySaveChangesAsync<T>(Func<Task> action, T result, Func<T, IResult> onSuccess)
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
            return Results.BadRequest(e.Message);
        }
    }
}
