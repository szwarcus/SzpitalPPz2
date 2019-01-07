using System;

namespace Hospital.Service.OutDTOs
{
    public class ApplicationUserDTO
    {
        public Guid _userID { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }

        public ApplicationUserDTO(Guid UserID, string firstName, string lastName)
        {
            _userID = UserID;
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}