using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Net;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;


namespace SCMCore.Controllers
{
    public class MaterialListPreparationController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.MaterialListPreparationMethod BisMaterialListPreparation = new Bis.MaterialListPreparationMethod();


        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCountNewMaterialListCreatedNotShown()
        {
            try
            {
                ViewModel.tblMaterialListPreparation MLP = new ViewModel.tblMaterialListPreparation();
                JArray JsonMaterialListPreparation = BisMaterialListPreparation.GetCountNewMaterialListCreatedNotShown(MLP);
                return Ok(JsonMaterialListPreparation);

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetNewMaterialListCreatedByCustomer()
        {
            try
            {
                ViewModel.tblMaterialListPreparation MLP = new ViewModel.tblMaterialListPreparation();
                JArray JsonMaterialListPreparation = BisMaterialListPreparation.GetNewMaterialListCreatedByCustomer(MLP);
                return Ok(JsonMaterialListPreparation);

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddMaterialListPreparationByCustomer(ViewModelSite.MaterialListPreparation obj)
        {
            try
            {
                bool ret = BisMaterialListPreparation.AddMaterialListPreparationByCustomer(obj);
                if(ret)
                {
                    return Ok(ret);

                }
                else
                {

                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
