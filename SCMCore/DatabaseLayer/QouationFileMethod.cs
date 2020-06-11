using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class QouationFileMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetQouationFile(ViewModel.tblQouationFile QouationFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblQouationFile_GetQouationFile", QouationFile);
        }
        public bool AddQouationFileWithExcel(ViewModel.tblQouationFile QouationFile)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationFile_InsertWithExcel", QouationFile, true) > 0);
        }
        public bool AddQouationFileWithOutExcel(ViewModel.tblQouationFile QouationFile)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationFile_InsertWithOutExcel", QouationFile) > 0);
        }
        public bool UpdateQouationFile(ViewModel.tblQouationFile QouationFile)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationFile_Update", QouationFile) > 0);
        }
        public bool ChangeSortInQouationFile(ViewModel.tblQouationFile QouationFile)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationFile_UpdateSort", QouationFile) > 0);
        }
        public bool UpdateRatioQouationFile(ViewModel.tblQouationFile QouationFile)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationFile_UpdateRatioQouationFile", QouationFile, true) > 0);
        }
        public bool DeleteQouationFile(ViewModel.tblQouationFile QouationFile)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationFile_Delete", QouationFile, true) > 0);
        }
    }
}