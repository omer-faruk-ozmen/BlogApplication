using BlogApplication.Api.Application.Features.Commands.User.ConfirmEmail;
using BlogApplication.Common.Events.User;
using BlogApplication.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommandRequest command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] CreateUserCommandRequest command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var guid = await _mediator.Send(new ConfirmEmailCommandRequest() { ConfirmationId = id });
            return Ok(guid);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordChangedCommandRequest command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

    }
}
