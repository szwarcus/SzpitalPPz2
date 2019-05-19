namespace Hospital.Service.OutDTOs.Prescriptions
{
    using System;
    using System.Collections.Generic;

    public class PrescriptionOutDTO
    {
        public string DoctorName { get; set; }
       
        public DateTime DueDate { get; set; }

        public string Comments { get; set; }
    }
}