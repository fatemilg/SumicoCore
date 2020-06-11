using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModelSite
{
    public class MaterialListItem : Model.IMaterialListItem
    {
        public Guid? IDMaterialListItem { get; set; }
        public Guid? IDMaterialList { get; set; }
        [Required(ErrorMessage = "محصول مورد نظر را انتخاب کنید")]
        public Guid? IDDefineDetailProduct { get; set; }
        public Int64? Count { get; set; }
        public DateTime? CreateDate { get; set; }
        [Required(ErrorMessage = "ابتدا به حساب کاربری خود وارد شوید")]
        public Guid? IDLogUser { get; set; }
        public Int64? IDX { get; set; }

    }
}