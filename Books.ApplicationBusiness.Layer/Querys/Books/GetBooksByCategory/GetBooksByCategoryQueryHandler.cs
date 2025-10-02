using MediatR;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Books.Application.Layer.Querys.Books.GetBooksByCategory
{
    public class GetBooksByCategoryQuery(string category) : IRequest<ResultDto<BookDto>>
    {
        public string Category { get; set; } = category;
    }

    public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, ResultDto<BookDto>>
    {
        private readonly BookServices<BookEntity> _bookServices;
        public GetBooksByCategoryQueryHandler(BookServices<BookEntity> bookServices)
        {
            _bookServices = bookServices;
        }
        public async Task<ResultDto<BookDto>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookServices.GetBooksByCategory(request.Category);
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
