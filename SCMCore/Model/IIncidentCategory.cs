using System;
namespace SCMCore.Model
{
    interface IIncidentCategory
    {
        Guid? IDIncidentCategory { get; set; }
        int? IDX { get; set; }
        string Name_Fa { get; set; }
        Guid? IDParent { get; set; }
    }
}






