using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using Books.Application.Layer.Services;

namespace BooksPresentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/books")]
    public class BooksController : ControllerBase
    {
        private readonly BookServices<BookEntity> _bookServices;

        public BooksController(BookServices<BookEntity> bookServices)
        {
            _bookServices = bookServices;
        }

        /// <summary>
        /// Obtiene todos los libros
        /// </summary>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<BookEntity>>> GetAllBooks()
        {
            var result = await _bookServices.GetAllBooks();
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene un libro por su ID
        /// </summary>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<BookEntity>>> GetBookById(string id)
        {
            var result = await _bookServices.GetBookById(id);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene libros por autor
        /// </summary>
        [HttpGet("author/{author}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<BookEntity>>> GetBooksByAuthor(string author)
        {
            var result = await _bookServices.GetBookByauthor(author);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene libros por título
        /// </summary>
        [HttpGet("title/{title}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<BookEntity>>> GetBooksByTitle(string title)
        {
            var result = await _bookServices.GetBookByTitle(title);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene libros por categoría
        /// </summary>
        [HttpGet("category/{category}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<BookEntity>>> GetBooksByCategory(string category)
        {
            var result = await _bookServices.GetBooksByCategory(category);
            return StatusCode(result.Code, result);
        }
    }
}
