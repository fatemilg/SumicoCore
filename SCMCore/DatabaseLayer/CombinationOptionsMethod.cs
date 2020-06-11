using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class CombinationOptionsMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetCombinationOptionsByIDSeller(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return sqlHelper.ReturnJsonData("sp_tblCombinationOptions_GetDataByIDSeller", CombinationOptions);
        }
        public JArray GetCountCombinationOptions(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return sqlHelper.ReturnJsonData("sp_tblCombinationOptions_GetCountCombinationOptions", CombinationOptions);
        }
        public JArray GetRestOtItemsNotExistForOtherLevel(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return sqlHelper.ReturnJsonData("sp_tblCombinationOptions_GetRestOtItemsNotExistForOtherLevel", CombinationOptions);
        }
        public JArray GetRestOtItemsNotExistInRootByIDOptionInheritance(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return sqlHelper.ReturnJsonData("sp_tblCombinationOptions_GetRestOtItemsNotExistInRootByIDOptionInheritance", CombinationOptions);
        }
        public JArray GetRestOtItemsNotExistInRoot(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return sqlHelper.ReturnJsonData("sp_tblCombinationOptions_GetRestOtItemsNotExistInRoot", CombinationOptions);
        }
        public bool AddCombinationOptionsForRoot(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return (sqlHelper.RunProcedure("sp_tblCombinationOptions_InsertForRoot", CombinationOptions) > 0);
        }
        public bool AddCombinationOptionsForNextLevels(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return (sqlHelper.RunProcedure("sp_tblCombinationOptions_InsertForNextLevels", CombinationOptions) > 0);
        }
        public bool DeleteCombinationOptionsBYIDCombinationOptions(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return (sqlHelper.RunProcedure("sp_tblCombinationOptions_DeleteBYIDCombinationOptions", CombinationOptions) > 0);
        }
        public bool DeleteAllCombination(ViewModel.tblCombinationOptions CombinationOptions)
        {
            return (sqlHelper.RunProcedure("sp_tblCombinationOptions_DeleteAllCombination", CombinationOptions,true) > 0);
        }
    }
}