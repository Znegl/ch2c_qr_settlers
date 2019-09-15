﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test2.Models;

namespace Test2.Migrations
{
    [DbContext(typeof(Ch2CContext))]
    [Migration("20190907050950_addedactioncounter")]
    partial class addedactioncounter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Test2.Models.LogLine", b =>
                {
                    b.Property<int>("LogLineID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LogTime");

                    b.Property<string>("Message");

                    b.HasKey("LogLineID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Test2.Models.ResourceRead", b =>
                {
                    b.Property<int>("ResourceReadID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ResourceUUID");

                    b.Property<int>("TeamID");

                    b.Property<DateTime>("dateTime");

                    b.HasKey("ResourceReadID");

                    b.ToTable("ResourceReads");
                });

            modelBuilder.Entity("Test2.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cloth");

                    b.Property<int>("Metal");

                    b.Property<int>("NumberOfActions");

                    b.Property<int>("Plastic");

                    b.Property<DateTime>("StarTtime");

                    b.Property<string>("TeamName");

                    b.Property<int>("Wood");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
