using AutoMapper;
using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Constants;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Enums;
using Books.EnterpriseBusiness.Layer.Models;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;


namespace Books.ApplicationBusiness.Layer
{
    public class BookServices<T> where T : BookEntity 
    {
        private readonly IRepository<T> _repository;
        private readonly IModelResult<T> _modelResult;

        public BookServices(IRepository<T> repository, IModelResult<T> modelResult)
        {
            _repository = repository;
            _modelResult = modelResult;
        }
        public async Task<ModelResult<T>> GetAllBooks() 
        {
            _modelResult.Code = (int)CodesResponse.OK;

            try
            {
                var books = await _repository.GetAllAsync();
                if (books == null || !books.Any())
                {
                    _modelResult.Code = (int)CodesResponse.OK;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} No books found";
                }
                else
                {
                    _modelResult.Data = books.OfType<T>().ToList();
                    
                }

            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");                
            }

            return (ModelResult<T>)_modelResult;
        }
        public async Task<ModelResult<T>> GetBookById(string Id) 
        {
            _modelResult.Code = (int)CodesResponse.OK;
            try
            {
                if(string.IsNullOrEmpty(Id) || !int.TryParse(Id, out int bookId))
                {
                    _modelResult.Code = (int)CodesResponse.BadRequest;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} Invalid book ID provided";
                    return (ModelResult<T>)_modelResult;
                }

                var book = await _repository.GetByIdAsync(bookId);

                if (book == null)
                {
                    _modelResult.Code = (int)CodesResponse.NotFound;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} Book not found";
                }
                else
                {
                    _modelResult.Data = [book as T];
                    _modelResult.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ModelResult<T>)_modelResult;
        }

        public async Task<ModelResult<T>> GetBookByauthor(string Author) 
        {
            _modelResult.Code = (int)CodesResponse.OK;
            if (string.IsNullOrEmpty(Author))
            {
                _modelResult.Code = (int)CodesResponse.BadRequest;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Author name cannot be null or empty";
                return (ModelResult<T>)_modelResult;
            }

            try
            {
                var books = await _repository.GetListAsync(b => b.Author.ToUpper() == Author.ToUpper());
                if (books == null || !books.Any())
                {
                    _modelResult.Code = (int)CodesResponse.NotFound;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} No books found for author";
                }
                else
                {
                    _modelResult.Data = books.OfType<T>().ToList();
                    _modelResult.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ModelResult<T>)_modelResult;

        }
        public async Task<ModelResult<T>> GetBookByTitle(string  Title) 
        {

            _modelResult.Code = (int)CodesResponse.OK;
            if (string.IsNullOrEmpty(Title))
            {
                _modelResult.Code = (int)CodesResponse.BadRequest;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Title cannot be null or empty";
                return (ModelResult<T>)_modelResult;
            }
            try
            {
                var books = await _repository.GetListAsync(b =>  b.Title.ToUpper() == Title.ToUpper());
                if (books == null || !books.Any())
                {
                    _modelResult.Code = (int)CodesResponse.NotFound;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} No books found with the specified title";
                }
                else
                {
                    _modelResult.Data = books.OfType<T>().ToList();
                    _modelResult.Message = Constants.MSG_SUCCESS;
                }

            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ModelResult<T>)_modelResult;

        }
        public async Task<ModelResult<T>> GetBooksByCategory(string Category) 
        {

            _modelResult.Code = (int)CodesResponse.OK;
            if (string.IsNullOrEmpty(Category))
            {
                _modelResult.Code = (int)CodesResponse.BadRequest;
                _modelResult.Message = $"{Constants.MSG_FAILURE} Category cannot be null or empty";
                return (ModelResult<T>)_modelResult;
            }
            try
            {
                var books = await _repository.GetListAsync(b => b.Category.ToUpper() == Category.ToUpper());
                if (books == null || !books.Any())
                {
                    _modelResult.Code = (int)CodesResponse.NotFound;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} No books found in the specified category";
                }
                else
                {
                    _modelResult.Data = books.OfType<T>().ToList();
                    _modelResult.Message = Constants.MSG_SUCCESS;
                }

            }
            catch (Exception ex)
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                _modelResult.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ModelResult<T>)_modelResult;

        }
    }
}
