using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using Books.Domain.Layer.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Books.Application.Layer.Querys.Books.GetBookByTitle
{
    public class GetBookByTitleQuery(string title) : IRequest<IResultDto<BookDto>>
    {
        public string Title { get; set; } = title;
    }

    public class GetBookByTitleQueryHandler : IRequestHandler<GetBookByTitleQuery, IResultDto<BookDto>>
    {
        private readonly BookServices<BookEntity> _bookServices;
        public GetBookByTitleQueryHandler(BookServices<BookEntity> bookServices)
        {
            _bookServices = bookServices;
        }
        public async Task<IResultDto<BookDto>> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookServices.GetBookByTitle(request.Title);
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
