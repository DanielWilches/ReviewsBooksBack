using MediatR;
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using Books.Application.Layer.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Application.Layer.Command.Users.CreateUser
{
    public class CreateUserCommand : IRequest<ResultDto<CustomUserProfile>>
    {
        public RegisterDto Register { get; set; }
        public CreateUserCommand(RegisterDto Register) { this.Register = Register; }
        public CreateUserCommand() { }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultDto<CustomUserProfile>>
    {
        private readonly UserServices<CustomUserProfile> _userService;
        public CreateUserCommandHandler(UserServices<CustomUserProfile> userService)
        {
            _userService = userService;
        }
        public async Task<ResultDto<CustomUserProfile>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUserAsync(request.Register);
        }
    }
}
