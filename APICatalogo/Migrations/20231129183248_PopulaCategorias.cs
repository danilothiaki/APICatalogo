﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categorias values('Bedidas', 'bebidas.jpg')");
            migrationBuilder.Sql("Insert into Categorias values('Lanches', 'lanches.jpg')");
            migrationBuilder.Sql("Insert into Categorias values('Sobremesas', 'sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
