﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vetappback.Entities;

namespace Net.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210823192157_roles")]
    partial class roles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("vetappback.Entities.Agenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PetId");

                    b.ToTable("Agendas");
                });

            modelBuilder.Entity("vetappback.Entities.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServiceTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("vetappback.Entities.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("vetappback.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("vetappback.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Born")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int?>("PetTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Race")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PetTypeId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("vetappback.Entities.PetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PetTypes");
                });

            modelBuilder.Entity("vetappback.Entities.ServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("vetappback.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("vetappback.Entities.Agenda", b =>
                {
                    b.HasOne("vetappback.Entities.Owner", "Owner")
                        .WithMany("Agendas")
                        .HasForeignKey("OwnerId");

                    b.HasOne("vetappback.Entities.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId");

                    b.Navigation("Owner");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("vetappback.Entities.History", b =>
                {
                    b.HasOne("vetappback.Entities.Pet", "Pet")
                        .WithMany("Histories")
                        .HasForeignKey("PetId");

                    b.HasOne("vetappback.Entities.ServiceType", "ServiceType")
                        .WithMany()
                        .HasForeignKey("ServiceTypeId");

                    b.Navigation("Pet");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("vetappback.Entities.Manager", b =>
                {
                    b.HasOne("vetappback.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("vetappback.Entities.Owner", b =>
                {
                    b.HasOne("vetappback.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("vetappback.Entities.Pet", b =>
                {
                    b.HasOne("vetappback.Entities.Owner", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId");

                    b.HasOne("vetappback.Entities.PetType", "PetType")
                        .WithMany()
                        .HasForeignKey("PetTypeId");

                    b.Navigation("Owner");

                    b.Navigation("PetType");
                });

            modelBuilder.Entity("vetappback.Entities.Owner", b =>
                {
                    b.Navigation("Agendas");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("vetappback.Entities.Pet", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
