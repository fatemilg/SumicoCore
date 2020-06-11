using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class DynamicFormController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.DynamicFormMethod BisDynamicForm = new Bis.DynamicFormMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDynamicForm()
        {
            try
            {
                ViewModel.tblDynamicForm DynamicFormSearch = new ViewModel.tblDynamicForm();
                JArray JsonDynamicForm = BisDynamicForm.GetDynamicFormJsonData(DynamicFormSearch);
                return Ok(JsonDynamicForm);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddDynamicForm(ViewModel.tblDynamicForm NewDynamicForm)
        {
            try
            {
                bool ret = BisDynamicForm.AddDynamicForm(NewDynamicForm);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateDynamicForm(ViewModel.tblDynamicForm UpdateDynamicForm)
        {
            try
            {
                bool ret = BisDynamicForm.UpdateDynamicForm(UpdateDynamicForm);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteDynamicForm(ViewModel.tblDynamicForm DelDynamicForm)
        {
            try
            {
                bool ret = BisDynamicForm.DeleteDynamicForm(DelDynamicForm);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}



