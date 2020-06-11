using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPreparationPriceList : Model.IPreparationPriceList
    {
        public Guid? IDPreparationPriceList { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDSupplierPriceListFile { get; set; }
        public Guid? IDQouationFile { get; set; }
        public Guid? IDPeyvastPriceFile { get; set; }
        public string Title { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Show { get; set; }
    }
}