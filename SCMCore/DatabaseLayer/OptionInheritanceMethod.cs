using Newtonsoft.Json.Linq;
using SCMCore.Classes;


namespace SCMCore.DatabaseLayer
{
    public class OptionInheritanceMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetOptionInheritance(ViewModel.tblOptionInheritance OptionInheritance)
        {
            return sqlHelper.ReturnJsonData("sp_tblOptionInheritance_GetData", OptionInheritance);
        }
        public bool InitialOptionInheritance(ViewModel.tblOptionInheritance OptionInheritance)
        {
            return (sqlHelper.RunProcedure("sp_tblOptionInheritance_Initial", OptionInheritance) > 0);
        }
    }
}