using Hospital.Model.Configurations;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repository.Concrete
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationIdentityRole, string>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Harmonogram> Harmonograms { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineApplied> VaccineApplieds{ get; set; }
        public DbSet<NurseSpecialization> NurseSpecializations{ get; set; }
        public DbSet<Prescription> Prescriptions{ get; set; }
        public DbSet<Medicament> Medicaments{ get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments{ get; set; }
        public DbSet<Referral> Referrals{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new HarmonogramConfiguration());
            builder.ApplyConfiguration(new PatientConfiguration());
            builder.ApplyConfiguration(new SpecializationConfiguration());
            builder.ApplyConfiguration(new VisitConfiguration());
            builder.ApplyConfiguration(new NurseConfigurations());
            builder.ApplyConfiguration(new VaccineConfigurations());
            builder.ApplyConfiguration(new VaccineAppliedConfigurations());
            builder.ApplyConfiguration(new NurseSpecializationConfiguration());
            builder.ApplyConfiguration(new MedicamentConfiguration());
            builder.ApplyConfiguration(new PrescriptionConfiguration());
            builder.ApplyConfiguration(new PrescriptionMedicamentConfiguration());
            builder.ApplyConfiguration(new ReferralConfiguration());

        }
    }
}