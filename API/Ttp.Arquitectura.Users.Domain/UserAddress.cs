namespace Ttp.Arquitectura.Users.Domain
{
    public class UserAddress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdUser { get; set; }
        public string Address { get; set; }
        public bool MainAddress { get; set; } = false;
    }
}
