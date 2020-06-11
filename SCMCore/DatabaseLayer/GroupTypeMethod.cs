using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class GroupTypeMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public DataSet GetGroupTypeData(ViewModel.tblGroupType tblGroupType)
        {
            return sqlHelper.returnDataSet("sp_tblGroupType_GetData", tblGroupType);
        }
    }
}