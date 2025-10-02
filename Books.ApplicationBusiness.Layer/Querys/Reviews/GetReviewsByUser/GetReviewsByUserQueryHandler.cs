using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using MediatR;

namespace Books.Application.Layer.Querys.Reviews.GetReviewsByUser
{
    public class GetReviewsByUserQuery : IRequest<ResultDto<ReviewEntity>>
    {
        public int UserId { get; set; }
        public GetReviewsByUserQuery(int userId) { UserId = userId; }
        public GetReviewsByUserQuery() { }
    }

    public class GetReviewsByUserQueryHandler : IRequestHandler<GetReviewsByUserQuery, ResultDto<ReviewEntity>>
    {
        private readonly ReviewServices<ReviewEntity> _reviewServices;
        public GetReviewsByUserQueryHandler(ReviewServices<ReviewEntity> reviewServices)
        {
            _reviewServices = reviewServices;
        }
        public async Task<ResultDto<ReviewEntity>> Handle(GetReviewsByUserQuery request, CancellationToken cancellationToken)
        {
            return await _reviewServices.GetReviewsByUser(request.UserId);
        }
    }
}
