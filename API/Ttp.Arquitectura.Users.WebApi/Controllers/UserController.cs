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
    public class UserController(GetUsersHandler getUsersHandler, AddUserHandler addUserHandler, DeleteUserHandler deleteUserHandler) : ControllerBase
    {
        private readonly GetUsersHandler _getUsersHandler = getUsersHandler;
        private readonly AddUserHandler _addUserHandler = addUserHandler;
        private readonly DeleteUserHandler _deleteUserHandler = deleteUserHandler;

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

        [HttpDelete]
        public IActionResult Delete(Guid userId)
        {
            var result = _deleteUserHandler.Handle(userId);

            if (result)
                return NoContent(); // Código 204: Eliminación exitosa sin contenido

            return NotFound($"El usuario con ID {userId} no existe.");

        }
    }
}
