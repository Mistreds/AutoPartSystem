﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConnectDB.Migrations
{
    [DbContext(typeof(ConDB))]
    partial class ConDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Data.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Brand");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = ""
                        });
                });

            modelBuilder.Entity("Data.CashDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Cash")
                        .HasColumnType("double");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("CashDay");
                });

            modelBuilder.Entity("Data.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

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

                    b.Property<double>("Cash")
                        .HasColumnType("double");

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
                            Cash = 0.0,
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

                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

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

                    b.HasIndex("BrandId");

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

                    b.Property<int>("TypePayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TypePayId");

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

            modelBuilder.Entity("Data.InsertOutCash", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Cash")
                        .HasColumnType("double");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("NewCash")
                        .HasColumnType("double");

                    b.Property<double>("OldCash")
                        .HasColumnType("double");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("InsertOutCash");
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

                    b.Property<bool>("IsDelMarzh")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsEnd")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsInvoice")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Data.MainMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CityFromId")
                        .HasColumnType("int");

                    b.Property<int>("CityToId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityFromId");

                    b.HasIndex("CityToId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("MainMove");
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

            modelBuilder.Entity("Data.MarzhEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<double>("Marz")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("MarzhEmployee");
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

            modelBuilder.Entity("Data.MoveGoods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MainMoveId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MainMoveId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("MoveGoods");
                });

            modelBuilder.Entity("Data.OpenCloseCash", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("CloseCash")
                        .HasColumnType("double");

                    b.Property<DateTime>("CloseData")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<double>("OpenCash")
                        .HasColumnType("double");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("OpenCloseCash");
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
                            Name = "Менеджер"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Региональный менеджер"
                        });
                });

            modelBuilder.Entity("Data.TypePay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TypePay");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Наличная оплата"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Карта"
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

            modelBuilder.Entity("Data.CashDay", b =>
                {
                    b.HasOne("Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
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
                    b.HasOne("Data.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Warehouse", "Warehouse")
                        .WithOne("Goods")
                        .HasForeignKey("Data.Goods", "WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

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

                    b.HasOne("Data.TypePay", "TypePay")
                        .WithMany()
                        .HasForeignKey("TypePayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("Invoice");

                    b.Navigation("Model");

                    b.Navigation("TypePay");
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

            modelBuilder.Entity("Data.InsertOutCash", b =>
                {
                    b.HasOne("Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
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

            modelBuilder.Entity("Data.MainMove", b =>
                {
                    b.HasOne("Data.City", "CityFrom")
                        .WithMany()
                        .HasForeignKey("CityFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.City", "CityTo")
                        .WithMany()
                        .HasForeignKey("CityToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CityFrom");

                    b.Navigation("CityTo");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.MarzhEmployee", b =>
                {
                    b.HasOne("Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Invoice");
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

            modelBuilder.Entity("Data.MoveGoods", b =>
                {
                    b.HasOne("Data.MainMove", "MainMove")
                        .WithMany("MoveGoods")
                        .HasForeignKey("MainMoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainMove");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Data.OpenCloseCash", b =>
                {
                    b.HasOne("Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Data.Goods", b =>
                {
                    b.Navigation("GoodsModel");
                });

            modelBuilder.Entity("Data.Invoice", b =>
                {
                    b.Navigation("GoodsInvoice");
                });

            modelBuilder.Entity("Data.MainMove", b =>
                {
                    b.Navigation("MoveGoods");
                });

            modelBuilder.Entity("Data.Warehouse", b =>
                {
                    b.Navigation("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
