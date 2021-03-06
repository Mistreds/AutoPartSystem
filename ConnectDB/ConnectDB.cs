using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data
{
    public class ConDB : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Mark> Mark { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsInvoice> GoodsInvoices { get; set;}
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<GoodsModel> GoodModel { get; set; }
        public DbSet<MainMove> MainMove { get; set; }
        public DbSet<MoveGoods> MoveGoods{ get; set; }
        public DbSet<TypePay> TypePay { get; set; }
        public DbSet<MarzhEmployee> MarzhEmployee { get; set; }
        public DbSet<CashDay> CashDay { get; set; }
        public DbSet<InsertOutCash> InsertOutCash { get; set; }
        public DbSet<OpenCloseCash> OpenCloseCash { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<GoodsImage> GoodsImage { get; set; }
        public DbSet<Data.TypeExpenses> TypeExpenses { get; set;}
        public DbSet<Data.Expenses> Expenses { get; set; }
        public DbSet<Data.BackInvoice> backInvoices { get; set; }
        private static string path_connect;
        private static string query_connect;
        public ConDB()
        {
            query_connect = "server=spiriddp.beget.tech;user=spiriddp_auto_te;password=AutoPart123;database=spiriddp_auto_te;";
            //if (string.IsNullOrEmpty(path_connect))
            //    path_connect = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AutoPartSystem\connect.ini";
            //if(string.IsNullOrEmpty(query_connect))
            //    if (File.Exists(path_connect))
            //    {
            //        using (FileStream fstream = new FileStream(path_connect, FileMode.Open))
            //        {
            //            byte[] array = new byte[fstream.Length];
            //            // считываем данные
            //            fstream.Read(array, 0, array.Length);
            //            // декодируем байты в строку b 
            //            query_connect = Encoding.Default.GetString(array);
            //            //Database.EnsureDeleted();
            //            Database.EnsureCreated();
            //        }
                //}

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                query_connect,
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Admin
            modelBuilder.Entity<City>(b => b.ToTable("City"));
            modelBuilder.Entity<Employee>(b => b.ToTable("Employee"));
            modelBuilder.Entity<Position>(b => b.ToTable("Position"));
            modelBuilder.Entity<City>().HasData(new City[] { new City(1, "Алмата"), new City(2, "Астане"), new City(3, "Актау") });
            modelBuilder.Entity<Position>().HasData(new Position[] { new Position(1, "Администратор"), new Position(2, "Завсклад"), new Position(3, "Менеджер"), new Position(4, "Региональный менеджер") });
            modelBuilder.Entity<Employee>().HasData(new Employee[] { new Employee(1, "Администратор", "Admin", "Admin", 1, 1) });
            #endregion
            #region MarkModel
            modelBuilder.Entity<Mark>(b => b.ToTable("Mark"));
            modelBuilder.Entity<Mark>().HasData(new Mark[] { new Mark(1, "") });
            modelBuilder.Entity<Model>(b => b.ToTable("Model"));
            modelBuilder.Entity<Model>().HasData(new Model[] { new Model(1, "", 1) });
            modelBuilder.Entity<Brand>(b => b.ToTable("Brand"));
            modelBuilder.Entity<Brand>().HasData(new Brand[] { new Brand(1, "") });
            #endregion
            #region Warehouse
            modelBuilder.Entity<Warehouse>(b => b.ToTable("Warehouse"));
            modelBuilder.Entity<Warehouse>(b => b.ToTable("Warehouse"));
            modelBuilder.Entity<Goods>(b => b.ToTable("Goods"));
            modelBuilder.Entity<GoodsImage>(b => b.ToTable("GoodsImage"));
            modelBuilder.Entity<Goods>().Ignore(p => p.CountCell);
            modelBuilder.Entity<Goods>().Ignore(p => p.PriceCell);
            modelBuilder.Entity<Goods>().Property(p => p.BrandId).HasDefaultValue(1);
            modelBuilder.Entity<GoodsInvoice>(b => b.ToTable("GoodsInvoice"));
            modelBuilder.Entity<GoodsInvoice>().Ignore(p => p.DontHaveGoods);
            modelBuilder.Entity<Invoice>(b => b.ToTable("Invoice"));
            modelBuilder.Entity<Client>(b => b.ToTable("Client"));
            modelBuilder.Entity<Client>().Ignore(p => p.Mark);
            modelBuilder.Entity<GoodsModel>(b => b.ToTable("GoodModel"));
            modelBuilder.Entity<Client>().Ignore(p => p.CityName);
            modelBuilder.Entity<Client>().Ignore(p => p.MarkId);
            modelBuilder.Entity<MoveGoods>(b => b.ToTable("MoveGoods"));
            modelBuilder.Entity<MainMove>(b => b.ToTable("MainMove"));
            modelBuilder.Entity<TypePay>(b => b.ToTable("TypePay"));
            modelBuilder.Entity<TypePay>().HasData(new TypePay[] { new TypePay(1, "Наличная оплата"), new TypePay(2, "Карта") });
            modelBuilder.Entity<MarzhEmployee>(b => b.ToTable("MarzhEmployee"));
            #endregion
            #region Cash
            modelBuilder.Entity<InsertOutCash>(b => b.ToTable("InsertOutCash"));
            modelBuilder.Entity<CashDay>(b => b.ToTable("CashDay"));
            modelBuilder.Entity<OpenCloseCash>(b => b.ToTable("OpenCloseCash"));
            modelBuilder.Entity<Data.TypeExpenses>(b => b.ToTable("TypeExpenses"));
            modelBuilder.Entity<Data.TypeExpenses>().HasData(new TypeExpenses[] { new TypeExpenses(1, "Личные расходы"), new TypeExpenses(2, "Расходы на аренду"), new TypeExpenses(3, "Расходы на рекламу"), new TypeExpenses(4, "Иные расходы"), new TypeExpenses(5, "Возврат"), });
            modelBuilder.Entity<Data.Expenses>(b => b.ToTable("Expenses"));
            modelBuilder.Entity<Data.BackInvoice>(b => b.ToTable("BackInvoice"));
            #endregion
        }
    }
}
