using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface ISystemEmail
    {
        Guid? IDSystemEmail { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string SMTP_Address { get; set; }
        int? PortNumber { get; set; }
    }
}
