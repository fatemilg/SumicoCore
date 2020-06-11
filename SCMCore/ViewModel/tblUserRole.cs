using System;

namespace SCMCore.ViewModel
{
    public class tblUserRole : Model.IUserRole
    {
        public Guid? IDUserRole { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDRole { get; set; }

        public string UnicRoleName { get; set; }

    }
}