using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class ContentCategoryContentMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public bool AddContentCategoryContent(ViewModel.tblContentCategoryContent tblContentCategoryContent)
        {
            return (sqlHelper.RunProcedure("sp_tblContentCategoryContent_Insert", tblContentCategoryContent) > 0);
        }
        public bool UpdateSortContentCategoryContent(ViewModel.tblContentCategoryContent tblContentCategoryContent)
        {
            return (sqlHelper.RunProcedure("sp_tblContentCategoryContent_UpdateSort", tblContentCategoryContent) > 0);
        }
        public bool DeleteContentCategoryContent(ViewModel.tblContentCategoryContent tblContentCategoryContent)
        {
            return (sqlHelper.RunProcedure("sp_tblContentCategoryContent_Delete", tblContentCategoryContent) > 0);
        }
    }
}