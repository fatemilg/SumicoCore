using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using SCMCore.Classes;
using System.IO;

namespace SCMCore.Admin
{
    public partial class Dictionary : System.Web.UI.Page
    {
        Bis.DictionaryMethod BisDictionary = new Bis.DictionaryMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hfMode.Value = "New";
                fillGrdDictionary();
            }
        }
        public void fillGrdDictionary()
        {
            ViewModel.Search SearchDictionary = new ViewModel.Search();
            SearchDictionary.Order = "Order by KeyWord ";
            DataSet dsDictionary = BisDictionary.GetDictionaryData(SearchDictionary);
            grdDictionary.DataSource = dsDictionary;
            grdDictionary.DataBind();
            Session["dsDictionary"] = dsDictionary;
            Session["dsDictionaryIndex"] = dsDictionary;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txtValue.Text != "" && txtTitle.Text != "" && txtSourceText.Text != "" && txtMetaTag.Text != "" && txtMetaDescription.Text != "" && txtKeyWord.Text != "" && txtAbstract.Text != "")
                {
                    ViewModel.tblDictionary newDictionary = new ViewModel.tblDictionary();
                    newDictionary.Title = txtTitle.Text.FixFarsi();
                    newDictionary.Value = txtValue.Text.FixFarsi();
                    newDictionary.Abstract = txtAbstract.Text.FixFarsi();
                    newDictionary.KeyWord = txtKeyWord.Text.FixFarsi();
                    newDictionary.SourceText = txtSourceText.Text.FixFarsi();
                    newDictionary.MetaDescription = txtMetaDescription.Text.FixFarsi();
                    newDictionary.MetaTag = txtMetaTag.Text.FixFarsi();
                    newDictionary.Status = 1;
                    switch (hfMode.Value)
                    {
                        case "New":
                            try
                            {
                                DataSet dsCheckDictionaryName = (DataSet)Session["dsDictionary"];
                                int ChekDictionaryCount = dsCheckDictionaryName.Tables[0].Select("Title = '" + txtTitle.Text + "'").Count();
                                if (ChekDictionaryCount == 0)
                                {
                                    newDictionary.PicUrl = fulDictionary.MoveFile(@"..\Picture\Dictionary");
                                    newDictionary.IDDictionary = Guid.NewGuid();
                                    bool ret = BisDictionary.AddDictionary(newDictionary);
                                    if (ret)
                                    {
                                        newFields();
                                        hfMode.Value = "New";
                                        fillGrdDictionary();
                                        hfIDDictionary.Value = newDictionary.IDDictionary.ToString();
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                                    }
                                    else
                                    {
                                        try { File.Delete(@"\" + newDictionary.PicUrl); }
                                        catch { }
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorSave", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRepeat", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> عبارت وارد شده تکراری است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }
                            }
                            catch
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }

                            break;
                        case "Edit":
                            try
                            {
                                string Url = fulDictionary.MoveFile(@"..\Picture\Dictionary");
                                if (Url != "")
                                {
                                    DeleteOldPicDictionary(hfDictionaryPicUrl.Value);
                                    newDictionary.PicUrl = Url;
                                }
                                else
                                {
                                    ViewModel.Search SearchUrl = new ViewModel.Search();
                                    SearchUrl.Filter = " And tblDictionary.IDDictionary = '" + hfIDDictionary.Value + "'";
                                    DataSet dsUrl = BisDictionary.GetDictionaryData(SearchUrl);
                                    newDictionary.PicUrl = dsUrl.ReturnDataSetField("PicUrl");
                                }
                                newDictionary.IDDictionary = hfIDDictionary.Value.StringToGuid();
                                bool result = BisDictionary.UpdateDictionary(newDictionary);
                                if (result)
                                {
                                    hfMode.Value = "New";
                                    newFields();
                                    fillGrdDictionary();
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات ویرایش شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorSave", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }

                            }
                            catch
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }

                            break;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrortxtValue", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا همه فیلدها را کامل نمایید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
        }
        protected void grdDictionary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDDictionary = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Edit":
                    try
                    {
                        hfIDDictionary.Value = IDDictionary.ToString();
                        ViewModel.Search getDictionary = new ViewModel.Search();
                        getDictionary.Filter = " and tblDictionary.IDDictionary ='" + IDDictionary + "'";
                        DataSet ds = BisDictionary.GetDictionaryData(getDictionary);

                        if (!ds.Null_Ds())
                        {
                            txtTitle.Text = ds.ReturnDataSetField("Title");
                            txtValue.Text = ds.ReturnDataSetField("Value");
                            txtAbstract.Text = ds.ReturnDataSetField("Abstract");
                            txtKeyWord.Text = ds.ReturnDataSetField("KeyWord");
                            txtSourceText.Text = ds.ReturnDataSetField("SourceText");
                            txtMetaTag.Text = ds.ReturnDataSetField("MetaTag");
                            txtMetaDescription.Text = ds.ReturnDataSetField("MetaDescription");
                            hfDictionaryPicUrl.Value = ds.ReturnDataSetField("PicUrl");
                            hfMode.Value = "Edit";
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
                case "Delete":
                    try
                    {
                        ViewModel.Search getDictionary = new ViewModel.Search();
                        getDictionary.Filter = " and tblDictionary.IDDictionary ='" + IDDictionary + "'";
                        DataSet ds = BisDictionary.GetDictionaryData(getDictionary);

                        ViewModel.tblDictionary DelDictionary = new ViewModel.tblDictionary();
                        DelDictionary.IDDictionary = IDDictionary;
                        bool retDel = BisDictionary.DeleteDictionary(DelDictionary);
                        if (retDel)
                        {
                            try { File.Delete(Server.MapPath(@"\" + ds.ReturnDataSetField("PicUrl"))); }
                            catch { }
                            fillGrdDictionary();
                            hfMode.Value = "New";
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
            }
        }
        protected void grdDictionary_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdDictionary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            hfMode.Value = "New";
            newFields();

        }
        public void newFields()
        {
            txtTitle.Text = "";
            txtValue.Text = "";
            txtAbstract.Text = "";
            txtKeyWord.Text = "";
            txtSourceText.Text = "";
            txtMetaTag.Text = "";
            txtMetaDescription.Text = "";
            hfDictionaryPicUrl.Value = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchDictionary();
        }
        protected void grdDictionary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDictionary.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsDictionaryIndex"];
            grdDictionary.DataSource = ds;
            grdDictionary.DataBind();
        }
        public bool ShowImage(string PicUrl)
        {
            if (PicUrl == "")
                return false;
            else
                return true;

        }
        public void searchDictionary()
        {
            DataSet dsSearch = (DataSet)Session["dsDictionary"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("Title like '%" + txtSearch.Text.FixFarsi() + "%' or Value like '%" + txtSearch.Text.FixFarsi() + "%' "))
            {
                dt.ImportRow(row);
            }
            grdDictionary.DataSource = dt;  // baraye search
            grdDictionary.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsDictionaryIndex"] = ds;
            Session["dsDictionary"] = ds;
        }
        public void DeleteOldPicDictionary(string PicUrl)
        {
            try
            {
                if (PicUrl == "")
                {
                    return;
                }
                else
                {
                    File.Delete(Server.MapPath(@"..\" + PicUrl));
                    ViewModel.tblDictionary updatePicUrl = new ViewModel.tblDictionary();
                    updatePicUrl.PicUrl = "";
                    updatePicUrl.IDDictionary = hfIDDictionary.Value.StringToGuid();
                    int ret = BisDictionary.UpdatePicUrl(updatePicUrl);
                    if (ret <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال درخذف عکس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }
    }
}