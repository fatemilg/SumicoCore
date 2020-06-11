using System;
namespace SCMCore.Model
{
    public interface IContactWay
    {
        Guid? IDContactWay { get; set; }
        Guid? IDContactWayType { get; set; }
        Guid? IDUser { get; set; }
        string Input { get; set; }
        string InternalInput { get; set; }
        int? Status { get; set; }

        string ContactWayTypeName_Fa { get; set; }
    }
}






