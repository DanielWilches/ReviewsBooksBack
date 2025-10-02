using MediatR;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;

namespace Books.Application.Layer.Querys.Books.GetBookById
{

    public class GetBookByIdQuery : IRequest<ResultDto<BookDto>>
    {
        public GetBookByIdQuery(string id)
        {
            Id = id;
        }

        public string? Id { get; set; }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ResultDto<BookDto>>
    {
        private readonly BookServices<BookEntity> _bookServices;

        public GetBookByIdQueryHandler(BookServices<BookEntity> bookServices)
        {
            _bookServices = bookServices;
        }

        public async Task<ResultDto<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookServices.GetBookById(request?.Id ?? "0");
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
