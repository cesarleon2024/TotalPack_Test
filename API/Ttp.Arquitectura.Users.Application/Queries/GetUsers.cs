using Mapster;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;

namespace Ttp.Arquitectura.Users.Application.Queries
{
    public class GetUsersQuery
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public string MainAddress { get; set; }
    }

    public class GetUsersHandler(IGenericRepository<User> user)
    {
        private IGenericRepository<User> _user { get; } = user;

        public List<GetUsersQuery> Handle()
        {
            return _user.Get().ToList().Adapt<List<GetUsersQuery>>();
        }
    }
}