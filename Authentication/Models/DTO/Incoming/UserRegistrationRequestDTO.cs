using System;

namespace Authentication.Models.DTO.Incoming
{
    public class UserRegistrationRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}