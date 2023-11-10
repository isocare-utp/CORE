using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_ucf_fundcollkeeprate_ctrl
{
    public partial class ws_fund_ucf_fundcollkeeprate : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string JsPostRetriveData { get; set; }
        [JsPostBack]
        public string JsPostRetriveRate { get; set; }
        
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdFundcolltype(state.SsCoopControl);
            }            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertLastRow();
                dsList.DdLoantype();
                int ln_seqno = dsList.RowCount;
                dsList.DATA[ln_seqno - 1].SEQ_NO = ln_seqno;        
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsList.GetRowFocus();
                string ls_coopcontrol =state.SsCoopControl;
                string ls_fundtype = dsMain.DATA[0].FUNDKEEPTYPE;
                string ls_loantype = dsList.DATA[row].LOANTYPE_CODE;
                decimal ld_seq_no  = dsList.DATA[row].SEQ_NO;
                try
                {
                    string sql = @"DELETE FROM FUNDCOLLKEEPRATE WHERE  COOP_ID = {0} and FUNDKEEPTYPE = {1} 
                    AND LOANTYPE_CODE = {2} AND SEQ_NO = {3}";
                    sql = WebUtil.SQLFormat(sql, ls_coopcontrol, ls_fundtype, ls_loantype, ld_seq_no);
                    WebUtil.ExeSQL(sql);
                    dsList.RetriveData(ls_coopcontrol, ls_fundtype);
                    dsList.DdLoantype();
                    LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                }
                catch(Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ "+ex);
                    return;
                }

            }          
            else if (eventArg == JsPostRetriveData)
            {
                string ls_fundtype = dsMain.DATA[0].FUNDKEEPTYPE;
                dsList.RetriveData(state.SsCoopControl, ls_fundtype);
                dsList.DdLoantype();
            }           
        }
     

        public void SaveWebSheet()
        {
            string sqlStr, ls_loantype="";
            decimal ln_seqno = 0, ld_loan_stepin = 0, ld_loan_step = 0, ld_keep_min = 0, ld_keep_max = 0, ld_keep_percent=0;
            int li_row;
            try
            {
                string ls_coopcontrol = state.SsCoopControl;
                string ls_fundcode = dsMain.DATA[0].FUNDKEEPTYPE;
                for (li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    ls_loantype = dsList.DATA[li_row].LOANTYPE_CODE;
                    ln_seqno = dsList.DATA[li_row].SEQ_NO;
                    ld_loan_stepin = dsList.DATA[li_row].LOAN_STEPMIN;
                    ld_loan_step = dsList.DATA[li_row].LOAN_STEP;
                    ld_keep_min = dsList.DATA[li_row].KEEPMIN_AMT;
                    ld_keep_max = dsList.DATA[li_row].KEEPMAX_AMT;
                    ld_keep_percent = dsList.DATA[li_row].keep_percent;
                    string sql = @"SELECT LOANTYPE_CODE FROM FUNDCOLLKEEPRATE where COOP_ID={0} and FUNDKEEPTYPE={1} AND LOANTYPE_CODE={2}  AND SEQ_NO = {3}";
                    sql = WebUtil.SQLFormat(sql, ls_coopcontrol, ls_fundcode, ls_loantype, ln_seqno);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        sqlStr = @"UPDATE FUNDCOLLKEEPRATE SET 
                                LOAN_STEPMIN = {4} , LOAN_STEP = {5} , KEEPMIN_AMT = {6} , KEEPMAX_AMT = {7} , keep_percent = {8}
                                WHERE COOP_ID = {0} and FUNDKEEPTYPE = {1} AND LOANTYPE_CODE = {2} AND SEQ_NO = {3}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopcontrol, ls_fundcode, ls_loantype, ln_seqno
                            , ld_loan_stepin, ld_loan_step, ld_keep_min, ld_keep_max,ld_keep_percent);
                        WebUtil.ExeSQL(sqlStr);
                    }
                    else
                    {
                        sqlStr = @"INSERT INTO FUNDCOLLKEEPRATE 
                            (COOP_ID        , FUNDKEEPTYPE  ,LOANTYPE_CODE  ,SEQ_NO
                            ,LOAN_STEPMIN   , LOAN_STEP     , KEEPMIN_AMT   , KEEPMAX_AMT 
                            , keep_percent)
                            VALUES
                            ({0}            , {1}           , {2}           ,{3}
                            ,{4}            , {5}           , {6}           ,{7}
                            ,{8})";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopcontrol, ls_fundcode, ls_loantype, ln_seqno
                            , ld_loan_stepin, ld_loan_step, ld_keep_min, ld_keep_max, ld_keep_percent);
                        WebUtil.ExeSQL(sqlStr);
                    }
                }
                dsList.ResetRow();
                dsList.RetriveData(ls_coopcontrol, ls_fundcode);
                dsList.DdLoantype();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ " + ex.Message); return;
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}