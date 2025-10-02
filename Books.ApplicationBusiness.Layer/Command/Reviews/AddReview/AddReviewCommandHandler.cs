using MediatR;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Application.Layer.Command.Reviews.AddReview
{
    public class AddReviewCommand : IRequest<ResultDto<ReviewEntity>>
    {
        public ReviewEntity Review { get; set; }
        public AddReviewCommand(ReviewEntity review) { Review = review; }
    }

    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, ResultDto<ReviewEntity>>
    {
        private readonly ReviewServices<ReviewEntity> _reviewServices;
        public AddReviewCommandHandler(ReviewServices<ReviewEntity> reviewServices)
        {
            _reviewServices = reviewServices;
        }
        public async Task<ResultDto<ReviewEntity>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            return await _reviewServices.AddReviewAsync(request.Review);
        }
    }
}
