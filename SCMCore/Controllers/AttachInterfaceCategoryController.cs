using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class AttachInterfaceCategoryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.AttachInterfaceCategoryMethod BisAttachInterfaceCategory = new Bis.AttachInterfaceCategoryMethod();

        // GET api/menu
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetFolders()
        {
            try
            {
                ViewModel.tblAttachInterfaceCategory FileManager = new ViewModel.tblAttachInterfaceCategory();
                JArray JsonFileManager = BisAttachInterfaceCategory.GetFolders(FileManager);
                return Ok(JsonFileManager);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ChangeParentFolders(object obj)
        {
            try
            {

                Bis.AttachInterfaceCategoryMethod BisAttachInterfaceCategory = new Bis.AttachInterfaceCategoryMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblAttachInterfaceCategory UpdateParent = JsonObject.ToObject<ViewModel.tblAttachInterfaceCategory>();
                bool ret = BisAttachInterfaceCategory.UpdateParentFolders(UpdateParent);
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
        public IHttpActionResult LoadBreadcrumbs(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblAttachInterfaceCategory AttachInterfaceCategoryParams = JsonObject.ToObject<ViewModel.tblAttachInterfaceCategory>();
                JArray JsonBreadcrumbs = BisAttachInterfaceCategory.LoadBreadcrumbs(AttachInterfaceCategoryParams);
                return Ok(JsonBreadcrumbs);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ListFoldersFiles(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblAttachInterfaceCategory AttachInterfaceCategoryParams = JsonObject.ToObject<ViewModel.tblAttachInterfaceCategory>();
                JArray JsonFoldersFiles = BisAttachInterfaceCategory.ListFoldersFiles(AttachInterfaceCategoryParams);
                return Ok(JsonFoldersFiles);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ListFoldersFilesByIDXDefineDetailProduct(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblAttachInterfaceCategory AttachInterfaceCategory = new ViewModel.tblAttachInterfaceCategory();
                AttachInterfaceCategory.IDXDefineDetailProduct = JsonObject["IDXDefineDetailProduct"].ToString().StringToInt();
                JArray JsonFoldersFiles = BisAttachInterfaceCategory.ListFoldersFilesByIDXDefineDetailProduct(AttachInterfaceCategory);
                return Ok(JsonFoldersFiles);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillOtherImagesOfDefineDetailProductByIDX(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                FileTypes ft = new FileTypes();
                Bis.AttachCrmInterfaceMethod bisAttachCrmInterfaceMethod = new Bis.AttachCrmInterfaceMethod();
                ViewModel.Search AttachCrmInterfaceSearch = new ViewModel.Search();
                AttachCrmInterfaceSearch.Filter = " and tblDefineDetailProduct.IDX = " + JsonObject["IDXDefineDetailProduct"].ToString().StringToInt() ;
                foreach (object img in ft.imgType())
                {
                    int index = ft.imgType().IndexOf(img);
                    if (index == 0)
                    {
                        AttachCrmInterfaceSearch.Filter += " and (tblAttachSite.FileType = '" + img.ToString() + "' or ";
                    }
                    else if (index == ft.imgType().Count - 1)
                    {
                        AttachCrmInterfaceSearch.Filter += " tblAttachSite.FileType = '" + img.ToString() + "')";
                    }
                    else
                    {
                        AttachCrmInterfaceSearch.Filter += "  tblAttachSite.FileType = '" + img.ToString() + "' or ";
                    }
                }
                AttachCrmInterfaceSearch.Order = "Order By tblAttachCrmInterface.[Order]";
                AttachCrmInterfaceSearch.JsonResult = " FOR JSON PATH";
                JArray JsonAttachCrmInterface = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceJsonData_DefineDetailProductSite(AttachCrmInterfaceSearch);
                return Ok(JsonAttachCrmInterface);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
