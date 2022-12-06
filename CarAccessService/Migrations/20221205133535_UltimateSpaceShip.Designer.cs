﻿// <auto-generated />
using System;
using CarAccessService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarAccessService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221205133535_UltimateSpaceShip")]
    partial class UltimateSpaceShip
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CarModelService.ParkingLotModel", b =>
                {
                    b.Property<int>("SpaceParkingLotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpaceParkingLotId"), 1L, 1);

                    b.Property<int?>("SpaceShipID")
                        .HasColumnType("int");

                    b.Property<int>("Zone")
                        .HasColumnType("int");

                    b.Property<int>("parkingLotLevel")
                        .HasColumnType("int");

                    b.Property<int>("parkingLotNumber")
                        .HasColumnType("int");

                    b.HasKey("SpaceParkingLotId");

                    b.HasIndex("SpaceShipID");

                    b.ToTable("ParkingLotModels");
                });

            modelBuilder.Entity("CarModelService.SpaceShipModel", b =>
                {
                    b.Property<int?>("SpaceShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("SpaceShipID"), 1L, 1);

                    b.Property<double?>("CurrentPrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("EnterTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExitTimeEarlierTimeWatcher")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParkingLotNumber")
                        .HasColumnType("int");

                    b.Property<string>("RegisteringsNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("TotalCost")
                        .HasColumnType("float");

                    b.HasKey("SpaceShipID");

                    b.ToTable("SpaceShipModels");
                });

            modelBuilder.Entity("CarModelService.ParkingLotModel", b =>
                {
                    b.HasOne("CarModelService.SpaceShipModel", "SpaceShip")
                        .WithMany()
                        .HasForeignKey("SpaceShipID");

                    b.Navigation("SpaceShip");
                });
#pragma warning restore 612, 618
        }
    }
}