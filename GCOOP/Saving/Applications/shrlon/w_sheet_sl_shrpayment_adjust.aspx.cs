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
    public partial class w_sheet_sl_shrpayment_adjust : PageWebSheet, WebSheet
    {

        private DwThDate tdw_data;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String memNoItemChange;
        protected String memNoFromDlg;
        protected String newClear;
        private void GetMemberDetail()
        {
            try
            {
                String as_xmlreq = dw_data.Describe("DataWindow.Data.XML");
                String member_no = Hfmember_no.Value;
                DateTime adtm_datereq = dw_data.GetItemDate(1, "payadjust_date");
                string memcoop_id = WebUtil.getmemcoopid(state.SsCoopId, member_no);
                int result = shrlonService.of_initreq_chgmthshr(state.SsWsPass, state.SsCoopId, member_no, state.SsCoopControl, adtm_datereq, ref as_xmlreq);
                dw_data.Reset();
                dw_data.ImportString(as_xmlreq, FileSaveAsType.Xml);
                dw_data.SetItemString(1, "member_no", member_no);
                this.SetAvpDateAllRow();
                tdw_data.Eng2ThaiAllRow();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
        }
        private void SetAvpDateAllRow()
        {

            dw_data.SetItemDateTime(1, "payadjust_date", state.SsWorkDate);

        }
        public void InitJsPostBack()
        {
            newClear = WebUtil.JsPostBack(this, "newClear");
            memNoItemChange = WebUtil.JsPostBack(this, "memNoItemChange");
            memNoFromDlg = WebUtil.JsPostBack(this, "memNoFromDlg");
            tdw_data = new DwThDate(dw_data, this);
            tdw_data.Add("payadjust_date", "payadjust_tdate");
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
            if (dw_data.RowCount < 1)
            {
                dw_data.InsertRow(1);

                dw_data.SetItemDate(1, "payadjust_date", state.SsWorkDate);
                dw_data.SetItemString(1, "entry_id", state.SsUsername);
                tdw_data.Eng2ThaiAllRow();
            }
            if (IsPostBack)
            {

                try
                {
                    dw_data.RestoreContext();


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
        private void JsNewClear()
        {
            dw_data.Reset();
            dw_data.InsertRow(1);
            dw_data.SetItemDate(1, "payadjust_date", state.SsWorkDate);
            dw_data.SetItemString(1, "entry_id", state.SsUsername);
            tdw_data.Eng2ThaiAllRow();

        }
        public void SaveWebSheet()
        {
            try
            {
                String as_xmlreq = dw_data.Describe("DataWindow.Data.XML");

                DateTime entry_date = state.SsWorkDate;
                String entry_id = state.SsUsername;
                str_mbreqresign mbreqresign = new str_mbreqresign();
                int result = shrlonService.of_savereq_chgmthshr(state.SsWsPass, as_xmlreq, entry_id, entry_date);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                    dw_data.Reset();
                    dw_data.InsertRow(0);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ"); }
        }

        public void WebSheetLoadEnd()
        {
            tdw_data.Eng2ThaiAllRow();
            dw_data.SetItemString(1, "entry_id", state.SsUsername);
        }

    }
}

