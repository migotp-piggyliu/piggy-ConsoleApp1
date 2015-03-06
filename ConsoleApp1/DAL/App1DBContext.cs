namespace ConsoleApp1
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class AppDBContext : DbContext
    {
        // Your context has been configured to use a 'App1DBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.App1DBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'App1DBContext' 
        // connection string in the application configuration file.
        public AppDBContext()
            : base("name=App1DBContext")
        {
            // this.Configuration.LazyLoadingEnabled = false;                    
            Database.SetInitializer<AppDBContext>(new DataInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //查找名为 Id 或 <TypeName> Id 的属性，并将他们配置作为主键。
            //modelBuilder.Conventions.Remove<IdKeyDiscoveryConvention>();
            //使用外键关系，使用 <NavigationProperty> <PrimaryKeyProperty> 模式作为属性的外观。
            //modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
            //使用外键关系，使用 <PrimaryKeyProperty> 模式作为属性的外观。
            //modelBuilder.Conventions.Remove<PrimaryKeyNameForeignKeyDiscoveryConvention>();
        } 

        public DbSet<AppCompany> AppCompanies { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}