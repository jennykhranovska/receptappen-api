using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using receptappen_api.Data;
using receptappen_api.Models;


namespace receptappen_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();

            return Ok(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok(recipe);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, Recipe updatedRecipe)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Name = updatedRecipe.Name;
            recipe.Category = updatedRecipe.Category;
            recipe.CookingTime = updatedRecipe.CookingTime;

            await _context.SaveChangesAsync();

            return Ok(recipe);
        }
    }

}