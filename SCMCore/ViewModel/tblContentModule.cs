using System;
namespace SCMCore.ViewModel
{
    public class tblContentModule:Model.IContentModule
    {
       public Guid? IDContentModule { get; set; }
       public string UniqueName { get; set; }
       public string Name_Fa { get; set; }
    }
}






