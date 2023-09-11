using Microsoft.AspNetCore.Mvc;

namespace SocialWebAPI.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAPIController : ControllerBase
    {
    }
}
