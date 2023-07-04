using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator mediator;
        readonly IMailService mailService;

        public UsersController(IMediator mediator, IMailService mailService)
        {
            this.mediator = mediator;
            this.mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ExampleMailTest()
        {
            await mailService.SendMailAsync("muhammedyucedagg@gmail.com", "Örnek mail", "<strong> Bu bir örnek maildir. </strong>");
            return Ok();
        }
    }
}
