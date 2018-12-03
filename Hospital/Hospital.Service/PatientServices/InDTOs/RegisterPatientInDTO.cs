namespace Hospital.Service.PatientServices.InDTOs
{
    public class RegisterPatientInDTO
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SystemRole { get; set; }
        public string Password { get; set; }
    }
}