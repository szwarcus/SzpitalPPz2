namespace Hospital.Service.Abstract
{
    using System.Threading.Tasks;
    using Hospital.Service.InDTOs;
    using Hospital.Service.OutDTOs;
    using Hospital.Service.OutDTOs.Prescriptions;
    using Hospital.Service.OutDTOs.Referrals;

    public interface IPatientService
    {
        Task<bool> Register(RegisterPatientInDTO model);
        Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model);
        Task<PatientOutDTO> GetPatientById(long id);
        Task<PrescriptionsOutDTO> GetPrescriptions(string userId);
        Task<ReferralsOutDTO> GetReferrals(string userId);
    }
}