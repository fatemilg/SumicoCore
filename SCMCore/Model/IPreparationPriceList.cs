using System;

namespace SCMCore.Model
{
    public interface IPreparationPriceList
    {
         Guid? IDPreparationPriceList { get; set; }
         Guid? IDPersonel { get; set; }
         Guid? IDSupplier { get; set; }
         Guid? IDSupplierPriceListFile { get; set; }
         Guid? IDQouationFile { get; set; }
         Guid? IDPeyvastPriceFile { get; set; }
         string Title { get; set; }
         DateTime? CreateDate { get; set; }
         bool? Show { get; set; }
    }
}