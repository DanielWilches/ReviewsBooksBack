using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Constants;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Enums;
using Books.EnterpriseBusiness.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.ApplicationBusiness.Layer
{
    public class ReviewServices<T> where T : ReviewEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IModelResult<T> _modelResult;

        public ReviewServices(IRepository<T> repository, IModelResult<T> modelResult)
        {
            _repository = repository;
            _modelResult = modelResult;
        }

        public async Task<ModelResult<T>> AddReviewAsync(T review)
        {
            _modelResult.Code = (int)CodesResponse.OK;
            try
            {
                await _repository.AddAsync(review);
                _modelResult.Message = Constants.MSG_SUCCESS;
                _modelResult.Data = new List<T> { review };
            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Error adding review: {ex.Message}";
                _modelResult.Data = null;
            }
            return (ModelResult<T>)_modelResult;
        }

        public async Task<ModelResult<T>> GetReviewsByUser(int userId)
        {
            _modelResult.Code = (int)CodesResponse.OK;
            try
            {
                var reviews = await _repository.GetListAsync(r => r.UserId == userId);
                if (reviews == null || !reviews.Any())
                {
                    _modelResult.Code = (int)CodesResponse.NotFound;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} No reviews found for user";
                }
                else
                {
                    _modelResult.Data = reviews.OfType<T>().ToList();
                    _modelResult.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Error retrieving reviews: {ex.Message}";
            }
            return (ModelResult<T>)_modelResult;
        }

        public async Task<ModelResult<T>> GetReviewsByBook(int bookId)
        {
            _modelResult.Code = (int)CodesResponse.OK;
            try
            {
                var reviews = await _repository.GetListAsync(r => r.BookId == bookId);
                if (reviews == null || !reviews.Any())
                {
                    _modelResult.Code = (int)CodesResponse.NotFound;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} No reviews found for book";
                }
                else
                {
                    _modelResult.Data = reviews.OfType<T>().ToList();
                    _modelResult.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Error retrieving reviews: {ex.Message}";
            }
            return (ModelResult<T>)_modelResult;
        }

        public async Task<ModelResult<T>> UpdateReviewAsync(T review)
        {
            _modelResult.Code = (int)CodesResponse.OK;
            try
            {
                await _repository.UpdateAsync(review);
                _modelResult.Message = Constants.MSG_SUCCESS;
                _modelResult.Data = new List<T> { review };
            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Error updating review: {ex.Message}";
                _modelResult.Data = null;
            }
            return (ModelResult<T>)_modelResult;
        }
    }
}