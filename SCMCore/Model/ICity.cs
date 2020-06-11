using System;

namespace SCMCore.Model
{
    public interface ICity
    {
        Guid IDCity { get; set; }
        Guid? IDState { get; set; }
        string Name_Fa { get; set; }
        int? status { get; set; }
    }
}