using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModel
{
    public class tblMaterialListItem : Model.IMaterialListItem
    {
        public Guid? IDMaterialListItem { get; set; }
        public Guid? IDMaterialList { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public Int64? Count { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? IDLogUser { get; set; }
        public Int64? IDX { get; set; }
    }
}






