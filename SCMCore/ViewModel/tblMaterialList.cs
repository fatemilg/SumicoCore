using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModel
{
    public class tblMaterialList : Model.IMaterialList
    {
        public Guid? IDMaterialList { get; set; }
        public Guid? IDParent { get; set; }
        public Guid? IDUser { get; set; }
        public string Title { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}






