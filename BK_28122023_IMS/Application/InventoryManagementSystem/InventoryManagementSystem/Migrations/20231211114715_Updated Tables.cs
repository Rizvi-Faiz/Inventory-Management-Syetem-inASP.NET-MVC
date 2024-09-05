using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetName",
                table: "AssetDeployement");

            migrationBuilder.DropColumn(
                name: "AssetName",
                table: "AssetAquisation");

            migrationBuilder.RenameColumn(
                name: "AssetName",
                table: "AssetProcurement",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "VendorDescription",
                table: "VendorMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AssetMasterId",
                table: "AssetProcurement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeliveryDate",
                table: "AssetProcurement",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AssetProcurement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AssetModel",
                table: "AssetMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AssetMasterId",
                table: "AssetDeployement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AssetMasterId",
                table: "AssetAquisation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssetProcurement_AssetMasterId",
                table: "AssetProcurement",
                column: "AssetMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDeployement_AssetMasterId",
                table: "AssetDeployement",
                column: "AssetMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAquisation_AssetMasterId",
                table: "AssetAquisation",
                column: "AssetMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetAquisation_AssetMaster_AssetMasterId",
                table: "AssetAquisation",
                column: "AssetMasterId",
                principalTable: "AssetMaster",
                principalColumn: "AMid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDeployement_AssetMaster_AssetMasterId",
                table: "AssetDeployement",
                column: "AssetMasterId",
                principalTable: "AssetMaster",
                principalColumn: "AMid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetProcurement_AssetMaster_AssetMasterId",
                table: "AssetProcurement",
                column: "AssetMasterId",
                principalTable: "AssetMaster",
                principalColumn: "AMid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetAquisation_AssetMaster_AssetMasterId",
                table: "AssetAquisation");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetDeployement_AssetMaster_AssetMasterId",
                table: "AssetDeployement");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetProcurement_AssetMaster_AssetMasterId",
                table: "AssetProcurement");

            migrationBuilder.DropIndex(
                name: "IX_AssetProcurement_AssetMasterId",
                table: "AssetProcurement");

            migrationBuilder.DropIndex(
                name: "IX_AssetDeployement_AssetMasterId",
                table: "AssetDeployement");

            migrationBuilder.DropIndex(
                name: "IX_AssetAquisation_AssetMasterId",
                table: "AssetAquisation");

            migrationBuilder.DropColumn(
                name: "VendorDescription",
                table: "VendorMaster");

            migrationBuilder.DropColumn(
                name: "AssetMasterId",
                table: "AssetProcurement");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "AssetProcurement");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AssetProcurement");

            migrationBuilder.DropColumn(
                name: "AssetModel",
                table: "AssetMaster");

            migrationBuilder.DropColumn(
                name: "AssetMasterId",
                table: "AssetDeployement");

            migrationBuilder.DropColumn(
                name: "AssetMasterId",
                table: "AssetAquisation");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AssetProcurement",
                newName: "AssetName");

            migrationBuilder.AddColumn<string>(
                name: "AssetName",
                table: "AssetDeployement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssetName",
                table: "AssetAquisation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
