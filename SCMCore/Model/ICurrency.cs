using System;

namespace SCMCore.Model
{
    public interface ICurrency
    {
        Guid IDCurrency { get; set; }
        string Title { get; set; }

    }
}