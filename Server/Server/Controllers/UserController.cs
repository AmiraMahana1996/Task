using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Model;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBContext _context;
        public UserController(DBContext context)
        {
            _context = context;
        }

        //register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
