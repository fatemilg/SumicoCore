using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class GroupMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetGroupData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblGroup_GetData", search);
        }
        public JArray GetUserGroupByGroupTypeJsonData(ViewModel.tblGroupType GroupType)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserGroupByGroupType_GetData", GroupType);
        }
        public bool AddGroup(ViewModel.tblGroup Group)
        {
            return (sqlHelper.RunProcedure("sp_tblGroup_Insert", Group) > 0);
        }

        public bool UpdateGroup(ViewModel.tblGroup Group)
        {
            return (sqlHelper.RunProcedure("sp_tblGroup_Update", Group) > 0);
        }

        public bool DeleteGroup(ViewModel.tblGroup Group)
        {
            return (sqlHelper.RunProcedure("sp_tblGroup_DeleteRow", Group) > 0);
        }
        public DataSet GetCompleteChildGroup(ViewModel.tblGroup Group)
        {
            return sqlHelper.returnDataSet("sp_tblGroup_GetCompleteChildGroup", Group);
        }
    }
}