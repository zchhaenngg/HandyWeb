namespace HandyWork.Model.Entity
{
    using Abstracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hy_data_history")]
    public partial class hy_data_history : hy_Creator
    {
        [Required]
        [StringLength(50)]
        public string category_name { get; set; }

        [StringLength(50)]
        public string  entity_name { get; set; }

        [Required]
        [StringLength(40)]
        public string unique_key { get; set; }

        [StringLength(50)]
        public string operation { get; set; }

        [Required]
        public string description { get; set; }
    }
}
