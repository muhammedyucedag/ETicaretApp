using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult GetRoles(string Id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateRole()
        {
            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateRole()
        {
            return Ok();
        }
        [HttpPut("{name}")]
        public IActionResult DeleteRole()
        {
            return Ok();
        }
    }
}
