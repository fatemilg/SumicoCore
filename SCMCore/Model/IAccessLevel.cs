using System;

namespace SCMCore.Model
{
    public interface IAccessLevel
    {
        Guid? IDAccessLevel { get; set; }
        Guid? IDUser { get; set; }
        Guid? IDMenu { get; set; }
        Guid? IDRole { get; set; }
        //bool Add { get; set; }
        //bool Update { get; set; }
        //bool Delete { get; set; }
        int? Status { get; set; }
        string MenuUrl { get; set; }
        string Title_En { get; set; }
        bool? Access { get; set; }

        // field add,delete ,update dar jadvale Accesslevel mojood hast vali inja nayavordim,tooie procedure marboot be insert va update bayad be soorate defualt null rad shavad
    }
}