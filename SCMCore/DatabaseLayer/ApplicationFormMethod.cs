using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using VMSite = SCMCore.ViewModelSite;

namespace SCMCore.DatabaseLayer
{
    public class ApplicationFormMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetApplicationFormData(ViewModel.tblApplicationForm ApplicationForm)
        {
            return sqlHelper.ReturnJsonData("sp_tblApplicationForm_GetData", ApplicationForm);
        }
        public bool AddApplicationForm(VMSite.ApplicationForm ApplicationForm)
        {
            return (sqlHelper.RunProcedure("sp_tblApplicationForm_Insert", ApplicationForm) > 0);
        }
    }
}