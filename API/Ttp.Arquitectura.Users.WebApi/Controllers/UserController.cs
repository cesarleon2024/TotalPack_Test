using Mapster;
using Microsoft.AspNetCore.Mvc;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Queries;
using Ttp.Arquitectura.Users.WebApi.Models.Request;
using Ttp.Arquitectura.Users.WebApi.Models.Response;

namespace Ttp.Arquitectura.Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(AddUserHandler addUserHandler, GetUsersHandler getUsersHandler) : ControllerBase
    {
        private readonly AddUserHandler _addUserHandler = addUserHandler;
        private readonly GetUsersHandler _getUsersHandler = getUsersHandler;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_getUsersHandler.Handle().Adapt<List<GetUserResponse>>());
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUserRequest request)
        {
            _addUserHandler.Handle(request.Adapt<AddUserCommand>());
            return Ok();
        }
    }
}
