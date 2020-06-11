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
    public partial class ProductProperty : System.Web.UI.Page
    {
        Bis.PropertyMethod BisProperty = new Bis.PropertyMethod();
        Bis.DetailAssignPropertyMethod BisDetailAssignProperty = new Bis.DetailAssignPropertyMethod();
        Bis.RulePropertyProductMethod BisRulePropertyProduct = new Bis.RulePropertyProductMethod();
        Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                filltvProperty();
            }
        }
        public void filltvProperty()
        {
            try
            {
                ViewModel.Search searchProperty = new ViewModel.Search();
                searchProperty.Order = " Order By tblProperty.[Sort]";
                DataSet ds = BisProperty.GetPropertyData(searchProperty);
                tvProperty.Nodes.Clear();
                TreeNode newNode = new TreeNode("ویژگی های تعریف شده", Guid.Empty.ToString());
                tvProperty.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDProperty", tvProperty);

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
                TreeNode newNode = new TreeNode("<div dir=\"ltr\">" + dr["Name_En"].ToString() + "</div>", dr[Key].ToString());
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


        protected void tvProperty_SelectedNodeChanged(object sender, EventArgs e)
        {
            ViewModel.Search SearchProperty = new ViewModel.Search();
            SearchProperty.Filter = " And tblProperty.IDProperty = '" + tvProperty.SelectedNode.Value + "'";
            DataSet dsSelectedProperty = BisProperty.GetPropertyData(SearchProperty);

            lblModalPropertyName.Text = dsSelectedProperty.ReturnDataSetField("Name_En");
            txtPropertyEditTitle_Fa.Text = dsSelectedProperty.ReturnDataSetField("Name_Fa");
            txtPropertyEditTitle_En.Text = dsSelectedProperty.ReturnDataSetField("Name_En");
            txtPropertyEditSort.Text = dsSelectedProperty.ReturnDataSetField("Sort");
            txtPropertyEditDescription_En.Text = dsSelectedProperty.ReturnDataSetField("Description_En");
            if (dsSelectedProperty.ReturnDataSetField("PicUrl") != "")
            {
                imagePropertyEdit.ImageUrl = "../" + dsSelectedProperty.ReturnDataSetField("PicUrl");
                imagePropertyEdit.Visible = true;
            }
            else
            {
                imagePropertyEdit.Visible = false;
            }

            hfIdProperty.Value = tvProperty.SelectedNode.Value;
            hfIdParentProperty.Value = dsSelectedProperty.ReturnDataSetField("IDParent");

            filltvEditProperty();
            tvEditProperty.ExpandAll();

            SelectTreeViewNode(tvEditProperty.Nodes, dsSelectedProperty.ReturnDataSetField("IDParent"));
            ScriptManager.RegisterStartupScript(this, GetType(), "modal", "$('#ModalPropertyEvents').modal('show');$('.EditPro').css(\"display\", \"none\");$('.AddPro').css(\"display\", \"none\");clearFulProperty();clearFulPropertyEdit()", true);
            tvProperty.SelectedNode.Selected = false;
            txtPropertyTitle_Fa.Text = "";
            txtPropertyTitle_En.Text = "";
            txtPropertyDescription_En.Text = "";
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

        protected bool checkAccess(string eventName)
        {
            ViewModel.tblEventUser getUserEvent = new ViewModel.tblEventUser();
            DataSet dsUser = (DataSet)Session["User"];
            getUserEvent.IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
            getUserEvent.EventName = eventName;
            DataSet ds = BisEventUser.CheckForAccessInEventUser(getUserEvent);
            if (ds.Null_Ds())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnAddSubProperty_Click(object sender, EventArgs e)
        {
            bool check = checkAccess(EventName.ListofEvents.AddProperty.ToString());
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }

            if (Page.IsValid)
            {
                if (txtPropertyTitle_En.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s4", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> عنوان(En) را وارد کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                //if (hfIdParentProperty.Value == "" || hfIdParentProperty.Value == Guid.Empty.ToString())
                //{
                    if (hfIdParentProperty.Value == "")
                    {
                        ViewModel.Search getProperty = new ViewModel.Search();
                        getProperty.Filter = " and tblProperty.IDParent = '" + Guid.Empty.ToString() + "' and tblProperty.Name_En='" + txtPropertyTitle_En.Text.Trim() + "'";
                        DataSet ds = BisProperty.GetPropertyData(getProperty);
                        if (!ds.Null_Ds())
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s5", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> عنوان(En) تکراری است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }

                    }

                    if (hfIdParentProperty.Value == Guid.Empty.ToString())
                    {
                        ViewModel.Search getProperty = new ViewModel.Search();
                        getProperty.Filter = " and tblProperty.IDParent = '" + hfIdProperty.Value + "' and tblProperty.Name_En='" + txtPropertyTitle_En.Text.Trim() + "'";
                        DataSet ds = BisProperty.GetPropertyData(getProperty);
                        if (!ds.Null_Ds())
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s5", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> عنوان(En) تکراری است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }
                    }


                    ViewModel.tblProperty newProperty = new ViewModel.tblProperty();
                    newProperty.Name_Fa = txtPropertyTitle_Fa.Text.FixFarsi();
                    newProperty.Name_En = txtPropertyTitle_En.Text;
                    newProperty.Description_Fa = "";
                    newProperty.Description_En = txtPropertyDescription_En.Text;
                    newProperty.IDParent = hfIdProperty.Value.StringToGuid();
                    newProperty.Sort = txtPropertySort.Text.StringToInt();
               
                    newProperty.Status = 1;
                    try
                    {
                        string GenerateName = Guid.NewGuid().ToString();
                        newProperty.IDProperty = Guid.NewGuid();
                        if (hfFilelName.Value != "")
                        {
                            string Prefix = hfFilelName.Value.Substring(hfFilelName.Value.IndexOf('.'), hfFilelName.Value.Length - hfFilelName.Value.IndexOf('.'));
                            if (Prefix == ".png" || Prefix == ".jpg" || Prefix == ".jpeg")
                            {
                                string url = "Picture/Property/" + GenerateName + Prefix;
                                newProperty.PicUrl = url;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> فایل باید با پسوند png یا jpg یا jpeg باشد!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                return;
                            }
                        }

                        bool ret = BisProperty.AddProperty(newProperty);
                        if (ret)
                        {
                            filltvProperty();
                            txtPropertyTitle_Fa.Text = "";
                            txtPropertyTitle_En.Text = "";
                            txtPropertySort.Text = "";
                            tvProperty.ExpandAll();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "UploadFiles", "UploadFiles('" + fulProperty.ClientID + "','../Picture/Property/','" + GenerateName + "');", true);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "S1", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalPropertyEvents').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s2", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }

                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s4", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> امکان ثبت آیتم برای این دسته مهیا نیست !</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                //}
            }

        }

        protected void btnEditProperty_Click(object sender, EventArgs e)
        {
            bool check = checkAccess(EventName.ListofEvents.EditProperty.ToString());
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }
            if (Page.IsValid)
            {
                if (txtPropertyEditTitle_En.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s7", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> عنوان(En) را وارد کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }

                ViewModel.Search SearchPropertyEdit = new ViewModel.Search();
                SearchPropertyEdit.Filter = " And tblProperty.IDProperty = '" + tvEditProperty.SelectedNode.Value + "'";
                DataSet dsSelectedPropertyEdit = BisProperty.GetPropertyData(SearchPropertyEdit);
                //if (!dsSelectedPropertyEdit.Null_Ds() && dsSelectedPropertyEdit.ReturnDataSetField("IDParent") != Guid.Empty.ToString())
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> امکان انتقال آیتم به دسته انتخاب شده مهیا نیست !</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                //    return;
                //}


                ViewModel.tblProperty CheckProperty = new ViewModel.tblProperty();
                CheckProperty.IDProperty = hfIdProperty.Value.StringToGuid();
                DataSet dsCheckChilds = BisProperty.GetCompleteChildProperty(CheckProperty);
                if (dsCheckChilds.Tables[0].Select("IDProperty = '" + tvEditProperty.SelectedNode.Value + "'").Count() == 0)
                {
                    ViewModel.tblProperty UpdateProperty = new ViewModel.tblProperty();
                    UpdateProperty.Name_Fa = txtPropertyEditTitle_Fa.Text.FixFarsi();
                    UpdateProperty.Name_En = txtPropertyEditTitle_En.Text;
                    UpdateProperty.Description_Fa = "";
                    UpdateProperty.Description_En = txtPropertyEditDescription_En.Text;
                    UpdateProperty.IDParent = tvEditProperty.SelectedNode.Value.StringToGuid();
                    UpdateProperty.Sort = txtPropertyEditSort.Text.StringToInt();
                    UpdateProperty.PicUrl = "";
                    UpdateProperty.Status = 1;
                    try
                    {
                        string GenerateName = Guid.NewGuid().ToString();

                        ViewModel.Search searchProperty = new ViewModel.Search();
                        searchProperty.Filter = " and tblProperty.IDProperty = '" + hfIdProperty.Value + "'";
                        DataSet dsProperty = BisProperty.GetPropertyData(searchProperty);
                        if (hfFilelNameEdit.Value == "")
                        {
                            UpdateProperty.PicUrl = dsProperty.ReturnDataSetField("PicUrl");
                        }
                        else
                        {
                            string Prefix = hfFilelNameEdit.Value.Substring(hfFilelNameEdit.Value.IndexOf('.'), hfFilelNameEdit.Value.Length - hfFilelNameEdit.Value.IndexOf('.'));
                            if (Prefix == ".png" || Prefix == ".jpg" || Prefix == ".jpeg")
                            {
                                string url = "Picture/Property/" + GenerateName + Prefix;
                                UpdateProperty.PicUrl = url;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> فایل باید با پسوند png یا jpg یا jpeg باشد!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                return;
                            }
                        }
                        UpdateProperty.IDProperty = hfIdProperty.Value.StringToGuid();
                        bool ret = BisProperty.UpdateProperty(UpdateProperty);
                        if (ret)
                        {
                            filltvProperty();
                            txtPropertyTitle_Fa.Text = "";
                            txtPropertyTitle_En.Text = "";
                            tvProperty.ExpandAll();

                            if (hfFilelNameEdit.Value != "" && dsProperty.ReturnDataSetField("PicUrl") != "") //faile jadidi entekhab shode va ghablan ham file entekhab shode boode pas bayad ghabli pak shavad
                            {
                                File.Delete(Server.MapPath("../" + dsProperty.ReturnDataSetField("PicUrl")));
                            }
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "UploadFiles", "UploadFiles('" + FulPropertyEdit.ClientID + "','../Picture/Property/','" + GenerateName + "');", true);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s1", "$('#ModalPropertyEvents').modal('hide'); bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ویرایش اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalPropertyEvents').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s2", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "s4", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> انتخاب این ویژگی به عنوان ویژگی بالادستی امکان پذیر نیست!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
        }

        protected void btnDeleteProperty_Click(object sender, EventArgs e)
        {
            bool check = checkAccess(EventName.ListofEvents.DeleteProperty.ToString());
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }
            ViewModel.Search getAssignProperty = new ViewModel.Search();
            getAssignProperty.Filter = " and tblDetailAssignProperty.IDProperty = '" + hfIdProperty.Value + "'";
            DataSet dsAssignProperty = BisDetailAssignProperty.GetDetailAssignPropertyData(getAssignProperty);
            if (!dsAssignProperty.Null_Ds())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>این ویژگی به گروهی انتساب داده شده است. امکان حذف موجو نیست!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }
            ViewModel.Search getRulePropertyProduct = new ViewModel.Search();
            getRulePropertyProduct.Filter = " and tblRulePropertyProduct.IDProperty = '" + hfIdProperty.Value + "'";
            DataSet dsRulePropertyProduct = BisRulePropertyProduct.GetRulePropertyProductData(getRulePropertyProduct);
            if (!dsRulePropertyProduct.Null_Ds())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>این ویژگی در تولید ترکیب محصولی استفاده شده است. امکان حذف موجو نیست!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }

            ViewModel.tblProperty CheckProperty = new ViewModel.tblProperty();
            CheckProperty.IDProperty = hfIdProperty.Value.StringToGuid();
            DataSet dsCheckChilds = BisProperty.GetCompleteChildProperty(CheckProperty);
            if (dsCheckChilds.Tables[0].Select("IDProperty <> '" + hfIdProperty.Value + "'").Count() == 0)
            {
                ViewModel.tblProperty UpdateProperty = new ViewModel.tblProperty();
                try
                {
                    UpdateProperty.IDProperty = hfIdProperty.Value.StringToGuid();
                    ViewModel.Search searchProperty = new ViewModel.Search();
                    searchProperty.Filter = " and tblProperty.IDProperty = '" + hfIdProperty.Value + "'";
                    DataSet dsProperty = BisProperty.GetPropertyData(searchProperty);
                    string picUrl = dsProperty.ReturnDataSetField("PicUrl");
                    bool ret = BisProperty.DeleteProperty(UpdateProperty);
                    if (ret)
                    {
                        if (picUrl != "")
                        { File.Delete(Server.MapPath("../" + picUrl)); }
                        filltvProperty();
                        txtPropertyEditTitle_Fa.Text = "";
                        txtPropertyEditTitle_En.Text = "";
                        txtPropertyEditSort.Text = "";
                        tvProperty.ExpandAll();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", "$('#ModalPropertyEvents').modal('hide'); bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalPropertyEvents').modal('hide');", true);

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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ابتدا باید ویژگی های پایین دستی این ویژگی  حذف شوند!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        public void filltvEditProperty()
        {
            try
            {
                ViewModel.Search searchProperty = new ViewModel.Search();
                searchProperty.Order = " ORder By tblProperty.[Sort]";
                DataSet ds = BisProperty.GetPropertyData(searchProperty);
                tvEditProperty.Nodes.Clear();
                TreeNode newNode = new TreeNode("ویژگی های تعریف شده", Guid.Empty.ToString());
                tvEditProperty.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDProperty", tvEditProperty);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }

        public bool ISDuplicateSort(Guid IDParent, int sort)
        {
            ViewModel.Search getProperty = new ViewModel.Search();
            getProperty.Filter = " and tblProperty.IDParent =  '" + IDParent + "' and tblProperty.Sort= '" + sort + "'";
            DataSet ds = BisProperty.GetPropertyData(getProperty);
            if (ds.Tables[0].Select("IDProperty <> '" + hfIdProperty.Value + "'").Count() == 0)
            {
                return false; //agar khali bood iani peida nakard ,va adade vared shode jadid ast
            }
            else
            {
                return true; // agar khali nabood iani peida karde ,va adade vared shode tekrari ast
            }
        }



        protected void delPicProperty_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "aa", "$('.AddPro').css(\"display\", \"none\");clearFulProperty();", true);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

            }

        }

        protected void delPicPropertyEdit_Click(object sender, EventArgs e)
        {
            try
            {
                imagePropertyEdit.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "aa", "$('.EditPro').css(\"display\", \"none\");clearFulPropertyEdit();", true);
                ViewModel.Search searchProperty = new ViewModel.Search();
                searchProperty.Filter = " and tblProperty.IDProperty = '" + hfIdProperty.Value + "'";
                DataSet dsProperty = BisProperty.GetPropertyData(searchProperty);
                string PicUrl = dsProperty.ReturnDataSetField("PicUrl");
                if (dsProperty.ReturnDataSetField("PicUrl") != "")
                {
                    ViewModel.tblProperty updatePicUrl = new ViewModel.tblProperty();
                    updatePicUrl.PicUrl = "";
                    updatePicUrl.IDProperty = hfIdProperty.Value.StringToGuid();
                    bool ret = BisProperty.UpdatePicUrlProperty(updatePicUrl);
                    if (ret)
                    {
                        File.Delete(Server.MapPath("../" + PicUrl));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف عکس!');", true);

                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

            }
        }
    }
}