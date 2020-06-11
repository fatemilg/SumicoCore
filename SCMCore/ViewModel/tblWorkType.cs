using System;

namespace SCMCore.ViewModel
{
    public class tblWorkType : Model.IWorkType
    {
        public Guid? IDWorkType { get; set; }
        public Guid? ParentID { get; set; }
        public string Name_Fa { get; set; }
        public int? Status { get; set; }
    }
}