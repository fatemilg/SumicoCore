using System;

namespace SCMCore.ViewModelSite
{
    public class MaterialListPreparation : Model.IMaterialListPreparation
    {
        public Guid? IDMaterialListPreparation { get; set; }
        public Guid? IDParent { get; set; }
        public Guid? IDMaterialList { get; set; }
        public DateTime? ShowDate { get; set; }

        public string QuestionAnswerByCustomer { get; set; }
        public Guid? IDLogUser { get; set; }
        public int? IDXMaterialList { get; set; }


    }
}