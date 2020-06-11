using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class AttachCrmMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        //public DataSet GetAttachCrmData(ViewModel.Search search)
        //{
        //    return sqlHelper.returnDataSet("sp_tblAttachCrm_GetData", search);
        //}

        public bool AddAttachCrm(ViewModel.tblAttachCrm AttachCrm)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachCrm_Insert", AttachCrm) > 0);
        }

        //public bool UpdateAttachCrm(ViewModel.tblAttachCrm AttachCrm)
        //{
        //    return (sqlHelper.RunProcedure("sp_tblAttachCrm_Update", AttachCrm) > 0);
        //}

        public bool DeleteAttachCrm(ViewModel.tblAttachCrm AttachCrm)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachCrm_DeleteRow", AttachCrm) > 0);
        }
    }
}