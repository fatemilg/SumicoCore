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
    public partial class DefineRole : System.Web.UI.Page
    {

        Bis.UserRoleMethod BisUserRole = new Bis.UserRoleMethod();
        Bis.RoleMenuMethod BisRoleMenu = new Bis.RoleMenuMethod();
        Bis.RoleMethod BisRole = new Bis.RoleMethod();
        Bis.MenuMethod BisMenu = new Bis.MenuMethod();
        DataSet dsUser = new DataSet();
        protected void Page_Init(object sender, EventArgs e)
        {

            dsUser = (DataSet)Session["User"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                newFiledRole();
                fillGrdRole();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblRole newRole = new ViewModel.tblRole();
                newRole.Title = txtTitle.Text.FixFarsi();
                newRole.Status = 1;
                switch (hfMode.Value)
                {
                    case "New":
                        try
                        {
                            newRole.IDRole = Guid.NewGuid();
                            if (checkNameOfRole(txtTitle.Text.FixFarsi(), ""))
                            {
                                bool ret = BisRole.AddRole(newRole);
                                if (ret)
                                {
                                    fillGrdRole();
                                    newFiledRole();
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>قبلا این نقش ثبت شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }

                        break;
                    case "Edit":
                        try
                        {
                            newRole.IDRole = hfIdRole.Value.StringToGuid();
                            if (checkNameOfRole(txtTitle.Text.FixFarsi(), hfIdRole.Value))
                            {
                                bool result = BisRole.UpdateRole(newRole);
                                if (result)
                                {
                                    fillGrdRole();
                                    hfMode.Value = "New";
                                    txtTitle.Text = "";
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات ویرایش شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>قبلا این نقش ثبت شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }

                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }

                        break;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchRole();
        }

        protected void grdRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDRole = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Edit":
                    try
                    {
                        hfIdRole.Value = IDRole.ToString();
                        ViewModel.Search getRole = new ViewModel.Search();
                        getRole.Filter = " and tblRole.IDRole ='" + IDRole + "'";
                        DataSet ds = BisRole.GetRoleData(getRole);
                        if (!ds.Null_Ds())
                        {
                            txtTitle.Text = ds.ReturnDataSetField("Title");
                            hfMode.Value = "Edit";
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در واکشی اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

                case "Del":
                    try
                    {
                        ViewModel.Search SearchUserRole = new ViewModel.Search();
                        SearchUserRole.Filter = " and tblUserRole.IDRole = '" + IDRole + "'";
                        DataSet dsCheckDeleteUserRole = BisUserRole.GetUserRoleData(SearchUserRole);
                        if (!dsCheckDeleteUserRole.Null_Ds())
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> این نقش دارای عضوهایی می باشد. ابتدا اعضا این نقش را حدف کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }
                        ViewModel.Search SearchRoleMenu = new ViewModel.Search();
                        SearchRoleMenu.Filter = " and tblRoleMenu.IDRole = '" + IDRole + "' and Access='true'";
                        DataSet dsCheckDeleteRoleMenu = BisRoleMenu.GetRoleMenuData(SearchRoleMenu);
                        if (!dsCheckDeleteRoleMenu.Null_Ds())
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> این نقش دارای منوهایی می باشد. ابتدا تیک ها را بردارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }

                        ViewModel.tblRole DeleteRole = new ViewModel.tblRole();
                        DeleteRole.IDRole = IDRole;
                        bool ret = BisRole.DeleteRole(DeleteRole);
                        if (ret)
                        {
                            fillGrdRole();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                        }

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

                case "Users":
                    try
                    {
                        hfIdRole.Value = IDRole.ToString();
                        NewFildsUserRole();
                        ViewModel.Search getRole = new ViewModel.Search();
                        getRole.Filter = " and tblRole.IDRole ='" + IDRole + "'";
                        DataSet ds = BisRole.GetRoleData(getRole);
                        if (!ds.Null_Ds())
                        {
                            fillGrdUserRole(IDRole);
                            lblRoleForUser.Text = ds.ReturnDataSetField("Title");
                            ScriptManager.RegisterStartupScript(this, GetType(), "modal", "$('#ModalUsers').modal('show');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در واکشی اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
                case "Menu":
                    try
                    {
                        hfIdRole.Value = IDRole.ToString();
                        ViewModel.Search getRole = new ViewModel.Search();
                        getRole.Filter = " and tblRole.IDRole ='" + IDRole + "'";
                        DataSet ds = BisRole.GetRoleData(getRole);
                        if (!ds.Null_Ds())
                        {
                            InitialRoleMenu(IDRole);
                            filltvMenu(IDRole.ToString());
                            lblRoleFormenu.Text = ds.ReturnDataSetField("Title");
                            ScriptManager.RegisterStartupScript(this, GetType(), "modal", "$('#ModalMenu').modal('show');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در واکشی اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

            }
        }

        protected void grdRole_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRole.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsRoleIndex"];
            grdRole.DataSource = ds;
            grdRole.DataBind();
        }

        public void filltvMenu(string IDRole)
        {
            try
            {
                ViewModel.Search searchMenuRole = new ViewModel.Search();
                searchMenuRole.Filter = "And tblRoleMenu.IDRole = '" + IDRole.ToString() + "' ";
                searchMenuRole.Order = " order by tblMenu.Name_Fa";
                DataSet ds = BisRoleMenu.GetRoleMenuData(searchMenuRole);
                tvMenu.Nodes.Clear();
                TreeNode newNode = new TreeNode("لیست منو ها", Guid.Empty.ToString());
                tvMenu.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDMenu", tvMenu);
                tvMenu.ExpandAll();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);
            }

        }
        private void BindTree(DataSet ds, TreeNode parentNode, string Key, TreeView tv)
        {
            DataRow[] ChildRows;
            if (parentNode == null)
            {
                string strExpr = "ParentID = '" + Guid.Empty.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            else
            {
                string strExpr = "ParentID = '" + parentNode.Value.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            foreach (DataRow dr in ChildRows)
            {
                TreeNode newNode = new TreeNode(ShowMenuAccess(dr["Access"].ToString().StringToBool()) + " " + dr["Name_Fa"].ToString(), dr[Key].ToString());
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
        public string ShowMenuAccess(bool Access)
        {
            if (Access)
            {
                return "<i class=\"fa  fa-check-square-o\"></i>";
            }
            else
            {
                return "<i class=\"fa fa-square-o\"></i>";
            }
        }
        public void InitialRoleMenu(Guid IDRole)
        {
            try
            {

                ViewModel.Search MenuSearch = new ViewModel.Search();
                ViewModel.tblRoleMenu RoleMenu = new ViewModel.tblRoleMenu();
                DataSet dsMenu = BisMenu.GetMenuData(MenuSearch);

                for (int i = 0; i < dsMenu.Tables[0].Rows.Count; i++)
                {
                    RoleMenu.IDRoleMenu = Guid.NewGuid();
                    RoleMenu.IDMenu = dsMenu.Tables[0].Rows[i]["IDMenu"].ToString().StringToGuid();
                    RoleMenu.IDRole = IDRole;
                    RoleMenu.Access = false;
                    BisRoleMenu.InserDataFromMenuToRoleMenu(RoleMenu);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);

            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            newFiledRole();
        }
        protected void newFiledRole()
        {
            txtTitle.Text = "";
            hfMode.Value = "New";
        }
        public void fillGrdRole()
        {
            ViewModel.Search SearchRole = new ViewModel.Search();
            SearchRole.Order = "Order by Title ";
            DataSet dsRole = BisRole.GetRoleData(SearchRole);
            grdRole.DataSource = dsRole;
            grdRole.DataBind();
            Session["dsRole"] = dsRole;
            Session["dsRoleIndex"] = dsRole;
        }
        public void searchRole()
        {
            DataSet dsSearch = (DataSet)Session["dsRole"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("Title like '%" + txtSearch.Text.FixFarsi() + "%'"))
            {
                dt.ImportRow(row);
            }
            grdRole.DataSource = dt;  // baraye search
            grdRole.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsRoleIndex"] = ds;
        }

        protected void grdUserRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDUserRole = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Del":
                    try
                    {
                        ViewModel.Search SearchUser = new ViewModel.Search();
                        SearchUser.Filter = " And tblUserRole.IDUserRole = '" + IDUserRole + "'";
                        DataSet dsUserRole = BisUserRole.GetUserRoleData(SearchUser);
                        if (dsUserRole.ReturnDataSetField("Developer") == "True" && dsUser.ReturnDataSetField("Developer") != "True")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>تیم برنامه نویس را نمی توانید حذف کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }

                        ViewModel.tblUserRole DeleteUserRole = new ViewModel.tblUserRole();
                        DeleteUserRole.IDUserRole = IDUserRole;
                        DeleteUserRole.IDRole = hfIdRole.Value.StringToGuid();
                        bool ret = BisUserRole.DeleteUserRole(DeleteUserRole);
                        if (ret)
                        {
                            fillGrdUserRole(hfIdRole.Value.StringToGuid());
                            NewFildsUserRole();
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

        protected void grdUserRole_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdUserRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdUserRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRole.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsUserRoleIndex "];
            grdRole.DataSource = ds;
            grdRole.DataBind();
        }

        public void fillGrdUserRole(Guid IDRole)
        {
            ViewModel.Search SearchUser = new ViewModel.Search();
            SearchUser.Filter = " And tblUserRole.IDRole = '" + IDRole + "'";
            SearchUser.Order = "Order by FullName ";
            DataSet dsUser = BisUserRole.GetUserRoleData(SearchUser);
            grdUserRole.DataSource = dsUser;
            grdUserRole.DataBind();
            Session["dsUserRole"] = dsUser;
            Session["dsUserRoleIndex"] = dsUser;
        }

        protected void btnAddUserRole_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                if (PersonelAutoComplete.IDUser == "" || PersonelAutoComplete.IDUser == Guid.Empty.ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا کاربر مورد نظر را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                DataSet dsCheck = (DataSet)Session["dsUserRole"];
                int Check = dsCheck.Tables[0].Select("IDUser = '" + PersonelAutoComplete.IDUser + "' ").Count();
                if (Check == 0)
                {
                    ViewModel.tblUserRole newUserRole = new ViewModel.tblUserRole();
                    newUserRole.IDUser = PersonelAutoComplete.IDUser.StringToGuid();
                    newUserRole.IDRole = hfIdRole.Value.StringToGuid();
                    switch (hfMode.Value)
                    {
                        case "New":
                            try
                            {
                                newUserRole.IDUserRole = Guid.NewGuid();
                                bool ret = BisUserRole.AddUserRole(newUserRole);
                                if (ret)
                                {
                                    fillGrdUserRole(hfIdRole.Value.StringToGuid());
                                    NewFildsUserRole();
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

                            break;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> این کاربر برای نقش " + lblRoleForUser.Text + " قبلا انتخاب شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    NewFildsUserRole();
                }
            }
        }

        protected void btnNewUSerRole_Click(object sender, EventArgs e)
        {
            NewFildsUserRole();
        }
        protected void NewFildsUserRole()
        {
            PersonelAutoComplete.NewFildsUser();
        }
        protected bool checkNameOfRole(string name, string ID)
        {
            ViewModel.Search searchName = new ViewModel.Search();
            if (ID != "")
            {
                searchName.Filter = " and tblRole.Title=N'" + name + "' and tblRole.IDRole <>'" + ID + "'";
            }
            else
            {
                searchName.Filter = " and tblRole.Title=N'" + name + "'";
            }
            DataSet ds = BisRole.GetRoleData(searchName);

            return ds.Null_Ds(); //قبلا با این نام  ثبت نشده است

        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            Guid IDMenu = tvMenu.SelectedValue.StringToGuid();
            ViewModel.Search searchOldNode = new ViewModel.Search();
            searchOldNode.Filter = " and tblRoleMenu.IDMenu = '" + IDMenu + "' and tblRoleMenu.IDRole ='" + hfIdRole.Value + "'";
            DataSet dsOldNode = BisRoleMenu.GetRoleMenuData(searchOldNode);


            ViewModel.tblRoleMenu roleMenu = new ViewModel.tblRoleMenu();
            roleMenu.IDRoleMenu = dsOldNode.ReturnDataSetField("IDRoleMenu").StringToGuid();
            roleMenu.IDMenu = IDMenu;
            roleMenu.IDRole = hfIdRole.Value.StringToGuid();
            roleMenu.Access = !dsOldNode.ReturnDataSetField("Access").StringToBool();

            try
            {

                bool ret = BisRoleMenu.UpdateRoleMenu(roleMenu);
                if (ret)
                {
                    ViewModel.Search searchNewNode = new ViewModel.Search();
                    searchNewNode.Filter = " and IDRoleMenu = '" + dsOldNode.ReturnDataSetField("IDRoleMenu").StringToGuid() + "'";
                    DataSet dsNewNode = BisRoleMenu.GetRoleMenuData(searchNewNode);

                    ViewModel.Search searchParentNodes = new ViewModel.Search();
                    searchParentNodes.Filter = " and tblRoleMenu.IDMenu = '" + dsNewNode.ReturnDataSetField("ParentID") + "' and tblRoleMenu.IDRole ='" + hfIdRole.Value + "'";
                    DataSet dsParentNodes = BisRoleMenu.GetRoleMenuData(searchParentNodes);

                    ViewModel.Search searchChildNodes = new ViewModel.Search();
                    searchChildNodes.Filter = " and tblMenu.ParentID = '" + dsNewNode.ReturnDataSetField("IDMenu") + "' and tblRoleMenu.IDRole ='" + hfIdRole.Value + "'";
                    DataSet dsChilNodes = BisRoleMenu.GetRoleMenuData(searchChildNodes);


                    // yani age pedar ra uncheck kardim hameye childha uncheck savand
                    if (!dsNewNode.ReturnDataSetField("Access").StringToBool())
                    {
                        foreach (DataRow dr in dsChilNodes.Tables[0].Rows)
                        {
                            roleMenu.IDRoleMenu = dr["IDRoleMenu"].ToString().StringToGuid();
                            roleMenu.Access = false;
                            ret = BisRoleMenu.UpdateRoleMenu(roleMenu);
                        }
                    }
                    if (dsNewNode.ReturnDataSetField("Access").StringToBool())
                    {
                        roleMenu.IDRoleMenu = dsParentNodes.ReturnDataSetField("IDRoleMenu").StringToGuid();
                        roleMenu.Access = true;
                        ret = BisRoleMenu.UpdateRoleMenu(roleMenu);
                    }
                    filltvMenu(hfIdRole.Value);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);
            }

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
        public bool ShowMenuRole()
        {
            if (dsUser.ReturnDataSetField("Developer").StringToBool())
            {
                return true; //faghat baraye sv ghable namaiesh ast
            }
            else
            {
                return false;
            }

        }
    }
}