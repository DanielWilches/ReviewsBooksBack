using MediatR;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Application.Layer.Querys.Reviews.GetReviewsByBook
{
    public class GetReviewsByBookQuery : IRequest<ResultDto<ReviewEntity>>
    {
        public int BookId { get; set; }
        public GetReviewsByBookQuery(int bookId) { BookId = bookId; }
        public GetReviewsByBookQuery() { }
    }

    public class GetReviewsByBookQueryHandler : IRequestHandler<GetReviewsByBookQuery, ResultDto<ReviewEntity>>
    {
        private readonly ReviewServices<ReviewEntity> _reviewServices;
        public GetReviewsByBookQueryHandler(ReviewServices<ReviewEntity> reviewServices)
        {
            _reviewServices = reviewServices;
        }
        public async Task<ResultDto<ReviewEntity>> Handle(GetReviewsByBookQuery request, CancellationToken cancellationToken)
        {
            return await _reviewServices.GetReviewsByBook(request.BookId);
        }
    }
}
