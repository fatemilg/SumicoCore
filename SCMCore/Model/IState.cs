using System;

namespace SCMCore.Model
{
    public interface IState
    {
        Guid? IDState { get; set; }
        Guid? IDCountry { get; set; }
        string Name_Fa { get; set; }
        int? status { get; set; }
    }
}