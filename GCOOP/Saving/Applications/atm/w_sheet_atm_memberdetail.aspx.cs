using System;
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
using Sybase.DataWindow;
using DataLibrary;
using Saving.WcfATM;
using System.Globalization;
using CoreSavingLibrary;

namespace Saving.Applications.atm
{
    public partial class w_sheet_atm_memberdetail : PageWebSheet, WebSheet
    {
        protected String jsPostBack;
        protected String jsPostMemberNo;
        protected String jsRetrieve;

        String pbl = "dp_atm_memberdetail.pbl";
        public void InitJsPostBack()
        {
            jsPostBack = WebUtil.JsPostBack(this, "jsPostBack");
            jsPostMemberNo = WebUtil.JsPostBack(this, "jsPostMemberNo");
            jsRetrieve = WebUtil.JsPostBack(this, "jsRetrieve");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwDetail.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwDetail);
                this.RestoreContextDw(DwMemberlog);
                this.RestoreContextDw(DwErrorlog);
                this.RestoreContextDw(DwWithdraw);
                this.RestoreContextDw(DwDept);
                this.RestoreContextDw(DwLoan);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostMemberNo":
                    JsPostMemberNo();
                    break;
                case "jsRetrieve":
                    JsRetrieve();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                //DwUtil.RetrieveDDDW(DwMain, "coop_id", "dddw_coopmaster", null);
            }
            catch { }
            DwDetail.SaveDataCache();
            DwErrorlog.SaveDataCache();
            DwMemberlog.SaveDataCache();
            DwWithdraw.SaveDataCache();
            DwDept.SaveDataCache();
            DwLoan.SaveDataCache();
        }

        public void JsPostMemberNo()
        {
            Sta ta = new Sta(state.SsConnectionString);
            try
            {
                string member_no = "00000000" + DwDetail.GetItemString(1, "member_no");
                member_no = member_no.Substring(member_no.Length - 8, 8);
                DwDetail.Reset();
                DwDetail.InsertRow(0);
                try
                {
                    DwDetail.SetItemString(1, "member_no", member_no);
                }
                catch { }
                WebUtil.RetrieveDDDW(DwDetail, "atmcard_id", "dddw_atmcard_id", member_no);
                String SqlSelect = "select atmcard_id from atmmember where member_No  = '" + member_no + "' order by atmclose,atmeffective_date";
                Sdt dt = ta.Query(SqlSelect);
                if (dt.Next())
                {
                    DwDetail.SetItemString(1, "atmcard_id", dt.GetString("atmcard_id"));
                    JsRetrieve();
                }
                ta.Close();
                
            }
            catch (Exception ex)
            {
                ta.Close();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void JsRetrieve()
        {
            try
            {
                String pbl = "dp_atm_memberdetail.pbl";
                String member_no = DwDetail.GetItemString(1, "member_no").Trim();
                String atmcard_id = DwDetail.GetItemString(1, "atmcard_id").Trim();
                DwUtil.RetrieveDataWindow(DwDetail, pbl, null, atmcard_id);
                DwUtil.RetrieveDataWindow(DwErrorlog, pbl, null, "%" + member_no + "%", "%" + atmcard_id + "%");
                DwUtil.RetrieveDataWindow(DwMemberlog, pbl, null, atmcard_id);
                DwUtil.RetrieveDataWindow(DwWithdraw, pbl, null, atmcard_id);
                DwUtil.RetrieveDataWindow(DwDept, pbl, null, atmcard_id);
                DwUtil.RetrieveDataWindow(DwLoan, pbl, null, atmcard_id);

                WebUtil.RetrieveDDDW(DwErrorlog, "atmresponse_code", "dddw_atmucfresponse");
                WebUtil.RetrieveDDDW(DwErrorlog, "atmtran_code", "dddw_atmucftranresult");
                WebUtil.RetrieveDDDW(DwMemberlog, "atmitemtype_code", "dddw_atmucfitemtype");

                DwUtil.RetrieveDDDW(DwWithdraw, "atmapproval_code", "dddw_atmucfapproval", null);
                DwUtil.RetrieveDDDW(DwWithdraw, "atmsystem_code", "dddw_atmucftransystem", null);
                DwUtil.RetrieveDDDW(DwWithdraw, "atmoperate_code", "dddw_atmucfoperate", null);
                DwUtil.RetrieveDDDW(DwWithdraw, "atmtran_code", "dddw_atmucftranresult", null);

                for (int i = 1; i <= DwLoan.RowCount; i++)
                {
                    String loancontract_no = DwLoan.GetItemString(i, "loancontract_no");
                    WcfATM.ATMcoreWebClient atmService = new WcfATM.ATMcoreWebClient();
                    WcfATM.RsInquiry RsInquiry = atmService.RqInquiry("LN", loancontract_no);
                    Double ledgerBalance = RsInquiry.availBalance;
                    DwLoan.SetItemDecimal(i, "credit_amt", Convert.ToDecimal(ledgerBalance));
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}