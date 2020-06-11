using System;

namespace SCMCore.ViewModel
{
    public class tblUserGroup : Model.IUserGroup
    {
        public Guid IDUserGroup { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDGroup { get; set; }
        public int? Sort { get; set; }

    }
}