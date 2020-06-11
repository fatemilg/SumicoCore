using System;
namespace SCMCore.ViewModel
{
    public class tblIncidentCategory : Model.IIncidentCategory
    {
        public Guid? IDIncidentCategory { get; set; }
        public int? IDX { get; set; }
        public string Name_Fa { get; set; }
        public Guid? IDParent { get; set; }
    }
}






