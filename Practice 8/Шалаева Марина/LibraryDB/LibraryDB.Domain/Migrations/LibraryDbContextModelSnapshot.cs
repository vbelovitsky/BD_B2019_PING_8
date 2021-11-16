﻿// <auto-generated />
using System;
using LibraryDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibraryDB.Domain.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LibraryDB.Domain.Book", b =>
                {
                    b.Property<int>("Isbn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("PagesNum");

                    b.Property<string>("PubName");

                    b.Property<int>("PubYear");

                    b.Property<string>("Title");

                    b.HasKey("Isbn");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryDB.Domain.BookCat", b =>
                {
                    b.Property<int>("Isbn");

                    b.Property<string>("CategoryName");

                    b.HasKey("Isbn", "CategoryName");

                    b.ToTable("BookCats");
                });

            modelBuilder.Entity("LibraryDB.Domain.Borrowing", b =>
                {
                    b.Property<int>("ReaderNr");

                    b.Property<int>("Isbn");

                    b.Property<int>("CopyNumber");

                    b.Property<DateTime>("ReturnDate");

                    b.HasKey("ReaderNr", "Isbn", "CopyNumber");

                    b.ToTable("Borrowings");
                });

            modelBuilder.Entity("LibraryDB.Domain.Category", b =>
                {
                    b.Property<string>("CategoryName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ParentCatCategoryName");

                    b.HasKey("CategoryName");

                    b.HasIndex("ParentCatCategoryName");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LibraryDB.Domain.Copy", b =>
                {
                    b.Property<int>("Isbn");

                    b.Property<int>("CopyNumber");

                    b.Property<int>("ShelfPosition");

                    b.HasKey("Isbn", "CopyNumber");

                    b.ToTable("Copies");
                });

            modelBuilder.Entity("LibraryDB.Domain.Publisher", b =>
                {
                    b.Property<string>("PubName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PubAddress");

                    b.HasKey("PubName");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("LibraryDB.Domain.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("LibraryDB.Domain.Category", b =>
                {
                    b.HasOne("LibraryDB.Domain.Category", "ParentCat")
                        .WithMany()
                        .HasForeignKey("ParentCatCategoryName");
                });
#pragma warning restore 612, 618
        }
    }
}
