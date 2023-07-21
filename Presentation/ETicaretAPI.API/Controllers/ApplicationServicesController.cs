using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Authorize Definition EndPoints", Menu = "Application Services")]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var datas = _applicationService.GetAuthorizeDefinitionEndPoints(typeof(Program));
            return Ok(datas);
        }
    }
}
