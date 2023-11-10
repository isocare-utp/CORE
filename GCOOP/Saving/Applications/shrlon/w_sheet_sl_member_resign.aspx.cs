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
using DataLibrary;
using System.Globalization;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_member_resign : PageWebSheet, WebSheet
    {

        private DwThDate tdw_head;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String memNoItemChange;
        protected String memNoFromDlg;
        protected String newClear;


        public void InitJsPostBack()
        {
            memNoItemChange = WebUtil.JsPostBack(this, "memNoItemChange");
            memNoFromDlg = WebUtil.JsPostBack(this, "memNoFromDlg");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tdw_head = new DwThDate(dw_head, this);
            tdw_head.Add("resignreq_date", "resignreq_tdate");
            tdw_head.Add("approve_date", "approve_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
            if (dw_head.RowCount < 1)
            {
                dw_head.InsertRow(1);
                dw_sum.InsertRow(1);
                dw_share.InsertRow(1);
                dw_loan.InsertRow(1);
                dw_grt.InsertRow(1);
                dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);
                dw_head.SetItemDate(1, "approve_date", state.SsWorkDate);
                dw_deposit.InsertRow(1);
                DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", null);
                tdw_head.Eng2ThaiAllRow();
            }
            if (IsPostBack)
            {

                try
                {
                    dw_head.RestoreContext();
                    dw_sum.RestoreContext();
                    dw_share.RestoreContext();
                    dw_loan.RestoreContext();
                    dw_grt.RestoreContext();
                    dw_deposit.RestoreContext();

                }
                catch { }

            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "memNoItemChange")
            {
                GetMemberDetail();
            }
            else if (eventArg == "memNoFromDlg")
            {
                GetMemberDetail();
            }
            else if (eventArg == "newClear")
            {

                JsNewClear();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String xml_grt = xml_grt = dw_grt.Describe("DataWindow.Data.XML");
                if (xml_grt == "")
                {
                    xml_grt = TextGRT.Text;
                }
                String xml_dept = dw_deposit.Describe("DataWindow.Data.XML");
                String xml_loan = dw_loan.Describe("DataWindow.Data.XML");
                String xml_request = dw_head.Describe("DataWindow.Data.XML");
                String xml_share = dw_share.Describe("DataWindow.Data.XML");
                String xml_sum = dw_sum.Describe("DataWindow.Data.XML");
                String member_no = Hfmember_no.Value;
                DateTime entry_date = state.SsWorkDate;
                String entry_id = state.SsUsername;
                str_mbreqresign mbreqresign = new str_mbreqresign();

                mbreqresign.xml_grt = xml_grt;
                mbreqresign.xml_dept = xml_dept;
                mbreqresign.xml_loan = xml_loan;
                mbreqresign.xml_request = xml_request;
                mbreqresign.xml_share = xml_share;
                mbreqresign.xml_sum = xml_sum;
                mbreqresign.member_no = member_no;
                mbreqresign.entry_date = entry_date;
                mbreqresign.entry_id = entry_id;

                int result = shrlonService.of_savereq_mbresign(state.SsWsPass, ref mbreqresign);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                JsNewClear();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void WebSheetLoadEnd()
        {
            dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "approve_date", state.SsWorkDate);
            tdw_head.Eng2ThaiAllRow();
            DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", null);
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
        }

        private void GetMemberDetail()
        {
            try
            {
                String xml_dept = dw_deposit.Describe("DataWindow.Data.XML");
                String xml_grt = dw_grt.Describe("DataWindow.Data.XML");
                TextGRT.Text = xml_grt;
                String xml_loan = dw_loan.Describe("DataWindow.Data.XML");
                String xml_request = dw_head.Describe("DataWindow.Data.XML");
                String xml_share = dw_share.Describe("DataWindow.Data.XML");
                String xml_sum = dw_sum.Describe("DataWindow.Data.XML");
                String member_no = Hfmember_no.Value;
                DateTime entry_date = state.SsWorkDate;
                String entry_id = state.SsUsername;
                str_mbreqresign mbreqresign = new str_mbreqresign();
                int result = shrlonService.of_initreq_mbresign(state.SsWsPass, ref mbreqresign);
                if (result == 1)
                {
                    try
                    {
                        dw_deposit.Reset();
                        dw_deposit.ImportString(mbreqresign.xml_dept, FileSaveAsType.Xml);

                    }
                    catch { dw_deposit.InsertRow(0); }
                    try
                    {
                        dw_head.Reset();
                        dw_head.ImportString(mbreqresign.xml_request, FileSaveAsType.Xml);

                    }
                    catch { dw_head.InsertRow(0); }

                    try
                    {
                        dw_sum.Reset();
                        dw_sum.ImportString(mbreqresign.xml_sum, FileSaveAsType.Xml);
                    }
                    catch { dw_sum.InsertRow(0); }
                    try
                    {
                        dw_share.Reset();
                        dw_share.ImportString(mbreqresign.xml_share, FileSaveAsType.Xml);
                    }
                    catch { dw_share.InsertRow(0); }

                    try
                    {
                        dw_loan.Reset();
                        dw_loan.ImportString(mbreqresign.xml_loan, FileSaveAsType.Xml);
                    }
                    catch { dw_loan.InsertRow(0); }

                    try
                    {
                        dw_grt.Reset();
                        dw_grt.ImportString(mbreqresign.xml_grt, FileSaveAsType.Xml);
                    }
                    catch { dw_grt.InsertRow(0); }


                    DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", null);

                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
        }

        private void JsNewClear()
        {
            dw_head.Reset();
            dw_sum.Reset();
            dw_share.Reset();
            dw_loan.Reset();
            dw_grt.Reset();
            dw_deposit.Reset();

            dw_head.InsertRow(1);
            dw_sum.InsertRow(1);
            dw_share.InsertRow(1);
            dw_loan.InsertRow(1);
            dw_grt.InsertRow(1);
            dw_head.SetItemDate(1, "resignreq_date", state.SsWorkDate);
            dw_head.SetItemDate(1, "approve_date", state.SsWorkDate);
            dw_deposit.InsertRow(1);
            DwUtil.RetrieveDDDW(dw_head, "resigncause_code", "sl_member_resign.pbl", null);
            tdw_head.Eng2ThaiAllRow();

        }
    }
}

