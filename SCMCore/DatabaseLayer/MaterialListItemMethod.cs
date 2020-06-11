using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class MaterialListItemMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetMaterialListItemByIDXMaterialList(ViewModelSite.MaterialListItem MaterialListItem)
        {
            return sqlHelper.ReturnJsonData("sp_tblMaterialListItem_GetDataByIDXMaterialList", MaterialListItem);
        }
        public bool AddMaterialListItem(ViewModelSite.MaterialListItem tblMaterialListItem)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialListItem_Insert", tblMaterialListItem) > 0);
        }
        public bool UpdateMaterialListItem(ViewModel.tblMaterialListItem tblMaterialListItem)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialListItem_Update", tblMaterialListItem) > 0);
        }
        public bool DeleteMaterialListItem(ViewModel.tblMaterialListItem tblMaterialListItem)
        {
            return (sqlHelper.RunProcedure("sp_tblMaterialListItem_Delete", tblMaterialListItem) > 0);
        }
    }
}






