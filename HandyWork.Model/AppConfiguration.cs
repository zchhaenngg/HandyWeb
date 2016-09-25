namespace HandyWork.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppConfiguration")]
    public partial class AppConfiguration
    {
        [StringLength(40)]
        public string Id { get; set; }

        [StringLength(50)]
        public string AppKey { get; set; }

        [StringLength(5000)]
        public string AppValue { get; set; }

        [StringLength(50)]
        public string AppCategory { get; set; }

        [StringLength(500)]
        public string Keep1 { get; set; }

        [StringLength(500)]
        public string Keep2 { get; set; }

        [StringLength(500)]
        public string Keep3 { get; set; }

        [StringLength(500)]
        public string Keep4 { get; set; }

        [StringLength(500)]
        public string Keep5 { get; set; }

        [StringLength(500)]
        public string Keep6 { get; set; }

        [StringLength(500)]
        public string Keep7 { get; set; }

        [StringLength(500)]
        public string Keep8 { get; set; }

        [StringLength(500)]
        public string Keep9 { get; set; }

        [StringLength(500)]
        public string Keep10 { get; set; }

        [StringLength(500)]
        public string Keep11 { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
