namespace BuildSchedule.ExcelAddIn.Controllers
{
    using BuildSchedule.ExcelAddIn.Models;
    using SigmaCape.Business.Syspro;
    using SigmaCape.Business.Syspro.MRP;
    using SigmaCape.Business.Syspro.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public class SysproRepository
    {
        #region Public Methods and Operators

        public SysproResult PostBuildSchedule(IEnumerable<BuildScheduleExcel> bsItems, SysproIdentity sysIdentity)
        {
            var buildSchedule = new BuildSchedule();
            var buildScheduleItem = new BuildScheduleItem();

            foreach (var item in bsItems)
            {
                buildScheduleItem = new BuildScheduleItem();
                buildScheduleItem.StockCode = item.StockCode;
                buildScheduleItem.Warehouse = item.Warehouse;
                buildScheduleItem.DateRequired = item.DateRequired;
                buildScheduleItem.OutstQtyToMake = item.OutstQtyToMake;
                buildScheduleItem.Reference = item.Reference;
                //buildScheduleItem.LineAction = ActionType.A;
                buildSchedule.Items.Add(buildScheduleItem);
            }
            var bsSer = buildSchedule.Serialize();
            var bsRes = buildSchedule.Post(GetBSParam(),sysIdentity);
            return bsRes;
        }

        public BuildScheduleParameters GetBSParam()
        {
            return new BuildScheduleParameters()
            {
                ActionType = BSAction.A,
                IgnoreWarnings = SysproBoolean.Y,
                ValidateOnly = SysproBoolean.N,
                ApplyIfEntireDocumentValid = SysproBoolean.N,
                Snapshot = Snapshot.N
            };
        }

        private string CheckReference(string reference)
        {
            if (reference.Length > 9)
            {
                return reference.Substring(0, 9);
            }

            return reference;
        }

        static string RemoveInvalidXmlChars(string text)
        {
            var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }

        #endregion
    }
}