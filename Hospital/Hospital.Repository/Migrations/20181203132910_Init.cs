using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false),
                    SystemRoleName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    NIP = table.Column<string>(maxLength: 30, nullable: true),
                    PESEL = table.Column<string>(maxLength: 20, nullable: true),
                    Street = table.Column<string>(maxLength: 30, nullable: true),
                    Province = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Harmonograms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    MondayStart = table.Column<TimeSpan>(nullable: false),
                    MondayEnd = table.Column<TimeSpan>(nullable: false),
                    TuesdayStart = table.Column<TimeSpan>(nullable: false),
                    TuesdayEnd = table.Column<TimeSpan>(nullable: false),
                    WednesdayStart = table.Column<TimeSpan>(nullable: false),
                    WednesdayEnd = table.Column<TimeSpan>(nullable: false),
                    ThursdayStart = table.Column<TimeSpan>(nullable: false),
                    ThursdayEnd = table.Column<TimeSpan>(nullable: false),
                    FridayStart = table.Column<TimeSpan>(nullable: false),
                    FridayEnd = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harmonograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SpecializationId = table.Column<long>(nullable: false),
                    HarmonogramId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Harmonograms_HarmonogramId",
                        column: x => x.HarmonogramId,
                        principalTable: "Harmonograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    DoctorId = table.Column<long>(nullable: false),
                    PatientId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Created", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastLoginTime", "LastName", "LockoutEnabled", "LockoutEnd", "NIP", "NormalizedEmail", "NormalizedUserName", "PESEL", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "Province", "SecurityStamp", "Street", "SystemRoleName", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9f2cfdcb-9f4d-4d65-a81c-43deacf27741", 0, "Toruń", "c0bd7e85-027b-42cf-9703-ac059423921e", new DateTime(2018, 12, 3, 13, 29, 10, 149, DateTimeKind.Utc), new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), "doctor@test.com", false, "Marian", 0, false, new DateTime(2018, 12, 3, 13, 29, 10, 149, DateTimeKind.Utc), "Nowak", false, null, null, null, null, "11111111111", "AQAAAAEAACcQAAAAECKLtts8yfs643jZ79ss7Oj7shA9VVpWxwCwDN361Rn93O6aHWvMzquScKdHxFdLQQ==", "123456789", false, "87-100", "Kujawsko Pomorskie", null, "Szeroka 10", "Doctor", false, "doctor@test.com" },
                    { "9f2cfdcb-9f4d-4d65-a81c-43deacf27742", 0, "Toruń", "9abf304a-db42-4fe3-9179-bbdc94e2b649", new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), "patient@test.com", false, "Piotr", 0, false, new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), "Kiepski", false, null, null, null, null, "11111111112", "AQAAAAEAACcQAAAAECKLtts8yfs643jZ79ss7Oj7shA9VVpWxwCwDN361Rn93O6aHWvMzquScKdHxFdLQQ==", "123456780", false, "87-100", "Kujawsko Pomorskie", null, "Szeroka 10", "Patient", false, "patient@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Harmonograms",
                columns: new[] { "Id", "Created", "FridayEnd", "FridayStart", "MondayEnd", "MondayStart", "ThursdayEnd", "ThursdayStart", "TuesdayEnd", "TuesdayStart", "WednesdayEnd", "WednesdayStart" },
                values: new object[] { 1L, new DateTime(2018, 12, 3, 13, 29, 10, 155, DateTimeKind.Utc), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[] { 1L, new DateTime(2018, 12, 3, 13, 29, 10, 157, DateTimeKind.Utc), "Dentysta" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Created", "HarmonogramId", "SpecializationId", "UserId" },
                values: new object[] { 1L, new DateTime(2018, 12, 3, 13, 29, 10, 154, DateTimeKind.Utc), 1L, 1L, "9f2cfdcb-9f4d-4d65-a81c-43deacf27741" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Created", "UserId" },
                values: new object[] { 1L, new DateTime(2018, 12, 3, 13, 29, 10, 156, DateTimeKind.Utc), "9f2cfdcb-9f4d-4d65-a81c-43deacf27742" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HarmonogramId",
                table: "Doctors",
                column: "HarmonogramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorId",
                table: "Visits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientId",
                table: "Visits",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Harmonograms");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
