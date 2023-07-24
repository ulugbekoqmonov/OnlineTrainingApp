using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected IMediator _mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
