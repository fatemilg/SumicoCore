





using SCMCore.Classes; 
using Newtonsoft.Json.Linq; 
using System.Data; 
using ViewModel = SCMCore.ViewModel; 
namespace SCMCore.DatabaseLayer 
{ 
public class CompanyPersonelInCompanyMethod 
{ 
SqlHelper sqlHelper = new SqlHelper(); 
public JArray GetCompanyPersonelInCompanyJsonData(ViewModel.Search search) 
{ 
return sqlHelper.ReturnJsonData("sp_tblCompanyPersonelInCompany_GetData", search); 
} 
public bool AddCompanyPersonelInCompany(ViewModel.tblCompanyPersonelInCompany tblCompanyPersonelInCompany) 
{ 
return (sqlHelper.RunProcedure("sp_tblCompanyPersonelInCompany_Insert", tblCompanyPersonelInCompany) > 0); 
} 
public bool UpdateCompanyPersonelInCompany(ViewModel.tblCompanyPersonelInCompany tblCompanyPersonelInCompany) 
{ 
return (sqlHelper.RunProcedure("sp_tblCompanyPersonelInCompany_Update", tblCompanyPersonelInCompany) > 0); 
} 
public bool DeleteCompanyPersonelInCompany(ViewModel.tblCompanyPersonelInCompany tblCompanyPersonelInCompany) 
{ 
return (sqlHelper.RunProcedure("sp_tblCompanyPersonelInCompany_Delete", tblCompanyPersonelInCompany) > 0); 
} 
 } 
 }






