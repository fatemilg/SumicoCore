using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Data;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;


namespace SCMCore.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        Bis.PersonelMethod BisPersonel = new Bis.PersonelMethod();
        Bis.LogUserMethods BislogUser = new Bis.LogUserMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            string Client = "";
            Projects project = new Projects();
            if (Request.QueryString["Client"] != null)
            {
                Client = Request.QueryString["Client"];
                if (project.ReturnClientName(Client) == "")
                {
                    btLogin.Visible = false;
                }
                else
                {
                    Response.Cookies["DomainName"].Value = project.ReturnClientName(Client);
                    Response.Cookies["ClientUrl"].Value = project.ReturnClientUrl(Client);
                    btLogin.Visible = true;
                }
            }
            else
            {
                btLogin.Visible = false;
            }

        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.tblPersonel newPersonel = new ViewModel.tblPersonel();
                newPersonel.UserName = txtUserName.Text.FixFarsi();
                newPersonel.Password = txtPassword.Text.FixFarsi().EncryptData();
                DataSet dsLogin = BisPersonel.Login(newPersonel);
                if (dsLogin.Null_Ds())
                {
                    lblMessage.Text = "نام کاربری یا کلمه عبور صحیح نیست. ";
                }
                else
                {

                    Session["User"] = dsLogin;
                    Response.Cookies["UserName"].Value = dsLogin.ReturnDataSetField("UserName");
                    ViewModel.tblLogUser NewlogUser = new ViewModel.tblLogUser();
                    NewlogUser.IDLogUser = Guid.NewGuid();
                    NewlogUser.IDRet = Guid.Empty;
                    NewlogUser.UserAction = "login";
                    NewlogUser.IDUser = dsLogin.ReturnDataSetField("IDUser").StringToGuid();
                    NewlogUser.IDTableName = Guid.Empty;
                    bool ret = BislogUser.AddLogUser(NewlogUser);

                    Response.Cookies["IDLogUser"].Value = NewlogUser.IDLogUser.ToString();
                    Response.Redirect("Default.aspx");

                }

            }
            catch { }
        }
    }
}