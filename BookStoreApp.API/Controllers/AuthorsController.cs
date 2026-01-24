using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTOs.Author;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(BookStoreDbContext _context, IMapper mapper, ILogger<AuthorsController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadOnlyAuthorDto>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
                var authorDtos = mapper.Map<List<ReadOnlyAuthorDto>>(authors);
                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadOnlyAuthorDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<ReadOnlyAuthorDto>(author));
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error Performing GET in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, UpdateAuthorDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }
            var author = mapper.Map<Author>(authorDto);
            _context.Entry(author).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (! await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CreateAuthorDto>> PostAuthor(CreateAuthorDto authorDto)
        {
            var author = mapper.Map<Author>(authorDto);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();


            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private async Task< bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
