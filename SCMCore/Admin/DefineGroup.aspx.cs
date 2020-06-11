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


namespace SCMCore.Admin
{
    public partial class DefineGroup : System.Web.UI.Page
    {
        Bis.UserGroupMethod BisUserGroup = new Bis.UserGroupMethod();
        Bis.GroupMethod BisGroup = new Bis.GroupMethod();
        Bis.GroupTypeMethod BisGroupType = new Bis.GroupTypeMethod();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                filltvGroup();
            }
        }

        public void filltvGroup()
        {
            try
            {
                ViewModel.Search searchGroup = new ViewModel.Search();
                searchGroup.Order = " ORder By tblGroup.[Order]";
                DataSet ds = BisGroup.GetGroupData(searchGroup);
                tvGroup.Nodes.Clear();
                TreeNode newNode = new TreeNode("گروه های کاربران", Guid.Empty.ToString());
                tvGroup.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDGroup", tvGroup);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }

        public void GetGroupType()
        {
            try
            {
                ViewModel.tblGroupType searchGroup = new ViewModel.tblGroupType();
                DataSet ds = BisGroupType.GetGroupTypeData(searchGroup);
                drpGroupType.DataSource = ds;
                drpGroupType.DataTextField = "Title_Fa";
                drpGroupType.DataValueField = "IDGroupType";
                drpGroupType.DataBind();

                drpGroupTypeEdit.DataSource = ds;
                drpGroupTypeEdit.DataTextField = "Title_Fa";
                drpGroupTypeEdit.DataValueField = "IDGroupType";
                drpGroupTypeEdit.DataBind();

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }
        private void BindTree(DataSet ds, TreeNode parentNode, string Key, TreeView tv)
        {
            DataRow[] ChildRows;
            if (parentNode == null)
            {
                string strExpr = "IDParent = '" + Guid.Empty.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            else
            {
                string strExpr = "IDParent = '" + parentNode.Value.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            foreach (DataRow dr in ChildRows)
            {
                TreeNode newNode = new TreeNode(dr["Title"].ToString(), dr[Key].ToString());
                if (parentNode == null)
                {
                    tv.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTree(ds, newNode, Key, tv);
            }
        }

        protected void grdUserGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDUserGroup = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Del":
                    try
                    {
                        ViewModel.tblUserGroup DeleteUserGroup = new ViewModel.tblUserGroup();
                        DeleteUserGroup.IDUserGroup = IDUserGroup;
                        bool ret = BisUserGroup.DeleteUserGroup(DeleteUserGroup);
                        if (ret)
                        {
                            fillGrdUserGroup(hfIdGroup.Value.StringToGuid());
                            NewFildsUserGroup();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                        }

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
            }
        }

        protected void grdUserGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdUserGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        public void fillGrdUserGroup(Guid IDGroup)
        {
            ViewModel.Search SearchUser = new ViewModel.Search();
            SearchUser.Filter = " And tblUserGroup.IDGroup = '" + IDGroup + "'";
            SearchUser.Order = "Order by Sort ";
            DataSet dsUser = BisUserGroup.GetUserGroupData(SearchUser);
            grdUserGroup.DataSource = dsUser;
            grdUserGroup.DataBind();
            Session["dsUserGroup"] = dsUser;
            Session["dsUserGroupIndex"] = dsUser;
        }

        protected void btnAddUserGroup_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                if (PersonelAutoComplete.IDUser == "" || PersonelAutoComplete.IDUser == Guid.Empty.ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا کاربر مورد نظر را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                DataSet dsCheck = (DataSet)Session["dsUserGroup"];
                int Check = dsCheck.Tables[0].Select("IDUser='" + PersonelAutoComplete.IDUser + "'").Count();
                if (Check == 0)
                {
                    ViewModel.tblUserGroup newUserGroup = new ViewModel.tblUserGroup();
                    newUserGroup.IDUser = PersonelAutoComplete.IDUser.StringToGuid();
                    newUserGroup.IDGroup = hfIdGroup.Value.StringToGuid();

                    try
                    {
                        newUserGroup.IDUserGroup = Guid.NewGuid();
                        bool ret = BisUserGroup.AddUserGroup(newUserGroup);
                        if (ret)
                        {
                            fillGrdUserGroup(hfIdGroup.Value.StringToGuid());
                            NewFildsUserGroup();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> این کاربر برای گروه " + lblModalGroupName.Text + " قبلا انتخاب شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    NewFildsUserGroup();
                }
            }
        }

        protected void btnNewUSerGroup_Click(object sender, EventArgs e)
        {


        }
        public void NewFildsUserGroup()
        {
            PersonelAutoComplete.NewFildsUser();
        }

        protected void tvGroup_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblModalGroupName.Text = tvGroup.SelectedNode.Text;
            txtGroupEditTitle.Text = tvGroup.SelectedNode.Text;

            GetGroupType();
            hfIdGroup.Value = tvGroup.SelectedNode.Value;
            fillGrdUserGroup(tvGroup.SelectedNode.Value.StringToGuid());

            filltvEditGroup();
            tvEditGroup.ExpandAll();
            ViewModel.Search SearchGroup = new ViewModel.Search();
            SearchGroup.Filter = " where tblGroup.IDGroup = '" + tvGroup.SelectedNode.Value + "'";
            DataSet dsSelectedGroup = BisGroup.GetGroupData(SearchGroup);
            if (dsSelectedGroup.ReturnDataSetField("IDParent") != "")
            {
                divGroupType.Visible = false;

            }
            else
            {
                divGroupType.Visible = true;
            }
            if (dsSelectedGroup.ReturnDataSetField("IDParent") != Guid.Empty.ToString())
            {
                divGroupTypeEdit.Visible = false;

            }
            else
            {
                divGroupTypeEdit.Visible = true;
                drpGroupTypeEdit.SelectedValue = dsSelectedGroup.ReturnDataSetField("IDGroupType");

            }

            SelectTreeViewNode(tvEditGroup.Nodes, dsSelectedGroup.ReturnDataSetField("IDParent"));
            ScriptManager.RegisterStartupScript(this, GetType(), "modal", "$('#ModalGroupEvents').modal('show');", true);
            tvGroup.SelectedNode.Selected = false;
        }

        protected void SelectTreeViewNode(TreeNodeCollection tn, string Value)
        {
            foreach (TreeNode node in tn)
            {
                if (node.Value == Value)
                {
                    node.Selected = true;
                }
                else
                {
                    foreach (TreeNode childNode in node.ChildNodes)
                    {
                        if (childNode.Value == Value)
                        {
                            childNode.Selected = true;
                        }
                        else
                        {
                            SelectTreeViewNode(childNode.ChildNodes, Value);
                        }
                    }
                }
            }
        }

        protected void grdUserGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnAddSubGroup_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblGroup newGroup = new ViewModel.tblGroup();
                newGroup.Title = txtGroupTitle.Text.FixFarsi();
                newGroup.IDParent = hfIdGroup.Value.StringToGuid();
                if (divGroupType.Visible)
                {
                    newGroup.IDGroupType = drpGroupType.SelectedValue.StringToGuid();

                }

                try
                {
                    newGroup.IDGroup = Guid.NewGuid();
                    bool ret = BisGroup.AddGroup(newGroup);
                    if (ret)
                    {
                        filltvGroup();
                        txtGroupTitle.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }

            }
        }

        protected void btnEditGroup_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblGroup CheckGroup = new ViewModel.tblGroup();
                CheckGroup.IDGroup = hfIdGroup.Value.StringToGuid();
                DataSet dsCheckChilds = BisGroup.GetCompleteChildGroup(CheckGroup);
                if (dsCheckChilds.Tables[0].Select("IDGroup = '" + tvEditGroup.SelectedNode.Value + "'").Count() == 0)
                {
                    ViewModel.tblGroup UpdateGroup = new ViewModel.tblGroup();
                    UpdateGroup.Title = txtGroupEditTitle.Text.FixFarsi();
                    UpdateGroup.IDParent = tvEditGroup.SelectedNode.Value.StringToGuid();
                    if (divGroupTypeEdit.Visible)
                    {
                        UpdateGroup.IDGroupType = drpGroupTypeEdit.SelectedValue.StringToGuid();

                    }
                    try
                    {
                        UpdateGroup.IDGroup = hfIdGroup.Value.StringToGuid();
                        bool ret = BisGroup.UpdateGroup(UpdateGroup);
                        if (ret)
                        {
                            filltvGroup();
                            txtGroupTitle.Text = "";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", "$('#ModalGroupEvents').modal('hide'); bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ویرایش اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> انتخاب این گروه به عنوان گروه بالادستی امکان پذیر نیست!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
        }

        protected void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            ViewModel.Search userGroup = new ViewModel.Search();
            userGroup.Filter = " and tblUserGroup.IDGroup='" + hfIdGroup.Value + "'";
            DataSet dsUserGroup = BisUserGroup.GetUserGroupData(userGroup);
            if (!dsUserGroup.Null_Ds())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> افرادی به این گروه انتساب داده شده اند.ابتدا آن ها را حذف کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }


            ViewModel.tblGroup CheckGroup = new ViewModel.tblGroup();
            CheckGroup.IDGroup = hfIdGroup.Value.StringToGuid();
            DataSet dsCheckChilds = BisGroup.GetCompleteChildGroup(CheckGroup);
            if (dsCheckChilds.Tables[0].Select("IDGroup <> '" + hfIdGroup.Value + "'").Count() == 0)
            {
                ViewModel.tblGroup UpdateGroup = new ViewModel.tblGroup();
                try
                {
                    UpdateGroup.IDGroup = hfIdGroup.Value.StringToGuid();
                    bool ret = BisGroup.DeleteGroup(UpdateGroup);
                    if (ret)
                    {
                        filltvGroup();
                        txtGroupTitle.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", "$('#ModalGroupEvents').modal('hide'); bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ابتدا باید گروه های پایین دستی این گروه  حذف شوند!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        public void filltvEditGroup()
        {
            try
            {
                ViewModel.Search searchGroup = new ViewModel.Search();
                searchGroup.Order = " ORder By tblGroup.[Order]";
                DataSet ds = BisGroup.GetGroupData(searchGroup);
                tvEditGroup.Nodes.Clear();
                TreeNode newNode = new TreeNode("گروه های کاربران", Guid.Empty.ToString());
                tvEditGroup.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDGroup", tvEditGroup);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }

        protected void txtSort_TextChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
                int index = row.RowIndex;
                TextBox txtSort = (TextBox)grdUserGroup.Rows[index].FindControl("txtSort");
                HiddenField hfIDUserGroup = (HiddenField)grdUserGroup.Rows[index].FindControl("hfIDUserGroup");

                ViewModel.tblUserGroup update = new ViewModel.tblUserGroup();
                update.IDUserGroup = hfIDUserGroup.Value.StringToGuid();
                update.Sort = txtSort.Text.StringToInt();
                bool ret = BisUserGroup.UpdateUserGroupBySort(update);
                if (ret)
                {
                    fillGrdUserGroup(hfIdGroup.Value.StringToGuid());
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اولویت!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                }

            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }

        }
    }
}