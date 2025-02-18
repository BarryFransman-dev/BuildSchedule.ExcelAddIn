namespace BuildSchedule.ExcelAddIn.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InvWarehouse")]
    public class InvWarehouse
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string StockCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Warehouse { get; set; }

        public decimal QtyOnHand { get; set; }
    }
}
