﻿// <auto-generated />
using System;
using ForestrySystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForestrySystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230203123516_FKs")]
    partial class FKs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("ForestrySystem.Models.CategoryOfTimber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("AmountForLogging")
                        .HasColumnType("REAL");

                    b.Property<int>("CategoryName")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("YearOfLogging")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CategoryOfTimber");
                });

            modelBuilder.Entity("ForestrySystem.Models.Events", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("FIEventRefID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstitutionsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionsId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ForestrySystem.Models.ForestryInstitution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("GreenArea")
                        .HasColumnType("REAL");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("TotalArea")
                        .HasColumnType("REAL");

                    b.Property<float>("UrbanizedArea")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("ForestrySystem.Models.PurposeOfCutOff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("PercentagePerYear")
                        .HasColumnType("REAL");

                    b.Property<int>("Purpose")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PurposeOfCutOff");
                });

            modelBuilder.Entity("ForestrySystem.Models.TypeOfTimber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("AmountForLogging")
                        .HasColumnType("REAL");

                    b.Property<int>("TimberName")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("YearOfLogging")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TypeOfTimber");
                });

            modelBuilder.Entity("ForestrySystem.Models.TypeOfWood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("AmountForLogging")
                        .HasColumnType("REAL");

                    b.Property<int>("Origin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("YearOfLogging")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WoodTypes");
                });

            modelBuilder.Entity("ForestrySystem.Models.SiteViewers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FIRefID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstitutionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateLastChange")
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("SiteViewers");
                });

            modelBuilder.Entity("ForestrySystem.Models.Events", b =>
                {
                    b.HasOne("ForestrySystem.Models.ForestryInstitution", "Institutions")
                        .WithMany("Events")
                        .HasForeignKey("InstitutionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institutions");
                });

            modelBuilder.Entity("ForestrySystem.Models.SiteViewers", b =>
                {
                    b.HasOne("ForestrySystem.Models.ForestryInstitution", "Institution")
                        .WithMany("SiteViewers")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("ForestrySystem.Models.ForestryInstitution", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("SiteViewers");
                });
#pragma warning restore 612, 618
        }
    }
}
