using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.DatabaseLayer
{
    public class RelatePurchaseOrderFileMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetDataForPOHistory(ViewModel.tblRelatePurchaseOrderFile RelatePurchaseOrderFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblRelatePurchaseOrderFile_GetDataForPOHistory", RelatePurchaseOrderFile);
        }

        public bool AddRelatePurchaseOrderFile(ViewModel.tblRelatePurchaseOrderFile RelatePurchaseOrderFile)
        {
            return (sqlHelper.RunProcedure("sp_tblRelatePurchaseOrderFile_Insert", RelatePurchaseOrderFile,true) > 0);
        }
    }
}