namespace BuildSchedule.ExcelAddIn.Models
{
    public class BuildScheduleExcel
    {
        public string StockCode { get; set; }
        public string Warehouse { get; set; }
        public string DateRequired { get; set; }
        public decimal OutstQtyToMake { get; set; }
        //public ActionType LineAction { get; set; }
        public string Reference { get; set; }
    }
}
