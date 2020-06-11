using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class AssignPropertyRetMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetAssignPropertyRetDataByIDProduct(ViewModel.tblAssignPropertyRet tblAssignPropertyRet)
        {
            return sqlHelper.returnDataSet("sp_tblAssignPropertyRet_GetDataByIDProduct", tblAssignPropertyRet);
        }
    }
}