using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using VMSite = SCMCore.ViewModelSite;

namespace SCMCore.DatabaseLayer
{
    public class ApplicationFormTypeMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetApplicationFormTypeData(ViewModel.tblApplicationFormType ApplicationFormType)
        {
            return sqlHelper.ReturnJsonData("sp_tblApplicationFormType_GetData", ApplicationFormType);
        }
    }
}