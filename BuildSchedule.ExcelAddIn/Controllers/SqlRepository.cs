using BuildSchedule.ExcelAddIn.Models;
using SigmaCape.Business.Syspro;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BuildSchedule.ExcelAddIn.Controllers
{
    public class SqlRepository
    {
        public List<string> GetWarehouse(string stockCode, SysproIdentity sysIdentity)
        {
            var warehouses = new List<string>();

            using (var context = new SysproContext(sysIdentity))
            {
                warehouses = context.InvWarehouse.Where(x => x.StockCode == stockCode).Select(y => y.Warehouse).ToList();
            }

            return warehouses;
        }

        public string GetUom(string stockCode, SysproIdentity sysIdentity)
        {
            var uom = string.Empty;

            using (var context = new SysproContext(sysIdentity))
            {
                uom = context.InvMaster.Where(x => x.StockCode == stockCode).Select(y => y.StockUom).FirstOrDefault();
            }

            return uom.ToUpper();
        }

        public string GetCompanyDB(string letter)
        {
            var comp = string.Empty;

            using (var context = new SysprodbContext())
            {
                comp = context.SysproAdmin.Where(x => x.Company.Trim() == letter).Select(y => y.DatabaseName.Trim()).FirstOrDefault();
            }

            return comp;
        }

        public int DeletePrevBS(IEnumerable<BuildScheduleExcel> bsItems, SysproIdentity sysIdentity)
        {
            int recSaved = 0;
            using (var sysContext = new SysproContext(sysIdentity))
            {
                foreach (var item in bsItems.Select(m => new { m.StockCode, m.Warehouse }).Distinct())
                {
                    var entity = sysContext.MrpBuildSchedule.Where(x => x.StockCode == item.StockCode & x.Warehouse == item.Warehouse).ToList();
                    var del = sysContext.MrpBuildSchedule.RemoveRange(entity);
                    recSaved = sysContext.SaveChanges();
                }
            }
            return recSaved;
        }
    }
}
