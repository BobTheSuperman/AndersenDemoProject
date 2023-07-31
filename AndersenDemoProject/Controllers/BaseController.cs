using Domain.Core.DTO.Messages;
using Microsoft.AspNetCore.Mvc;

namespace AndersenDemoProject.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult StatusCode(BaseResponse response) =>
            StatusCode((int)response.Result.Error.Key, response.Result.Error.Value);
    }
}
