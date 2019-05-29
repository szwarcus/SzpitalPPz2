﻿// <auto-generated />
using System;
using Hospital.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hospital.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190529172510_referral")]
    partial class referral
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hospital.Model.Entities.Admin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Doctor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long>("HarmonogramId");

                    b.Property<long>("SpecializationId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("HarmonogramId")
                        .IsUnique();

                    b.HasIndex("SpecializationId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Harmonogram", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<TimeSpan>("FridayEnd");

                    b.Property<TimeSpan>("FridayStart");

                    b.Property<TimeSpan>("MondayEnd");

                    b.Property<TimeSpan>("MondayStart");

                    b.Property<TimeSpan>("ThursdayEnd");

                    b.Property<TimeSpan>("ThursdayStart");

                    b.Property<TimeSpan>("TuesdayEnd");

                    b.Property<TimeSpan>("TuesdayStart");

                    b.Property<TimeSpan>("WednesdayEnd");

                    b.Property<TimeSpan>("WednesdayStart");

                    b.HasKey("Id");

                    b.ToTable("Harmonograms");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Medicament", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Nurse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long>("NurseSpecializationId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("NurseSpecializationId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("Hospital.Model.Entities.NurseSpecialization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NurseSpecializations");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Prescription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DueDate");

                    b.Property<long>("VisitId");

                    b.HasKey("Id");

                    b.HasIndex("VisitId")
                        .IsUnique();

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Hospital.Model.Entities.PrescriptionMedicament", b =>
                {
                    b.Property<long>("PrescriptionId");

                    b.Property<long>("MedicamentId");

                    b.HasKey("PrescriptionId", "MedicamentId");

                    b.HasIndex("MedicamentId");

                    b.ToTable("PrescriptionMedicaments");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Referral", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<long>("SpecializationId");

                    b.Property<long>("VisitId");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("VisitId")
                        .IsUnique();

                    b.ToTable("Referrals");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Specialization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Vaccine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Dosage");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("Hospital.Model.Entities.VaccineApplied", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateTime");

                    b.Property<long>("NurseId");

                    b.Property<long>("PatientId");

                    b.Property<long>("VaccineId");

                    b.HasKey("Id");

                    b.HasIndex("NurseId");

                    b.HasIndex("PatientId");

                    b.HasIndex("VaccineId");

                    b.ToTable("VaccineApplieds");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Visit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<long>("DoctorId");

                    b.Property<int>("NumberInDay");

                    b.Property<long>("PatientId");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Hospital.Model.Identity.ApplicationIdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Hospital.Model.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<int>("Gender");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NIP")
                        .HasMaxLength(30);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PESEL")
                        .HasMaxLength(20);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10);

                    b.Property<string>("Province")
                        .HasMaxLength(30);

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Street")
                        .HasMaxLength(30);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Admin", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationUser", "User")
                        .WithOne("Admin")
                        .HasForeignKey("Hospital.Model.Entities.Admin", "UserId");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Doctor", b =>
                {
                    b.HasOne("Hospital.Model.Entities.Harmonogram", "Harmonogram")
                        .WithOne("Doctor")
                        .HasForeignKey("Hospital.Model.Entities.Doctor", "HarmonogramId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Entities.Specialization", "Specialization")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Identity.ApplicationUser", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("Hospital.Model.Entities.Doctor", "UserId");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Nurse", b =>
                {
                    b.HasOne("Hospital.Model.Entities.NurseSpecialization", "NurseSpecialization")
                        .WithMany("Nurses")
                        .HasForeignKey("NurseSpecializationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Identity.ApplicationUser", "User")
                        .WithOne("Nurse")
                        .HasForeignKey("Hospital.Model.Entities.Nurse", "UserId");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Patient", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationUser", "User")
                        .WithOne("Patient")
                        .HasForeignKey("Hospital.Model.Entities.Patient", "UserId");
                });

            modelBuilder.Entity("Hospital.Model.Entities.Prescription", b =>
                {
                    b.HasOne("Hospital.Model.Entities.Visit", "Visit")
                        .WithOne("Prescription")
                        .HasForeignKey("Hospital.Model.Entities.Prescription", "VisitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hospital.Model.Entities.PrescriptionMedicament", b =>
                {
                    b.HasOne("Hospital.Model.Entities.Medicament", "Medicament")
                        .WithMany("PrescriptionMedicaments")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Entities.Prescription", "Prescription")
                        .WithMany("PrescriptionMedicaments")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hospital.Model.Entities.Referral", b =>
                {
                    b.HasOne("Hospital.Model.Entities.Specialization", "Specialization")
                        .WithMany("Referrals")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Entities.Visit", "Visit")
                        .WithOne("Referral")
                        .HasForeignKey("Hospital.Model.Entities.Referral", "VisitId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Hospital.Model.Entities.VaccineApplied", b =>
                {
                    b.HasOne("Hospital.Model.Entities.Nurse", "Nurse")
                        .WithMany("VaccineApplieds")
                        .HasForeignKey("NurseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Entities.Patient", "Patient")
                        .WithMany("VaccineApplieds")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Entities.Vaccine", "Vaccine")
                        .WithMany("VaccineApplieds")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hospital.Model.Entities.Visit", b =>
                {
                    b.HasOne("Hospital.Model.Entities.Doctor", "Doctor")
                        .WithMany("Visits")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Entities.Patient", "Patient")
                        .WithMany("Visits")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationIdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationIdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hospital.Model.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Hospital.Model.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
