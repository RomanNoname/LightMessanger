﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightMessanger.DAL.Migrations
{
    public partial class AddedPropCreatedToGroupMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "GroupMessages",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "GroupMessages");
        }
    }
}
