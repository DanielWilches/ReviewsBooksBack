using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Books.Domain.Layer.Models;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;

namespace BooksPresentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewServices<ReviewEntity> _reviewServices;

        public ReviewsController(ReviewServices<ReviewEntity> reviewServices)
        {
            _reviewServices = reviewServices;
        }

        /// <summary>
        /// Agrega una nueva review
        /// </summary>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<ReviewEntity>>> AddReview([FromBody] ReviewEntity review)
        {
            var result = await _reviewServices.AddReviewAsync(review);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene reviews por usuario
        /// </summary>
        [HttpGet("user/{userId:int}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<ReviewEntity>>> GetReviewsByUser(int userId)
        {
            var result = await _reviewServices.GetReviewsByUser(userId);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Obtiene reviews por libro
        /// </summary>
        [HttpGet("book/{bookId:int}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<ReviewEntity>>> GetReviewsByBook(int bookId)
        {
            var result = await _reviewServices.GetReviewsByBook(bookId);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Actualiza una review existente
        /// </summary>
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<ReviewEntity>>> UpdateReview([FromBody] ReviewEntity review)
        {
            var result = await _reviewServices.UpdateReviewAsync(review);
            return StatusCode(result.Code, result);
        }
    }
}
