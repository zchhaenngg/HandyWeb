﻿namespace HandyWork.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using HandyModel.Entity.Abstracts;

    [Table("hy_configuration")]
    public partial class hy_configuration : hy_Entity
    {
        [StringLength(50)]
        public string app_key { get; set; }

        [StringLength(5000)]
        public string app_value { get; set; }

        [StringLength(50)]
        public string category { get; set; }
        
        [StringLength(200)]
        public string description { get; set; }
    }
}
