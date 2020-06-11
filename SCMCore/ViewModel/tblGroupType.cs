using System;

namespace SCMCore.ViewModel
{
    public class tblGroupType: Model.IGroupType
    {
        public Guid? IDGroupType { get; set; }
        public string Title_Fa { get; set; }
        public string UnicName { get; set; }
    }
}