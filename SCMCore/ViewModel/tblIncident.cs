using System;
namespace SCMCore.ViewModel
{
    public class tblIncident : Model.IIncident
    {
        public Guid? IDIncident { get; set; }
        public int? IDX { get; set; }
        public Guid? IDIncidentCategory { get; set; }
        public string Name_Fa { get; set; }
        public string PicUrl { get; set; }
        public string Description_Fa { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}






