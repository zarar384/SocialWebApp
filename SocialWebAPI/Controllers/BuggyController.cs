using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialWebAPI.Db;

namespace SocialWebAPI.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly AppDbContext _context;

        public BuggyController(AppDbContext context)
        {
            this._context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }
    }
}
