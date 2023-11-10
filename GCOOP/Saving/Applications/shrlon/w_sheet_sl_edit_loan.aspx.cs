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
    public partial class w_sheet_sl_edit_loan : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jscontract_no;
        protected String newClear;
        public void InitJsPostBack()
        {
            jscontract_no = WebUtil.JsPostBack(this, "jscontract_no");
            tDwMain = new DwThDate(dw_main, this);

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
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);

            }
            else
            {
                if (dw_main.RowCount < 1)
                {
                    dw_main.InsertRow(0);
                    dw_main.SetItemString(1, "cancel_id", state.SsUsername);
                    dw_main.SetItemDate(1, "cancel_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jscontract_no")
            {

                Jscontract_no();

            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                String loancontract_no = "", member_no = "";
                Decimal loanapprove_amt = 0, withdrawable_amt = 0, period_payamt = 0, period_payment = 0;
                member_no = dw_main.GetItemString(1, "member_no");
                loancontract_no = dw_main.GetItemString(1, "loancontract_no");
                loanapprove_amt = dw_main.GetItemDecimal(1, "loanapprove_amt");
                withdrawable_amt = dw_main.GetItemDecimal(1, "withdrawable_amt");
                period_payamt = dw_main.GetItemDecimal(1, "period_payamt");
                period_payment = dw_main.GetItemDecimal(1, "period_payment");
                String sqlupdate = @"  UPDATE LNCONTMASTER  
                         SET LOANAPPROVE_AMT = " + loanapprove_amt + @",   
                             withdrawable_amt = " + withdrawable_amt + @",
                             LOANREQUEST_AMT = " + loanapprove_amt + @",    
                             PERIOD_PAYMENT = " + period_payment + @",   
                             PERIOD_PAYAMT = " + period_payamt + @"  
                       WHERE ( LNCONTMASTER.LOANCONTRACT_NO ='" + loancontract_no + @"'  ) AND  
                             ( LNCONTMASTER.MEMBER_NO = '" + member_no + @"'  ) ";

                ta.Exe(sqlupdate);

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            ta.Close();
        }

        public void WebSheetLoadEnd()
        {
            this.dw_main.SaveDataCache();
        }

        public void Jscontract_no()
        {
            try
            {
                string as_coopid = state.SsCoopControl;
                String as_contno = HContract.Value;
                String xmlcontno = shrlonService.of_initreq_contcancel(state.SsWsPass, as_coopid, as_contno);

                dw_main.Reset();
                DwUtil.ImportData(xmlcontno, dw_main, null, FileSaveAsType.Xml);

                tDwMain.Eng2ThaiAllRow();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                //"ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                dw_main.Reset(); dw_main.InsertRow(0); dw_main.SetItemString(1, "cancel_id", state.SsUsername);
                dw_main.SetItemDate(1, "cancel_date", state.SsWorkDate); tDwMain.Eng2ThaiAllRow();
            }
        }

        //JS-EVENT
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_main.InsertRow(0);
            dw_main.SetItemString(1, "cancel_id", state.SsUsername);
            dw_main.SetItemDate(1, "cancel_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
        }
    }
}
