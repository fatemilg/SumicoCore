using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class MaterialListPreparationMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetCountNewMaterialListCreatedNotShown(ViewModel.tblMaterialListPreparation MaterialListPreparation)
        {
            return sqlHelper.ReturnJsonData("sp_tblMaterialListPreparation_GetCountNewMaterialListCreatedNotShown", MaterialListPreparation);
        }
        public JArray GetNewMaterialListCreatedByCustomer(ViewModel.tblMaterialListPreparation MaterialListPreparation)
        {
            return sqlHelper.ReturnJsonData("sp_tblMaterialListPreparation_GetNewMaterialListCreatedByCustomer", MaterialListPreparation);
        }
        public bool AddMaterialListPreparationByCustomer(ViewModelSite.MaterialListPreparation MaterialListPreparation)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialListPreparation_InsertByCustomer", MaterialListPreparation,true) > 0);
        }
    }
}