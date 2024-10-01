﻿// <auto-generated />
using System;
using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241001094342_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Clean Code"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Algorithms"
                        });
                });

            modelBuilder.Entity("App.Models.BookStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Status = "Good"
                        },
                        new
                        {
                            Id = 2,
                            Status = "Borrowed"
                        },
                        new
                        {
                            Id = 3,
                            Status = "Damaged"
                        },
                        new
                        {
                            Id = 4,
                            Status = "Lost"
                        });
                });

            modelBuilder.Entity("App.Models.BorrowingRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("ActualReturnDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("BorrowDate")
                        .HasColumnType("date");

                    b.Property<int>("CopyId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ExpectedReturnDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CopyId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StudentId");

                    b.ToTable("BorrowingRecords");
                });

            modelBuilder.Entity("App.Models.Copy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("StatusId");

                    b.ToTable("Copies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 3,
                            BookId = 1,
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("App.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Ali@enozom.com",
                            Name = "Ali",
                            Phone = "01222224400"
                        },
                        new
                        {
                            Id = 2,
                            Email = "mohamed@enozom.com",
                            Name = "Mohamed",
                            Phone = "0111155000"
                        },
                        new
                        {
                            Id = 3,
                            Email = "ahmed@enozom.com",
                            Name = "Ahmed",
                            Phone = "0155553311"
                        });
                });

            modelBuilder.Entity("App.Models.BorrowingRecord", b =>
                {
                    b.HasOne("App.Models.Copy", "Copy")
                        .WithMany("BorrowingRecords")
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.BookStatus", "BookStatus")
                        .WithMany("BorrowingRecords")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.Student", "Student")
                        .WithMany("BorrowingRecords")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookStatus");

                    b.Navigation("Copy");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("App.Models.Copy", b =>
                {
                    b.HasOne("App.Models.Book", "Book")
                        .WithMany("Copies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Models.BookStatus", "BookStatus")
                        .WithMany("Copies")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("BookStatus");
                });

            modelBuilder.Entity("App.Models.Book", b =>
                {
                    b.Navigation("Copies");
                });

            modelBuilder.Entity("App.Models.BookStatus", b =>
                {
                    b.Navigation("BorrowingRecords");

                    b.Navigation("Copies");
                });

            modelBuilder.Entity("App.Models.Copy", b =>
                {
                    b.Navigation("BorrowingRecords");
                });

            modelBuilder.Entity("App.Models.Student", b =>
                {
                    b.Navigation("BorrowingRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
