using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;


namespace SCMCore.Controllers
{
    public class CommentController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.CommentMethod BisComment = new Bis.CommentMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillCommentByIDXContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search CommentSearch = new ViewModel.Search();
                CommentSearch.Filter = " AND tblComment.IDContent = (SELECT TOP 1 IDContent FROM tblContent WHERE IDX = " + JsonObject["IDX"].ToString() + ")";
                CommentSearch.Order = " ORDER BY tblComment.CreateDate DESC";
                CommentSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonComment = BisComment.GetCommentJsonData(CommentSearch);
                return Ok(JsonComment);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddComment(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblComment NewComment = JsonObject.ToObject<ViewModel.tblComment>();
                bool ret = BisComment.AddComment(NewComment);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateComment(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblComment UpdateComment = JsonObject.ToObject<ViewModel.tblComment>();
                bool ret = BisComment.UpdateComment(UpdateComment);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }

        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteComment(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblComment DelComment = JsonObject.ToObject<ViewModel.tblComment>();
                bool ret = BisComment.DeleteComment(DelComment);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }

        }
    }
}