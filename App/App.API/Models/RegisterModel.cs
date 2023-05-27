using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class RegisterModel
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50),Required]
        public string Username { get; set; }

        [StringLength(128)]
        public string Email { get; set; }
        [StringLength(11)]
        public string Mobile { get; set; }

        [StringLength(256)]
        public string Password { get; set; }
    }
}