using Mapster;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class AddUserCommand
    {
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
    }

    public class AddUserHandler(IGenericRepository<User> user)
    {
        private IGenericRepository<User> _user { get; } = user;

        public void Handle(AddUserCommand command)
        {
            _user.Insert(command.Adapt<User>());
            _user.Save();
        }
    }
}