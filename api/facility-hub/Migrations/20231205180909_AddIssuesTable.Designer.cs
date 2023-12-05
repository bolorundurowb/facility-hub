﻿// <auto-generated />
using System;
using FacilityHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace FacilityHub.Migrations
{
    [DbContext(typeof(FacilityHubDbContext))]
    [Migration("20231205180909_AddIssuesTable")]
    partial class AddIssuesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("char(40)")
                        .HasDefaultValueSql("(UUID())");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Point>("Location")
                        .HasColumnType("point");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("char(40)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("char(40)")
                        .HasDefaultValueSql("(UUID())");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("char(40)");

                    b.Property<DateTimeOffset>("FiledAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FiledById")
                        .HasColumnType("char(40)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("FiledById");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("char(40)")
                        .HasDefaultValueSql("(UUID())");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("char(40)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("JoinedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

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
