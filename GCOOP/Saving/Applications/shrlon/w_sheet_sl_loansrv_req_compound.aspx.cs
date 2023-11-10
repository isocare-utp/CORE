using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loansrv_req_compound : PageWebSheet, WebSheet
    {
        private DwThDate tdw_head;
        private DwThDate tdw_share;
        private DwThDate tdw_loan;

        private n_shrlonClient ShrlonSv;
        protected String callInitCompound;
        protected String itemChangedReload;
        protected String calMonth;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tdw_head = new DwThDate(dw_head, this);
            tdw_head.Add("reqcomp_date", "reqcomp_tdate");
            tdw_share = new DwThDate(dw_share, this);
            tdw_share.Add("comp_duedate", "comp_duetdate");
            tdw_loan = new DwThDate(dw_loan, this);
            tdw_loan.Add("comp_duedate", "comp_duetdate");

            callInitCompound = WebUtil.JsPostBack(this, "callInitCompound");
            itemChangedReload = WebUtil.JsPostBack(this, "itemChangedReload");
            calMonth = WebUtil.JsPostBack(this, "calMonth");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            dw_head.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                dw_head.InsertRow(1);
                dw_share.InsertRow(1);
                dw_loan.InsertRow(1);
                dw_head.SetItemDateTime(1, "reqcomp_date", state.SsWorkDate);
                tdw_head.Eng2ThaiAllRow();
            }
            else
            {
                dw_head.RestoreContext();
                dw_share.RestoreContext();
                dw_loan.RestoreContext();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "callInitCompound")
            {
                //GetMemberNo();
                InitCompoundSv();
            }
            else if (eventArg == "itemChangedReload") { }
            else if (eventArg == "calMonth") {
                CalMonthChanged();
            }
        }

        public void SaveWebSheet()
        {
            SaveReqCompound();
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

        //เรียก เซอรวิส init_compound
        private void InitCompoundSv()
        {
            try
            {
                string membno = dw_head.GetItemString(1, "member_no").Trim();
                str_compound strCompound = new str_compound();
                strCompound.member_no = membno;
                strCompound.comp_date = state.SsWorkDate;
                strCompound.coop_id = state.SsCoopControl;
                strCompound.entry_id = state.SsUsername;
                strCompound.xml_comphead = "";
                strCompound.xml_comploan = "";
                strCompound.xml_compshare = "";
                ShrlonSv = wcf.NShrlon;
                Int32 result = ShrlonSv.of_initreq_compound(state.SsWsPass, ref strCompound);
                dw_head.Reset();
                dw_share.Reset();
                dw_loan.Reset();
                try
                {
                    DwUtil.ImportData(strCompound.xml_comphead, dw_head, tdw_head);
                }
                catch (Exception e)
                {
                    dw_head.ImportString(strCompound.xml_comphead, FileSaveAsType.Xml);
                }
                try
                {
                    DwUtil.ImportData(strCompound.xml_compshare, dw_share, tdw_share);
                }
                catch (Exception e)
                {
                    dw_share.ImportString(strCompound.xml_compshare, FileSaveAsType.Xml);
                }
                try
                {
                    DwUtil.ImportData(strCompound.xml_comploan, dw_loan, tdw_loan);
                }
                catch (Exception e)
                {
                    dw_loan.ImportString(strCompound.xml_comploan, FileSaveAsType.Xml);
                }
                //วันที่
                for (int i = 1; i <= dw_share.RowCount; i++)
                {
                    dw_share.SetItemDateTime(i, "comp_duedate", state.SsWorkDate);
                    try
                    {
                        DateTime dt = dw_share.GetItemDate(i, "bfcomp_duedate");
                        DateTime dt2 = dt.AddYears(543);
                        dw_share.SetItemDateTime(i, "bfcomp_duedate", dt2);
                    }
                    catch
                    {
                        DateTime dt = state.SsWorkDate;
                        DateTime dt2 = dt.AddYears(543);
                        dw_share.SetItemDateTime(i, "bfcomp_duedate", dt2);
                    }
                }
                for (int i = 1; i <= dw_loan.RowCount; i++)
                {
                    dw_loan.SetItemDate(i, "comp_duedate", state.SsWorkDate);
                    try
                    {
                        DateTime dt = dw_loan.GetItemDate(i, "bfcomp_duedate");
                        DateTime dt2 = dt.AddYears(543);
                        dw_loan.SetItemDateTime(i, "bfcomp_duedate", dt2);
                    }
                    catch
                    {
                        DateTime dt = state.SsWorkDate;
                        DateTime dt2 = dt.AddYears(543);
                        dw_loan.SetItemDateTime(i, "bfcomp_duedate", dt2);
                    }
                }

                tdw_head.Eng2ThaiAllRow();
                tdw_share.Eng2ThaiAllRow();
                tdw_loan.Eng2ThaiAllRow();
            }
            catch (Exception exx)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(exx);
            }
        }

        private void SaveReqCompound()
        {
            //แปลงวันที่กลับ
            for (int i = 1; i <= dw_share.RowCount; i++)
            {
                try
                {
                    DateTime dt = dw_share.GetItemDate(i, "bfcomp_duedate");
                    DateTime dt2 = dt.AddYears(-543);
                    dw_share.SetItemDateTime(i, "bfcomp_duedate", dt2);
                }
                catch { }
            }
            for (int i = 1; i <= dw_loan.RowCount; i++)
            {
                try
                {
                    DateTime dt = dw_loan.GetItemDate(i, "bfcomp_duedate");
                    DateTime dt2 = dt.AddYears(-543);
                    dw_loan.SetItemDateTime(i, "bfcomp_duedate", dt2);
                }catch{}
            }
            //จบแปลงวันที่กลับ

            ShrlonSv = wcf.NShrlon;
            str_compound strCompound = new str_compound();
            strCompound.coop_id = state.SsCoopControl;
            strCompound.comp_date = state.SsWorkDate;
            strCompound.entry_id = state.SsUsername;
            strCompound.member_no = dw_head.GetItemString(1, "member_no").Trim();
            strCompound.xml_comphead = dw_head.Describe("DataWindow.Data.XML");
            strCompound.xml_compshare = dw_share.Describe("DataWindow.Data.XML");
            strCompound.xml_comploan = dw_loan.Describe("DataWindow.Data.XML");
            try
            {
                ShrlonSv.of_savereq_compound(state.SsWsPass, ref strCompound);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ee)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ee);
            }
        }

        private void CalMonthChanged()
        {
            //comp_period
            for (int i = 1; i <= dw_share.RowCount; i++)
            {
                try
                {
                    decimal period = dw_share.GetItemDecimal(i, "comp_period");
                    if (period > 0) {
                        dw_share.SetItemDate(i, "comp_duedate", CalculateMonth(period));
                        tdw_share.Eng2ThaiAllRow();
                    }
                }
                catch(Exception e) { }
            }
            for (int i = 1; i <= dw_loan.RowCount; i++)
            {
                try
                {
                    decimal period = dw_loan.GetItemDecimal(i, "comp_period");
                    if (period > 0)
                    {
                        dw_loan.SetItemDate(i, "comp_duedate", CalculateMonth(period));
                        tdw_loan.Eng2ThaiAllRow();
                    }
                }
                catch (Exception e) { }
            }
        }

        private DateTime CalculateMonth(decimal month) {
            DateTime dt = state.SsWorkDate;
            dt = dt.AddMonths( Convert.ToInt32( month));
            return dt;
        }

    }
}
