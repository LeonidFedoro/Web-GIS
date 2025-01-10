using WebApp_MVC_auth_jwt.Models;

namespace WebApp_MVC_auth_cookiee.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
