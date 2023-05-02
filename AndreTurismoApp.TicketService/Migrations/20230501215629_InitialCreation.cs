using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndreTurismoApp.TicketService.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
                //name: "City",
                //columns: table => new
                //{
                //    Id = table.Column<int>(type: "int", nullable: false)
                //        .Annotation("SqlServer:Identity", "1, 1"),
                //    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                //    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_City", x => x.Id);
                //});

            //migrationBuilder.CreateTable(
            //    name: "Address",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Number = table.Column<int>(type: "int", nullable: false),
            //        Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Complement = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CityId = table.Column<int>(type: "int", nullable: false),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Address", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Address_City_CityId",
            //            column: x => x.CityId,
            //            principalTable: "City",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Client",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AddressId = table.Column<int>(type: "int", nullable: false),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Client", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Client_Address_AddressId",
            //            column: x => x.AddressId,
            //            principalTable: "Address",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Checkin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Address_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Address_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Address_CityId",
            //    table: "Address",
            //    column: "CityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Client_AddressId",
            //    table: "Client",
            //    column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ClientId",
                table: "Ticket",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DestinationId",
                table: "Ticket",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OriginId",
                table: "Ticket",
                column: "OriginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
