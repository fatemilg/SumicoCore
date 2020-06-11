using System;

namespace SCMCore.Model
{
    interface IBulkEmail
    {
        Guid? IDBulkEmail { get; set; }
        string Email { get; set; }
    }
}
