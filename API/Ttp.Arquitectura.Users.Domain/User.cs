namespace Ttp.Arquitectura.Users.Domain
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
    }
}
