using Microsoft.AspNetCore.Mvc;
using receptappen_api.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}
