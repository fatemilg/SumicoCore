using System;

namespace SCMCore.ViewModel
{
    public class tblMaterialListPreparation : Model.IMaterialListPreparation
    {
        public Guid? IDMaterialListPreparation { get; set; }
        public Guid? IDParent { get; set; }
        public Guid? IDMaterialList { get; set; }
        public DateTime? ShowDate { get; set; }

    }
}