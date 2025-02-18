namespace BuildSchedule.ExcelAddIn.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InvMaster")]
    public class InvMaster
    {
        [Key]
        [StringLength(30)]
        public string StockCode { get; set; }

        public string StockUom { get; set; }
    }
}
