using HandyWork.Model.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Model.Entity.Abstracts
{
    public abstract class hy_Entity : hy_IEntity
    {
        [Key]
        [StringLength(40)]
        public string id { get; set; }

        [StringLength(40)]
        public string created_by_id { get; set; }

        public DateTime created_time { get; set; }

        [StringLength(40)]
        public string last_modified_by_id { get; set; }

        public DateTime last_modified_time { get; set; }
    }
}
