namespace Ttp.Arquitectura.Users.WebApi.Models.Request
{
    public class AddUserRequest
    {
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
    }
}
