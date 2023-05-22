﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NightSky.API.DAL;

#nullable disable

namespace NightSky.Migrations.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230522201115_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("ConstellationStar", b =>
                {
                    b.Property<int>("ConstallationsConstallationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StarsStarId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConstallationsConstallationId", "StarsStarId");

                    b.HasIndex("StarsStarId");

                    b.ToTable("ConstellationStar");
                });

            modelBuilder.Entity("NightSky.API.DAL.Entities.Constellation", b =>
                {
                    b.Property<int>("ConstallationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConstallationId");

                    b.ToTable("Constellation", (string)null);
                });

            modelBuilder.Entity("NightSky.API.DAL.Entities.SkyData", b =>
                {
                    b.Property<int>("SkyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CloudsLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("FogLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrecipitationType")
                        .HasColumnType("INTEGER");

                    b.HasKey("SkyId");

                    b.ToTable("SkyData");
                });

            modelBuilder.Entity("NightSky.API.DAL.Entities.Star", b =>
                {
                    b.Property<int>("StarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StarName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StarId");

                    b.ToTable("Star");
                });

            modelBuilder.Entity("ConstellationStar", b =>
                {
                    b.HasOne("NightSky.API.DAL.Entities.Constellation", null)
                        .WithMany()
                        .HasForeignKey("ConstallationsConstallationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NightSky.API.DAL.Entities.Star", null)
                        .WithMany()
                        .HasForeignKey("StarsStarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
