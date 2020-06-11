using System;
namespace SCMCore.Model
{
    public interface IContentModule
    {
        Guid? IDContentModule { get; set; }
        string UniqueName { get; set; }
        string Name_Fa { get; set; }
    }
}






