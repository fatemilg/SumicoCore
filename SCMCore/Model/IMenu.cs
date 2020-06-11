using System;

namespace SCMCore.Model
{
    public interface IMenu
    {
         Guid IDMenu { get; set; }
         Guid? ParentID { get; set; }
         string Name_Fa { get; set; }
         string Name_En { get; set; }
         int? Order { get; set; }
         bool? Active { get; set; }
         int? Status { get; set; }
         string MenuUrl { get; set; }
         string PicUrl { get; set; }

    }
}