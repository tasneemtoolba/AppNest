﻿// <auto-generated />
using DealsApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DealsApp.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20230811212648_I")]
    partial class I
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DealsApp.Models.Deal", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("Productid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("discount_percentage")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.HasIndex("Productid");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("DealsApp.Models.Product", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("price")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DealsApp.Models.Score", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("Dealid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Productid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("product_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("score_value")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("DealsApp.Models.Deal", b =>
                {
                    b.HasOne("DealsApp.Models.Product", null)
                        .WithMany("deals")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DealsApp.Models.Product", b =>
                {
                    b.Navigation("deals");
                });
#pragma warning restore 612, 618
        }
    }
}
