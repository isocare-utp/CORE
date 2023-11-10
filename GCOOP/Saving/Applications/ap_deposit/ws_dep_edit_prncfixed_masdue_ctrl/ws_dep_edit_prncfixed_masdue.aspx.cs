using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl
{
    public partial class ws_dep_edit_prncfixed_masdue : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String JsPostDeptaccountno { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMasdue.InitDsMasdue(this);
            dsPrncfixed.InitDsPrncfixed(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                LoadBegin(); 
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "JsPostDeptaccountno")
            {
                PostDeptaccountno();
            } 
        }
        private void LoadBegin()
        {
            dsMain.ResetRow();
        }
        private void PostDeptaccountno()
        {
            try
            {
                string ls_deptaccountno = "";
                string ls_coopcontrol = state.SsCoopControl;
                ls_deptaccountno = dsMain.DATA[0].DEPTACCOUNT_NO.Replace("-", "").Trim();
                ls_deptaccountno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, ls_deptaccountno);
                LoadBegin();
                //retrive data
                dsMain.RetrieveData(ls_coopcontrol, ls_deptaccountno);
                if (dsMain.DATA[0].DEPTACCOUNT_NO != "")
                {
                    dsPrncfixed.Retrieve(ls_coopcontrol, ls_deptaccountno);
                    dsMasdue.Retrieve(ls_coopcontrol, ls_deptaccountno);                   
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขบัญชี " + ls_deptaccountno + " กรุณาตรวจสอบ"); return;
                }

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

        }
        public void SaveWebSheet()
        {
           try
            {
                String ls_error = "";
                String sql = "";
                string ls_depno = dsMain.DATA[0].DEPTACCOUNT_NO;
                ls_depno = ls_depno.Replace("-", "").Trim();
                ls_depno = wcf.NDeposit.of_analizeaccno(state.SsWsPass, ls_depno);
                string ls_coopcontrol = state.SsCoopControl;
                string ls_username = state.SsUsername;
                string ls_clientip = state.SsClientIp;
                int rowdata = 0;
                if (ls_depno != "" && ls_depno != null)
                {
                    //dsMasdue
                    rowdata = dsMasdue.RowCount;
                    if (rowdata > 0)
                    {
                        decimal ld_seq_no = 0, ld_peroid_count = 0, ld_peroid_lasst = 0;
                        for (int i = 0; i < rowdata; i++)
                        {
                            ld_seq_no = 0; ld_peroid_count = 0; ld_peroid_lasst = 0;
                            try
                            {
                                ld_seq_no = dsMasdue.DATA[i].SEQ_NO;
                            }
                            catch { ld_seq_no = 0; }
                            try
                            {
                                ld_peroid_count = dsMasdue.DATA[i].PEROID_COUNT;
                            }
                            catch { ld_peroid_count = 0; }
                            try
                            {
                                ld_peroid_lasst = dsMasdue.DATA[i].PEROID_LAST;
                            }
                            catch { ld_peroid_lasst = 0; }
                            try
                            {
                                sql = @"UPDATE DPDEPTMASDUE SET 
                                 DPDEPTMASDUE.SEQ_NO = {2},   
                                 DPDEPTMASDUE.START_DATE = {3},    
                                 DPDEPTMASDUE.END_DATE = {4},    
                                 DPDEPTMASDUE.PEROID_COUNT = {5},   
                                 DPDEPTMASDUE.PEROID_LAST = {6}   
                           WHERE ( DPDEPTMASDUE.DEPTACCOUNT_NO = {1} ) AND  
                                 ( DPDEPTMASDUE.COOP_ID = {0} ) AND  
                                 ( DPDEPTMASDUE.STILL_USE = 'Y' )  ";
                                sql = WebUtil.SQLFormat(sql, ls_coopcontrol, ls_depno
                                , ld_seq_no, dsMasdue.DATA[i].START_DATE, dsMasdue.DATA[i].END_DATE,
                                ld_peroid_count, ld_peroid_lasst);
                                WebUtil.ExeSQL(sql);
                            }
                            catch (Exception ex)
                            {
                                ls_error += ex;
                            }
                        }
                    }
                        //dsMasdue
                    rowdata = dsPrncfixed.RowCount;
                    if (rowdata > 0)
                    {
                        decimal ld_prnc_no = 0, ld_prnc_amt = 0, ld_prnc_bal = 0, ld_int_rate = 0;

                        for (int i = 0; i < rowdata; i++)
                        {
                            ld_prnc_no = 0; ld_prnc_amt = 0; ld_prnc_bal = 0; ld_int_rate = 0;
                            try
                            {
                                ld_prnc_no = dsPrncfixed.DATA[i].PRNC_NO;
                            }
                            catch { ld_prnc_no = 0; }
                            try
                            {
                                ld_prnc_amt = dsPrncfixed.DATA[i].PRNC_AMT;
                            }
                            catch { ld_prnc_amt = 0; }
                            try
                            {
                                ld_prnc_bal = dsPrncfixed.DATA[i].PRNC_BAL;
                            }
                            catch { ld_prnc_bal = 0; }
                             try
                            {
                                ld_int_rate = dsPrncfixed.DATA[i].INTEREST_RATE;
                            }
                             catch { ld_int_rate = 0; }
                            
                            try
                            {
                                sql = @"UPDATE DPDEPTPRNCFIXED SET
                                DPDEPTPRNCFIXED.PRNC_NO={2} ,
                                DPDEPTPRNCFIXED.PRNC_AMT={3} ,  
                                DPDEPTPRNCFIXED.PRNC_DATE={4} , 
                                DPDEPTPRNCFIXED.PRNCDUE_DATE={5} ,
                                DPDEPTPRNCFIXED.LASTCALINT_DATE={6} ,
                                DPDEPTPRNCFIXED.PRNC_BAL  = {7} ,
                                interest_rate={8} 
                            WHERE ( DPDEPTPRNCFIXED.COOP_ID = {0} ) AND  
                            ( DPDEPTPRNCFIXED.DEPTACCOUNT_NO = {1} )  ";
                                sql = WebUtil.SQLFormat(sql, ls_coopcontrol, ls_depno
                                , ld_prnc_no, ld_prnc_amt, dsPrncfixed.DATA[i].PRNC_DATE, dsPrncfixed.DATA[i].PRNCDUE_DATE,
                                dsPrncfixed.DATA[i].LASTCALINT_DATE, ld_prnc_bal, ld_int_rate);
                                WebUtil.ExeSQL(sql);
                            }
                            catch (Exception ex)
                            {
                                ls_error += ex;
                            }
                        }
                    }
                        
                    if (ls_error.Trim().Length > 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ls_error); return;
                    }
                    else
                    {
                        PostDeptaccountno();
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                    }                    
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }        
        }

        public void WebSheetLoadEnd()
        {
           
        }
    }
}