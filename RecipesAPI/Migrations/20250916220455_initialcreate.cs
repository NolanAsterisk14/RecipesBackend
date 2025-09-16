using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipesAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Ingredients = table.Column<string>(type: "text", nullable: false),
                    Steps = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Ingredients", "Notes", "Steps", "Title" },
                values: new object[,]
                {
                    { 1, "Easy pan-seared chicken breasts cooked to perfection and simmered in a luscious roasted pepper sauce.", "2 lbs. (900g) boneless skinless chicken breasts or 3 medium chicken breasts; 1 jar roasted bell pepper sauce (usually found in the pasta sauce aisle of your grocery store); 2 teaspoons Italian seasoning; 1 pinch of Cayenne Pepper, optional; 2 - 3 tablespoons olive oil; 1/2 teaspoon salt and freshly cracked pepper to taste; 1/3 cup unsalted butter, diced; 2 heaping tablespoons grated Parmesan cheese, plus more for garnish; Fresh chopped parsley or basil for garnish;", null, "Start by seasoning chicken breasts with Italian seasoning, salt, and pepper on both sides.; In a large skillet, add olive oil and heat over medium-low heat.; Add the chicken breasts to the pan and cook for about 5 minutes per side, or until a nice golden brown and juices run clear. Transfer cooked chicken to a plate.; In the same skillet, add the roasted bell pepper sauce, oil, and Cayenne pepper, and reduce the heat to low. Slowly increase the heat to medium-low and bring the sauce to a gentle simmer. Simmer for 5 minutes, stirring occasionally.; In the sauce, whisk in diced butter and grated parmesan cheese until melted.; Carefully return the cooked chicken breasts to the skillet and allow them to simmer in the sauce for 3 minutes more.; Serve chicken over steamed cauliflower rice, bulgur, pasta or rice and garnish with chopped parsley or basil, and more parmesan cheese. Enjoy!", "Pan-Seared Chicken Breasts in Pepper Parmesan Sauce" },
                    { 2, "Parmesan Roasted Broccoli will be your new favorite vegetable! Broccoli florets take just 5 minutes of prep and are coated with parmesan cheese and Italian bread crumbs for that perfect crunchy texture.", "6 cups fresh broccoli florets; 3 tablespoons olive oil; 3 cloves garlic minced; 1/2 teaspoon salt; 1/4 teaspoon pepper; 1/4 cup Italian style bread crumbs; 1/2 cup fresh grated Parmesan cheese", null, "Preheat oven to 425 degrees. Line a baking sheet with foil and spray generously with nonstick cooking spray; In a large bowl add broccoli, olive oil, garlic, bread crumbs, and Parmesan cheese and mix until combined; Pour broccoli onto a baking sheet and sprinkle any leftover crumb mixture over the top; Bake on the top rack for 18-20 minutes; Remove from oven and let set 1-2 minutes.", "Parmesan Roasted Broccoli" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
