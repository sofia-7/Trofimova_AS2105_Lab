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
    [Migration("20241123150114_Initial1")]
    partial class Initial1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MvcFlowers.Models.Bouqet", b =>
                {
                    b.Property<int>("BouqetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BouqetId"));

                    b.Property<string>("SelectedFlowerIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BouqetId");

                    b.ToTable("Bouqet");
                });

            modelBuilder.Entity("MvcFlowers.Models.Flower", b =>
                {
                    b.Property<int>("FlowerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlowerId"));

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FlowerId");

                    b.ToTable("Flowers");
                });

            modelBuilder.Entity("MvcFlowers.Models.FlowerInBouqet", b =>
                {
                    b.Property<int>("FlowerId")
                        .HasColumnType("int");

                    b.Property<int?>("BouqetId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("FlowerId");

                    b.HasIndex("BouqetId");

                    b.ToTable("FlowerInBouqets");
                });

            modelBuilder.Entity("MvcFlowers.Models.Pack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("FlowerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RecievementDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FlowerId");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("BouqetId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MvcFlowers.Models.FlowerInBouqet", b =>
                {
                    b.HasOne("MvcFlowers.Models.Bouqet", null)
                        .WithMany("Flowers")
                        .HasForeignKey("BouqetId");

                    b.HasOne("MvcFlowers.Models.Flower", "Flower")
                        .WithMany()
                        .HasForeignKey("FlowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flower");
                });

            modelBuilder.Entity("MvcFlowers.Models.Pack", b =>
                {
                    b.HasOne("MvcFlowers.Models.Flower", "Flower")
                        .WithMany("Packs")
                        .HasForeignKey("FlowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flower");
                });

            modelBuilder.Entity("MvcFlowers.Models.Bouqet", b =>
                {
                    b.Navigation("Flowers");
                });

            modelBuilder.Entity("MvcFlowers.Models.Flower", b =>
                {
                    b.Navigation("Packs");
                });
#pragma warning restore 612, 618
        }
    }
}
