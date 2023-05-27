
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; }

 
        public List<RefreshToken>? RefreshTokens { get; set; }
    }

    
}