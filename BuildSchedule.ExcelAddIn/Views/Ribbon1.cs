using System;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;

using SigmaCape.Business.Syspro;
using SigmaCape.Business.Syspro.UI;

using Application = Microsoft.Office.Interop.Excel.Application;
using SigmaCape.Business.Syspro.Inv;
using System.Collections.Generic;
using BuildSchedule.ExcelAddIn.Controllers;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace BuildSchedule.ExcelAddIn
{
    public partial class SysproBuildSchedule
    {

        private SysproRepository sysproRepository;
        private SysproIdentity sysId;
        private SqlRepository sqlRepository;
        private Application application;
        //private UserPrincipal user;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            application = Globals.ThisWorkbook.Application;
            sysproRepository = new SysproRepository();
            sqlRepository = new SqlRepository();
            //var username = System.Security.Principal.WindowsIdentity.GetCurrent().User.ToString();
            //var domainContext = new PrincipalContext(ContextType.Domain, "VITAL");
            //user = UserPrincipal.FindByIdentity(domainContext, username);
        }

        private void btnLogin_Click(object sender, RibbonControlEventArgs e)
        {
            this.SetCursor("Refreshing...");
            try
            {
                if (Syspro.ShowLoginDialog() != true)
                {
                    return;
                }

                this.sysId = SysproIdentity.Current;
                this.lblUser.Label = "User: " + SysproIdentity.Current.Profile.OperatorName;
                this.lblCompany.Label = "Company: " + SysproIdentity.Current.Profile.Company;

                //if (this.ddWarehouse.Items.Count < 1 | this.tempCompany != SysproIdentity.Current.Profile.Company)
                //{
                //    using (var sysContext = new SysproContext(this.sysId.Profile.Company))
                //    {
                //        //var sysWarehouse = sysContext.InvWhControl.OrderBy(x => x.Warehouse);
                //        //foreach (var rec in sysWarehouse)
                //        //{
                //        //    var ddi = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                //        //    ddi.Label = rec.Warehouse;
                //        //    this.ddWarehouse.Items.Add(ddi);
                //        //}
                //        //tempCompany = SysproIdentity.Current.Profile.Company;
                //    }
                //}

                //this.application.Cursor = XlMousePointer.xlWait;
                //this.application.StatusBar = "Loading payments...";
                //this.application.ScreenUpdating = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"LoginClick: " + ex.Message);
            }
            finally
            {
                this.ResetCursor();
            }
        }

        private void SetCursor(string displayText)
        {
            this.application.Cursor = XlMousePointer.xlWait;
            this.application.StatusBar = displayText;
            this.application.ScreenUpdating = true;
        }

        private void ResetCursor()
        {
            this.application.Cursor = XlMousePointer.xlDefault;
            this.application.StatusBar = null;
            this.application.ScreenUpdating = true;
        }

        private void btnPost_Click(object sender, RibbonControlEventArgs e)
        {

            SetCursor("Posting...");
            try
            {
                if (!(sysId.IsAuthenticated))// && ddWarehouse.Items.Count < 1
                {
                    MessageBox.Show("Please log in first");
                    ResetCursor();
                    return;
                }
                if (MessageBox.Show("This will post the build schedule detail. Continue?", "POST", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                application.Cursor = XlMousePointer.xlWait;
                application.StatusBar = "Posting Build Schedule...";
                application.ScreenUpdating = false;

                var captureDetail = ReadRows();
                if (captureDetail != null && captureDetail.Count > 0)
                {
                    var delRes = sqlRepository.DeletePrevBS(captureDetail, sysId);
                    var result = sysproRepository.PostBuildSchedule(captureDetail, sysId);
                    if (result.HasErrors)
                    {
                        MessageBox.Show(result.Errors);
                        ResetCursor();
                        return;
                    }
                    ResetCursor();
                    MessageBox.Show("Posting Complete - " + captureDetail.Count + " records posted.", "POST", MessageBoxButtons.OK);
                    //ClearRange();
                }
            }
            catch (Exception ex)
            {
                ResetCursor();
                MessageBox.Show(@"PostClick: " + ex.Message);
            }
            finally
            {
                ResetCursor();
            }
        }

        private void btImport_Click(object sender, RibbonControlEventArgs e)
        {
            {
                SetCursor("Importing...");
                try
                {
                    if (!(sysId.IsAuthenticated) && ddWarehouse.Items.Count < 1)
                    {
                        MessageBox.Show("Please log in first");
                        ResetCursor();
                        return;
                    }

                    //    if (MessageBox.Show("This will reset your selection. Continue?", "IMPORT", MessageBoxButtons.YesNo) == DialogResult.No)
                    //    {
                    //        return;
                    //    }
                    //    application.Cursor = XlMousePointer.xlWait;
                    //    application.StatusBar = "Loading stock codes...";
                    //    application.ScreenUpdating = false;
                    //    var stockCodes = new List<InvStockTake>();
                    //    if (ddCheckQty.Checked)
                    //    {
                    //        stockCodes = sysproRepository.GetWHStockTake(sysId.Profile.Company, ddWarehouse.SelectedItem.Label);
                    //    }
                    //    else
                    //    {
                    //        stockCodes = sysproRepository.GetWHStockTakeZero(sysId.Profile.Company, ddWarehouse.SelectedItem.Label);
                    //    }
                    //    ClearRange();
                    //    SetColVal(stockCodes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"ImportClick: " + ex.Message);
                }
                finally
                {
                    ResetCursor();
                }
            }
        }

        private List<Models.BuildScheduleExcel> ReadRows()
        {
            var activeSheet = Globals.ThisWorkbook.Application.ActiveSheet;
            var ssRange = (Range)activeSheet.UsedRange;
            var bsItemLst = new List<Models.BuildScheduleExcel>();
            try
            {
                for (var r = 4; r <= ssRange.Rows.Count; r++)
                {
                    var divThou = sqlRepository.GetUom(Convert.ToString(ssRange[r, 1].Value2), sysId);

                    for (var c = 4; c <= ssRange.Columns.Count; c++)
                    {
                        if (ssRange[r, c].Value2 == null || Convert.ToString(ssRange[3, c].Value2) == "Description" || Convert.ToString(ssRange[3, c].Value2) == "MRP")
                        {
                            continue;
                        }
                        //MessageBox.Show(Convert.ToString(ssRange[r, 1].Value2) + " - " + Convert.ToString(ssRange[3, c].Value2) + " - " + Convert.ToString(ssRange[r, c].Value2));
                        var bsItem = new Models.BuildScheduleExcel();
                        bsItem.StockCode = Convert.ToString(ssRange[r, 1].Value2);
                        bsItem.Warehouse = Convert.ToString(ssRange[1, 2].Value2);
                        var dt = ((string)Convert.ToString(ssRange[3, c].Value2)).Split('.');
                        var dtReq = FirstDateOfWeek(int.Parse(dt[1]), int.Parse(dt[0]), CultureInfo.CurrentCulture);
                        bsItem.DateRequired = dtReq.ToString("yyyy-MM-dd");
                        bsItem.Reference = Convert.ToString(ssRange[2, 2].Value2);
                        bsItem.OutstQtyToMake = divThou == "THO" ? ((decimal)ssRange[r, c].Value2) / 1000 : (decimal)ssRange[r, c].Value2;
                        bsItemLst.Add(bsItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"ReadRows: " + ex.Message);
                bsItemLst.Clear();
            }

            return bsItemLst;
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }



        //private object GetColVal(string colName, Range area, int row)
        //{
        //    var val = area.Find(colName, Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //    var retval = val != null ? (object)area[row, val.Column].Value2 : null;
        //    return retval;
        //}

        //private void Button3_Click(object sender, RibbonControlEventArgs e)
        //{
        //    SetCursor("Refreshing data...");
        //    try
        //    {
        //        var activeSheet = Globals.ThisWorkbook;
        //        activeSheet.RefreshAll();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(@"Refresh: " + ex.Message);
        //    }
        //    ResetCursor();
        //}

        //private void SetColVal(List<InvStockTake> stkDet)
        //{
        //    var activeSheet = Globals.ThisAddIn.Application.ActiveSheet;
        //    var ssRange = activeSheet.UsedRange;
        //    var area = ssRange.Areas[1];

        //    var cnt = 2;
        //    foreach (var item in stkDet)
        //    {
        //        var stockCodeVal = area.Find("StockCode", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //        ((Range)area[cnt, stockCodeVal.Column]).Value2 = item.StockCode;
        //        var qtyVal = area.Find("CaptureQty", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //        ((Range)area[cnt, qtyVal.Column]).Value2 = 0;
        //        var refVal = area.Find("Reference", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //        ((Range)area[cnt, refVal.Column]).Value2 = item.Reference;
        //        var uomVal = area.Find("UOM", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //        ((Range)area[cnt, uomVal.Column]).Value2 = item.Uom;
        //        var capQtyVal = area.Find("TimesCaptured", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //        ((Range)area[cnt, capQtyVal.Column]).Value2 = item.NumTimesCaptured;

        //        //if ("Fransman,Floris,Koen".Contains(user.Surname))
        //        //{
        //        //    var origQtyVal = area.Find("OrigQtyOnHand", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlWhole, XlSearchOrder.xlByColumns, XlSearchDirection.xlNext, false, Type.Missing, Type.Missing);
        //        //    ((Range)area[cnt, origQtyVal.Column]).Value2 = item.OrigQtyOnHand;
        //        //}
        //        cnt += 1;
        //    }
        //}

        //private void ClearRange()
        //{
        //    var activeSheet = Globals.ThisAddIn.Application.ActiveSheet;
        //    var ssRange = activeSheet.UsedRange;
        //    var area = ssRange.Areas[1];

        //    for (int i = 2; i < area.Rows.Count + 1; i++)
        //    {
        //        ((Range)area[i, 1]).Value2 = string.Empty;
        //        ((Range)area[i, 2]).Value2 = string.Empty;
        //        ((Range)area[i, 3]).Value2 = string.Empty;
        //        ((Range)area[i, 4]).Value2 = string.Empty;
        //        ((Range)area[i, 5]).Value2 = string.Empty;
        //        //if ("Fransman,Floris,Koen".Contains(user.Surname))
        //        //{
        //        //    ((Range)area[i, 6]).Value2 = string.Empty;
        //        //}
        //    }
        //}
    }
}
