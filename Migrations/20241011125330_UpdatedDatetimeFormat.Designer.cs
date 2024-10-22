﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SAP.Data;

#nullable disable

namespace SAP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241011125330_UpdatedDatetimeFormat")]
    partial class UpdatedDatetimeFormat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SAP.Models.AddCriminalRec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuedAt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IssuedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OffenceCommitted")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Sentence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CriminalRecords_Table");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IssueDate = new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IssuedAt = "Sandton",
                            IssuedBy = "Kelvin Moores",
                            OffenceCommitted = "",
                            Sentence = 3
                        });
                });

            modelBuilder.Entity("SAP.Models.ListofOffences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OffenceCode")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Offences")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Offences_Table");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OffenceCode = "A1",
                            Offences = "Assault"
                        },
                        new
                        {
                            Id = 2,
                            OffenceCode = "A2",
                            Offences = "Burglary"
                        },
                        new
                        {
                            Id = 3,
                            OffenceCode = "A3",
                            Offences = "Drugs"
                        },
                        new
                        {
                            Id = 4,
                            OffenceCode = "A4",
                            Offences = "Hijacking"
                        },
                        new
                        {
                            Id = 5,
                            OffenceCode = "A5",
                            Offences = "Murder"
                        },
                        new
                        {
                            Id = 6,
                            OffenceCode = "A6",
                            Offences = "Offences of Dishonesty"
                        },
                        new
                        {
                            Id = 7,
                            OffenceCode = "A7",
                            Offences = "Other offences"
                        },
                        new
                        {
                            Id = 8,
                            OffenceCode = "A8",
                            Offences = "Public drinking"
                        },
                        new
                        {
                            Id = 9,
                            OffenceCode = "A9",
                            Offences = "Rape"
                        },
                        new
                        {
                            Id = 10,
                            OffenceCode = "A10",
                            Offences = "Robbery"
                        },
                        new
                        {
                            Id = 11,
                            OffenceCode = "A11",
                            Offences = "Sexual Harrassment"
                        },
                        new
                        {
                            Id = 12,
                            OffenceCode = "A12",
                            Offences = "Sexual Offences"
                        },
                        new
                        {
                            Id = 13,
                            OffenceCode = "A13",
                            Offences = "Violence"
                        });
                });

            modelBuilder.Entity("SAP.Models.NewSuspect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Eye_Color")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Height")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("SuspectId")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Suspects_Table");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 0,
                            Eye_Color = "Green",
                            First_Name = "Andrew",
                            Height = 177.5m,
                            Last_Name = "Dusk",
                            Sex = "Male",
                            SuspectId = "0303016812181"
                        },
                        new
                        {
                            Id = 2,
                            Age = 0,
                            Eye_Color = "Brown",
                            First_Name = "Amanda",
                            Height = 158m,
                            Last_Name = "White",
                            Sex = "Female",
                            SuspectId = "0303011180187"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
