namespace Ttp.Arquitectura.Users.WebApi.Models.Response
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public string MainAddress { get; set; }
    }
}
