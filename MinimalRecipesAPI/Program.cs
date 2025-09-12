using Microsoft.EntityFrameworkCore;
using MinimalRecipesAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MinimalRecipesAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/Recipes", Requests.GetRecipes);
app.MapGet("/api/Recipes/{id}", Requests.GetRecipe);
app.MapPost("/api/Recipes", Requests.AddRecipe);
app.MapPut("/api/Recipes/{id}", Requests.UpdateRecipe);
app.MapDelete("/api/Recipes/{id}", Requests.DeleteRecipe);


app.Run();
