using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModelSite
{
    public class tblMaterialList : Model.IMaterialList
    {
        public Guid? IDMaterialList { get; set; }
        public Guid? IDParent { get; set; }
        public Guid? IDUser { get; set; }
        [Required(ErrorMessage = "ابتدا به حساب کاربری خود وارد شوید")]
        public Guid? IDLogUser { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        public string Title { get; set; }
        public DateTime? CreateDate { get; set; }
    }

}
