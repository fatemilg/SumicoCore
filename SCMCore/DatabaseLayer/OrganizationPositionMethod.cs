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
    public class OrganizationalPositionMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetOrganizationalPositionData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblOrganizationPosition_GetData", search);
        }
        public JArray GetOrganizationalPositionJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblOrganizationPosition_GetData", search);
        }

        public bool AddOrganizationalPosition(ViewModel.tblOrganizationalPosition OrganizationalPosition)
        {
            return (sqlHelper.RunProcedure("sp_tblOrganizationalPosition_Insert", OrganizationalPosition) > 0);
        }

        public bool UpdateOrganizationalPosition(ViewModel.tblOrganizationalPosition OrganizationalPosition)
        {
            return (sqlHelper.RunProcedure("sp_tblOrganizationalPosition_Update", OrganizationalPosition) > 0);
        }

        public bool DeleteOrganizationalPosition(ViewModel.tblOrganizationalPosition OrganizationalPosition)
        {
            return (sqlHelper.RunProcedure("sp_tblOrganizationalPosition_DeleteRow", OrganizationalPosition) > 0);
        }
    }
}