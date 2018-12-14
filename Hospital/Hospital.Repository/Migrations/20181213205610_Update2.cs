using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Repository.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicaments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    VisitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicaments",
                columns: table => new
                {
                    PrescriptionId = table.Column<long>(nullable: false),
                    MedicamentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicaments", x => new { x.PrescriptionId, x.MedicamentId });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicaments_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicaments_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27741",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "ed79543f-6875-4bc5-9c70-e0cc07fcb76f", new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc), new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc), new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27742",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "ee04850c-4fa2-419f-a4aa-9bc4489cfd1c", new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc), new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc), new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27743",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "8906892e-779c-4720-9bc4-584e111d9e7e", new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc), new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc), new DateTime(2018, 12, 13, 20, 56, 9, 565, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 569, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Harmonograms",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 571, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "NurseSpecializations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 588, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 578, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 571, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 572, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 20, 56, 9, 579, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicaments_MedicamentId",
                table: "PrescriptionMedicaments",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_VisitId",
                table: "Prescriptions",
                column: "VisitId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionMedicaments");

            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "Prescriptions");

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f2cfdcb-9f4d-4d65-a81c-43deacf27743",
                columns: new[] { "ConcurrencyStamp", "Created", "DateOfBirth", "LastLoginTime" },
                values: new object[] { "e14b7980-d150-4ede-8961-bab99c13330d", new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc), new DateTime(2018, 12, 13, 18, 25, 46, 651, DateTimeKind.Utc) });

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

            migrationBuilder.UpdateData(
                table: "NurseSpecializations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 676, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 667, DateTimeKind.Utc));

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

            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created",
                value: new DateTime(2018, 12, 13, 18, 25, 46, 669, DateTimeKind.Utc));
        }
    }
}
