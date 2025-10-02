#region Project Usings
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using Books.Domain.Layer.Enums;
using Books.Domain.Layer.Interfaces;
using Books.Domain.Layer.Constants;
#endregion


namespace Books.Application.Layer.Services
{
    public class BookServices<T> where T : BookEntity 
    {
        private readonly IRepository<T> _repository;
        private readonly IResultDto<T> _ResultDto;

        public BookServices(IRepository<T> repository, IResultDto<T> ResultDto)
        {
            _repository = repository;
            _ResultDto = ResultDto;
        }
        public async Task<ResultDto<T>> GetAllBooks() 
        {
            _ResultDto.Code = (int)CodesResponse.OK;

            try
            {
                var books = await _repository.GetAllAsync();
                if (books == null || !books.Any())
                {
                    _ResultDto.Code = (int)CodesResponse.OK;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} No books found";
                }
                else
                {
                    _ResultDto.Data = books.OfType<T>().ToList();
                    
                }

            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");                
            }

            return (ResultDto<T>)_ResultDto;
        }
        public async Task<ResultDto<T>> GetBookById(string Id) 
        {
            _ResultDto.Code = (int)CodesResponse.OK;
            try
            {
                if(string.IsNullOrEmpty(Id) || !int.TryParse(Id, out int bookId))
                {
                    _ResultDto.Code = (int)CodesResponse.BadRequest;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} Invalid book ID provided";
                    return (ResultDto<T>)_ResultDto;
                }

                var book = await _repository.GetByIdAsync(bookId);

                if (book == null)
                {
                    _ResultDto.Code = (int)CodesResponse.NotFound;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} Book not found";
                }
                else
                {
                    _ResultDto.Data = [book as T];
                    _ResultDto.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ResultDto<T>)_ResultDto;
        }
        public async Task<ResultDto<T>> GetBookByauthor(string Author) 
        {
            _ResultDto.Code = (int)CodesResponse.OK;
            if (string.IsNullOrEmpty(Author))
            {
                _ResultDto.Code = (int)CodesResponse.BadRequest;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Author name cannot be null or empty";
                return (ResultDto<T>)_ResultDto;
            }

            try
            {
                var books = await _repository.GetListAsync(b => b.Author.ToUpper() == Author.ToUpper());
                if (books == null || !books.Any())
                {
                    _ResultDto.Code = (int)CodesResponse.NotFound;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} No books found for author";
                }
                else
                {
                    _ResultDto.Data = books.OfType<T>().ToList();
                    _ResultDto.Message = Constants.MSG_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ResultDto<T>)_ResultDto;

        }
        public async Task<ResultDto<T>> GetBookByTitle(string  Title) 
        {

            _ResultDto.Code = (int)CodesResponse.OK;
            if (string.IsNullOrEmpty(Title))
            {
                _ResultDto.Code = (int)CodesResponse.BadRequest;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Title cannot be null or empty";
                return (ResultDto<T>)_ResultDto;
            }
            try
            {
                var books = await _repository.GetListAsync(b =>  b.Title.ToUpper() == Title.ToUpper());
                if (books == null || !books.Any())
                {
                    _ResultDto.Code = (int)CodesResponse.NotFound;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} No books found with the specified title";
                }
                else
                {
                    _ResultDto.Data = books.OfType<T>().ToList();
                    _ResultDto.Message = Constants.MSG_SUCCESS;
                }

            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ResultDto<T>)_ResultDto;

        }
        public async Task<ResultDto<T>> GetBooksByCategory(string Category) 
        {

            _ResultDto.Code = (int)CodesResponse.OK;
            if (string.IsNullOrEmpty(Category))
            {
                _ResultDto.Code = (int)CodesResponse.BadRequest;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} Category cannot be null or empty";
                return (ResultDto<T>)_ResultDto;
            }
            try
            {
                var books = await _repository.GetListAsync(b => b.Category.ToUpper() == Category.ToUpper());
                if (books == null || !books.Any())
                {
                    _ResultDto.Code = (int)CodesResponse.NotFound;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} No books found in the specified category";
                }
                else
                {
                    _ResultDto.Data = books.OfType<T>().ToList();
                    _ResultDto.Message = Constants.MSG_SUCCESS;
                }

            }
            catch (Exception ex)
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} An error occurred while retrieving books";

                Console.WriteLine($"An error occurred while retrieving books: {ex.Message}");
            }

            return (ResultDto<T>)_ResultDto;

        }
    }
}
