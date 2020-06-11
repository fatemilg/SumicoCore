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
    public class AttachCrmInterfaceMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        //crm
        public DataSet GetAttachCrmInterfaceDataJoinAttachCrm(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetDataJoinAttachCrm", search);
        }
        public DataSet GetAttachCrmInterfaceData_getUrlForDeleteInAttachCrm(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetUrlForDeleteInAttachCrm", search);
        }
        public DataSet GetAttachCrmInterfaceData_CustomerHistoryCrm(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetDataCustomerHistory", search);
        }




        //site
        public DataSet GetAttachCrmInterfaceDataJoinAttachSite(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetDataJoinAttachSite", search);
        }
        public DataSet GetAttachCrmInterfaceData_getUrlForDeleteInAttachSite(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetUrlForDeleteInAttachSite", search);
        }
        public DataSet GetAttachCrmInterfaceData_ContentSite(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetDataContentSite", search);
        }
        public DataSet GetAttachCrmInterfaceData_ProductSite(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetDataProductSite", search);
        }
        public DataSet GetAttachCrmInterfaceData_DefineDetailProductSite(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetDataDefineDetailProductSite", search);
        }
        public JArray GetAttachCrmInterfaceJsonData_DefineDetailProductSite(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblAttachCrmInterface_GetDataDefineDetailProductSite", search);
        }
        public bool AttachCrmInterface_Insert_FromLastFiles(ViewModel.tblAttachCrmInterface AttachCrmInterface)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachCrmInterface_Insert_FromLastFiles", AttachCrmInterface) > 0);
        }
        public DataSet GetAttachCrmInterface_GetData_CheckRepeat(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetData_CheckRepeat", search);
        }
        public DataSet GetAttachCrmInterface_GetData_MaxOrderByIDRet(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachCrmInterface_GetData_MaxOrderByIDRet", search);
        }



        //delete
        public bool DeleteAllAttachCrmInterfaceByUserForAttachSite(ViewModel.tblAttachCrmInterface AttachCrmInterface)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachCrmInterface_DeleteAllRowByUserForAttachSite", AttachCrmInterface, true) > 0);
        }
        public bool DeleteJustThisAttachCrmInterfaceByUserForAttachSite(ViewModel.tblAttachCrmInterface AttachCrmInterface)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachCrmInterface_DeleteJustThisRowByUserForAttachSite", AttachCrmInterface) > 0);
        }
    }
}