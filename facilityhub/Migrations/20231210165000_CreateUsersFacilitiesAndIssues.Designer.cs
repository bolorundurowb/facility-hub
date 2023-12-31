﻿// <auto-generated />
using System;
using FacilityHub;
using FacilityHub.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FacilityHub.Migrations
{
    [DbContext(typeof(FacilityHubDbContext))]
    [Migration("20231210165000_CreateUsersFacilitiesAndIssues")]
    partial class CreateUsersFacilitiesAndIssues
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Point>("Location")
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("FiledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FiledById")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("FiledById");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("JoinedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Issue", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Facility", null)
                        .WithMany("Issues")
                        .HasForeignKey("FacilityId");

                    b.HasOne("FacilityHub.Models.Data.User", "FiledBy")
                        .WithMany()
                        .HasForeignKey("FiledById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FiledBy");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.User", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Facility", null)
                        .WithMany("Managers")
                        .HasForeignKey("FacilityId");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.Navigation("Issues");

                    b.Navigation("Managers");
                });
#pragma warning restore 612, 618
        }
    }
}
