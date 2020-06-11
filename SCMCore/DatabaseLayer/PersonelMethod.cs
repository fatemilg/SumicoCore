using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;
namespace SCMCore.DatabaseLayer
{
    public class PersonelMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetPersonelData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblPersonel_GetData", search);
        }
        public JArray GetPersoneJsonlData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblPersonel_GetData", search);
        }
        public DataSet Login (ViewModel.tblPersonel personel)
        {
            return sqlHelper.returnDataSet("sp_tblPersonel_Login", personel);
        }
        public bool AddPersonel(ViewModel.tblPersonel personel)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonel_Insert", personel) > 0);
        }

        public bool UpdatePersonel(ViewModel.tblPersonel personel)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonel_Update", personel) > 0);
        }
        public bool UpdatePersonelChangePass(ViewModel.tblPersonel personel)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonelChangePass_Update", personel) > 0);
        }


        public bool DeletePersonel(ViewModel.tblPersonel personel)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonel_DeleteRow", personel) > 0);
        }
    }

}