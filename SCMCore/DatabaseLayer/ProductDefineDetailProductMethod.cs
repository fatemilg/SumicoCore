using SCMCore.Classes;
using System.Data;
namespace SCMCore.DatabaseLayer
{
    public class ProductDefineDetailProductMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetProductDefineDetailProductData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProductDefineDetailProduct_GetData", search);
        }
        public bool AddProductDefineDetailProduct(ViewModel.tblProductDefineDetailProduct tblProductDefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblProductDefineDetailProduct_Insert", tblProductDefineDetailProduct) > 0);
        }
        public bool DeleteProductDefineDetailProduct(ViewModel.tblProductDefineDetailProduct tblProductDefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblProductDefineDetailProduct_Delete", tblProductDefineDetailProduct) > 0);
        }
    }
}