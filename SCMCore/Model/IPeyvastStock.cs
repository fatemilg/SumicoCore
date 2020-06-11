using System;

namespace SCMCore.Model
{
    public interface IPeyvastStock
    {
        Guid? IDPeyvastStock { get; set; }
        int? IDImportedProduct { get; set; }
        Int64? Stock { get; set; }
        int? IDStore { get; set; }
        DateTime? CreateDate { get; set; }
    }
}
