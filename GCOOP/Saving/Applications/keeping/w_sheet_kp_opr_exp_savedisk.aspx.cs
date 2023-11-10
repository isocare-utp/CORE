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
using DataLibrary;
using System.IO;


namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_opr_exp_savedisk : PageWebSheet, WebSheet
    {
        DataStore DStore;
        public String pbl = "kp_exp_savedisk.pbl";
        protected String postInit;
        protected String postChangeOption;
        protected String postRetrieve;
        protected String postRetrieveSum;
        private DwThDate tDw_main;
        //==============================
        public void InitJsPostBack()
        {

            postInit = WebUtil.JsPostBack(this, "postInit");
            postChangeOption = WebUtil.JsPostBack(this, "postChangeOption");
            postRetrieve = WebUtil.JsPostBack(this, "postRetrieve");
            postRetrieveSum = WebUtil.JsPostBack(this, "postRetrieveSum");
            //==================================
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("operate_date", "operate_tdate");

            //===================================
            DwUtil.RetrieveDDDW(Dw_choice, "format_text", pbl, state.SsCoopId);
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_detail.SetTransaction(sqlca);

            try
            {
                if (!IsPostBack)
                {
                    JspostNewClear();
                }
                else
                {
                    this.RestoreContextDw(Dw_choice);
                    this.RestoreContextDw(Dw_main);
                    this.RestoreContextDw(Dw_detail);
                    this.RestoreContextDw(Dw_list);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postChangeOption")
            {
                JspostChangeOption();
            }
            else if (eventArg == "postRetrieve")
            {
                Dw_list.InsertRow(0);
                String ls_sql = "", ls_sqlwhere = "";

                String format_text = Dw_choice.GetItemString(1, "format_text");
                String year = Dw_main.GetItemString(1, "year");
                String month = Dw_main.GetItemString(1, "month");
                if (month.Length != 2)
                {
                    month = "0" + month;
                }
                String recv_period = year + month;

                ls_sqlwhere = of_getconfigexp();

                if (format_text == "001" || format_text == "002")
                {

                    ls_sql = @"select kp.coop_id,
                        kp.member_no, 
                        mb.salary_id, 
                        mup.prename_desc,
                        mb.memb_name,
                        mb.memb_surname,
                        kp.membgroup_code,
                        mug.membgroup_desc,
                        sum(exp.item_payment) as receive_amt  
                        from kptempreceive kp, 
                        kpreceiveexpense exp, 
                        mbmembmaster mb,
                        mbucfprename mup,
                        mbucfmembgroup mug  
                        where ( kp.coop_id = exp.coop_id )  
	                        and ( kp.kpslip_no = exp.kpslip_no )  
	                        and ( kp.recv_period = exp.recv_period ) 
	                        and ( kp.coop_id = mb.coop_id )  
	                        and ( kp.member_no = mb.member_no )  
	                        and ( mb.prename_code = mup.prename_code (+) )  
	                        and ( kp.coop_id = mug.coop_id (+) )
	                        and ( kp.membgroup_code = mug.membgroup_code (+) )
	                        and ( kp.coop_id = {0} )    
	                        and ( kp.recv_period = {1} )  
	                        and ( exp.expense_code = 'SAL' )
	                        and ( exp.item_payment > 0 )  
	                        " + ls_sqlwhere + @"  
                        group by kp.coop_id,
                        kp.member_no, 
                        mb.salary_id, 
                        mup.prename_desc,
                        mb.memb_name,
                        mb.memb_surname,
                        kp.membgroup_code,
                        mug.membgroup_desc";
                }
                else
                {
                    ls_sql = @"select kp.member_no, 
                        mb.salary_id, 
                        kp.membgroup_code, 
                        sum(exp.item_payment) as total  
                        from kptempreceive kp, 
                        kpreceiveexpense exp, 
                        mbmembmaster mb  
                        where ( kp.coop_id = exp.coop_id )  
	                        and ( kp.kpslip_no = exp.kpslip_no )  
	                        and ( kp.recv_period = exp.recv_period ) 
	                        and ( kp.coop_id = mb.coop_id )  
	                        and ( kp.member_no = mb.member_no )  
	                        and ( kp.coop_id = {0} )    
	                        and ( kp.recv_period = {1} )  
	                        and ( exp.expense_code = 'SAL' )
	                        and ( exp.item_payment > 0 )  
	                        " + ls_sqlwhere + @"  
                        group by kp.member_no, mb.salary_id, kp.membgroup_code
                        order by kp.member_no";
                }
                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, recv_period);
                DwUtil.ImportData(ls_sql, Dw_list, null);
            }
            else if (eventArg == "postRetrieveSum")
            {
                Dw_sum.InsertRow(0);
                String ls_sql = "", ls_sqlwhere = "";

                String format_text = Dw_choice.GetItemString(1, "format_text");
                String year = Dw_main.GetItemString(1, "year");
                String month = Dw_main.GetItemString(1, "month");
                if (month.Length != 2)
                {
                    month = "0" + month;
                }
                String recv_period = year + month;

                ls_sqlwhere = of_getconfigexp();

                if (format_text == "001" || format_text == "002")
                {

                    ls_sql = @"select count(distinct kp.member_no) as sum_amt,
                        sum(exp.item_payment) as receive_amt  
                        from kptempreceive kp, 
                        kpreceiveexpense exp, 
                        mbmembmaster mb  
                        where ( kp.coop_id = exp.coop_id )  
	                        and ( kp.kpslip_no = exp.kpslip_no )  
	                        and ( kp.recv_period = exp.recv_period ) 
	                        and ( kp.coop_id = mb.coop_id )  
	                        and ( kp.member_no = mb.member_no )  
	                        and ( kp.coop_id = {0} )    
	                        and ( kp.recv_period = {1} )  
	                        and ( exp.expense_code = 'SAL' )
	                        and ( exp.item_payment > 0 )  
	                        " + ls_sqlwhere;
                }
                else
                {
                    ls_sql = @"select count(distinct kp.member_no) as sum_amt,
                        sum(exp.item_payment) as receive_amt  
                        from kptempreceive kp, 
                        kpreceiveexpense exp, 
                        mbmembmaster mb  
                        where ( kp.coop_id = exp.coop_id )  
	                        and ( kp.kpslip_no = exp.kpslip_no )  
	                        and ( kp.recv_period = exp.recv_period ) 
	                        and ( kp.coop_id = mb.coop_id )  
	                        and ( kp.member_no = mb.member_no )  
	                        and ( kp.coop_id = {0} )    
	                        and ( kp.recv_period = {1} )  
	                        and ( exp.expense_code = 'CBT' )
	                        and ( exp.item_payment > 0 )  
	                        " + ls_sqlwhere;
                }
                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, recv_period);
                DwUtil.ImportData(ls_sql, Dw_sum, null);
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                String ls_filename = "", ls_path = "", ls_trndate = "";
                String format_text = Dw_choice.GetItemString(1, "format_text");
                String filename = "";
                DateTime ldtm_trndate = new DateTime();
                //ออก Text File แบบเงินเดือน TMT
                if (format_text == "001" || format_text == "002")
                {
                    filename = "FN8";

                    DStore = new DataStore();
                    DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_exp_savedisk.pbl";
                    DStore.DataWindowObject = "d_kp_dsksrv_hrtext";

                    String ls_datacode, ls_salaryid;
                    int li_row, li_count;
                    Decimal ldc_moneyamt = 0;


                    ls_datacode = Dw_main.GetItemString(1, "data_code");
                    ldtm_trndate = Dw_main.GetItemDate(1, "operate_date");
                    ls_trndate = ldtm_trndate.ToString("dd.MM.yyyy");
                    ls_filename = filename + (ldtm_trndate.Year).ToString() + ldtm_trndate.ToString("MMdd") + ".txt";
                    ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + filename + (ldtm_trndate.Year).ToString() + ldtm_trndate.ToString("MMdd") + ".txt";

                    li_row = DStore.InsertRow(0);
                    DStore.SetItemString(li_row, "PERNR", "หมายเลขพนักงาน");
                    DStore.SetItemString(li_row, "DATE", "วันเริ่มต้น");
                    DStore.SetItemString(li_row, "WAGETYPE", "กลุ่มข้อมูลย่อย");
                    DStore.SetItemString(li_row, "AMOUNT", "จำนวนเงิน");
                    DStore.SetItemString(li_row, "NUMBER", "จำนวน/หน่วย");
                    DStore.SetItemString(li_row, "ZUORD", "เลขที่การกำหนด");

                    li_row = DStore.InsertRow(0);
                    li_row = DStore.InsertRow(0);

                    //   li_count = Dw_detail.RowCount;
                    li_count = int.Parse(HdRowCount.Value);

                    for (int i = 1; i <= li_count; i++)
                    {
                        try { ls_salaryid = Dw_detail.GetItemString(i, "salary_id").Trim(); }
                        catch { ls_salaryid = ""; }
                        ldc_moneyamt = Dw_detail.GetItemDecimal(i, "total");

                        li_row = DStore.InsertRow(0);
                        DStore.SetItemString(li_row, "PERNR", ls_salaryid);
                        DStore.SetItemString(li_row, "DATE", ls_trndate);
                        DStore.SetItemString(li_row, "WAGETYPE", ls_datacode);
                        DStore.SetItemString(li_row, "AMOUNT", ldc_moneyamt.ToString());
                    }

                    DStore.SaveAs(ls_path, FileSaveAsType.Text, true);
                }
                //กรณี Export เป็นแบบเงินฝาก
                else
                {
                    String company_bank = Dw_main.GetItemString(1, "company_bank").Trim();
                    String company_accid = Dw_main.GetItemString(1, "company_accid").Trim();
                    String company_name = Dw_main.GetItemString(1, "company_name").Trim();
                    String transfer_type = Dw_main.GetItemString(1, "transfer_type").Trim();
                    String transfer_code = Dw_main.GetItemString(1, "transfer_code").Trim();
                    //  DateTime ldtm_operate = new DateTime();

                    //สมาชิกปกติ โอนธนาคาร
                    if (format_text == "003")
                    {
                        filename = "D00603";
                    }
                    else
                    {
                        //สมาชิกสมทบ โอนธนาคาร
                        filename = "D00703";
                    }

                    DStore = new DataStore();
                    DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_exp_savedisk.pbl";
                    DStore.DataWindowObject = "d_kp_dsksrv_linetext";

                    String ls_memberno, ls_space, ls_space1, ls_space2, ls_space3, ls_space4, ls_expaccid, ls_memname, ls_surname, ls_name;
                    int li_row, li_count, li_running;
                    Decimal ldc_moneyamt = 0, ldc_moneytotal = 0;

                    ldtm_trndate = Dw_main.GetItemDate(1, "operate_date");
                    ls_trndate = ldtm_trndate.ToString("ddMMyy");
                    ls_filename = filename + (ldtm_trndate.Year.ToString()) + ldtm_trndate.ToString("MMdd") + ".txt";
                    ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + filename + (ldtm_trndate.Year.ToString()) + ldtm_trndate.ToString("MMdd") + ".txt";

                    //Header
                    li_row = DStore.InsertRow(0);
                    li_running = 1;
                    ls_space = "               ";
                    ls_space1 = "                                                                               ";

                    String linetext = "H" + li_running.ToString("000000") + company_bank + company_accid + company_name + ls_space + ls_trndate + ls_space1;
                    DStore.SetItemString(li_row, "line_text", linetext);

                    li_count = Dw_detail.RowCount;
                    ls_space2 = "                                            ";
                    ls_space3 = "         ";
                    ls_space4 = "                                                                  ";

                    //Detail
                    for (int i = 1; i <= li_count; i++)
                    {
                        try { ls_memberno = Dw_detail.GetItemString(i, "member_no").Trim(); }
                        catch { ls_memberno = ""; }
                        //ตัดเลขสมาชิกเหลือ 6
                        ls_memberno = ls_memberno.Substring(2, 6);
                        try { ls_expaccid = Dw_detail.GetItemString(i, "expense_accid").Trim(); }
                        catch { ls_expaccid = ""; }
                        try { ls_name = Dw_detail.GetItemString(i, "memb_name").Trim(); }
                        catch { ls_name = ""; }

                        try { ls_surname = Dw_detail.GetItemString(i, "memb_surname").Trim(); }
                        catch { ls_surname = ""; }

                        ls_memname = ls_name + "  " + ls_surname;

                        ldc_moneyamt = Dw_detail.GetItemDecimal(i, "total");
                        ldc_moneytotal = ldc_moneytotal + ldc_moneyamt;
                        li_row = DStore.InsertRow(0);
                        linetext = "D" + li_row.ToString("000000") + company_bank + ls_expaccid + "D" + (ldc_moneyamt * 100).ToString("000000000000") + transfer_code + "9" + ls_space2 + ldtm_trndate.ToString("yyMMdd") + ls_space3 + ls_memberno + " " + ls_memname;
                        DStore.SetItemString(li_row, "line_text", linetext);
                    }

                    //footer
                    li_row = DStore.InsertRow(0);

                    //if (transfer_type == "D")
                    //{
                    linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + Dw_detail.RowCount.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + "0000000" + "000000000000000" + ls_space4;
                    //}
                    //else
                    //{
                    // linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + "0000000" + "000000000000000" + Dw_detail.RowCount.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + ls_space4;
                    //}
                    DStore.SetItemString(li_row, "line_text", linetext);
                    DStore.SaveAs(ls_path, FileSaveAsType.Text, false);
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จแล้ว <a href=\"" + state.SsUrl + "filecommon/" + ls_filename + "\" target='_blank'>" + ls_filename + "</a>");
                JspostNewClear();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_choice.SaveDataCache();
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
            Dw_list.SaveDataCache();
        }

        private void JspostNewClear()
        {
            Dw_choice.Reset();
            Dw_choice.InsertRow(0);
            Dw_main.DataWindowObject = "d_kp_exp_savedisk_option";
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
            Dw_main.SetItemString(1, "month", Convert.ToString(DateTime.Now.Month));
            Dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
            Dw_main.SetItemString(1, "data_code", "2490");
            Dw_detail.Reset();
            Dw_list.Reset();
            tDw_main.Eng2ThaiAllRow();
        }

        private void JspostInit()
        {
            try
            {
                String filename = "";
                String ls_filename = "", ls_path = "", ls_trndate = "";
                DateTime ldtm_trndate = new DateTime();

                string ls_sqlsal = "", ls_sqlcbt = "", ls_sqlwhere = "";

                string format_text = Dw_choice.GetItemString(1, "format_text");

                String year = Dw_main.GetItemString(1, "year");
                String month = Dw_main.GetItemString(1, "month");
                if (month.Length != 2)
                {
                    month = "0" + month;
                }

                String recv_period = year + month;

                ls_sqlwhere = of_getconfigexp();

                //sqlดึงข้อมูล
                //กรณีที่เป็น เงินเดือน TMT
                if (format_text == "001" || format_text == "002")
                {

                    ls_sqlsal = @"select kp.member_no, 
                        mb.salary_id, 
                        kp.membgroup_code, 
                        sum(exp.item_payment) as total  
                        from kptempreceive kp, 
                        kpreceiveexpense exp, 
                        mbmembmaster mb  
                        where ( kp.coop_id = exp.coop_id )  
	                        and ( kp.kpslip_no = exp.kpslip_no )  
	                        and ( kp.recv_period = exp.recv_period ) 
	                        and ( kp.coop_id = mb.coop_id )  
	                        and ( kp.member_no = mb.member_no )  
	                        and ( kp.coop_id = {0} )    
	                        and ( kp.recv_period = {1} )  
	                        and ( exp.expense_code = 'SAL' )
	                        and ( exp.item_payment > 0 )  
	                        " + ls_sqlwhere + @"  
                        group by kp.member_no, mb.salary_id, kp.membgroup_code
                        order by mb.salary_id";

                    ls_sqlsal = WebUtil.SQLFormat(ls_sqlsal, state.SsCoopControl, recv_period);
                    Sdt dtsal = WebUtil.QuerySdt(ls_sqlsal);
                    if (dtsal.Rows.Count > 0)
                    {
                        filename = "FN8";

                        DStore = new DataStore();
                        DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_exp_savedisk.pbl";
                        DStore.DataWindowObject = "d_kp_dsksrv_hrtext";

                        String ls_datacode, ls_salaryid;
                        int li_row;
                        Decimal ldc_moneyamt = 0;

                        ls_datacode = Dw_main.GetItemString(1, "data_code");
                        ldtm_trndate = Dw_main.GetItemDate(1, "operate_date");
                        ls_trndate = ldtm_trndate.ToString("dd.MM.yyyy");
                        ls_filename = filename + (ldtm_trndate.Year).ToString() + ldtm_trndate.ToString("MMdd") + ".txt";
                        ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + filename + (ldtm_trndate.Year).ToString() + ldtm_trndate.ToString("MMdd") + ".txt";

                        li_row = DStore.InsertRow(0);
                        DStore.SetItemString(li_row, "PERNR", "หมายเลขพนักงาน");
                        DStore.SetItemString(li_row, "DATE", "วันเริ่มต้น");
                        DStore.SetItemString(li_row, "WAGETYPE", "กลุ่มข้อมูลย่อย");
                        DStore.SetItemString(li_row, "AMOUNT", "จำนวนเงิน");
                        DStore.SetItemString(li_row, "NUMBER", "จำนวน/หน่วย");
                        DStore.SetItemString(li_row, "ZUORD", "เลขที่การกำหนด");

                        li_row = DStore.InsertRow(0);
                        li_row = DStore.InsertRow(0);

                        while (dtsal.Next())
                        {
                            try { ls_salaryid = dtsal.GetString("salary_id").Trim(); }
                            catch { ls_salaryid = ""; }
                            try { ldc_moneyamt = dtsal.GetDecimal("total"); }
                            catch { ldc_moneyamt = 0; }


                            li_row = DStore.InsertRow(0);
                            DStore.SetItemString(li_row, "PERNR", ls_salaryid);
                            DStore.SetItemString(li_row, "DATE", ls_trndate);
                            DStore.SetItemString(li_row, "WAGETYPE", ls_datacode);
                            DStore.SetItemString(li_row, "AMOUNT", ldc_moneyamt.ToString());
                        }

                        DStore.SaveAs(ls_path, FileSaveAsType.Text, true);
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จแล้ว <a href=\"" + state.SsUrl + "filecommon/" + ls_filename + "\" target='_blank'>" + ls_filename + "</a>");
                        JspostNewClear();
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ต้องการออก TextFile");
                        JspostNewClear();
                    }
                }
                //กรณีที่เป็น บัญชี  CBT
                else
                {
                    ls_sqlcbt = @"select kp.member_no, 
                        mb.salary_id, 
                        kp.membgroup_code, 
                        kp.expense_accid, 
                        sum(exp.item_payment) as total,
                        mb.memb_name,
                        mb.memb_surname 
                        from kptempreceive kp, 
                        kpreceiveexpense exp, 
                        mbmembmaster mb  
                        where ( kp.coop_id = exp.coop_id )  
	                        and ( kp.kpslip_no = exp.kpslip_no )  
	                        and ( kp.recv_period = exp.recv_period ) 
	                        and ( kp.coop_id = mb.coop_id )  
	                        and ( kp.member_no = mb.member_no )  
	                        and ( kp.coop_id = {0} )    
	                        and ( kp.recv_period = {1} )  
	                        and ( exp.expense_code = 'CBT' )
	                        and ( exp.item_payment > 0 )  
	                        " + ls_sqlwhere + @"  
                        group by kp.member_no, mb.salary_id, kp.membgroup_code, kp.expense_accid, mb.memb_name, mb.memb_surname
                        order by kp.member_no desc";
                    ls_sqlcbt = WebUtil.SQLFormat(ls_sqlcbt, state.SsCoopControl, recv_period);
                    Sdt dtcbt = WebUtil.QuerySdt(ls_sqlcbt);
                    if (dtcbt.Rows.Count > 0)
                    {

                        String company_bank = Dw_main.GetItemString(1, "company_bank").Trim();
                        String company_accid = Dw_main.GetItemString(1, "company_accid").Trim();
                        String company_name = Dw_main.GetItemString(1, "company_name").Trim();
                        String transfer_type = Dw_main.GetItemString(1, "transfer_type").Trim();
                        String transfer_code = Dw_main.GetItemString(1, "transfer_code").Trim();

                        //สมาชิกปกติ โอนธนาคาร
                        if (format_text == "003")
                        {
                            filename = "D00603";
                        }
                        else
                        {
                            //สมาชิกสมทบ โอนธนาคาร
                            filename = "D00703";
                        }

                        DStore = new DataStore();
                        DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_exp_savedisk.pbl";
                        DStore.DataWindowObject = "d_kp_dsksrv_linetext";

                        String ls_memberno, ls_space, ls_space1, ls_space2, ls_space3, ls_space4, ls_expaccid, ls_memname, ls_surname, ls_name;
                        int li_row, li_count, li_running;
                        Decimal ldc_moneyamt = 0, ldc_moneytotal = 0;

                        ldtm_trndate = Dw_main.GetItemDate(1, "operate_date");
                        ls_trndate = ldtm_trndate.ToString("ddMMyy");
                        ls_filename = filename + (ldtm_trndate.Year.ToString()) + ldtm_trndate.ToString("MMdd") + ".txt";
                        ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + filename + (ldtm_trndate.Year.ToString()) + ldtm_trndate.ToString("MMdd") + ".txt";

                        //Header
                        li_row = DStore.InsertRow(0);
                        li_running = 1;
                        ls_space = "               ";
                        ls_space1 = "                                                                               ";

                        String linetext = "H" + li_running.ToString("000000") + company_bank + company_accid + company_name + ls_space + ls_trndate + ls_space1;
                        DStore.SetItemString(li_row, "line_text", linetext);

                        li_count = dtcbt.GetRowCount();
                        ls_space2 = "                                            ";
                        ls_space3 = "         ";
                        ls_space4 = new String(' ', 66); //"                                                                  ";

                        //Detail
                        while (dtcbt.Next())
                        {
                            try { ls_memberno = dtcbt.GetString("member_no").Trim(); }
                            catch { ls_memberno = ""; }
                            //ตัดเลขสมาชิกเหลือ 6
                            ls_memberno = ls_memberno.Substring(2, 6);
                            try { ls_expaccid = dtcbt.GetString("expense_accid").Trim(); }
                            catch { ls_expaccid = ""; }

                            try { ls_name = dtcbt.GetString("memb_name").Trim(); }
                            catch { ls_name = ""; }

                            try { ls_surname = dtcbt.GetString("memb_surname").Trim(); }
                            catch { ls_surname = ""; }

                            ls_memname = ls_name + "  " + ls_surname;
                            try { ldc_moneyamt = dtcbt.GetDecimal("total"); }
                            catch { ldc_moneyamt = 0; }

                            ldc_moneytotal = ldc_moneytotal + ldc_moneyamt;
                            li_row = DStore.InsertRow(0);
                            linetext = "D" + li_row.ToString("000000") + company_bank + ls_expaccid + "D" + (ldc_moneyamt * 100).ToString("000000000000") + transfer_code + "9" + ls_space2 + ldtm_trndate.ToString("yyMMdd") + ls_space3 + ls_memberno + " " + ls_memname;
                            DStore.SetItemString(li_row, "line_text", linetext);
                        }
                        
                        //footer
                        li_row = DStore.InsertRow(0);

                        linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + li_count.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + "0000000" + "000000000000000" + ls_space4;

                        DStore.SetItemString(li_row, "line_text", linetext);
                        DStore.SaveAs(ls_path, FileSaveAsType.Text, false);

                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จแล้ว <a href=\"" + state.SsUrl + "filecommon/" + ls_filename + "\" target='_blank'>" + ls_filename + "</a>");
                        JspostNewClear();
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ต้องการออก TextFile");
                        JspostNewClear();
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostChangeOption()
        {
            try
            {
                String format_text = Dw_choice.GetItemString(1, "format_text");
                //กรณีเป็นเงินเดือน 001,002
                if (format_text == "001" || format_text == "002")
                {
                    Dw_main.DataWindowObject = "d_kp_exp_savedisk_option";
                }
                //กรณีเป็นเงินธนาคาร  003,004
                else
                {
                    Dw_main.DataWindowObject = "d_kp_exp_savedisk_option_tdp";
                }

                JspostSetData();

                //mai แก้ไข 570313
                if (format_text == "001" || format_text == "002")
                {
                    Dw_detail.DataWindowObject = "d_kp_exp_savedisk_data_tmt";
                }
                else
                {
                    Dw_detail.DataWindowObject = "d_kp_exp_savedisk_data_cbt";
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetData()
        {
            try
            {
                String format_text = Dw_choice.GetItemString(1, "format_text");
                //SAL เงินเดือน
                if (format_text == "001" || format_text == "002")
                {
                    Dw_main.Reset();
                    Dw_main.InsertRow(0);
                    Dw_main.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
                    Dw_main.SetItemString(1, "month", Convert.ToString(DateTime.Now.Month));
                    Dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
                    Dw_main.SetItemString(1, "data_code", "2490");
                    Dw_detail.Reset();
                    tDw_main.Eng2ThaiAllRow();
                }
                else
                {
                    //CBT ธนาคาร
                    Dw_main.Reset();
                    Dw_main.InsertRow(0);
                    Dw_main.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
                    Dw_main.SetItemString(1, "month", Convert.ToString(DateTime.Now.Month));
                    Dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
                    DwUtil.RetrieveDDDW(Dw_main, "company_bank", pbl, null);
                    Dw_main.SetItemString(1, "company_bank", "034");
                    Dw_main.SetItemString(1, "company_accid", "010001007401");
                    Dw_main.SetItemString(1, "company_name", "SAVINGCOOP");
                    Dw_main.SetItemString(1, "transfer_type", "C");
                    //สมาชิกปกติบัญชี
                    if (format_text == "003")
                    {
                        Dw_main.SetItemString(1, "transfer_code", "06");
                    }
                    else
                    {
                        Dw_main.SetItemString(1, "transfer_code", "01");
                    }
                    Dw_detail.Reset();
                    tDw_main.Eng2ThaiAllRow();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private string of_getconfigexp()
        {
            decimal memmain_flag, memco_flag, group_flag, type_flag;
            string ls_sql = "", ls_sqlwhere = "";
            string member_type = "";
            string format_text = Dw_choice.GetItemString(1, "format_text");

            //หาประเภทสมาชิกก่อน ดึงเฉพาะปกติ หรือ สมทบ หรือดึงทั้งคู่
            ls_sql = "select memmain_flag, memco_flag, group_flag, type_flag from kpcfdisk where coop_id = '" + state.SsCoopControl + "' and disk_code = '" + format_text + "'";
            Sdt dtdisk = WebUtil.QuerySdt(ls_sql);
            if (dtdisk.Next())
            {
                memmain_flag = dtdisk.GetDecimal("memmain_flag");
                memco_flag = dtdisk.GetDecimal("memco_flag");
                group_flag = dtdisk.GetDecimal("group_flag");
                type_flag = dtdisk.GetDecimal("type_flag");

                if (memmain_flag == 1)
                {
                    member_type = "1";
                }
                if (memco_flag == 1)
                {
                    if (member_type != "")
                    {
                        member_type += ",2";
                    }
                    else
                    {
                        member_type = "2";
                    }
                }
                ls_sqlwhere = "and mb.member_type in (" + member_type + ")";

                if (group_flag == 1)
                {
                    ls_sqlwhere += " and kp.membgroup_code in ( select trim(membgroup_code) from kpcfdiskmembgroup where disk_code = '" + format_text + "' )";
                }
                if (memco_flag == 1)
                {
                    ls_sqlwhere += "and kp.membtype_code in ( select membtype_code from kpcfdiskmembtype where disk_code = '" + format_text + "' )";
                }
            }
            return ls_sqlwhere;
        }
    }
}