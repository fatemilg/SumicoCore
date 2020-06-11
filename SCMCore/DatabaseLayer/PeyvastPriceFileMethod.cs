using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class PeyvastPriceFileMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetPeyvastPriceFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblPeyvastPriceFile_GetPeyvastPriceFile", PeyvastPriceFile);
        }
        public JArray GetStockValuePeyvast(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblPeyvastPriceFile_GetStockValueFromLastOne", PeyvastPriceFile);
        }
        public JArray CheckPartNumberInPeyvastPriceFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblPeyvastPriceFile_CheckPartNumberInPeyvastPriceFile", PeyvastPriceFile);
        }
        public bool AddPeyvastPriceFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPeyvastPriceFile_Insert", PeyvastPriceFile, true) > 0);
        }
        public bool UpdatePeyvastPriceFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPeyvastPriceFile_Update", PeyvastPriceFile) > 0);
        }
        public bool ChangeSortInPeyvastFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPeyvastPriceFile_UpdateSort", PeyvastPriceFile) > 0);
        }
        public bool UpdateRatioPeyvastPriceFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPeyvastPriceFile_UpdateRatio", PeyvastPriceFile, true) > 0);
        }
        public bool DeletePeyvastPriceFile(ViewModel.tblPeyvastPriceFile PeyvastPriceFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPeyvastPriceFile_Delete", PeyvastPriceFile, true) > 0);
        }
    }
}