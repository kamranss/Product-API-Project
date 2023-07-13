using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public string? OTP { get; set; }
        public string? ConnectionId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime BirthDay { get; set; }
    }


}
