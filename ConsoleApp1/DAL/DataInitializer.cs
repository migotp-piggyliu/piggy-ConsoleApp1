
namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class DataInitializer : DropCreateDatabaseIfModelChanges<AppDBContext>
    {
        //  CreateDatabaseIfNotExists
        // DropCreateDatabaseIfModelChanges
        // DropCreateDatabaseAlways

        public DataInitializer()
            : base()
        {
        }

        protected override void Seed(AppDBContext db)
        {
            base.Seed(db);

            DefLogger.Log.Info("Initialize database...");

            List<AppCompany> compList = new List<AppCompany> { 
                                                                         new AppCompany { ID = "PPPP", Name = "PPPP公司", Email = "piggy_liu@migosoft.com", Contact = "PPPP 管理員" },
                                                                         new AppCompany { ID = "KKKK", Name = "KKKK公司", Email = "kency_huang@migosoft.com", Contact = "KKKK 管理員" },
                                                                         new AppCompany { ID = "XXXX", Name = "XXXX公司", Email = "XXXX@migosoft.com", Contact = "XXXX 管理員" }};
            compList.ForEach(c => db.AppCompanies.Add(c));

            AppUser user;
            for (int i=1;i<=11; i++)
            {
                user = new AppUser
                {
                    CompanyID = "PPPP",
                    ID = "piggy" + i.ToString(),
                    Name = "piggy" + i.ToString() + "名稱",
                    Password = "12345678",
                    Email = "piggy" + i.ToString() + "@migosoft.com"
                };
                db.AppUsers.Add(user);
            }
            for (int i = 1; i <= 3; i++)
            {
                user = new AppUser
                {
                    CompanyID = "KKKK",
                    ID = "kency" + i.ToString(),
                    Name = "kency" + i.ToString() + "名稱",
                    Password = "12345678",
                    Email = "kency" + i.ToString() + "@migosoft.com"
                };
                db.AppUsers.Add(user);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                DefLogger.Log.Error("fail to add data..." + ex.Message);
            }
        }
    }
}
