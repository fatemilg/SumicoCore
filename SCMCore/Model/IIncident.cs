using System;
namespace SCMCore.Model
{
    interface IIncident
    {
        Guid? IDIncident { get; set; }
        int? IDX { get; set; }
        Guid? IDIncidentCategory { get; set; }
        string Name_Fa { get; set; }
        string PicUrl { get; set; }
        string Description_Fa { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}






