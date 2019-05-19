namespace Hospital.Service.OutDTOs.Prescriptions
{
    using System.Collections.Generic;

    public class PrescriptionsOutDTO
    {
        public List<PrescriptionOutDTO> Prescriptions { get; set; } = new List<PrescriptionOutDTO>();
    }
}