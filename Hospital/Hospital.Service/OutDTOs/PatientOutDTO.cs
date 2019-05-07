using System;

namespace Hospital.Service.OutDTOs
{
    public class PatientOutDTO
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
    }
}