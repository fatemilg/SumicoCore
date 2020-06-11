using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class MaterialListMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetMaterialListJsonDataByIDLogUser(ViewModelSite.tblMaterialList MaterialList)
        {
            return sqlHelper.ReturnJsonData("sp_tblMaterialList_GetDataByIDUser", MaterialList);
        }
        public bool AddMaterialList(ViewModel.tblMaterialList tblMaterialList)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialList_Insert", tblMaterialList) > 0);
        }
        public bool AddMaterialListInSite(ViewModelSite.tblMaterialList MaterialList)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialList_Insert", MaterialList) > 0);
        }
        public bool UpdateMaterialList(ViewModel.tblMaterialList tblMaterialList)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialList_Update", tblMaterialList) > 0);
        }
        public bool DeleteMaterialList(ViewModel.tblMaterialList tblMaterialList)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialList_Delete", tblMaterialList,true) > 0);
        }
    }
}






