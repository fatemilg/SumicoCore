﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface IAnswerCustomer
    {
            
        Guid? IDAnswerCustomer { get; set; }
        Guid? IDOptionCustomer { get; set; }
        Guid? IDUserSite { get; set; }
    }
}