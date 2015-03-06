using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConsoleApp1
{
    public class AppCompanyController : BaseController
    {

        // Get
        //-------------------------------------------------------------------------------------------------------
        public AppCompany Details(string id)
        {
            this.SetLastError(0, "");
            if (id != null && id != "")
            {
                //AppCompany comp = this.db.AppCompanies.FirstOrDefault(c => c.ID == id);
                AppCompany comp = this.db.AppCompanies.Find(id);
                return (comp);
            }
            this.SetLastError(-1, "invalid params");
            return (null);
        }

        //-------------------------------------------------------------------------------------------------------
        public List<AppCompany> List(string id = null)
        {
            var q = from c in this.db.AppCompanies                          
                          select c;
            if (id != null && id != "")           
                q = q.Where(c => c.ID == id);
            q = q.OrderByDescending(c => c.CreateTime).ThenBy(c => c.Name);
            return (q.ToList());
        }

        //-------------------------------------------------------------------------------------------------------
        public AppCompany IsNameExisted(string name, string id = null)
        {
            this.SetLastError(0, "");
            if (name != null && name != "")
            {
                AppCompany comp;
                if (id != null && id != "")
                    comp = this.db.AppCompanies.FirstOrDefault(c => c.ID != id && c.Name == name);
                else
                    comp = this.db.AppCompanies.FirstOrDefault(c => c.Name == name);
                return (comp);
            }
            this.SetLastError(-1, "invalid params");
            return (null);
        }

        //-------------------------------------------------------------------------------------------------------
        public bool Create(string id, string name, string email, string contact)
        {            
            this.SetLastError(0, "");

            if (id == null || id == "" || name == null || name == "" || email == null || email == "")
            {
                this.SetLastError(-1, "invalid params");
                return (false);
            }

            AppCompany orgcomp = Details(id);
            if (orgcomp != null)
            {
                this.SetLastError(-2, "company_id is existed");
                return (false);
            }
            orgcomp = IsNameExisted(name);
            if (orgcomp != null)
            {
                this.SetLastError(-3, "name is existed");
                return (false);
            }

            bool result = false;
            AppCompany comp = new AppCompany { ID = id, Name = name, Email = email, Contact = contact };
            try
            {
                db.AppCompanies.Add(comp);
                db.SaveChanges();
                result = true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string error = "";
                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var item2 in item.ValidationErrors)
                        error += item2.ErrorMessage + ",";         // item2.PropertyName (define in AppUser, ex:CompanyID)
                }
                if (error != "") error = error.Substring(0, error.Length - 1);
                this.SetLastError(-4, "validate error " + error);
            }
            catch (Exception ex)
            {
                this.SetLastError(-5, "fail to update db " + ex.Message);
            }
            return (result);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public bool Edit(string id, string name, string email, string contact)
        {
            this.SetLastError(0, "");

            if (id == null || id == "" || name == null || name == "" || email == null || email == "")
            {
                this.SetLastError(-1, "invalid params");
                return (false);
            }

            AppCompany comp = Details(id);
            if (comp == null)
            {
                this.SetLastError(-2, "company is not existed");
                return (false);
            }

            if (string.Compare(comp.Name, name, true) != 0)
            {
                AppCompany orgcomp = IsNameExisted(name, id);
                if (orgcomp != null)
                {
                    this.SetLastError(-3, "name is existed");
                    return (false);
                }
            }

            bool result = false;
            comp.Name = name;
            comp.Email = email;
            comp.Contact = contact;
            comp.LastupdateTime = DateTime.Now;
            try
            {
                db.Entry(comp).State = EntityState.Modified;
                db.SaveChanges();
                result = true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string error = "";
                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var item2 in item.ValidationErrors)
                        error += item2.ErrorMessage + ",";         // item2.PropertyName (define in AppUser, ex:CompanyID)
                }
                if (error != "") error = error.Substring(0, error.Length - 1);
                this.SetLastError(-4, "validate error " + error);
            }
            catch (Exception ex)
            {
                this.SetLastError(-5, "fail to update db " + ex.Message);
            }
            return (result);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        public bool Delete(string id)
        {
            this.SetLastError(0, "");

            if (id == null || id == "")
            {
                this.SetLastError(-1, "invalid params");
                return (false);
            }

            AppCompany comp = Details(id);
            if (comp == null)
            {
                this.SetLastError(-2, "company is not existed");
                return (false);
            }

            bool result = false;
            try
            {
                db.AppCompanies.Remove(comp);
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
