using System;

namespace SCMCore.Model
{
    public interface IQouationDetail
    {
        Guid? IDQouationDetail { get; set; }
        Guid? IDQouationFile { get; set; }
        Guid? IDPersonel { get; set; }
        Guid? IDDefineDetailProduct { get; set; }
        string PartNumber { get; set; }
        decimal? Price { get; set; }
        int? Qty { get; set; }
        string ShortDescription { get; set; }
        string Description { get; set; }
        int? FixedPrice { get; set; }
        int? MarkUp { get; set; }
        int? SalesPrice { get; set; }

        string AllDetailJson { get; set; }
        string ItemsSelected { get; set; }
        decimal? RatioChfToEu { get; set; }



    }
}