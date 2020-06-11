using System;

namespace SCMCore.Model
{
    public interface IContent
    {
        Guid? IDContent { get; set; }
        int? IDX { get; set; }
        Guid? IDContentCategory { get; set; }
        Guid? IDPersonel { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        string Abstract_Fa { get; set; }
        string Abstract_En { get; set; }
        string MetaTags { get; set; }
        string MetaDescriptions { get; set; }
        string PicUrl { get; set; }
        int? Like { get; set; }
        int? DisLike { get; set; }
        int? Status { get; set; }
    }
}