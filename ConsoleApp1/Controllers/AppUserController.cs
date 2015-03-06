using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConsoleApp1
{
    public class AppUserController : BaseController
    {

        // Get
        //-------------------------------------------------------------------------------------------------------
        public AppUser Details(int no)
        {
            this.SetLastError(0, "");
            if (no > 0)
            {
                AppUser user = this.db.AppUsers.FirstOrDefault(u => u.No == no);
                return (user);
            }
            this.SetLastError(-1, "invalid params");
            return (null);
        }

        public AppUser Details(string company_id, string id)
        {
            this.SetLastError(0, "");
            if (company_id != null && company_id != "" && id != null && id != "")
            {
                //AppUser user = this.db.AppUsers.FirstOrDefault(u => u.CompanyID == company_id && u => u.ID == id);
                AppUser user = this.db.AppUsers.Find(new object[] { company_id, id });
                return (user);
            }
            this.SetLastError(-1, "invalid params");
            return (null);
        }

        //-------------------------------------------------------------------------------------------------------
        public List<AppUser> List(string company_id = null)
        {
            var q = from u in this.db.AppUsers.Include("AppCompany")
                          select u;
            if (company_id != null && company_id != "")
            {
                q = q.Where(u => u.CompanyID == company_id);
                //query = this.db.AppUsers.Include("AppCompany").Where(u => u.CompanyID == company_id);   // 為避免上面的寫法會多出一個 sub-query
            }
            q = q.OrderBy(u => u.CompanyID).ThenByDescending(u => u.Name);
            return (q.ToList());
        }

        //-------------------------------------------------------------------------------------------------------
        public AppUser IsNameExisted(string company_id, string name, int? no = null)
        {
            this.SetLastError(0, "");
            if (company_id != null && company_id != "" && name != null && name != "")
            {
                AppUser user;
                if (no != null && no > 0)
                    user = this.db.AppUsers.FirstOrDefault(u => u.No != no && u.CompanyID == company_id && u.Name == name);
                else
                    user = this.db.AppUsers.FirstOrDefault(u => u.CompanyID == company_id && u.Name == name);
                return (user);
            }
            this.SetLastError(-1, "invalid params");
            return (null);
        }

        //-------------------------------------------------------------------------------------------------------
        public int Create(string company_id, string id, string name, string password, string email)
        {
            int user_no = -1;
            this.SetLastError(0, "");

            if (company_id == null || company_id == "" || id == null || id == "" ||
                name == null || name == "" || password == null || password == "" ||
                email == null || email == "")
            {
                this.SetLastError(-1, "invalid params");
                return (user_no);
            }

            //AppCompany comp = this.db.AppCompanies.FirstOrDefault(c => c.ID == company_id);
            AppCompany comp = this.db.AppCompanies.Find(company_id);
            if (comp == null)
            {
                this.SetLastError(-2, "company_id is not existed");
                return (user_no);
            }

            AppUser orguser = Details(company_id, id);
            if (orguser != null)
            {
                this.SetLastError(-3, "user_id is existed");
                return (user_no);
            }
            orguser = IsNameExisted(company_id, name);
            if (orguser != null)
            {
                this.SetLastError(-4, "name is existed");
                return (user_no);
            }

            AppUser user = new AppUser { CompanyID = company_id, ID = id, Name = name, Password = password, Email = email };
            try
            {
                db.AppUsers.Add(user);
                db.SaveChanges();
                user_no = user.No;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                this.SetLastError(-5, "validate error " + ex.Message);
            }
            catch (Exception ex)
            {
                this.SetLastError(-6, "fail to update db " + ex.Message);
            }
            return (user_no);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public bool Edit(int no, string name, string password, string email)
        {
            this.SetLastError(0, "");

            if (no <= 0 || name == null || name == "" || password == null || password == "" ||
                email == null || email == "")
            {
                this.SetLastError(-1, "invalid params");
                return (false);
            }

            AppUser user = Details(no);
            if (user == null)
            {
                this.SetLastError(-2, "user is not existed");
                return (false);
            }
            if (string.Compare(user.Name, name, true) != 0)
            {
                AppUser orguser = IsNameExisted(user.CompanyID, name, user.No);
                if (orguser != null)
                {
                    this.SetLastError(-3, "name is not existed");
                    return (false);
                }
            }

            bool result = false;
            user.Name = name;
            user.Password = password;
            user.Email = email;
            user.LastupdateTime = DateTime.Now;
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                result = true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                this.SetLastError(-4, "validate error " + ex.Message);
            }
            catch (Exception ex)
            {
                this.SetLastError(-5, "fail to update db " + ex.Message);
            }
            return (result);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public bool Delete(int no)
        {
            this.SetLastError(0, "");

            if (no <= 0)
            {
                this.SetLastError(-1, "invalid params");
                return (false);
            }

            AppUser user = Details(no);
            if (user == null)
            {
                this.SetLastError(-2, "user is not existed");
                return (false);
            }

            bool result = false;
            try
            {
                db.AppUsers.Remove(user);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                this.SetLastError(-3, "fail  to update db " + ex.Message);
            }
            return (result);
        }

 
    }
}
