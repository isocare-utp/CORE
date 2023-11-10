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
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.Globalization;
//using CoreSavingLibrary.WcfNDeposit;
//using CoreSavingLibrary.WcfNCommon;
using Saving;
using DataLibrary;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNDeposit; // new deposit
using CoreSavingLibrary.WcfNCommon; //new common

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_procdeptuptran : PageWebSheet, WebSheet
    {
        // JavaSctipt PostBack
        protected String PostCutProcess;
        protected String PostRetriveDepttrans;
        protected String jsSetMemberFormat;
        protected String jsResetMemberno;
        private DwThDate tdw_processday;

        public string outputProcess;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            PostCutProcess = WebUtil.JsPostBack(this, "PostCutProcess");
            PostRetriveDepttrans = WebUtil.JsPostBack(this, "PostRetriveDepttrans");
            jsSetMemberFormat = WebUtil.JsPostBack(this, "jsSetMemberFormat");
            jsResetMemberno = WebUtil.JsPostBack(this, "jsResetMemberno");
            tdw_processday = new DwThDate(Dw_Main, this);
            tdw_processday.Add("process_date", "process_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdMountCut.Value = "false";
            if (!IsPostBack)
            {
                Dw_Main.InsertRow(0);
                Dw_Main.SetItemDate(1, "process_date", state.SsWorkDate);
                DwUtil.RetrieveDDDW(Dw_Main, "account_id", "dp_depttrans.pbl", null);
                tdw_processday.Eng2ThaiAllRow();
                try
                {
                    string sqlStr = @"update dpdeptmaster 
                    set laststmseq_no = ( select max(ds.seq_no) from dpdeptstatement ds 
                    where dpdeptmaster.deptaccount_no = ds.deptaccount_no )
                    where dpdeptmaster.deptclose_status = 0 and dpdeptmaster.laststmseq_no 
                    not in (select b.seq_no from dpdeptstatement b where dpdeptmaster.deptaccount_no = b.deptaccount_no)";
                    sqlStr = WebUtil.SQLFormat(sqlStr);
                    WebUtil.ExeSQL(sqlStr);
                }
                catch { }
            }
            else
            {
                this.RestoreContextDw(Dw_Main);
                this.RestoreContextDw(Dw_Detail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostCutProcess")
            {
                JsPostCutProcess();
            }
            if (eventArg == "PostRetriveDepttrans")
            {
                JsPostRetriveDepttrans();
            }
            if (eventArg == "jsSetMemberFormat")
            {
                String member_no = "";
                try { member_no = Dw_Main.GetItemString(1, "member_no"); }
                catch { member_no = ""; }
                if (member_no != null && member_no != "")
                {
                    member_no = WebUtil.MemberNoFormat(member_no);
                    Dw_Main.SetItemString(1, "member_no", member_no);
                }
            }
            if (eventArg == "jsResetMemberno")
            {
                Dw_Main.SetItemString(1, "member_no", "");
            }

        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            Dw_Main.SaveDataCache();
            Dw_Detail.SaveDataCache();
        }

        #endregion

        private void JsPostRetriveDepttrans()
        {
            Dw_Detail.InsertRow(0);
            String system_code = Dw_Main.GetItemString(1, "system_code");
            DateTime ProcessDate = new DateTime(1370, 1, 1);
            try
            {
                ProcessDate = Dw_Main.GetItemDateTime(1, "process_date");
            }
            catch { }
            object[] args = new object[] { state.SsCoopId, ProcessDate,system_code };
            DwUtil.RetrieveDataWindow(Dw_Detail, "dp_depttrans.pbl", null, args);
            //Dw_Detail.Retrieve(state.SsCoopControl, "KEP", ProcessDate);
            String member_no = "";
            try { member_no = Dw_Main.GetItemString(1, "member_no"); }
            catch { member_no = ""; }
            if (member_no != null && member_no != "")
            {
                Dw_Detail.SetFilter("member_no = '" + member_no + "'");
                Dw_Detail.Filter();
            }
            Label1.Text = "จำนวนรายการทั้งหมด " + Dw_Detail.RowCount.ToString() + " รายการ";
        }

        private void JsPostCutProcess()
        {
            String system_code = Dw_Main.GetItemString(1, "system_code");
     //       if (HdSkipError.Value.ToString().Trim() != "true")
      //      {
            
                //try
                //{
                //    DateTime ProcessDate = new DateTime(1370, 1, 1);
                //    try
                //    {
                //        ProcessDate = Dw_Main.GetItemDateTime(1, "process_date");
                //    }
                //    catch { }
                //    DepositClient depService = wcf.Deposit;
                //    depService.RunDeptDepttransLoan(state.SsWsPass, state.CurrentPage, state.SsApplication, ProcessDate,system_code, state.SsUsername, state.SsClientIp, state.SsCoopControl);
                //    // HdMountCut.Value = "true";
                //}
                //catch (Exception ex)
                //{
                //    HdSkipError.Value = "true";
                //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    
                //}
       //     }

                try
                {
                    DateTime ProcessDate = new DateTime(1370, 1, 1);
                    try
                    {
                        ProcessDate = Dw_Main.GetItemDateTime(1, "process_date");
                    }
                    catch { }
                    //DepositClient depService = wcf.Deposit;
                    string ls_assid = "";
                    try
                    {
                        ls_assid = Dw_Main.GetItemString(1, "account_id");
                    }
                    catch { ls_assid = ""; }
                    if (ls_assid != "")
                    {
                        string sql_update = @"update dpucfrecppaytype set default_accid={1} where recppaytype_code = {0} ";
                        sql_update = WebUtil.SQLFormat(sql_update, system_code, ls_assid);
                        WebUtil.ExeSQL(sql_update);
                    }
                    //depService.RunDeptDepttransLoan(state.SsWsPass, state.CurrentPage, state.SsApplication, ProcessDate, system_code, state.SsUsername, state.SsClientIp, state.SsCoopControl);
                    string option_xml = Dw_Main.Describe("DataWindow.Data.XML");
                    outputProcess = WebUtil.runProcessing(state, "DPTRANDEPT", option_xml, state.SsClientIp, "");

                  //  HdMountCut.Value = "true";
                   // HdSkipError.Value = "false";
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);

                }

        }
    }
}