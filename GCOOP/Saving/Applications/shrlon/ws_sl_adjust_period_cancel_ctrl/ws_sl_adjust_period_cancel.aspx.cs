using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_adjust_period_cancel_ctrl
{
    public partial class ws_sl_adjust_period_cancel : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostLoanContractNo { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }

        public void InitJsPostBack()
        {
            //dsCriteria.InitDsCriteria(this);
            //dsList.InitDsList(this);
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //dsCriteria.Ddloantype();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostMemberNo")
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].MEMBER_NO = member_no;
                dsMain.RetrieveMemb(member_no);                
            }
            if (eventArg == "PostLoanContractNo")
            {
                dsMain.RetrieveContract(dsMain.DATA[0].LOANCONTRACT_NO);
                //dsList.Retrieve(dsCriteria.DATA[0].sloantype_code, dsCriteria.DATA[0].eloantype_code);
            }
            //else if (eventArg == "PostDeleteRow")
            //{
            //    String sqldel = ("delete lnreqcontadjustdet where contadjust_docno ='" + dsList.DATA[dsList.GetRowFocus()].CONTADJUST_DOCNO + "'");
            //    Sta tadelcollmast = new Sta(state.SsConnectionString);
            //    tadelcollmast.Exe(sqldel);

            //}
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                string sql = @"update lnreqcontadjust set contadjust_status = -9 where coop_id = {0} and member_no = {1} and loancontract_no = {2}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].LOANCONTRACT_NO);
                Sdt dt = WebUtil.QuerySdt(sql);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                dsMain.ResetRow();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}