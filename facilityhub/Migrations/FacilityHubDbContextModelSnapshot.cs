﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FacilityHub.DataContext;
using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FacilityHub.Migrations
{
    [DbContext(typeof(FacilityHubDbContext))]
    partial class FacilityHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FacilityHub.Models.Data.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("FacilityId");

                    b.HasIndex("IssueId");

                    b.HasIndex("TenantId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Point>("Location")
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.FacilityInvitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid>("ClaimToken")
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FacilityId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("InvitedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("InvitedById")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsClaimed")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("InvitedById");

                    b.ToTable("FacilityInvitations");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("FiledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FiledById")
                        .HasColumnType("uuid");

                    b.Property<List<IssueLogEntry>>("Log")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("FiledById");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<List<TenancyHistory>>("History")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UserId");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTimeOffset>("JoinedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FacilityManagers", b =>
                {
                    b.Property<Guid>("ManagedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManagersId")
                        .HasColumnType("uuid");

                    b.HasKey("ManagedId", "ManagersId");

                    b.HasIndex("ManagersId");

                    b.ToTable("FacilityManagers");
                });

            modelBuilder.Entity("FacilityOwners", b =>
                {
                    b.Property<Guid>("OwnedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OwnersId")
                        .HasColumnType("uuid");

                    b.HasKey("OwnedId", "OwnersId");

                    b.HasIndex("OwnersId");

                    b.ToTable("FacilityOwners");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Document", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("FacilityHub.Models.Data.Facility", null)
                        .WithMany("Documents")
                        .HasForeignKey("FacilityId");

                    b.HasOne("FacilityHub.Models.Data.Issue", null)
                        .WithMany("Documents")
                        .HasForeignKey("IssueId");

                    b.HasOne("FacilityHub.Models.Data.Tenant", null)
                        .WithMany("Documents")
                        .HasForeignKey("TenantId");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.FacilityInvitation", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Facility", "Facility")
                        .WithMany()
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacilityHub.Models.Data.User", "InvitedBy")
                        .WithMany()
                        .HasForeignKey("InvitedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");

                    b.Navigation("InvitedBy");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Issue", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Facility", null)
                        .WithMany("Issues")
                        .HasForeignKey("FacilityId");

                    b.HasOne("FacilityHub.Models.Data.Tenant", "FiledBy")
                        .WithMany()
                        .HasForeignKey("FiledById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FiledBy");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Tenant", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacilityHub.Models.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("CreatedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FacilityManagers", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Facility", null)
                        .WithMany()
                        .HasForeignKey("ManagedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacilityHub.Models.Data.User", null)
                        .WithMany()
                        .HasForeignKey("ManagersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FacilityOwners", b =>
                {
                    b.HasOne("FacilityHub.Models.Data.Facility", null)
                        .WithMany()
                        .HasForeignKey("OwnedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FacilityHub.Models.Data.User", null)
                        .WithMany()
                        .HasForeignKey("OwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Facility", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Issues");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Issue", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("FacilityHub.Models.Data.Tenant", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
