using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Repository.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NurseSpecializations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseSpecializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Dosage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    NurseSpecializationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nurses_NurseSpecializations_NurseSpecializationId",
                        column: x => x.NurseSpecializationId,
                        principalTable: "NurseSpecializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nurses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaccineApplieds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    VaccineId = table.Column<long>(nullable: false),
                    NurseId = table.Column<long>(nullable: false),
                    PatientId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineApplieds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineApplieds_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineApplieds_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineApplieds_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27741",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "3bfb720b-5092-457b-a0f9-394be984efce", new DateTime(2018, 12, 13, 18, 25, 46, 650, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 650, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27742",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "19a525bf-8077-484c-847b-4e6b0c0a97a1", new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Created", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastLoginTime", "LastName", "LockoutEnabled", "LockoutEnd", "NIP", "NormalizedEmail", "NormalizedUserName", "PESEL", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "Province", "SecurityStamp", "Street", "SystemRoleName", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9f2cfdcb-9f4d-4d65-a81c-43deacf27743", 0, "Toruń", "e14b7980-d150-4ede-8961-bab99c13330d", new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), "nurse@test.com", false, "Katarzyna", 1, false, new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), "Boczek", false, null, null, null, null, "11111181112", "AQAAAAEAACcQAAAAECKLtts8yfs643jZ79ss7Oj7shA9VVpWxwCwDN361Rn93O6aHWvMzquScKdHxFdLQQ==", "123256780", false, "87-100", "Kujawsko Pomorskie", null, "Długa 11", "Nurse", false, "nurse@test.com" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 655, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Harmonograms",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 657, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "NurseSpecializations",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[] { 1L, new DateTime(2018, 12, 13, 18, 25, 46, 676, DateTimeKind.Utc), "Onkologiczna" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 658, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 660, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Created", "Description", "Dosage", "Name" },
                values: new object[] { 1L, new DateTime(2018, 12, 13, 18, 25, 46, 669, DateTimeKind.Utc), "Grypa", "(X ml/kg ", "VaxigripTetra" });

            migrationBuilder.InsertData(
                table: "Nurses",
                columns: new[] { "Id", "Created", "NurseSpecializationId", "UserId" },
                values: new object[] { 1L, new DateTime(2018, 12, 13, 18, 25, 46, 667, DateTimeKind.Utc), 1L, "9f2cfdcb-9f4d-4d65-a81c-43deacf27741" });

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_NurseSpecializationId",
                table: "Nurses",
                column: "NurseSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_UserId",
                table: "Nurses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineApplieds_NurseId",
                table: "VaccineApplieds",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineApplieds_PatientId",
                table: "VaccineApplieds",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineApplieds_VaccineId",
                table: "VaccineApplieds",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineApplieds");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "NurseSpecializations");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9f2cfdcb-9f4d-4d65-a81c-43deacf27743", "e14b7980-d150-4ede-8961-bab99c13330d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27741",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "c0bd7e85-027b-42cf-9703-ac059423921e", new DateTime(2018, 12, 3, 13, 29, 10, 149, DateTimeKind.Utc), new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), new DateTime(2018, 12, 3, 13, 29, 10, 149, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27742",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "9abf304a-db42-4fe3-9179-bbdc94e2b649", new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc), new DateTime(2018, 12, 3, 13, 29, 10, 150, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 3, 13, 29, 10, 154, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Harmonograms",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 3, 13, 29, 10, 155, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 3, 13, 29, 10, 156, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 3, 13, 29, 10, 157, DateTimeKind.Utc));
        }
    }
}
