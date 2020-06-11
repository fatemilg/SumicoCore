using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class CommentMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetCommentData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblComment_GetData", search);
        }
        public JArray GetCommentJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblComment_GetData", search);
        }
        public bool AddComment(ViewModel.tblComment Comment)
        {
            return (sqlHelper.RunProcedure("sp_tblComment_Insert", Comment) > 0);
        }

        public bool UpdateComment(ViewModel.tblComment Comment)
        {
            return (sqlHelper.RunProcedure("sp_tblComment_Update", Comment) > 0);
        }
        public bool UpdateShowComment()
        {
            return (sqlHelper.RunProcedure("sp_ShowComment_Update", null) > 0);
        }

        public bool DeleteComment(ViewModel.tblComment Comment)
        {
            return (sqlHelper.RunProcedure("sp_tblComment_DeleteRow", Comment) > 0);
        }
    }
}