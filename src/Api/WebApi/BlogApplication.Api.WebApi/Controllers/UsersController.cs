using BlogApplication.Api.Application.Features.Commands.User.ConfirmEmail;
using BlogApplication.Api.Application.Features.Queries.GetUserDetail;
using BlogApplication.Common.Events.User;
using BlogApplication.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserDetailQueryRequest(id));

            return Ok(user);
        }

        [HttpGet]
        [Route("UserName/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _mediator.Send(new GetUserDetailQueryRequest(Guid.Empty, userName));

            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserCommandRequest command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var guid = await _mediator.Send(new ConfirmEmailCommandRequest() { ConfirmationId = id });
            return Ok(guid);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordChangedCommandRequest command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;

            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

    }
}
