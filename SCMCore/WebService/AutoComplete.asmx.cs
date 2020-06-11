using SCMCore.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.WebService
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {
        /// <summary>
        /// لیست مشتریان حقوقی
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getLegalCustomer(string prefix)
        {
            Bis.LegalUserMethod bisCompany = new Bis.LegalUserMethod();
            ViewModel.Search searchCompany = new ViewModel.Search();
            searchCompany.Filter = " and tblLegalUser.Name_Fa like N'%" + prefix.FixFarsi() + "%' and Active='true'";
            //searchCompany.Order = "order by tblLegalUser.Name_Fa desc";
            DataSet dsCompany = bisCompany.GetCutomerData(searchCompany);

            List<string> CompanyNames = new List<string>();
            if (!dsCompany.Null_Ds())
            {
                for (int i = 0; i < dsCompany.Tables[0].Rows.Count; i++)
                {
                    CompanyNames.Add(string.Format("{0}~{1}", dsCompany.Tables[0].Rows[i]["Name_Fa"].ToString() + " ---> " + dsCompany.Tables[0].Rows[i]["ParentCompanyName"].ToString(), dsCompany.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return CompanyNames;
            }
            else
            {
                CompanyNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return CompanyNames;
            }

        }


        /// <summary>
        ///   پرسنل شرکت ها
        /// </summary>
        /// <param name="IDLegal"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getPersonelINLegalCustomer(Guid IDLegal, string prefix)
        {
            Bis.RealUserMethod bisRealuser = new Bis.RealUserMethod();
            ViewModel.Search searchRealUser = new ViewModel.Search();
            searchRealUser.Filter = " and tblRealUser.IDLegalUser ='" + IDLegal + "' and ( tblRealUser.FName like N'%" + prefix.FixFarsi() + "%' or tblRealUser.LName like N'%" + prefix.FixFarsi() + "%' ) ";
            DataSet dsRealUser = bisRealuser.GetRealUserCustomerData(searchRealUser);

            List<string> RealUserNames = new List<string>();
            if (!dsRealUser.Null_Ds())
            {
                for (int i = 0; i < dsRealUser.Tables[0].Rows.Count; i++)
                {
                    RealUserNames.Add(string.Format("{0}~{1}", dsRealUser.Tables[0].Rows[i]["FullName"].ToString(), dsRealUser.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return RealUserNames;
            }
            else
            {
                RealUserNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return RealUserNames;
            }

        }

        /// <summary>
        /// لیست مشتریان حقوقی
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getRealCustomer(string prefix)
        {
            Bis.RealUserMethod bisRealuser = new Bis.RealUserMethod();
            ViewModel.Search searchRealUser = new ViewModel.Search();
            searchRealUser.Filter = " and tblRealUser.IDLegalUser ='" + Guid.Empty + "' and ( tblRealUser.FName like N'%" + prefix.FixFarsi() + "%' or tblRealUser.LName like N'%" + prefix.FixFarsi() + "%') ";
            DataSet dsRealUser = bisRealuser.GetRealUserCustomerData(searchRealUser);

            List<string> RealUserNames = new List<string>();
            if (!dsRealUser.Null_Ds())
            {
                for (int i = 0; i < dsRealUser.Tables[0].Rows.Count; i++)
                {
                    RealUserNames.Add(string.Format("{0}~{1}", dsRealUser.Tables[0].Rows[i]["FullName"].ToString(), dsRealUser.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return RealUserNames;
            }
            else
            {
                RealUserNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return RealUserNames;
            }

        }


        /// <summary>
        /// لیست مشتریان حقوقی
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getLegalSupplier(string prefix)
        {
            Bis.LegalUserMethod bisCompany = new Bis.LegalUserMethod();
            ViewModel.Search searchCompany = new ViewModel.Search();
            searchCompany.Filter = " and tblLegalUser.Name_Fa like N'%" + prefix.FixFarsi() + "%' and Active='true'";
            //searchCompany.Order = "order by tblLegalUser.Name_Fa desc";
            DataSet dsCompany = bisCompany.GetSupplierData(searchCompany);

            List<string> CompanyNames = new List<string>();
            if (!dsCompany.Null_Ds())
            {
                for (int i = 0; i < dsCompany.Tables[0].Rows.Count; i++)
                {
                    CompanyNames.Add(string.Format("{0}~{1}", dsCompany.Tables[0].Rows[i]["Name_Fa"].ToString() + " ---> " + dsCompany.Tables[0].Rows[i]["ParentCompanyName"].ToString(), dsCompany.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return CompanyNames;
            }
            else
            {
                CompanyNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return CompanyNames;
            }

        }


        /// <summary>
        ///   پرسنل شرکت ها
        /// </summary>
        /// <param name="IDLegal"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getPersonelINLegalSupplier(Guid IDLegal, string prefix)
        {
            Bis.RealUserMethod bisRealuser = new Bis.RealUserMethod();
            ViewModel.Search searchRealUser = new ViewModel.Search();
            searchRealUser.Filter = " and tblRealUser.IDLegalUser ='" + IDLegal + "' and ( tblRealUser.FName like N'%" + prefix.FixFarsi() + "%' or tblRealUser.LName like N'%" + prefix.FixFarsi() + "%' ) ";
            DataSet dsRealUser = bisRealuser.GetRealUserSupplierData(searchRealUser);

            List<string> RealUserNames = new List<string>();
            if (!dsRealUser.Null_Ds())
            {
                for (int i = 0; i < dsRealUser.Tables[0].Rows.Count; i++)
                {
                    RealUserNames.Add(string.Format("{0}~{1}", dsRealUser.Tables[0].Rows[i]["FullName"].ToString(), dsRealUser.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return RealUserNames;
            }
            else
            {
                RealUserNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return RealUserNames;
            }

        }

        /// <summary>
        /// لیست پرسنل
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getPersonel(string prefix)
        {
            Bis.PersonelMethod bisPersonel = new Bis.PersonelMethod();
            ViewModel.Search searchPersonel = new ViewModel.Search();
            searchPersonel.Filter = " and tblPersonel.FName +' '+ tblPersonel.LName like N'%" + prefix.FixFarsi() + "%' and Active='true'";
            //searchCompany.Order = "order by tblLegalUser.Name_Fa desc";
            DataSet dsPersonel = bisPersonel.GetPersonelData(searchPersonel);

            List<string> PersonelNames = new List<string>();
            if (!dsPersonel.Null_Ds())
            {
                for (int i = 0; i < dsPersonel.Tables[0].Rows.Count; i++)
                {
                    PersonelNames.Add(string.Format("{0}~{1}", dsPersonel.Tables[0].Rows[i]["FullName"].ToString() , dsPersonel.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return PersonelNames;
            }
            else
            {
                PersonelNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return PersonelNames;
            }

        }
    }
}
