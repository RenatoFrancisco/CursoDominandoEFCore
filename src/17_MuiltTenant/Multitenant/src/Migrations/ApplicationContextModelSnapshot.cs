﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Multitenant.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EFCore.Multitenant.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Multitenant.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Person 1",
                            TenantId = "Teanant 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Person 2",
                            TenantId = "Teanant 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Person 3",
                            TenantId = "Teanant 3"
                        });
                });

            modelBuilder.Entity("Multitenant.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description 1",
                            TenantId = "Teanant 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description 2",
                            TenantId = "Teanant 2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Description 3",
                            TenantId = "Teanant 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
