using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using Newtonsoft.Json.Linq;

namespace SCMCore.Classes
{
    //
    public class AuthorizationUser
    {
        Bis.LogUserMethods BislogUser = new Bis.LogUserMethods();
        public Guid ReturnIDUser(Guid? IDLogUser)
        {
            ViewModel.tblLogUser getLogUser = new ViewModel.tblLogUser();
            getLogUser.IDLogUser = IDLogUser;
            JArray jsLoguser = BislogUser.GetActiveUsersInLast24Hours(getLogUser);
            return jsLoguser[0]["IDUser"].ToString().StringToGuid();
        }
        public JArray ReturnUser(Guid? IDLogUser)
        {
            ViewModel.tblLogUser getLogUser = new ViewModel.tblLogUser();
            getLogUser.IDLogUser = IDLogUser;
            JArray jsLoguser = BislogUser.GetActiveUsersInLast24Hours(getLogUser);
            return jsLoguser;
        }
    }
}