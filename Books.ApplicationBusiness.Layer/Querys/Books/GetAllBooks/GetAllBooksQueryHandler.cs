using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using MediatR;

namespace Books.Application.Layer.Querys.Books.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<ResultDto<BookDto>> { }
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ResultDto<BookDto>>
    {
        private readonly BookServices<BookEntity> _bookServices;

        public GetAllBooksQueryHandler(BookServices<BookEntity> bookServices)
            => _bookServices = bookServices;

        public async Task<ResultDto<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookServices.GetAllBooks();
            
            var data = result.Data?.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Category = b.Category,
                Summary = b.Summary,
                CreatedDate = b.CreatedDate,
                ModifiedDate = b.ModifiedDate
            }).ToList();

            return new ResultDto<BookDto>
            {
                Code = result.Code,
                Message = result.Message,
                IsSuccess = result.IsSuccess,
                Data = data,
                Token = result.Token
            };
        }
    }
}
