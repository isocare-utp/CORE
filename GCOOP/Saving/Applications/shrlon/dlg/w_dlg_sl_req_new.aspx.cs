using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using DataLibrary;
using System.Data;
namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_req_new: PageWebDialog, WebDialog
    {
        protected String jsPostMember;
        protected string jsSavesheet;
        protected string jsCheckduplicate;

        string member_no = "";
        private DwThDate tdwDetail;
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "member_no");
            jsSavesheet = WebUtil.JsPostBack(this, "jsSavesheet");
            jsCheckduplicate = WebUtil.JsPostBack(this, "jsCheckduplicate");

            tdwDetail = new DwThDate(Dw_detail);
            //lnreqreceive_tdate
            tdwDetail.Add("lnreqreceive_date", "lnreqreceive_tdate");
            tdwDetail.Add("lnmeeting_date", "lnmeeting_tdate");
            //refresh = WebUtil.JsPostBack(this, "refresh");
        }

        public void WebDialogLoadBegin()
        {            
            try
            {
                this.ConnectSQLCA();
                Dw_detail.SetTransaction(sqlca);
            }
            catch { }
            if (IsPostBack)
            {
                this.RestoreContextDw(Dw_detail,tdwDetail);
            }
            else
            {
                Dw_detail.InsertRow(0);
                try
                {
                    // member_no = Request["member_no"].ToString();
                    Dw_detail.SetItemDate(1, "lnreqreceive_date", state.SsWorkDate);
                    Dw_detail.SetItemDate(1, "lnmeeting_date", state.SsWorkDate);
                    Dw_detail.SetItemString(1, "coop_id", state.SsCoopId);
                    Dw_detail.SetItemString(1, "entry_id", state.SsUsername);
                    DwUtil.RetrieveDDDW(Dw_detail, "loanentry_id", "sl_req_loanregister.pbl", null);
                }
                catch { }
                //JsNewClear();
            }
            tdwDetail.Eng2ThaiAllRow();
            
            //
        }
        private void SaveWebsheet()
        {
            String newreqreg_no, loantype_code, remark, entry_id, loanentry_id, ems_docno;
            decimal loanrequest_amt,reqregister_status,loanapprove_amt;
            DateTime lnreqreceive_date,lnmeeting_date,lnapprove_date;
            try
            {
                newreqreg_no = wcf.NCommon.of_getnewdocno(state.SsWsPass,state.SsCoopId, "LONEBOOKNO");
                tdwDetail.Eng2ThaiAllRow();
                try
                {
                    member_no = Request["member_no"].ToString();
                }
                catch { member_no = ""; }
                try
                {
                    loantype_code = Dw_detail.GetItemString(1, "loantype_code");
                }
                catch { loantype_code = "20"; }
                try
                {
                    loanrequest_amt = Dw_detail.GetItemDecimal(1, "loanrequest_amt");
                }
                catch { loanrequest_amt = 0; }
                try
                {
                    remark = Dw_detail.GetItemString(1, "remark");
                }
                catch { remark = ""; }
                try
                {
                    reqregister_status = 8;
                }
                catch { reqregister_status = 8; }
                try
                {
                    entry_id = state.SsUsername;
                }
                catch { entry_id = ""; }
                try
                {
                    lnreqreceive_date = Dw_detail.GetItemDate(1, "lnreqreceive_date");
                }
                catch { lnreqreceive_date = state.SsWorkDate; }
                try
                {
                    lnmeeting_date = Dw_detail.GetItemDate(1, "lnmeeting_date");
                }
                catch { lnmeeting_date = state.SsWorkDate; }
                try 
                {
                    lnapprove_date = Dw_detail.GetItemDate(1,"lnapprove_date");

                }
                catch{lnapprove_date=state.SsWorkDate;}
                try
                {
                    loanapprove_amt = Dw_detail.GetItemDecimal (1,"loanapprove_amt");
                }
                catch { loanapprove_amt = 0; }                
                try
                {
                    loanentry_id = Dw_detail.GetItemString(1, "loanentry_id");
                }
                catch { loanentry_id = ""; }  
                try
                {
                    ems_docno = Dw_detail.GetItemString(1, "ems_docno");
                }
                catch { ems_docno = ""; }

            try
            {
                String sql = @"  INSERT INTO LNREQLOANREGISTER  
                ( REQREGISTER_DOCNO,    MEMBER_NO,      LOANTYPE_CODE,      LOANREQUEST_AMT,    LNREQRECEIVE_DATE,
                  LNMEETING_DATE,       LNAPPROVE_DATE, REQREGISTER_STATUS, REMARK,             ENTRY_ID,       
                  LOANAPPROVE_AMT,      COOP_ID,        loanentry_id,      ems_docno) 
          VALUES( {0},                  {1},            {2},                {3},                {4},                  
                  {5},                  {6},            {7},                {8},                {9},                  
                  {10},                 {11},           {12},               {13})
";
                object[] arr = new object[14];
                arr[0] = newreqreg_no;
                arr[1] = member_no;
                arr[2] = loantype_code;
                arr[3] = loanrequest_amt;
                try
                {
                    arr[4] = lnreqreceive_date;
                }
                catch { arr[4] = new DateTime(1500, 1, 1); }
                try
                {
                    arr[5] = lnmeeting_date;
                }
                catch { arr[5] = new DateTime(1500, 1, 1); }
                try
                {
                    arr[6] = lnapprove_date;
                }
                catch { arr[6] = new DateTime(1500, 1, 1); }
                arr[7] = reqregister_status;
                arr[8] = remark;
                arr[9] = entry_id;
                arr[10] = loanapprove_amt;
                arr[11] = state.SsCoopId;
                arr[12] = loanentry_id;
                arr[13] = ems_docno;
                sql = WebUtil.SQLFormat(sql, arr);
                int sql_q = WebUtil.ExeSQL(sql);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");

                this.SetOnLoadedScript("parent.jsPostMember() \n parent.RemoveIFrame()");
            }
            catch { }
            }catch{}
            JsNewClear();
        } 
        private void JsNewClear()
        {            
            Dw_detail.Reset();
            Dw_detail.InsertRow(0);
            Dw_detail.DisplayOnly=true;           
        }
        private void JsCheckduplicate() 
        {
            member_no = Request["member_no"].ToString();
            string loantype_code = Dw_detail.GetItemString(1, "loantype_code");

            string sql_chk = "select  member_no  from LNREQLOANREGISTER where REQREGISTER_STATUS = 8 and LOANTYPE_CODE  = '" + loantype_code + "' and member_no = '" + member_no + "'";
            Sdt dt = WebUtil.QuerySdt(sql_chk);
            if (dt.Next()) {
                LtServerMessage.Text = WebUtil.ErrorMessage(" สมาชิกท่านนี้ ได้บันทีก ลงรับหนังสือกู้ ประเภท นี้แล้ว ");
            
            }
        
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            if (eventArg == "jsSavesheet")
            {
                SaveWebsheet();
            }
            else if (eventArg == "jsCheckduplicate") {
                JsCheckduplicate();
            }         
        }
        public void JsPostMember()
        {
            string member_no = WebUtil.MemberNoFormat(Hfmember_no.Value.ToString());
        }

        public void WebDialogLoadEnd()
        {            
            tdwDetail.Eng2ThaiAllRow();
            Dw_detail.SaveDataCache();
            //DwUtil.RetrieveDDDW(Dw_detail, "loanentry_id", "sl_req_loanregister.pbl", null);
        }
        public void Refresh() { }     
    }
}
