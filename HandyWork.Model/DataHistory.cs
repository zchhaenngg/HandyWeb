namespace HandyWork.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataHistory")]
    public partial class DataHistory
    {
        [StringLength(40)]
        public string Id { get; set; }

        [StringLength(40)]
        public string CreatedById { get; set; }

        [StringLength(40)]
        public string LastModifiedById { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(40)]
        public string ForeignId { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

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

        [StringLength(500)]
        public string Keep12 { get; set; }

        [StringLength(500)]
        public string Keep13 { get; set; }

        [StringLength(500)]
        public string Keep14 { get; set; }

        [StringLength(500)]
        public string Keep15 { get; set; }
    }
}
