using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Asset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetMaster",
                columns: table => new
                {
                    AMid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetMaster", x => x.AMid);
                });

            migrationBuilder.CreateTable(
                name: "VendorMaster",
                columns: table => new
                {
                    VMid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorMaster", x => x.VMid);
                });

            migrationBuilder.CreateTable(
                name: "AssetAquisation",
                columns: table => new
                {
                    AAid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorMasterId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAquisation", x => x.AAid);
                    table.ForeignKey(
                        name: "FK_AssetAquisation_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Eid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetAquisation_VendorMaster_VendorMasterId",
                        column: x => x.VendorMasterId,
                        principalTable: "VendorMaster",
                        principalColumn: "VMid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetDeployement",
                columns: table => new
                {
                    ADid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorMasterId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDeployement", x => x.ADid);
                    table.ForeignKey(
                        name: "FK_AssetDeployement_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Depid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetDeployement_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Eid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetDeployement_VendorMaster_VendorMasterId",
                        column: x => x.VendorMasterId,
                        principalTable: "VendorMaster",
                        principalColumn: "VMid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetProcurement",
                columns: table => new
                {
                    APid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorMasterId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrder = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetProcurement", x => x.APid);
                    table.ForeignKey(
                        name: "FK_AssetProcurement_VendorMaster_VendorMasterId",
                        column: x => x.VendorMasterId,
                        principalTable: "VendorMaster",
                        principalColumn: "VMid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAquisation_EmployeeId",
                table: "AssetAquisation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAquisation_VendorMasterId",
                table: "AssetAquisation",
                column: "VendorMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDeployement_DepartmentId",
                table: "AssetDeployement",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDeployement_EmployeeId",
                table: "AssetDeployement",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDeployement_VendorMasterId",
                table: "AssetDeployement",
                column: "VendorMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetProcurement_VendorMasterId",
                table: "AssetProcurement",
                column: "VendorMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAquisation");

            migrationBuilder.DropTable(
                name: "AssetDeployement");

            migrationBuilder.DropTable(
                name: "AssetMaster");

            migrationBuilder.DropTable(
                name: "AssetProcurement");

            migrationBuilder.DropTable(
                name: "VendorMaster");
        }
    }
}
