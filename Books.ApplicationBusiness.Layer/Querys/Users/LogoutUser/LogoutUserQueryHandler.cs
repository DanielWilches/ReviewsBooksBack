using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Books.Application.Layer.Services;
using Books.Domain.Layer.Entitys;

namespace Books.Application.Layer.Querys.Users.LogoutUser
{
    public class LogoutUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public LogoutUserCommand(string userName) { UserName = userName; }
        public LogoutUserCommand() { }
    }

    public class LogoutUserQueryHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly UserServices<CustomUserProfile> _userService;
        public LogoutUserQueryHandler(UserServices<CustomUserProfile> userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.LogoutAsync(request.UserName);
        }
    }
}
