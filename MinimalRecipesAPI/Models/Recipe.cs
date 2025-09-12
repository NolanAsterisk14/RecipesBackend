
namespace MinimalRecipesAPI.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingredients { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public string? Notes { get; set; }

}