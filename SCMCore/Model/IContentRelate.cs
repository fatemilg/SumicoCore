using System;

namespace SCMCore.Model
{
    public interface IContentRelate
    {
        Guid? IDContentRelate { get; set; }
        Guid? IDSourceContent { get; set; }
        Guid? IDDestinationContent { get; set; } 

    }
}