using System;

namespace SCMCore.ViewModel
{
    public class tblPeyvastStock :Model.IPeyvastStock
    {
        public Guid? IDPeyvastStock { get; set; }
        public int? IDImportedProduct { get; set; }
        public Int64? Stock { get; set; }
        public int? IDStore { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}