#region Project Usings
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using Books.Domain.Layer.Enums;
using Books.Domain.Layer.Interfaces;
using Books.Domain.Layer.Constants;
#endregion

namespace Books.Application.Layer.Services
{
    public class ReviewServices<T> where T : ReviewEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IResultDto<T> _ResultDto;

        public ReviewServices(IRepository<T> repository, IResultDto<T> ResultDto)
        {
            _repository = repository;
            _ResultDto = ResultDto;
        }

        public async Task<ResultDto<T>> AddReviewAsync(T review)
        {
            _ResultDto.Code = (int)CodesResponse.OK;
            try
            {
                await _repository.AddAsync(review);
                _ResultDto.Message = Constants.MSG_SUCCESS;
                _ResultDto.Data = new List<T> { review };
            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Error adding review: {ex.Message}";
                _ResultDto.Data = null;
            }
            return (ResultDto<T>)_ResultDto;
        }

        public async Task<ResultDto<T>> GetReviewsByUser(int userId)
        {
            _ResultDto.Code = (int)CodesResponse.OK;
            try
            {
                var reviews = await _repository.GetListAsync(r => r.UserId == userId);
                if (reviews == null || !reviews.Any())
                {
                    _ResultDto.Code = (int)CodesResponse.NotFound;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} No reviews found for user";
                }
                else
                {
                    _ResultDto.Data = reviews.OfType<T>().ToList();
                    _ResultDto.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Error retrieving reviews: {ex.Message}";
            }
            return (ResultDto<T>)_ResultDto;
        }

        public async Task<ResultDto<T>> GetReviewsByBook(int bookId)
        {
            _ResultDto.Code = (int)CodesResponse.OK;
            try
            {
                var reviews = await _repository.GetListAsync(r => r.BookId == bookId);
                if (reviews == null || !reviews.Any())
                {
                    _ResultDto.Code = (int)CodesResponse.NotFound;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} No reviews found for book";
                }
                else
                {
                    _ResultDto.Data = reviews.OfType<T>().ToList();
                    _ResultDto.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Error retrieving reviews: {ex.Message}";
            }
            return (ResultDto<T>)_ResultDto;
        }

        public async Task<ResultDto<T>> UpdateReviewAsync(T review)
        {
            _ResultDto.Code = (int)CodesResponse.OK;
            try
            {
                await _repository.UpdateAsync(review);
                _ResultDto.Message = Constants.MSG_SUCCESS;
                _ResultDto.Data = new List<T> { review };
            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Error updating review: {ex.Message}";
                _ResultDto.Data = null;
            }
            return (ResultDto<T>)_ResultDto;
        }
    }
}