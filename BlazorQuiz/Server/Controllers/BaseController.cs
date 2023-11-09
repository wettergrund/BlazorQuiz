using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{

    // Base controller to take care of common properties etc.
    public class BaseController : ControllerBase
    {
        protected string UserId { get => User.FindFirst(ClaimTypes.NameIdentifier)?.Value; }
    }
}
