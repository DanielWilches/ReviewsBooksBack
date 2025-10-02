using MediatR;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;

namespace Books.Application.Layer.Querys.Books.GetBookByAuthor
{
    public class GetBookByAuthorQuery : IRequest<ResultDto<BookDto>> 
    {
        public GetBookByAuthorQuery(string author)
        {
            Author = author;
        }

        public string? Author { get; set; }

    }

    public class GetBookByAuthorQueryHandler : IRequestHandler<GetBookByAuthorQuery, ResultDto<BookDto>>
    {
        private readonly BookServices<BookEntity> _bookService;
        public GetBookByAuthorQueryHandler(BookServices<BookEntity> bookService)
            => _bookService = bookService;

        public async Task<ResultDto<BookDto>> Handle(GetBookByAuthorQuery request, CancellationToken cancellationToken)
        {
            var entityResult = await _bookService.GetBookByauthor(request?.Author ?? string.Empty);
            var dtoList = entityResult.Data?.Select(e => new BookDto
            {
                Id = e.Id,
                Title = e.Title,
                Author = e.Author,
                Category = e.Category,
                Summary = e.Summary,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate
            }).ToList();

            
            return new ResultDto<BookDto>
            {
                Code = entityResult.Code,
                Message = entityResult.Message,
                IsSuccess = entityResult.IsSuccess,
                Data = dtoList,
                Token = entityResult.Token
            };
        }
    }
}
