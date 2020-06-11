using System;

namespace SCMCore.Model
{
    public interface IDictionary
    {
        Guid? IDDictionary { get; set; }
        string Title { get; set; }
        string Value { get; set; }
        string Abstract { get; set; }
        string PicUrl { get; set; }
        string KeyWord { get; set; }
        string MetaTag { get; set; }
        string MetaDescription { get; set; }
         int? IDX { get; set; }
        int? Status { get; set; }
    }
}