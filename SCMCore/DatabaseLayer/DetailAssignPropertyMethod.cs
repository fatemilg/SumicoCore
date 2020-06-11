using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class DetailAssignPropertyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetDetailAssignPropertyData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDetailAssignProperty_GetData", search);
        }
        public DataSet CheckAssignItemsInTowCollection(ViewModel.tblDetailAssignProperty tblDetailAssignProperty)
        {
            return sqlHelper.returnDataSet("sp_tblDetailAssignProperty_CheckAssignItemsInTowCollection", tblDetailAssignProperty);
        }
        public bool AddDetailAssignProperty(ViewModel.tblDetailAssignProperty DetailAssignProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailAssignProperty_Insert", DetailAssignProperty) > 0);
        }

        public bool UpdateDetailAssignProperty(ViewModel.tblDetailAssignProperty DetailAssignProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailAssignProperty_Update", DetailAssignProperty) > 0);
        }
        public bool SetForShowInPorductCategory(ViewModel.tblDetailAssignProperty DetailAssignProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailAssignProperty_SetForShowInPorductCategory", DetailAssignProperty) > 0);
        }
        public bool DeleteDetailAssignProperty(ViewModel.tblDetailAssignProperty DetailAssignProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailAssignProperty_DeleteRow", DetailAssignProperty) > 0);
        }

    }
}