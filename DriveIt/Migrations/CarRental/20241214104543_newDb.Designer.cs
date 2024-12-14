﻿// <auto-generated />
using System;
using DriveIt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DriveIt.Migrations.CarRental
{
    [DbContext(typeof(CarRentalContext))]
    [Migration("20241214104543_newDb")]
    partial class newDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DriveIt.Model.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("InsurancePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IntegratorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IntegratorUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferTimeLimit")
                        .HasColumnType("int");

                    b.Property<decimal>("RentPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("DriveIt.Model.Rental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AcceptedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("DriveIt.Model.Rental", b =>
                {
                    b.OwnsOne("DriveIt.Model.Car", "Car", b1 =>
                        {
                            b1.Property<Guid>("RentalId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Brand")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Model")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Year")
                                .HasColumnType("int");

                            b1.HasKey("RentalId");

                            b1.ToTable("Rentals");

                            b1.WithOwner()
                                .HasForeignKey("RentalId");
                        });

                    b.Navigation("Car")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
