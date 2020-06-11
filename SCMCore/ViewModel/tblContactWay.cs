using System;
namespace SCMCore.ViewModel
{
    public class tblContactWay : Model.IContactWay
    {
        public Guid? IDContactWay { get; set; }
        public Guid? IDContactWayType { get; set; }
        public Guid? IDUser { get; set; }
        public string Input { get; set; }
        public string InternalInput { get; set; }
        public int? Status { get; set; }

        public string ContactWayTypeName_Fa { get; set; }
    }
}






