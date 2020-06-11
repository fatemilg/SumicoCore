using System;

namespace SCMCore.Model
{
    public interface IBanner
    {
        Guid? IDBanner { get; set; }
        Guid? IDRet { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        string PicUrl { get; set; }
        string PicUrl_Mobile { get; set; }
        string ForeignLink { get; set; }
        bool? Active { get; set; }
        int? Sort { get; set; } 
        int? Status { get; set; }

    }
}