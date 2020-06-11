using System;

namespace SCMCore.ViewModel
{
    public class tblTechnicalProperty : Model.ITechnicalProperty
    {
        public Guid IDTechnicalProperty { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public int? Status { get; set; }
    }
}