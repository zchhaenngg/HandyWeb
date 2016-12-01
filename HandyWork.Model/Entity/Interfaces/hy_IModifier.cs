using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Model.Entity.Interfaces
{
    public interface hy_IModifier
    {
        string id { get; set; }

        string last_modified_by_id { get; set; }

        DateTime last_modified_time { get; set; }
    }
}
