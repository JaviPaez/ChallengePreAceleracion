using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.DTO.Incoming
{
    public class UserRegistrationRequestDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}