using SCMCore.Classes;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
namespace SCMCore.Admin
{
    public partial class XMLGenerator : System.Web.UI.Page
    {
        Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.ContentMethod BisContent = new Bis.ContentMethod();

        string DomainName = "";
        string FullUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DomainName = HttpContext.Current.Request.Cookies["DomainName"].Value;
            Projects project = new Projects();
            FullUrl = project.ReturnClientUrl(DomainName);
        }

        protected void btnGenerateDefineDetailSiteMap_Click(object sender, EventArgs e)
        {
            try
            {
                int Meter = 0;
                ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductDataForSitemap(getDefineDetailProduct);

                ViewModel.Search getProductcategory = new ViewModel.Search();
                DataSet dsProductCategory = BisProductCategory.GetProductCategoryDataForSitemap(getProductcategory);

                ViewModel.Search getProduct = new ViewModel.Search();
                DataSet dsProduct = BisProduct.GetProductDataForSitemap(getProduct);

                ViewModel.Search getArticle = new ViewModel.Search();
                getArticle.Filter = " and tblContentCategoryType.Name_En='Articles' ";
                DataSet dsArticle = BisContent.GetContentDataForSitemap(getProduct);

                string UrlNodes = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">";

                UrlNodes += "<url>"
                           + "<loc>"
                           + "http://" + FullUrl + "/#!/Default/"
                           + "</loc>"
                           + "<changefreq>monthly</changefreq>"
                           + "<priority>0.5</priority >"
                           + "</url>";
                UrlNodes += "<url>"
                           + "<loc>"
                           + "http://" + FullUrl + "/#!/Guarantee/"
                           + "</loc>"
                           + "<changefreq>monthly</changefreq>"
                           + "<priority>0.5</priority >"
                           + "</url>";
                UrlNodes += "<url>"
                           + "<loc>"
                           + "http://" + FullUrl + "/#!/ArticleCategory/"
                           + "</loc>"
                           + "<changefreq>monthly</changefreq>"
                           + "<priority>0.5</priority >"
                           + "</url>";
                UrlNodes += "<url>"
                           + "<loc>"
                           + "http://" + FullUrl + "/#!/AboutUs/"
                           + "</loc>"
                           + "<changefreq>monthly</changefreq>"
                           + "<priority>0.5</priority >"
                           + "</url>";
                foreach (DataRow dr in dsDefineDetailProduct.Tables[0].Rows)
                {
                    if (dr["SupplierName"].ToString() == "SOL")
                    {
                        Meter = 1; // faghat baraye mahsoolate kargahii
                    }
                    else
                    {
                        Meter = 0;
                    }

                    UrlNodes += "<url>"
                                   + "<loc>"
                                   + "http://" + FullUrl + "/#!/DefineDetailProduct/" +
                                   dr["IDX"].ToString() + "/" + Meter.ToString() + "/" +
                                   dr["SupplierName"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["PartNumber"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ProductCategoryEN"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ProductCategoryFa"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["MasterProductNameEn"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["MasterProductNameFa"].ToString().RemoveExtraCharForSEO() + "/"
                                   + "</loc>"
                                   + "<changefreq>weekly</changefreq>"
                                   + "<priority>1.0</priority >"
                                   + "</url>";
                }

                foreach (DataRow dr in dsProductCategory.Tables[0].Rows)
                {
                    UrlNodes += "<url>"
                                   + "<loc>"
                                    + "http://" + FullUrl + "/#!/ProductCategory/" + dr["IDXSupplier"].ToString() + "/" +
                                    dr["IDX"].ToString() + "/" + 
                                    dr["SupplierName"].ToString().RemoveExtraCharForSEO() + "-" +
                                    dr["Name_En"].ToString().RemoveExtraCharForSEO() + "-" + 
                                    dr["Name_Fa"].ToString().RemoveExtraCharForSEO() + "-"+
                                    dr["ParentNameEn"].ToString().RemoveExtraCharForSEO() + "-" +
                                    dr["ParentNameFa"].ToString().RemoveExtraCharForSEO() + "/"
                                   + "</loc>"
                                   + "<changefreq>monthly</changefreq>"
                                   + "<priority>0.5</priority >"
                                   + "</url>";
                }

                foreach (DataRow dr in dsProduct.Tables[0].Rows)
                {
                    UrlNodes += "<url>"
                                   + "<loc>"
                                   + "http://" + FullUrl + "/#!/MasterProduct/" + dr["IDX"].ToString() + "/" +
                                   dr["SupplierName"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ProductName_En"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ProductName_Fa"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ProductCategory_En"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ProductCategory_Fa"].ToString().RemoveExtraCharForSEO() + "/"
                                   + "</loc>"
                                   + "<changefreq>monthly</changefreq>"
                                   + "<priority>0.5</priority >"
                                   + "</url>";
                }
                foreach (DataRow dr in dsArticle.Tables[0].Rows)
                {
                    UrlNodes += "<url>"
                                   + "<loc>"
                                   + "http://" + FullUrl + "/#!/Article/" + dr["IDX"].ToString() + "/" +
                                   dr["Name_Fa"].ToString().RemoveExtraCharForSEO() + "-" +
                                   dr["ContentCategoryName_Fa"].ToString().RemoveExtraCharForSEO() + "/"
                                   + "</loc>"
                                   + "<changefreq>monthly</changefreq>"
                                   + "<priority>0.5</priority >"
                                   + "</url>";
                }
                UrlNodes += "</urlset>";
                string SiteMapFilePath = AppDomain.CurrentDomain.BaseDirectory + DomainName + "Sitemap.xml";
                File.Create(SiteMapFilePath).Close();
                File.WriteAllText(SiteMapFilePath, UrlNodes);
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('عملیات با موفقیت انجام شد!');", true);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در  برقراری ارتباط با دیتابیس!');", true);

            }
        }
    }
}