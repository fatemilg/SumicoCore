using AjaxControlToolkit;
using SCMCore.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using Bis = SCMCore.DatabaseLayer;


namespace SCMCore.Admin
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. ...
    [ScriptService]
    public class Auto : System.Web.Services.WebService
    {

        /// <summary>
        /// in method baraye autocomplete ajax contorl toolkit estefade mishavad dar safheie legal user
        /// </summary>
        /// <param name="prefixText"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getCompanyName(string prefixText)
        {
            Bis.LegalUserMethod bisCompany = new Bis.LegalUserMethod();
            ViewModel.Search searchCompany = new ViewModel.Search();
            searchCompany.Filter = " and tblLegalUser.Name_Fa like N'%" + prefixText.FixFarsi() + "%'";
            //searchCompany.Order = "order by tblLegalUser.Name_Fa desc";
            DataSet dsCompany = bisCompany.GetCutomerData(searchCompany);

            List<string> CompanyNames = new List<string>();
            if (!dsCompany.Null_Ds())
            {
                for (int i = 0; i < dsCompany.Tables[0].Rows.Count; i++)
                {
                    string item = AutoCompleteExtender.CreateAutoCompleteItem(dsCompany.Tables[0].Rows[i]["Name_Fa"].ToString(), dsCompany.Tables[0].Rows[i]["IDUser"].ToString());
                    CompanyNames.Add(item);
                }

            }

            return CompanyNames;

        }

        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getAccessoryCodeAndName(string prefix)
        {
            Bis.ProductMethod bisAccessory = new Bis.ProductMethod();
            ViewModel.Search searchAccessory = new ViewModel.Search();
            searchAccessory.Filter = " and tblProduct.Accessory='true' and  (tblProduct.ProductCode like N'%" + prefix.FixFarsi() + "%' or tblProduct.Name_Fa like N'%" + prefix.FixFarsi() + "%' )";
            //searchCompany.Order = "order by tblLegalUser.Name_Fa desc";
            DataSet dsAccessory = bisAccessory.GetProductData(searchAccessory);

            List<string> AccessoryNames = new List<string>();
            if (!dsAccessory.Null_Ds())
            {
                for (int i = 0; i < dsAccessory.Tables[0].Rows.Count; i++)
                {

                    AccessoryNames.Add(string.Format("{0}~{1}", dsAccessory.Tables[0].Rows[i]["AccessoryCodeName"].ToString(), dsAccessory.Tables[0].Rows[i]["IDProduct"].ToString()));
                }
                return AccessoryNames;
            }
            else
            {
                AccessoryNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return AccessoryNames;
            }

        }

        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getTechnicalPropertyCodeAndName(string prefix)
        {
            Bis.TechnicalPropertyMethod bisTechnicalProperty = new Bis.TechnicalPropertyMethod();
            ViewModel.Search searchTechnicalProperty = new ViewModel.Search();
            searchTechnicalProperty.Filter = " and (tblTechnicalProperty.Code like N'%" + prefix.FixFarsi() + "%' or tblTechnicalProperty.Name_Fa like N'%" + prefix.FixFarsi() + "%' or tblTechnicalProperty.Name_En like N'%" + prefix + "%') ";

            DataSet dsTechnicalProperty = bisTechnicalProperty.GetTechnicalPropertyData(searchTechnicalProperty);

            List<string> TechnicalPropertyNames = new List<string>();
            if (!dsTechnicalProperty.Null_Ds())
            {
                for (int i = 0; i < dsTechnicalProperty.Tables[0].Rows.Count; i++)
                {

                    TechnicalPropertyNames.Add(string.Format("{0}~{1}", dsTechnicalProperty.Tables[0].Rows[i]["TechnicalPropertyCodeName_En"].ToString(), dsTechnicalProperty.Tables[0].Rows[i]["IDTechnicalProperty"].ToString()));
                }
                return TechnicalPropertyNames;
            }
            else
            {
                TechnicalPropertyNames.Add(string.Format("{0}~{1}", "اطلاعاتی یافت نشد", Guid.Empty));
                return TechnicalPropertyNames;
            }

        }


        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getProductName(string prefix)
        {
            Bis.ProductMethod bisProduct = new Bis.ProductMethod();
            ViewModel.Search searchProduct = new ViewModel.Search();
            searchProduct.Filter = "and tblProduct.Accessory='false' and tblProduct.Name_Fa like N'%" + prefix.FixFarsi() + "%'  ";

            DataSet dsProduct = bisProduct.GetProductData(searchProduct);

            List<string> ProductNames = new List<string>();
            if (!dsProduct.Null_Ds())
            {
                for (int i = 0; i < dsProduct.Tables[0].Rows.Count; i++)
                {

                    ProductNames.Add(string.Format("{0}~{1}", dsProduct.Tables[0].Rows[i]["Name_Fa"].ToString(), dsProduct.Tables[0].Rows[i]["IDProduct"].ToString()));
                    //string item = AutoCompleteExtender.CreateAutoCompleteItem(dsProduct.Tables[0].Rows[i]["ProductCodeName"].ToString(), dsProduct.Tables[0].Rows[i]["IDProduct"].ToString());
                    //ProductNames.Add(item);
                }
                return ProductNames;
            }
            else
            {
                ProductNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return ProductNames;
            }



        }

        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getProductName_En(string prefix)
        {
            Bis.ProductMethod bisProduct = new Bis.ProductMethod();
            ViewModel.Search searchProduct = new ViewModel.Search();
            searchProduct.Filter = "and tblProduct.Accessory='false' and tblProduct.Name_En like N'%" + prefix.FixFarsi() + "%'  ";

            DataSet dsProduct = bisProduct.GetProductData(searchProduct);

            List<string> ProductNames = new List<string>();
            if (!dsProduct.Null_Ds())
            {
                for (int i = 0; i < dsProduct.Tables[0].Rows.Count; i++)
                {

                    ProductNames.Add(string.Format("{0}~{1}", dsProduct.Tables[0].Rows[i]["Name_En"].ToString(), dsProduct.Tables[0].Rows[i]["IDProduct"].ToString()));
                    //string item = AutoCompleteExtender.CreateAutoCompleteItem(dsProduct.Tables[0].Rows[i]["ProductCodeName"].ToString(), dsProduct.Tables[0].Rows[i]["IDProduct"].ToString());
                    //ProductNames.Add(item);
                }
                return ProductNames;
            }
            else
            {
                ProductNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return ProductNames;
            }



        }


        //[System.Web.Services.WebMethod, ScriptMethod()]
        //public List<string> getJob(string prefix)
        //{
        //    Bis.JobMethod bisJob = new Bis.JobMethod();
        //    ViewModel.Search searchJob = new ViewModel.Search();
        //    searchJob.Filter = "and  tblJob.Title like N'%" + prefix.FixFarsi() + "%'  ";

        //    DataSet dsJob = bisJob.GetJobData(searchJob);

        //    List<string> JobTitle = new List<string>();
        //    if (!dsJob.Null_Ds())
        //    {
        //        for (int i = 0; i < dsJob.Tables[0].Rows.Count; i++)
        //        {
        //            JobTitle.Add(string.Format("{0}~{1}", dsJob.Tables[0].Rows[i]["Title"].ToString(), dsJob.Tables[0].Rows[i]["IDJob"].ToString()));
        //        }
        //        return JobTitle;
        //    }
        //    else
        //    {
        //        JobTitle.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
        //        return JobTitle;
        //    }



        //}

        /// <summary>
        /// لیست مشتریان حقوقی
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getLegalUser(string prefix)
        {
            Bis.LegalUserMethod bisCompany = new Bis.LegalUserMethod();
            ViewModel.Search searchCompany = new ViewModel.Search();
            searchCompany.Filter = " and tblLegalUser.Name_Fa like N'%" + prefix.FixFarsi() + "%'";
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
                CompanyNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return CompanyNames;
            }

        }

        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getLegalUserForShowingInlogin(string prefix)
        {


            Bis.LegalUserMethod bisCompany = new Bis.LegalUserMethod();
            ViewModel.Search searchCompany = new ViewModel.Search();
            searchCompany.Filter = " and tblLegalUser.Name_Fa like N'%" + prefix.FixFarsi() + "%' ";
            //searchCompany.Order = "order by tblLegalUser.Name_Fa desc";
            DataSet dsCompany = bisCompany.GetCutomerData(searchCompany);

            List<string> CompanyNames = new List<string>();
            if (!dsCompany.Null_Ds())
            {
                for (int i = 0; i < dsCompany.Tables[0].Rows.Count; i++)
                {
                    CompanyNames.Add(string.Format("{0}~{1}", dsCompany.Tables[0].Rows[i]["Name_Fa"].ToString(), dsCompany.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return CompanyNames;
            }
            else
            {
                CompanyNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return CompanyNames;
            }

        }

        /// <summary>
        /// لیست مشتریان حقوقی
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getRealUser(string prefix)
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
                RealUserNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return RealUserNames;
            }

        }

        /// <summary>
        /// لیست کلیه پرسنل شرکت ها
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getRealUserINLegalUser(string prefix)
        {
            Bis.RealUserMethod bisRealuser = new Bis.RealUserMethod();
            ViewModel.Search searchRealUser = new ViewModel.Search();
            searchRealUser.Filter = " and tblRealUser.IDLegalUser <>'" + Guid.Empty + "' and ( tblRealUser.FName like N'%" + prefix.FixFarsi() + "%' or tblRealUser.LName like N'%" + prefix.FixFarsi() + "%' or legal.Name_Fa like N'%" + prefix.FixFarsi() + "%') ";
            DataSet dsRealUser = bisRealuser.GetRealUserCustomerData(searchRealUser);

            List<string> RealUserNames = new List<string>();
            if (!dsRealUser.Null_Ds())
            {
                for (int i = 0; i < dsRealUser.Tables[0].Rows.Count; i++)
                {
                    RealUserNames.Add(string.Format("{0}~{1}", dsRealUser.Tables[0].Rows[i]["RealUserInLegal"].ToString(), dsRealUser.Tables[0].Rows[i]["IDUser"].ToString()));
                }
                return RealUserNames;
            }
            else
            {
                RealUserNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return RealUserNames;
            }

        }

        /// <summary>
        /// این متد برای لیست پرسنل شرکت ها وقتی شرکتی انتخاب شده است.که در صفحات قیمت گیری از همکاران و سابقه مشتری استفاده می شود
        /// </summary>
        /// <param name="IDLegal"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getPersonelINLegalUser(Guid IDLegal, string prefix)
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
                RealUserNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return RealUserNames;
            }

        }



        [System.Web.Services.WebMethod, ScriptMethod()]
        public List<string> getProducCategory(string prefix, string IDSupplier)
        {
            Bis.ProductCategoryMethod bisProductCategory = new Bis.ProductCategoryMethod();
            ViewModel.Search searchProductCategory = new ViewModel.Search();
            searchProductCategory.Filter = " and ParentName_En like N'%" + prefix.FixFarsi() + "%' and IDSupplier='" + IDSupplier + "'  ";
            DataSet dsProduct = bisProductCategory.GetProductCategoryData(searchProductCategory);

            List<string> ProductNames = new List<string>();
            if (!dsProduct.Null_Ds())
            {
                for (int i = 0; i < dsProduct.Tables[0].Rows.Count; i++)
                {
                    ProductNames.Add(string.Format("{0}~{1}", dsProduct.Tables[0].Rows[i]["ParentName_En"].ToString(), dsProduct.Tables[0].Rows[i]["IDProductCategory"].ToString()));
                }
                return ProductNames;
            }
            else
            {
                ProductNames.Add(string.Format("{0},{1}", "اطلاعاتی یافت نشد", "اطلاعاتی یافت نشد"));
                return ProductNames;
            }
        }

        [System.Web.Services.WebMethod, ScriptMethod()]
        public object ProductAutoComplete(object searchparam)
        {
            Bis.ProductMethod bisProduct = new Bis.ProductMethod();

            try
            {
                if (searchparam != "")
                {
                    ViewModel.Search searchProduct = new ViewModel.Search();
                    searchProduct.Filter = " and (tblProduct.Name_Fa Like N'%" + searchparam.ToString().FixFarsi() +
                        "%' or tblProduct.Name_En Like N'%" + searchparam.ToString().FixFarsi() +
                        "%') and tblProduct.Accessory = 'False' and tblProduct.AcceptByAdmin = 'true' and tblProduct.ShowInSite = 'true' and tblProductCategory.ShowInSite = 'true' ";
                    searchProduct.Order = "order by tblProduct.Name_Fa desc";
                    DataSet ds = bisProduct.GetDataAutoCompeleForCompare(searchProduct);

                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }

                    return serializer.Serialize(rows);
                }
                else return "";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {


            }
        }


        /// <summary>
        /// لیست عبارات دیکشنری
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod, ScriptMethod()]
        public object getAllDictionary()
        {
            Bis.DictionaryMethod bisDictionary = new Bis.DictionaryMethod();

            try
            {
                ViewModel.Search searchDictionary = new ViewModel.Search();              
                DataSet ds = bisDictionary.GetDictionaryData(searchDictionary);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
