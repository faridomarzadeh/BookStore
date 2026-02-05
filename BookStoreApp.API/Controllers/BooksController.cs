using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTOs.Book;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController(BookStoreDbContext _context, ILogger<BooksController> logger, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadOnlyBookDto>>> GetBooks()
        {
            var books = await _context.Books.Include(b=>b.Author)
                .ProjectTo<ReadOnlyBookDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedBookDto>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Author)
                .ProjectTo<DetailedBookDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, UpdateBookDto bookDto)
        {
            if(id!= bookDto.Id)
            {
                return BadRequest();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            mapper.Map(bookDto, book);
            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Performing GET in {nameof(PutBook)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Book>> PostBook(CreateBookDto bookDto)
        {
            var book = mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NotFound();
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Books.AnyAsync(e=>e.Id==id);
        }
    }
}
