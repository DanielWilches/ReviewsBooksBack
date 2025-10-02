using MediatR;
using Books.Application.Layer.DTOs;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Application.Layer.Command.Reviews.UpdateReview
{
    public class UpdateReviewCommand : IRequest<ResultDto<ReviewEntity>>
    {
        public ReviewEntity Review { get; set; }
        public UpdateReviewCommand(ReviewEntity review) { Review = review; }

    }

    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ResultDto<ReviewEntity>>
    {
        private readonly ReviewServices<ReviewEntity> _reviewServices;
        public UpdateReviewCommandHandler(ReviewServices<ReviewEntity> reviewServices)
        {
            _reviewServices = reviewServices;
        }
        public async Task<ResultDto<ReviewEntity>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            return await _reviewServices.UpdateReviewAsync(request.Review);
        }
    }
}
