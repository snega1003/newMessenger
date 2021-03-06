﻿// <auto-generated />
using ApiApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ApiApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("ApiApp.Models.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExtendedDataStr");

                    b.Property<string>("User");

                    b.HasKey("ID");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("ApiApp.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FromID");

                    b.Property<string>("Text");

                    b.Property<DateTime>("TimeSent");

                    b.Property<int>("ToID");

                    b.HasKey("ID");

                    b.ToTable("Message");
                });
#pragma warning restore 612, 618
        }
    }
}
