using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksPresentation.Controllers
{
    
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public readonly ISender _sender;
        public ApiBaseController(ISender sender)
            => _sender = sender;
    }
}
