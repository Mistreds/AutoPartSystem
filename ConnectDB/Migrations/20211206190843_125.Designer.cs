﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConnectDB.Migrations
{
    [DbContext(typeof(ConDB))]
    [Migration("20211206190843_125")]
    partial class _125
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Data.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Алмата"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Астане"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Актау"
                        });
                });

            modelBuilder.Entity("Data.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ModelId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Data.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Login = "Admin",
                            Name = "Администратор",
                            Password = "887375DAEC62A9F02D32A63C9E14C7641A9A8A42E4FA8F6590EB928D9744B57BB5057A1D227E4D40EF911AC030590BBCE2BFDB78103FF0B79094CEE8425601F5",
                            PositionId = 1
                        });
                });

            modelBuilder.Entity("Data.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Article")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<double>("InputAktau")
                        .HasColumnType("double");

                    b.Property<double>("InputAstana")
                        .HasColumnType("double");

                    b.Property<double>("InputPrice")
                        .HasColumnType("double");

                    b.Property<double>("RecomPrice")
                        .HasColumnType("double");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId")
                        .IsUnique();

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("Data.GoodsInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AllPrice")
                        .HasColumnType("double");

                    b.Property<double>("AllTrans")
                        .HasColumnType("double");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("GoodsId")
                        .HasColumnType("int");

                    b.Property<double>("InputPrice")
                        .HasColumnType("double");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<double>("Marz")
                        .HasColumnType("double");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<double>("RecomPrice")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ModelId");

                    b.ToTable("GoodsInvoice");
                });

            modelBuilder.Entity("Data.GoodsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("GoodsId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId");

                    b.HasIndex("ModelId");

                    b.ToTable("GoodModel");
                });

            modelBuilder.Entity("Data.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AllCount")
                        .HasColumnType("int");

                    b.Property<double>("AllInputPrice")
                        .HasColumnType("double");

                    b.Property<double>("AllMarz")
                        .HasColumnType("double");

                    b.Property<double>("AllPrice")
                        .HasColumnType("double");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsInvoice")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Data.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Mark");
                });

            modelBuilder.Entity("Data.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MarkId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MarkId");

                    b.ToTable("Model");
                });

            modelBuilder.Entity("Data.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Position");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Администратор"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Завсклад"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Продажник"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Продажник регионал"
                        });
                });

            modelBuilder.Entity("Data.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("GoodId")
                        .HasColumnType("int");

                    b.Property<int>("InAktau")
                        .HasColumnType("int");

                    b.Property<int>("InAlmata")
                        .HasColumnType("int");

                    b.Property<int>("InAstana")
                        .HasColumnType("int");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<string>("TypePay")
                        .HasColumnType("longtext");

                    b.Property<string>("WarehousePlace")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Data.Client", b =>
                {
                    b.HasOne("Data.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Data.Employee", b =>
                {
                    b.HasOne("Data.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Data.Goods", b =>
                {
                    b.HasOne("Data.Warehouse", "Warehouse")
                        .WithOne("Goods")
                        .HasForeignKey("Data.Goods", "WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Data.GoodsInvoice", b =>
                {
                    b.HasOne("Data.Goods", "Goods")
                        .WithMany()
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Invoice", "Invoice")
                        .WithMany("GoodsInvoice")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("Invoice");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Data.GoodsModel", b =>
                {
                    b.HasOne("Data.Goods", "Goods")
                        .WithMany("GoodsModel")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Data.Invoice", b =>
                {
                    b.HasOne("Data.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Model", b =>
                {
                    b.HasOne("Data.Mark", "Mark")
                        .WithMany()
                        .HasForeignKey("MarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mark");
                });

            modelBuilder.Entity("Data.Goods", b =>
                {
                    b.Navigation("GoodsModel");
                });

            modelBuilder.Entity("Data.Invoice", b =>
                {
                    b.Navigation("GoodsInvoice");
                });

            modelBuilder.Entity("Data.Warehouse", b =>
                {
                    b.Navigation("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
