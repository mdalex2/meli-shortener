﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using meli.Models;

namespace meli.Migrations
{
    [DbContext(typeof(ConnectionDbContextClass))]
    [Migration("20210425231419_AddUrlClassToDatabase")]
    partial class AddUrlClassToDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("meli.Models.UrlClass", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Habilitado")
                        .HasColumnType("bit");

                    b.Property<int>("NumVisitas")
                        .HasColumnType("int");

                    b.Property<string>("UrlCorta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlLarga")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UrlConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}
