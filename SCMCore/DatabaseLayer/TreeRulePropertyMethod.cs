using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class TreeRulePropertyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetTreeRulePropertyJsonDataByIDProduct(ViewModel.tblTreeRuleProperty tblTreeRuleProperty)
        {
            return sqlHelper.ReturnJsonData("sp_tblTreeRuleProperty_GetDataByIDProduct", tblTreeRuleProperty);
        }
        public bool AddTreeRuleProperty(ViewModel.tblTreeRuleProperty tblTreeRuleProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblTreeRuleProperty_Insert", tblTreeRuleProperty) > 0);
        }
    }
}