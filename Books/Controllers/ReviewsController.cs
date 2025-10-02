using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using MediatR;
using Books.Application.Layer.Querys.Reviews;

namespace BooksPresentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly ISender _sender;

        public ReviewsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Agrega una nueva review
        /// </summary>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<ReviewEntity>>> AddReview([FromBody] ReviewEntity review)
        {
            var result = await _sender.Send(new AddReviewCommand(review));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene reviews por usuario
        /// </summary>
        [HttpGet("user/{userId:int}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<ReviewEntity>>> GetReviewsByUser(int userId)
        {
            var result = await _sender.Send(new GetReviewsByUserQuery(userId));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene reviews por libro
        /// </summary>
        [HttpGet("book/{bookId:int}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<ReviewEntity>>> GetReviewsByBook(int bookId)
        {
            var result = await _sender.Send(new GetReviewsByBookQuery(bookId));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Actualiza una review existente
        /// </summary>
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<ReviewEntity>>> UpdateReview([FromBody] ReviewEntity review)
        {
            var result = await _sender.Send(new UpdateReviewCommand(review));
            return StatusCode(result.Code, result);
        }
    }
}
