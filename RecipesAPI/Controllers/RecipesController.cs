using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Models;

namespace RecipesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{
    private List<Recipe> recipes = new List<Recipe>
    {
        new Recipe
        {
            Id = 1,
            Title = "Pan-Seared Chicken Breasts in Pepper Parmesan Sauce",
            Description = "Easy pan-seared chicken breasts cooked to perfection and simmered in a luscious roasted pepper sauce.",
            Ingredients = "2 lbs. (900g) boneless skinless chicken breasts or 3 medium chicken breasts; 1 jar roasted bell pepper sauce (usually found in the pasta sauce aisle of your grocery store); 2 teaspoons Italian seasoning; 1 pinch of Cayenne Pepper, optional; 2 - 3 tablespoons olive oil; 1/2 teaspoon salt and freshly cracked pepper to taste; 1/3 cup unsalted butter, diced; 2 heaping tablespoons grated Parmesan cheese, plus more for garnish; Fresh chopped parsley or basil for garnish;",
            Steps = "Start by seasoning chicken breasts with Italian seasoning, salt, and pepper on both sides.; In a large skillet, add olive oil and heat over medium-low heat.; Add the chicken breasts to the pan and cook for about 5 minutes per side, or until a nice golden brown and juices run clear. Transfer cooked chicken to a plate.; In the same skillet, add the roasted bell pepper sauce, oil, and Cayenne pepper, and reduce the heat to low. Slowly increase the heat to medium-low and bring the sauce to a gentle simmer. Simmer for 5 minutes, stirring occasionally.; In the sauce, whisk in diced butter and grated parmesan cheese until melted.; Carefully return the cooked chicken breasts to the skillet and allow them to simmer in the sauce for 3 minutes more.; Serve chicken over steamed cauliflower rice, bulgur, pasta or rice and garnish with chopped parsley or basil, and more parmesan cheese. Enjoy!"
        },
        new Recipe
        {
            Id = 2,
            Title = "Parmesan Roasted Broccoli",
            Description = "Parmesan Roasted Broccoli will be your new favorite vegetable! Broccoli florets take just 5 minutes of prep and are coated with parmesan cheese and Italian bread crumbs for that perfect crunchy texture.",
            Ingredients = "6 cups fresh broccoli florets; 3 tablespoons olive oil; 3 cloves garlic minced; 1/2 teaspoon salt; 1/4 teaspoon pepper; 1/4 cup Italian style bread crumbs; 1/2 cup fresh grated Parmesan cheese",
            Steps = "Preheat oven to 425 degrees. Line a baking sheet with foil and spray generously with nonstick cooking spray; In a large bowl add broccoli, olive oil, garlic, bread crumbs, and Parmesan cheese and mix until combined; Pour broccoli onto a baking sheet and sprinkle any leftover crumb mixture over the top; Bake on the top rack for 18-20 minutes; Remove from oven and let set 1-2 minutes."
        }
    };

    [HttpGet]
    public ActionResult<List<Recipe>> GetRecipes()
    {
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> GetRecipe(int id)
    {
        var recipe = recipes.FirstOrDefault(x => x.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }

    [HttpPost]
    public ActionResult<Recipe> AddRecipe(Recipe newRecipe)
    {
        if (newRecipe == null)
        {
            return BadRequest();
        }

        recipes.Add(newRecipe);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRecipe(int id, Recipe updatedRecipe)
    {
        var recipe = recipes.FirstOrDefault(x => x.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        recipe.Title = updatedRecipe.Title;
        recipe.Description = updatedRecipe.Description;
        recipe.Ingredients = updatedRecipe.Ingredients;
        recipe.Steps = updatedRecipe.Steps;
        recipe.Notes = updatedRecipe.Notes;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRecipe(int id)
    {
        var recipe = recipes.FirstOrDefault(x => x.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        recipes.Remove(recipe);
        return NoContent();
    }
}