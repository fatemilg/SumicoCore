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
    public class AttachInterfaceCategoryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetCategoryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachInterfaceCategory_GetData", search);
        }
        public JArray GetFolders(ViewModel.tblAttachInterfaceCategory AttachInterfaceCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblAttachInterfaceCategory_GetFolders", AttachInterfaceCategory);
        }
        public JArray LoadBreadcrumbs(ViewModel.tblAttachInterfaceCategory category)
        {
            return sqlHelper.ReturnJsonData("sp_tblAttachInterfaceCategory_LoadBreadcrumbs", category);
        }
        public JArray ListFoldersFiles(ViewModel.tblAttachInterfaceCategory AttachInterfaceCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblAttachInterfaceCategory_ListFoldersFiles", AttachInterfaceCategory);
        }
        public JArray ListFoldersFilesByIDXDefineDetailProduct(ViewModel.tblAttachInterfaceCategory AttachInterfaceCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblAttachInterfaceCategory_GetJsonDataByIDXDefineDetailProduct", AttachInterfaceCategory);
        }
        public bool AddCategory(ViewModel.tblAttachInterfaceCategory category)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachInterfaceCategory_Insert", category) > 0);
        }

        public bool UpdateCategory(ViewModel.tblAttachInterfaceCategory category)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachInterfaceCategory_Update", category) > 0);
        }
        public bool UpdateParentFolders(ViewModel.tblAttachInterfaceCategory category)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachInterfaceCategory_UpdatParentFolders", category) > 0);
        }
        public bool DeleteCategory(ViewModel.tblAttachInterfaceCategory category)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachInterfaceCategory_DeleteRow", category) > 0);
        }
    }
}