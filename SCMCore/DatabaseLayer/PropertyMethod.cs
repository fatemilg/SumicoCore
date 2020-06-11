using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class PropertyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetPropertyData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProperty_GetData", search);
        }
        public DataSet GetQuickData_WhithoutSort(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProperty_GetQuickData_WhithoutSort", search);
        }
        public DataSet GetQuickData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProperty_GetQuickData", search);
        }
        public DataSet GetCompleteChildProperty(ViewModel.tblProperty Property)
        {
            return sqlHelper.returnDataSet("sp_tblProperty_GetCompleteChildProperty", Property);
        }
        public bool AddProperty(ViewModel.tblProperty Property)
        {
            return (sqlHelper.RunProcedure("sp_tblProperty_Insert", Property) > 0);
        }

        public bool UpdateProperty(ViewModel.tblProperty Property)
        {
            return (sqlHelper.RunProcedure("sp_tblProperty_Update", Property) > 0);
        }
        public bool UpdatePicUrlProperty(ViewModel.tblProperty Property)
        {
            return (sqlHelper.RunProcedure("sp_tblProperty_UpdatePicUrl", Property) > 0);
        }

        public bool DeleteProperty(ViewModel.tblProperty Property)
        {
            return (sqlHelper.RunProcedure("sp_tblProperty_DeleteRow", Property) > 0);
        }
    }
}