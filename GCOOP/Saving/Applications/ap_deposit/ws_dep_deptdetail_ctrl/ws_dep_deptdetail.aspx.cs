using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;
using CoreSavingLibrary.WcfNDeposit;
using System.Drawing;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class ws_dep_deptdetail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostDeptno { get; set; }
        [JsPostBack]
        public String PostCalaccuint { get; set; }
        
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);            
            dsFixed.InitDsFixed(this);
            dsCodept.InitDsCodept(this);
            //dsPics.InitDsPics(this);
            dsChgdept.InitDsChgdept(this);
            dsMasdue.InitDsMasdue(this);
        }

        public void WebSheetLoadBegin()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");            
            if (!IsPostBack)
            {
                LoadBegin();
            }           
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostDeptno")
            {
                string DEPTACCOUNT_NO = dsMain.DATA[0].DEPTACCOUNT_NO;
                DEPTACCOUNT_NO = wcf.NDeposit.of_analizeaccno(state.SsWsPass, DEPTACCOUNT_NO);
                LoadBegin();
                dsMain.RetrieveMain(DEPTACCOUNT_NO);
                if (dsMain.DATA[0].DEPTACCOUNT_NO == "" || dsMain.DATA[0].DEPTACCOUNT_NO == null) { 
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลเลขบัญชี " + DEPTACCOUNT_NO + " ที่ระบุ"); return; 
                }
                else
                {
                    decimal ldc_sequest_amount = 0, ldc_checkpend_amt = 0, ldc_minprncbal = 0, ldc_widthdraw = 0;
                    string ls_sequest_status = "";
                    ldc_sequest_amount = dsMain.DATA[0].SEQUEST_AMOUNT;
                    ls_sequest_status = dsMain.DATA[0].SEQUEST_STATUS;
                    ldc_checkpend_amt = dsMain.DATA[0].CHECKPEND_AMT;
                    ldc_minprncbal = dsMain.DATA[0].minprncbal;
                    if (ls_sequest_status != "1") { ldc_sequest_amount = 0; }
                    ldc_widthdraw = dsMain.DATA[0].PRNCBAL - ldc_sequest_amount - ldc_checkpend_amt - ldc_minprncbal;
                    dsMain.DATA[0].sum_withdraw = (ldc_widthdraw > 0) ? ldc_widthdraw : 0; 

                    btnUpload_Click(DEPTACCOUNT_NO);                    
                    dsFixed.RetrieveFixed(DEPTACCOUNT_NO);
                    dsCodept.RetrieveCodept(DEPTACCOUNT_NO);
                    cal_sum();
                    //dsPics.RetrievePisc(DEPTACCOUNT_NO);
                    dsMain.DATA[0].DEPTACCOUNT_NO = WebUtil.ViewAccountNoFormat(DEPTACCOUNT_NO);
                    dsChgdept.RetrieveData(DEPTACCOUNT_NO);
                    dsMasdue.RetrieveData(DEPTACCOUNT_NO);
                }
            }
            else if (eventArg == "PostCalaccuint") {
                Calaccuint();
            }
        }
        private void LoadBegin() {
            GridView1.DataSource = null;
            GridView1.DataBind();
            dsMain.ResetRow();
            dsFixed.ResetRow();
            dsCodept.ResetRow();
            //dsPics.ResetRow();
            dsChgdept.ResetRow();
            dsMasdue.ResetRow();
            HdChangePage.Value = "false";
        }

        private void Calaccuint() { 
            try{
                string ls_coopcontrol = state.SsCoopControl;
                string ls_depno = dsMain.DATA[0].DEPTACCOUNT_NO.Trim().Replace("-","");
                decimal accu_int = 0 ,tax_amt = 0;
                int result = wcf.NDeposit.of_recallinterest(state.SsWsPass, dsMain.DATA[0].DEPTTYPE_CODE, ls_depno, ls_coopcontrol, state.SsWorkDate, ref accu_int, ref tax_amt);
                if (result == 1) {
                    accu_int = Math.Round(accu_int, 2);
                    dsMain.DATA[0].ACCUINT_AMT = accu_int;
                    string sqlStr = @"update dpdeptmaster 
                                                set ACCUINT_AMT = {2} 
                                                where coop_id = {0} and deptaccount_no = {1}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_coopcontrol, ls_depno, accu_int);
                    WebUtil.ExeSQL(sqlStr);
                }
            }catch(Exception ex){
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถคำนวณดอกเบี้ยได้ " + ex); 
            }
        }
        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }
        public void cal_sum()
        {
            int ls_row_count = dsFixed.RowCount;
            decimal sum_brnbal = 0;
            for (int i = 0; i < ls_row_count; i++)
            {
                decimal ldc_prin = dsFixed.DATA[i].PRNC_BAL;
                sum_brnbal += ldc_prin;


            }
            dsFixed.TContract.Text = sum_brnbal.ToString("#,##0.00");
        }
        public void ReSet()
        {
            dsMain.ResetRow();
            dsCodept.ResetRow();
            dsFixed.ResetRow();
            //dsPics.ResetRow();

        }
        public void btnUpload_Click(string deptaccount_no)
        {
            DataTable table = new DataTable();
            String sql = @"SELECT  DPDEPTSTATEMENT.DEPTACCOUNT_NO,   
                        DPDEPTSTATEMENT.SEQ_NO,    
                        DPDEPTSTATEMENT.PRNCBAL,      
                        DPDEPTSTATEMENT.ENTRY_ID,                            
                        DPDEPTSTATEMENT.PRNC_NO,   
                        DPDEPTSTATEMENT.RETINT_AMT,   
                        DPDEPTSTATEMENT.ENTRY_DATE,  
                        DPDEPTSTATEMENT.OPERATE_DATE,   
						DPDEPTSTATEMENT.ITEM_STATUS,
                        DPDEPTSTATEMENT.ACCUINT_AMT,    
                        DPDEPTSTATEMENT.CHRG_AMT,   
                        DPDEPTSTATEMENT.REF_SEQ_NO,     
                        DPDEPTSTATEMENT.DEPTSLIP_NO, 
                        DPDEPTSTATEMENT.INT_AMT,  
                        DPUCFDEPTITEMTYPE.DEPTITEM_GROUP,
                        (case when DPUCFDEPTITEMTYPE.SIGN_FLAG =1 then DPDEPTSTATEMENT.DEPTITEM_AMT else NULL  end )  as cp_deposit ,
                        (case when DPUCFDEPTITEMTYPE.SIGN_FLAG =-1 then DPDEPTSTATEMENT.DEPTITEM_AMT else NULL  end )  as cp_withdraw,
					 (case when DPDEPTSTATEMENT.NO_BOOK_FLAG =1 then DPUCFDEPTITEMTYPE.DEPTITEMTYPE_CODE else DPUCFDEPTITEMTYPE.print_code end ) as cp_book_flag 
                FROM DPDEPTSTATEMENT,   
                        DPUCFDEPTITEMTYPE  
                WHERE ( DPDEPTSTATEMENT.DEPTITEMTYPE_CODE = DPUCFDEPTITEMTYPE.DEPTITEMTYPE_CODE ) and  
                        ( ( dpdeptstatement.deptaccount_no = {1}  ) ) AND  
                        DPDEPTSTATEMENT.COOP_ID = {0} 
            order by DPDEPTSTATEMENT.SEQ_NO ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, deptaccount_no);
            table = WebUtil.QuerySdt(sql);

            table.Columns.Add("seq_no");
            table.Columns.Add("entry_date");
            table.Columns.Add("deptslip_no");
            //DataRow r = null;
            //for (int i = 0; i < 25; i++)
            //{
            //    r = table.NewRow();
            //    r.ItemArray = new object[] { table.Columns.Add("seq_no"),
            //        table.Columns.Add("entry_date"),table.Columns.Add("deptslip_no"),table.Columns.Add("cp_book_flag"),table.Columns.Add("cp_withdraw"),
            //        table.Columns.Add("cp_deposit"),table.Columns.Add("prncbal"),table.Columns.Add("accuint_amt"),
            //        table.Columns.Add("entry_id"), table.Columns.Add("ref_seq_no"),table.Columns.Add("prnc_no"),table.Columns.Add("int_amt"),
            //        table.Columns.Add("chrg_amt")};
            //    table.Rows.Add(r);
            //}
            GridView1.DataSource = table;
            GridView1.DataBind();
            if (HdChangePage.Value != "true")
            {
                GridView1.PageIndex = GridView1.PageCount - 1;
                GridView1.DataBind();
            }                     
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                string a = e.Row.Cells[14].Text;
                if (e.Row.Cells[14].Text.Equals("-1"))//item_status
                {
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[8].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[9].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                    e.Row.Cells[13].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
					e.Row.Cells[14].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb8cf");
                }
                //else if (e.Row.Cells[15].Text.Equals("ERR") || e.Row.Cells[15].Text.Equals("ADJ"))
            }
            //if (e.Row.RowIndex >= 0)
            //{
            //    if (e.Row.Cells[4].Text.Equals(null))
            //    {
            //        e.Row.Cells[5].BackColor = Color.Green;
            //        e.Row.Cells[4].BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        e.Row.Cells[4].BackColor = Color.Red;
            //        e.Row.Cells[5].BackColor = Color.Green;
            //    }
            //}
        }

        //แบ่งหน้า
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HdChangePage.Value = "true";
            string deptaccount_no = dsMain.DATA[0].DEPTACCOUNT_NO;
            deptaccount_no = wcf.NDeposit.of_analizeaccno(state.SsWsPass, deptaccount_no);
            GridView1.PageIndex = e.NewPageIndex;
            btnUpload_Click(deptaccount_no);
        }
    }
}