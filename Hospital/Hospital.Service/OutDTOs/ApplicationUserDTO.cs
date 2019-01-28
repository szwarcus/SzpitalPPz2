using Hospital.Core.Enums;
using System;

namespace Hospital.Service.OutDTOs
{
    public class ApplicationUserDTO
    {
        public Guid userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PESEL { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public SystemRoleType SystemRole { get; set; }
        public string Password { get; set; }
        public ApplicationUserDTO()
        {

        }
        public ApplicationUserDTO(Guid UserID, string firstName, string lastName)
        {
            userId = UserID;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}