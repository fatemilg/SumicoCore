using System;

namespace SCMCore.ViewModel
{
    public class tblApplicationFormType : Model.IApplicationFormType
    {

        public Guid? IDApplicationFormType { get; set; }
        public string Title { get; set; }
        public int? Sort { get; set; }

    }
}