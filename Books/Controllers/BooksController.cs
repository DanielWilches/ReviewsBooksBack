using Asp.Versioning;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Querys.GetAllBooks;
using Books.Application.Layer.Querys.GetBookByauthor;
using Books.Application.Layer.Querys.GetBookById;
using Books.Application.Layer.Querys.GetBookByTitle;
using Books.Application.Layer.Querys.GetBooksByCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksPresentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/books")]
    public class BooksController : ApiBaseController
    {
        public BooksController(ISender sender) : base(sender) { }

        /// <summary>
        /// Obtiene todos los libros
        /// </summary>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<BookDto>>> GetAllBooks()
        {
            var result = await _sender.Send(new GetAllBooksQuery());
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene un libro por su ID
        /// </summary>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<BookDto>>> GetBookById(string id)
        {
            var result = await _sender.Send(new GetBookByIdQuery(id));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene libros por autor
        /// </summary>
        [HttpGet("author/{author}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<BookDto>>> GetBooksByAuthor(string author)
        {
            var result = await _sender.Send(new GetBookByAuthorQuery(author));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene libros por título
        /// </summary>
        [HttpGet("title/{title}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<BookDto>>> GetBooksByTitle(string title)
        {
            var result = await _sender.Send(new GetBookByTitleQuery(title));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene libros por categoría
        /// </summary>
        [HttpGet("category/{category}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<BookDto>>> GetBooksByCategory(string category)
        {
            var result = await _sender.Send(new GetBooksByCategoryQuery(category));
            return StatusCode(result.Code, result);
        }
    }
}
