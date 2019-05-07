using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.OutDTOs
{
    public class VisitDetailsOutDTO
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string VisitDescription { get; set; }
        public string Dosage { get; set; }
        public List<string> Medicaments { get; set; }
    }
}
