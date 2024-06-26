﻿// <auto-generated />
using System;
using Clinica.Doctors.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Clinica.Doctors.Infrastructure.Migrations
{
    [DbContext(typeof(DoctorDbContext))]
    [Migration("20240608191614_InitialDoctor")]
    partial class InitialDoctor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Clinica.Doctors.Domain.Aggregates.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Address");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("BirthDate");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("CRM");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email");

                    b.Property<Guid>("IdSpecialty")
                        .HasColumnType("uuid")
                        .HasColumnName("IdSpecialty");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasColumnName("Phone");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Doctors", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
