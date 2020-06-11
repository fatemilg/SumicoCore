using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface ITrainingCourseBatchTrainingCourse
    {
        Guid? IDTrainingCourseBatchTrainingCourse { get; set; }
        Guid? IDTrainingCourseBatch { get; set; }
        Guid? IDTrainingCourse { get; set; }
        int? Sort { get; set; }
    }
}
