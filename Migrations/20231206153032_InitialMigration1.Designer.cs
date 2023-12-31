﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Uprise.Repository.Power_Plant;

#nullable disable

namespace Uprise.Migrations
{
    [DbContext(typeof(PowerPlantDbContext))]
    [Migration("20231206153032_InitialMigration1")]
    partial class InitialMigration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("plants")
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Uprise.Repository.Power_Plant.Models.ForecastedProduction", b =>
                {
                    b.Property<double>("Power")
                        .HasColumnType("double precision")
                        .HasColumnName("power");

                    b.Property<int>("PowerPlantId")
                        .HasColumnType("integer")
                        .HasColumnName("power_plant_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("timestamp");

                    b.HasIndex("PowerPlantId");

                    b.ToTable("forecasted_productions", "plants");
                });

            modelBuilder.Entity("Uprise.Repository.Power_Plant.Models.PowerPlant", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("serial")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOfInstallation")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_installation");

                    b.Property<string>("InstalledPower")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("installed_power");

                    b.Property<float>("Latitude")
                        .HasColumnType("real")
                        .HasColumnName("latitude");

                    b.Property<float>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("power_plants", "plants");
                });

            modelBuilder.Entity("Uprise.Repository.Power_Plant.Models.RealProduction", b =>
                {
                    b.Property<double>("Power")
                        .HasColumnType("double precision")
                        .HasColumnName("power");

                    b.Property<int>("PowerPlantId")
                        .HasColumnType("integer")
                        .HasColumnName("power_plant_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("timestamp");

                    b.HasIndex("PowerPlantId");

                    b.ToTable("real_productions", "plants");
                });

            modelBuilder.Entity("Uprise.Repository.Power_Plant.Models.ForecastedProduction", b =>
                {
                    b.HasOne("Uprise.Repository.Power_Plant.Models.PowerPlant", "PowerPlant")
                        .WithMany()
                        .HasForeignKey("PowerPlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PowerPlant");
                });

            modelBuilder.Entity("Uprise.Repository.Power_Plant.Models.RealProduction", b =>
                {
                    b.HasOne("Uprise.Repository.Power_Plant.Models.PowerPlant", "PowerPlant")
                        .WithMany()
                        .HasForeignKey("PowerPlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PowerPlant");
                });
#pragma warning restore 612, 618
        }
    }
}
