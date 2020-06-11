using System;

namespace SCMCore.ViewModel
{
    public class tblDetailAssignProperty : Model.IDetailAssignProperty
    {
        public Guid? IDDetailAssignProperty { get; set; }
        public Guid? IDRet { get; set; }
        public Guid? IDProperty { get; set; }
        public int? Sort { get; set; }


        public Guid? IDMasterProductMain { get; set; }
        public Guid? IDDefineSelected { get; set; }

    }
}