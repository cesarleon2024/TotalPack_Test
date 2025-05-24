using Mapster;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class DeleteUserHandler(IGenericRepository<User> userRepository)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;

        public bool Handle(Guid userId)
        {
            var user = _userRepository.Get().FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            _userRepository.Delete(user);
            return true;
        }
    }
}