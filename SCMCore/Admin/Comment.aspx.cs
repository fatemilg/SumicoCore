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

namespace SCMCore.Admin
{
    public partial class Comment : System.Web.UI.Page
    {

        Bis.CommentMethod BisComment = new Bis.CommentMethod();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                divInfo.Visible = false;
                fillGrdComment();
                fillGrdProductComment();
                updateShowComment();


            }
        }
        public void fillGrdComment()
        {
            ViewModel.Search SearchComment = new ViewModel.Search();
            SearchComment.Filter = " and tblComment.IDContent not in (select IDProduct from tblProduct)";
            SearchComment.Order = " order by CreateDate desc";
            DataSet dsComment = BisComment.GetCommentData(SearchComment);
            grdComment.DataSource = dsComment;
            grdComment.DataBind();
            Session["dsSearch"] = dsComment;
            Session["dsSearchIndex"] = dsComment;
        }
        public void fillGrdProductComment()
        {
            ViewModel.Search SearchComment = new ViewModel.Search();
            SearchComment.Filter = " and tblComment.IDContent  in (select IDProduct from tblProduct)";
            SearchComment.Order = " order by CreateDate desc";
            DataSet dsProductComment = BisComment.GetCommentData(SearchComment);
            grdProductComment.DataSource = dsProductComment;
            grdProductComment.DataBind();
            Session["dsProductCommentSearch"] = dsProductComment;
            Session["dsProductCommentSearchIndex"] = dsProductComment;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblComment newComment = new ViewModel.tblComment();
                newComment.Comment = txtAdminComment.Text.FixFarsi();
                newComment.IDContent = hfIDContent.Value.StringToGuid();
                newComment.ParentCommentID = hfIdComment.Value.StringToGuid();
                newComment.SenderEmail = "";
                newComment.SenderWebSite = "";
                newComment.SenderFName = "";
                newComment.SenderLName = "";
                newComment.SenderID = Guid.Empty;
                newComment.Show = true;
                newComment.Status = 1;

                try
                {
                    bool ret;
                    if (((DataSet)Session["dsComment"]).Tables[0].Rows[0]["replyComment"].ToString() == "")
                    {

                        newComment.IDComment = Guid.NewGuid();
                        ret = BisComment.AddComment(newComment);
                    }
                    else
                    {

                        newComment.IDComment = Session["hfReplyID"].ToString().StringToGuid();
                        ret = BisComment.UpdateComment(newComment);
                    }
                    if (ret)
                    {

                        hfIdComment.Value = newComment.IDComment.ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ثبت شد!');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);

                    }


                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                }


            }
        }


        protected void grdComment_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Guid IDComment = e.CommandArgument.ToString().StringToGuid(); //delete
            switch (e.CommandName)
            {

                case "EditCommentAdmin":
                    try
                    {

                        hfIdComment.Value = IDComment.ToString();
                        ViewModel.Search getComment = new ViewModel.Search();
                        getComment.Filter = " and tblComment.IDComment ='" + IDComment + "'";

                        DataSet ds = BisComment.GetCommentData(getComment);
                        if (!ds.Null_Ds())
                        {
                            txtFullName.Text = ds.ReturnDataSetField("FullName");
                            txtEmail.Text = ds.ReturnDataSetField("SenderEmail");
                            txtWebSite.Text = ds.ReturnDataSetField("SenderWebSite");
                            txtUserComment.Text = ds.ReturnDataSetField("Comment");
                            txtAdminComment.Text = ds.ReturnDataSetField("ReplyComment");
                            if (ds.ReturnDataSetField("IDContent") != Guid.Empty.ToString())
                            {
                                txtContent.Text = ds.ReturnDataSetField("ContentName");
                            }
                            else
                            {
                                txtContent.Text = "تماس با ما";
                            }
                            hfIDContent.Value = ds.ReturnDataSetField("IDContent");

                            if (hfIDContent.Value == Guid.Empty.ToString())
                            {
                                divAdminComment.Visible = false;
                            }
                            else
                            {
                                divAdminComment.Visible = true;
                            }
                            Session["hfReplyID"] = ((HiddenField)grdComment.Rows[((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex].FindControl("hfReplyID")).Value;
                            divTable.Visible = false;
                            divProductTable.Visible = false;
                            divInfo.Visible = true;
                            Session["dsComment"] = ds;




                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در واکشی اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "DeleteCommentUser":

                    try
                    {

                        ViewModel.tblComment deleteCommentUser = new ViewModel.tblComment();
                        deleteCommentUser.IDContent = null;
                        deleteCommentUser.SenderID = null;
                        deleteCommentUser.ParentCommentID = null;
                        deleteCommentUser.IDComment = IDComment;
                        bool retDel = BisComment.DeleteComment(deleteCommentUser);
                        if (retDel)
                        {

                            fillGrdComment();
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "DeleteCommentAdmin":

                    try
                    {
                        ViewModel.tblComment deleteCommentAdmin = new ViewModel.tblComment();

                        deleteCommentAdmin.IDContent = null;
                        deleteCommentAdmin.SenderID = null;
                        deleteCommentAdmin.ParentCommentID = null;
                        Session["hfReplyID"] = ((HiddenField)grdComment.Rows[((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex].FindControl("hfReplyID")).Value;
                        deleteCommentAdmin.IDComment = Session["hfReplyID"].ToString().StringToGuid();
                        if (deleteCommentAdmin.IDComment != Guid.Empty)
                        {
                            bool retDel = BisComment.DeleteComment(deleteCommentAdmin);
                            if (retDel)
                            {

                                fillGrdComment();
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('مدیر نظری ثبت نکرده است!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
            }
        }

        protected void grdComment_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdComment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdComment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdComment.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsSearchIndex"];
            grdComment.DataSource = ds;
            grdComment.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {

            newFields();

        }
        public void newFields()
        {
            txtAdminComment.Text = "";
        }
        protected void btnGrid_Click(object sender, EventArgs e)
        {
            divTable.Visible = true;
            divProductTable.Visible = true;
            divInfo.Visible = false;
            fillGrdComment();
            fillGrdProductComment();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet dsSearch = (DataSet)Session["dsSearch"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("FullName like '%" + txtSearch.Text.FixFarsi() + "%' or Comment like '%" + txtSearch.Text.FixFarsi() + "%' or ReplyComment like '%" + txtSearch.Text.FixFarsi() + "%' or ContentName like '%" + txtSearch.Text.FixFarsi() + "%'"))
            {
                dt.ImportRow(row);
            }
            grdComment.DataSource = dt;  // baraye search
            grdComment.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsSearchIndex"] = ds;
        }
        public void updateShowComment()
        {
            ViewModel.tblComment updateComment = new ViewModel.tblComment();
            BisComment.UpdateShowComment();

        }
        public bool showDeleteButton(Guid IDContent)
        {
            if (IDContent == Guid.Empty)
                return false;
            else
                return true;

        }
        public string ReturnContentName(string ContentName)
        {
            if (ContentName == "")
                return "تماس با ما";
            else
                return ContentName;

        }


        // متد های زیر مرتبط با کامنت های محصولات می باشد

        protected void btnSearchCommentProduct_Click(object sender, EventArgs e)
        {
            DataSet dsSearch = (DataSet)Session["dsProductCommentSearch"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("FullName like '%" + txtSearchCommentProduct.Text.FixFarsi() + "%' or Comment like '%" + txtSearchCommentProduct.Text.FixFarsi() + "%' or ProductName like '%" + txtSearchCommentProduct.Text.FixFarsi() + "%' or ReplyComment like '%" + txtSearchCommentProduct.Text.FixFarsi() + "%'"))
            {
                dt.ImportRow(row);
            }
            grdProductComment.DataSource = dt;  // baraye search
            grdProductComment.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsProductCommentSearchIndex"] = ds;
        }

        protected void grdProductComment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDComment = e.CommandArgument.ToString().StringToGuid(); //delete
            switch (e.CommandName)
            {

                case "EditProductCommentAdmin":
                    try
                    {

                        hfIdComment.Value = IDComment.ToString();
                        ViewModel.Search getComment = new ViewModel.Search();
                        getComment.Filter = " and tblComment.IDComment ='" + IDComment + "'";

                        DataSet ds = BisComment.GetCommentData(getComment);
                        if (!ds.Null_Ds())
                        {
                            txtFullName.Text = ds.ReturnDataSetField("FullName");
                            txtEmail.Text = ds.ReturnDataSetField("SenderEmail");
                            txtWebSite.Text = ds.ReturnDataSetField("SenderWebSite");
                            txtUserComment.Text = ds.ReturnDataSetField("Comment");
                            txtAdminComment.Text = ds.ReturnDataSetField("ReplyComment");
                            txtContent.Text = ds.ReturnDataSetField("ProductName");
                            hfIDContent.Value = ds.ReturnDataSetField("IDContent");

                            if (hfIDContent.Value == Guid.Empty.ToString())
                            {
                                divAdminComment.Visible = false;
                            }
                            else
                            {
                                divAdminComment.Visible = true;
                            }
                            Session["hfReplyID"] = ((HiddenField)grdProductComment.Rows[((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex].FindControl("hfReplyID")).Value;
                            divTable.Visible = false;
                            divProductTable.Visible = false;
                            divInfo.Visible = true;
                            Session["dsComment"] = ds;




                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در واکشی اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "DeleteProductCommentUser":

                    try
                    {

                        ViewModel.tblComment deleteCommentUser = new ViewModel.tblComment();
                        deleteCommentUser.IDContent = null;
                        deleteCommentUser.SenderID = null;
                        deleteCommentUser.ParentCommentID = null;
                        deleteCommentUser.IDComment = IDComment;
                        bool retDel = BisComment.DeleteComment(deleteCommentUser);
                        if (retDel)
                        {

                            fillGrdProductComment();
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "DeleteProductCommentAdmin":

                    try
                    {
                        ViewModel.tblComment deleteCommentAdmin = new ViewModel.tblComment();

                        deleteCommentAdmin.IDContent = null;
                        deleteCommentAdmin.SenderID = null;
                        deleteCommentAdmin.ParentCommentID = null;
                        Session["hfReplyID"] = ((HiddenField)grdProductComment.Rows[((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex].FindControl("hfReplyID")).Value;
                        deleteCommentAdmin.IDComment = Session["hfReplyID"].ToString().StringToGuid();
                        if (deleteCommentAdmin.IDComment != Guid.Empty)
                        {
                            bool retDel = BisComment.DeleteComment(deleteCommentAdmin);
                            if (retDel)
                            {

                                fillGrdProductComment();
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('مدیر نظری ثبت نکرده است!');", true);
                        }

                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
            }
        }

        protected void grdProductComment_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdProductComment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdProductComment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProductComment.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsProductCommentSearchIndex"];
            grdProductComment.DataSource = ds;
            grdProductComment.DataBind();
        }

    }
}