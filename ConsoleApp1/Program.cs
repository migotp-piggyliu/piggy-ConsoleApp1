using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ConsoleApp1
{
    class Program
    {     
        static void Main(string[] args)
        {
            //TestAppUserAPI();
            TestAppCompanyAPI();
        }

        //----------------------------------------------------------------------------------------------------
        static void TestAppCompanyAPI()
        {
            AppCompanyController compapi = new AppCompanyController();

            // Get Company List
            List<AppCompany> compList = compapi.List();

            string id = "XXXX";            
            string name = "XXXX公司";            
            //string email = "xxxx@migosoft.com";
            string email = "x";
            string contact = "XXXX 管理員";
            if (compapi.Create(id, name, email, contact) == true)
            {
                compapi.Edit(id, name + "123", email, contact);

                AppUserController api = new AppUserController();
                int no = api.Create(id, "xxxx1", "xxxx1名稱", "12345678", "xxxx1@migosoft.com");
                if (no > 0)
                    compapi.Delete("XXXX");
            }
        }

        //----------------------------------------------------------------------------------------------------
        static void TestAppUserAPI()
        {
            AppUserController api = new AppUserController();

            string company_id = "PPPP";
            string id = "piggy22";
            string name = "piggy name22";
            string password = "12345678";
            string email = "piggy_liu@migosoft.com";

            // Get User List
            List<AppUser> userList = api.List(company_id);

            // Add User
            DefLogger.Log.Info(string.Format("AddUser( CompanyID={0},ID={1}, Name={2}, Password={3},Email={4})...",
                                                                            company_id, id, name, password, email));
            int no = api.Create(company_id, id, name, password, email);
            if (no <= 0)
                DefLogger.Log.Error(string.Format("Fail to add user ! {0}({1})", api.LastError, api.LastErrorCode));
            else
                DefLogger.Log.Info("Success to add user !");

            // Modify User
            if (no <= 0)
            {
                AppUser user = api.Details(company_id, id);   // Find user no
                if (user == null) return;
                no = user.No;
            }
            name = "piggy name22 xxx";
            password = "1234xxxx";
            DefLogger.Log.Info(string.Format("UpdateUser(No={0}, Name={1}, Password={2},Email={3})...",
                                                                            no, name, password, email));
            if (api.Edit(no, name, password, email) == true)
            {
                DefLogger.Log.Info("Success to update user ! ");

                DefLogger.Log.Info(string.Format("RemoveUser(No={0})...", no));
                if (api.Delete(no) == true)
                    DefLogger.Log.Info("Success to update user ! ");
                else
                    DefLogger.Log.Error(string.Format("Fail to remove user ! {0}({1})", api.LastError, api.LastErrorCode));
            }
            else
            {
                DefLogger.Log.Error(string.Format("Fail to update user ! {0}({1})", api.LastError, api.LastErrorCode));
            }
        }

    }
}
