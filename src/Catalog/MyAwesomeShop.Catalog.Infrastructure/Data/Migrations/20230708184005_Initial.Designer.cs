﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyAwesomeShop.Catalog.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyAwesomeShop.Catalog.Infrastructure.Data.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20230708184005_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyAwesomeShop.Catalog.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("MyAwesomeShop.Catalog.Domain.Product", b =>
                {
                    b.OwnsOne("MyAwesomeProject.Shared.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer")
                                .HasColumnName("price_currency");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId")
                                .HasConstraintName("fk_products_products_id");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
