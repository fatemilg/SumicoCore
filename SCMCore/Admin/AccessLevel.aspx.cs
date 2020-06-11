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
using System.Collections;

namespace SCMCore.Admin
{
    public partial class AccessLevel : System.Web.UI.Page
    {
        Bis.PersonelMethod BisPersonel = new Bis.PersonelMethod();
        Bis.AccessLevelMethod BisAccessLevel = new Bis.AccessLevelMethod();
        Bis.MenuMethod BisMenu = new Bis.MenuMethod();
        Bis.RoleMethod BisRole = new Bis.RoleMethod();
        Bis.RoleMenuMethod BisRoleMenu = new Bis.RoleMenuMethod();
        Bis.UserRoleMethod BisUserRole = new Bis.UserRoleMethod();
        Bis.EventMethods BisEvent = new Bis.EventMethods();
        Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDrpUser();
            }

        }

        public void filltvAccessLevel(string IDUser)
        {
            try
            {
                ViewModel.Search searchAccessLevel = new ViewModel.Search();
                searchAccessLevel.Filter = "And IDUser = '" + IDUser.ToString() + "'";
                searchAccessLevel.Order = " Order by tblMenu.Name_Fa ";
                DataSet ds = BisAccessLevel.GetAccessLevelDataForTree(searchAccessLevel);
                tvAccesslevel.Nodes.Clear();
                TreeNode newNode = new TreeNode("لیست منو ها", Guid.Empty.ToString());
                tvAccesslevel.Nodes.Add(newNode);
                BindTreeForAccessLevel(ds, newNode, "IDMenu", tvAccesslevel);
                tvAccesslevel.ExpandAll();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);
            }

        }
        public void filltvEvent(string IDMenu)
        {
            try
            {
                ViewModel.Search getEvent = new ViewModel.Search();
                getEvent.Filter = "And tblEvent.IDMenu = '" + IDMenu + "'";
                getEvent.Order = " Order by tblEvent.Title_En ";
                DataSet dsEvent = BisEvent.GetEventData(getEvent);
                tvEvent.Nodes.Clear();
                TreeNode newNode = new TreeNode("لیست رویداد ها", Guid.Empty.ToString());
                tvEvent.Nodes.Add(newNode);
                BindTreeForEventUser(dsEvent, newNode, "IDEvent", tvAccesslevel);
                tvEvent.ExpandAll();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);
            }

        }

        private void BindTreeForAccessLevel(DataSet ds, TreeNode parentNode, string Key, TreeView tv)
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
                TreeNode newNode = new TreeNode(ShowAccess(dr["Access"].ToString().StringToBool()) + " " + dr["Name_Fa"].ToString(), dr[Key].ToString());
                if (parentNode == null)
                {
                    tv.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTreeForAccessLevel(ds, newNode, Key, tv);
            }
        }
        private void BindTreeForEventUser(DataSet ds, TreeNode parentNode, string Key, TreeView tv)
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
                TreeNode newNode = new TreeNode(ShowEvent(dr["IDEvent"].ToString()) + " " + dr["Title_Fa"].ToString(), dr[Key].ToString());
                if (parentNode == null)
                {
                    tv.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTreeForEventUser(ds, newNode, Key, tv);
            }
        }

        public string ShowAccess(bool Access)
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
        public string ShowEvent(string IDEvent)
        {
            ViewModel.Search getEventUser = new ViewModel.Search();
            getEventUser.Filter = "  tblEventUser.IDUser='" + drpUser.SelectedValue + "' and tblEventUser.IDEvent='" + IDEvent + "'";
            DataSet dsEventUser = BisEventUser.GetEventUserData(getEventUser);
            if (dsEventUser.Null_Ds())
            {
                return "<i class=\"fa fa-square-o\"></i>";

            }
            else
            {
                return "<i class=\"fa  fa-check-square-o\"></i>";
               
            }
        }

        protected void tvAccesslevel_SelectedNodeChanged(object sender, EventArgs e)
        {


            try
            {

                Guid IDMenu = tvAccesslevel.SelectedValue.StringToGuid();
                hfIDMenuForEvent.Value = IDMenu.ToString();

                ViewModel.Search getMenu = new ViewModel.Search();
                getMenu.Filter = " and tblMenu.IDMenu = '" + IDMenu + "'";
                DataSet dsMenu = BisMenu.GetMenuData(getMenu);
                lblMenuName.Text = dsMenu.ReturnDataSetField("Name_Fa");

                ViewModel.Search searchAccessLevel = new ViewModel.Search();
                searchAccessLevel.Filter = "And IDUser = '" + drpUser.SelectedValue + "' and tblMenu.IDMenu = '" + IDMenu + "'";
                DataSet dsAccess = BisAccessLevel.GetAccessLevelDataForTree(searchAccessLevel);
                if (dsAccess.ReturnDataSetField("Access") == "True")
                {
                    chkSelectMenu.Checked = true;
                    panelEvent.Enabled = true;
                    panelEvent.Style.Add("background-color", "white");
                }
                else
                {
                    chkSelectMenu.Checked = false;
                    panelEvent.Enabled = false;
                    panelEvent.Style.Add("background-color", "lightgray");
                }

                filltvEvent(IDMenu.ToString());
                tvAccesslevel.SelectedNode.Selected = false;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalAssignEvent').modal('show');$('#ModalAssignEvent').css('overflow','hidden')", true);


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

        protected void fillDrpUser()
        {
            try
            {
                ViewModel.Search user = new ViewModel.Search();
                user.Filter = "and Active='True' ";
                user.Order = "  order by FName Asc";
                DataSet ds = BisPersonel.GetPersonelData(user);
                drpUser.DataSource = ds;

                drpUser.DataTextField = "FullName";
                drpUser.DataValueField = "IDUser";
                drpUser.DataBind();
                drpUser.Items.Insert(0, new ListItem("-انتخاب کنید -", Guid.Empty.ToString()));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

            }

        }
        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpUser.SelectedValue != Guid.Empty.ToString())
            {
                tvAccesslevel.Visible = true;
                ViewModel.Search getUserRole = new ViewModel.Search();
                getUserRole.Filter = " and tblUserRole.IDUser='" + drpUser.SelectedValue + "'";
                DataSet dsUserRole = BisUserRole.GetUserRoleData(getUserRole);
                if (dsUserRole.Null_Ds())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('برای این پرسنل نقشی تعریف نشده است!');", true);
                    tvAccesslevel.Visible = false;
                    return;
                }
                else
                {
                    for (int i = 0; i < dsUserRole.Tables[0].Rows.Count; i++)
                    {
                        InitialAccessLevel(dsUserRole.Tables[0].Rows[i]["IDRole"].ToString());
                    }
                    filltvAccessLevel(drpUser.SelectedValue);
                }

            }
            else
            {
                tvAccesslevel.Visible = false;
            }


        }


        public void InitialAccessLevel(string IDRole)
        {
            try
            {
                ViewModel.Search roleMenu = new ViewModel.Search();
                roleMenu.Filter = " and tblRole.IDRole='" + IDRole + "' and tblRoleMenu.Access ='true'";
                DataSet dsRoleMenu = BisRoleMenu.GetRoleMenuData(roleMenu);

                ViewModel.tblAccessLevel AccessLevelModel = new ViewModel.tblAccessLevel();
                for (int i = 0; i < dsRoleMenu.Tables[0].Rows.Count; i++)
                {
                    AccessLevelModel.IDAccessLevel = Guid.NewGuid();
                    AccessLevelModel.IDMenu = dsRoleMenu.Tables[0].Rows[i]["IDMenu"].ToString().StringToGuid();
                    AccessLevelModel.IDRole = IDRole.StringToGuid();
                    AccessLevelModel.IDUser = drpUser.SelectedValue.StringToGuid();
                    BisAccessLevel.InserDataFromMenuToAccessLevelAndGetData(AccessLevelModel);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);

            }

        }




        protected void chkSelectMenu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                ViewModel.tblAccessLevel AccessLevelModel = new ViewModel.tblAccessLevel();
                AccessLevelModel.IDMenu = hfIDMenuForEvent.Value.StringToGuid();
                AccessLevelModel.IDUser = drpUser.SelectedValue.StringToGuid();
                if (chkSelectMenu.Checked)
                {
                    panelEvent.Enabled = true;
                    panelEvent.Style.Add("background-color", "white");

                    AccessLevelModel.Access = true;
                    bool ret1 = BisAccessLevel.UpdateAccessLevel(AccessLevelModel);


                    // vaghti child click mishavad va parent tik nadasht,baraie parent tik mizanad
                    ViewModel.Search getMenu = new ViewModel.Search();
                    getMenu.Filter = " and tblMenu.IDMenu = '" + hfIDMenuForEvent.Value + "'";
                    DataSet dsMenu = BisMenu.GetMenuData(getMenu);

                    if (dsMenu.ReturnDataSetField("ParentID") != Guid.Empty.ToString())
                    {
                        AccessLevelModel.IDMenu = dsMenu.ReturnDataSetField("ParentID").StringToGuid();
                        AccessLevelModel.IDUser = drpUser.SelectedValue.StringToGuid();
                        AccessLevelModel.Access = true;
                        bool ret2 = BisAccessLevel.UpdateAccessLevel(AccessLevelModel);
                        if (!ret2)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);
                        }
                    }

                }
                else
                {
                    panelEvent.Enabled = false;
                    panelEvent.Style.Add("background-color", "lightgray");
                    AccessLevelModel.Access = false;
                    bool ret1 = BisAccessLevel.UpdateAccessLevel(AccessLevelModel);
                    if (!ret1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال !');", true);
                    }

                    //bayad tamame eventaie in menu pak shabad
                    deleteEventUser(hfIDMenuForEvent.Value);

                    // yani age pedar ra uncheck kardim hameye childha uncheck savand
                    ViewModel.Search searchChildNodes = new ViewModel.Search();
                    searchChildNodes.Filter = " and tblMenu.ParentID = '" + hfIDMenuForEvent.Value + "' and tblAccessLevel.IDUser ='" + drpUser.SelectedValue + "'";
                    DataSet dsChilNodes = BisAccessLevel.GetAccessLevelDataForTree(searchChildNodes);

                    foreach (DataRow dr in dsChilNodes.Tables[0].Rows)
                    {
                        AccessLevelModel.IDMenu = dr["IDMenu"].ToString().StringToGuid();
                        AccessLevelModel.IDUser = drpUser.SelectedValue.StringToGuid();
                        AccessLevelModel.Access = false;
                        bool ret2 = BisAccessLevel.UpdateAccessLevel(AccessLevelModel);
                        if (!ret2)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال !');", true);
                        }

                        deleteEventUser(dr["IDMenu"].ToString());
                    }


                    filltvEvent(hfIDMenuForEvent.Value);



                }
                filltvAccessLevel(drpUser.SelectedValue);
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);

            }
        }
        protected void deleteEventUser(string IDMenu)
        {
            try
            {


                ViewModel.Search getEvent = new ViewModel.Search();
                getEvent.Filter = " and tblEvent.IDMenu = '" + IDMenu + "'";
                DataSet dsEvent = BisEvent.GetEventData(getEvent);


                ViewModel.tblEventUser DeleteEventUser = new ViewModel.tblEventUser();
                foreach (DataRow dr in dsEvent.Tables[0].Rows)
                {
                    DeleteEventUser.IDEvent = dr["IDEvent"].ToString().StringToGuid();
                    DeleteEventUser.IDUser = drpUser.SelectedValue.StringToGuid();
                    bool ret3 = BisEventUser.DeleteEventUser(DeleteEventUser);
                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);

            }
        }

        protected void tvEvent_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                Guid IDEvent = tvEvent.SelectedValue.StringToGuid();
                ViewModel.Search getEventUser = new ViewModel.Search();
                getEventUser.Filter = " tblEventUser.IDEvent = '" + IDEvent + "' and tblEventUser.IDUser= '" + drpUser.SelectedValue + "'";
                DataSet dsEventUser = BisEventUser.GetEventUserData(getEventUser);

                if (dsEventUser.Null_Ds())
                {

                    ViewModel.tblEventUser AddEventUser = new ViewModel.tblEventUser();
                    AddEventUser.IDEvent = IDEvent;
                    AddEventUser.IDUser = drpUser.SelectedValue.StringToGuid();
                    bool ret3 = BisEventUser.AddEventUser(AddEventUser);
           
                }
                else
                {
                    ViewModel.tblEventUser DeleteEventUser = new ViewModel.tblEventUser();
                    DeleteEventUser.IDEvent = IDEvent;
                    DeleteEventUser.IDUser = drpUser.SelectedValue.StringToGuid();
                    bool ret3 = BisEventUser.DeleteEventUser(DeleteEventUser);
                 
                }

                filltvEvent(hfIDMenuForEvent.Value);

            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط بادیتابیس!');", true);

            }

        }
    }
}