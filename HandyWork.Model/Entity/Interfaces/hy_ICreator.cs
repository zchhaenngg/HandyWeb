using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Model.Entity.Interfaces
{
    public interface hy_ICreator
    {
        string id { get; set; }

        string created_by_id { get; set; }

        DateTime created_time { get; set; }
    }
}
