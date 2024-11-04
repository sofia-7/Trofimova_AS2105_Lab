﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcFlowers.Data;

#nullable disable

namespace MvcFlowers.Migrations
{
    [DbContext(typeof(MvcFlowersContext))]
    [Migration("20241104140554_CreateBouqetFlowersTable")]
    partial class CreateBouqetFlowersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BouqetMonoFlowers", b =>
                {
                    b.Property<int>("BouqetId")
                        .HasColumnType("int");

                    b.Property<int>("FlowersId")
                        .HasColumnType("int");

                    b.HasKey("BouqetId", "FlowersId");

                    b.HasIndex("FlowersId");

                    b.ToTable("BouqetFlowers", (string)null);
                });

            modelBuilder.Entity("MvcFlowers.Models.Bouqet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SelectedFlowerIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bouqet");
                });

            modelBuilder.Entity("MvcFlowers.Models.Bouqets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationtDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Flower_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Bouqets");
                });

            modelBuilder.Entity("MvcFlowers.Models.MonoFlowers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RecievementDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MonoFlowers");
                });

            modelBuilder.Entity("MvcFlowers.Models.PottedFlowers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Lightning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RecievementDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Temperature")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("PottedFlowers");
                });

            modelBuilder.Entity("BouqetMonoFlowers", b =>
                {
                    b.HasOne("MvcFlowers.Models.Bouqet", null)
                        .WithMany()
                        .HasForeignKey("BouqetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcFlowers.Models.MonoFlowers", null)
                        .WithMany()
                        .HasForeignKey("FlowersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
