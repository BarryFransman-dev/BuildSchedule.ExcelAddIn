namespace BuildSchedule.ExcelAddIn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MrpBuildSchedule")]
    public partial class MrpBuildSchedule
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string StockCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Warehouse { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DateRequired { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal Line { get; set; }

        public decimal OutstQtyToMake { get; set; }

        [Required]
        [StringLength(30)]
        public string Reference { get; set; }

        [Required]
        [StringLength(30)]
        public string ResourceParent { get; set; }

        public decimal TotalQtyReceived { get; set; }

        public DateTime? OrigDateReqd { get; set; }

        public decimal OriginalLine { get; set; }

        public decimal OrigOutstQty { get; set; }

        [Required]
        [StringLength(5)]
        public string Version { get; set; }

        [Required]
        [StringLength(5)]
        public string Release { get; set; }

        [Required]
        [StringLength(2)]
        public string Route { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }
    }
}
