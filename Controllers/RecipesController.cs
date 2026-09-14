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
        private readonly IWebHostEnvironment _environment;

        public RecipesController(
            AppDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public async Task<IActionResult> UpdateRecipe(
            int id,
            Recipe updatedRecipe)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Name = updatedRecipe.Name;
            recipe.Category = updatedRecipe.Category;
            recipe.CookingTime = updatedRecipe.CookingTime;
            recipe.Ingredients = updatedRecipe.Ingredients;
            recipe.Instructions = updatedRecipe.Instructions;

            await _context.SaveChangesAsync();

            return Ok(recipe);
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(
            int id,
            IFormFile image)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (image == null || image.Length == 0)
            {
                return BadRequest("Ingen bild valdes.");
            }

            var uploadsFolder = Path.Combine(
     _environment.ContentRootPath,
     "wwwroot",
     "uploads"
 );

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(image.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                uploadsFolder,
                fileName
            );

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            recipe.ImagePath = $"/uploads/{fileName}";

            await _context.SaveChangesAsync();

            return Ok(recipe);
        }

        [HttpDelete("{id}/image")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(recipe.ImagePath))
            {
                var fileName = Path.GetFileName(recipe.ImagePath);

                var filePath = Path.Combine(
                    _environment.ContentRootPath,
                    "wwwroot",
                    "uploads",
                    fileName
                );

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            recipe.ImagePath = "";

            await _context.SaveChangesAsync();

            return Ok(recipe);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}