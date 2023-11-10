using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;
using System.Text;

namespace Saving.Applications.keeping.ws_kp_opr_exp_textfile_ctrl
{
    public partial class ws_kp_opr_exp_textfile : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String postRetrieve { get; set; }
        //[JsPostBack]
        //public String postExpTextfile { get; set; }

        public void InitJsPostBack()
        {
            dsSalary.InitDsSalary(this);
            dsCbt.InitDsCbt(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsSalary.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsSalary.DATA[0].month = DateTime.Now.Month;
                dsSalary.DATA[0].operate_date = state.SsWorkDate;
                dsCbt.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsCbt.DATA[0].month = DateTime.Now.Month;
                dsCbt.DATA[0].operate_date = state.SsWorkDate;
                DdFormat();
                dsCbt.DdBank();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRetrieve")
            {
                String ls_sql = "", ls_sqlwhere = "";
                String format_text = dd_format.SelectedValue;
                String recv_period;

                ls_sqlwhere = of_getconfigexp();

                if (format_text == "001" || format_text == "002")
                {
                    recv_period = dsSalary.DATA[0].year + dsSalary.DATA[0].month.ToString("00");
                    ls_sql = @"select count(distinct kp.member_no) as total_no,
                        sum(exp.item_payment) as total_amt  
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
                    recv_period = dsCbt.DATA[0].year + dsCbt.DATA[0].month.ToString("00");
                    ls_sql = @"select count(distinct kp.member_no) as total_no,
                        sum(exp.item_payment) as total_amt  
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
                Sdt dt = WebUtil.QuerySdt(ls_sql);
                if (dt.Next())
                {
                    txt_totalno.Text = dt.GetInt32("total_no").ToString("#,##0");
                    txt_totalamt.Text = dt.GetDecimal("total_amt").ToString("#,##0.00");
                }
            }
        }

        public string Space(int length)
        {
            return new string(' ', length);
        }

        private string of_getconfigexp()
        {
            decimal memmain_flag, memco_flag, group_flag, type_flag;
            string ls_sql = "", ls_sqlwhere = "";
            string member_type = "";
            string format_text = dd_format.SelectedValue;

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

        public void DdFormat()
        {
            string ls_sql = @"select disk_code, disk_desc from kpcfdisk where coop_id = '" + state.SsCoopControl + "'";
            DataTable dt = new DataTable();
            dt = WebUtil.Query(ls_sql);
            dd_format.DataTextField = "disk_desc";
            dd_format.DataValueField = "disk_code";
            dd_format.DataSource = dt;
            dd_format.DataBind();
            dd_format.SelectedIndex = 0;
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        protected void postExpTextfile(object sender, EventArgs e)
        {
            DateTime ldtm_trndate = new DateTime();
            String filename = "", ls_filename = "", ls_trndate = "";
            String ls_sqlsal = "", ls_sqlcbt = "", ls_sqlwhere = "", recv_period = "";
            String format_text = dd_format.SelectedValue;

            ls_sqlwhere = of_getconfigexp();
            //Build the Text file data.
            string txt = string.Empty;

            //sqlดึงข้อมูล
            //กรณีที่เป็น เงินเดือน TMT
            if (format_text == "001" || format_text == "002")
            {
                recv_period = dsSalary.DATA[0].year + dsSalary.DATA[0].month.ToString("00");
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

                    String ls_datacode, ls_salaryid;
                    Decimal ldc_moneyamt = 0;

                    ls_datacode = dsSalary.DATA[0].data_code;
                    ldtm_trndate = dsSalary.DATA[0].operate_date;
                    ls_trndate = ldtm_trndate.ToString("dd.MM.yyyy");
                    ls_filename = filename + (ldtm_trndate.Year).ToString() + ldtm_trndate.ToString("MMdd") + ".txt";

                    Response.Clear();
                    Response.Write("variant_id\tvariant_text\tPERNR\tDATE\tWAGETYPE\tAMOUNT\tNUMBER\tZUORD");
                    Response.Write("\r\n");
                    Response.Write("\t\tหมายเลขพนักงาน\tวันเริ่มต้น	กลุ่มข้อมูลย่อย\tจำนวนเงิน\tจำนวน/หน่วย\tเลขที่การกำหนด");
                    Response.Write("\r\n");
                    Response.Write("\r\n");

                    while (dtsal.Next())
                    {
                        try { ls_salaryid = dtsal.GetString("salary_id").Trim(); }
                        catch { ls_salaryid = ""; }
                        try { ldc_moneyamt = dtsal.GetDecimal("total"); }
                        catch { ldc_moneyamt = 0; }

                        Response.Write("\r\n");
                        Response.Write("\t\t" + ls_salaryid + "\t" + ls_trndate + "\t" + ls_datacode + "\t" + ldc_moneyamt.ToString() + "\t\t");
                    }

                    //Download the Text file.                    
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + ls_filename);
                    Response.Charset = "UTF-8";
                    Response.ContentType = "application/text";
                    //Response.Output.Write(txt);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ต้องการออก TextFile");
                }
            }
            //กรณีที่เป็น บัญชี  CBT
            else if (format_text == "003" || format_text == "004")
            {
                recv_period = dsCbt.DATA[0].year + dsCbt.DATA[0].month.ToString("00");
                ls_sqlcbt = @"select kp.member_no, 
                        mb.salary_id, 
                        kp.membgroup_code, 
                        exp.expense_accid, 
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
                        group by kp.member_no, mb.salary_id, kp.membgroup_code, exp.expense_accid, mb.memb_name, mb.memb_surname
                        order by kp.member_no desc";
                ls_sqlcbt = WebUtil.SQLFormat(ls_sqlcbt, state.SsCoopControl, recv_period);
                Sdt dtcbt = WebUtil.QuerySdt(ls_sqlcbt);
                if (dtcbt.Rows.Count > 0)
                {

                    String company_bank = dsCbt.DATA[0].bank_code.Trim();
                    String company_accid = dsCbt.DATA[0].bank_accid.Trim();
                    String company_name = dsCbt.DATA[0].bank_name.Trim();
                    String transfer_type = dsCbt.DATA[0].transfer_type.Trim();
                    String transfer_code = dsCbt.DATA[0].transfer_code.Trim();

                    company_bank = company_bank.PadLeft(3, '0');
                    company_name = company_name.PadRight(25, ' ');

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

                    String ls_memberno, ls_expaccid, ls_memname, ls_surname, ls_name;
                    int li_row, li_count, li_running;
                    Decimal ldc_moneyamt = 0, ldc_moneytotal = 0;

                    ldtm_trndate = dsCbt.DATA[0].operate_date;
                    ls_trndate = ldtm_trndate.ToString("ddMMyy");
                    ls_filename = filename + (ldtm_trndate.Year.ToString()) + ldtm_trndate.ToString("MMdd") + ".txt";

                    //Header
                    li_row = 1;
                    li_running = 1;
                    li_count = dtcbt.GetRowCount();

                    String linetext = "H" + li_running.ToString("000000") + company_bank + company_accid + company_name + ls_trndate + Space(79);
                    txt = linetext;

                    //Detail
                    string[] arr_template = new string[13];
                    arr_template[0] = "D";
                    arr_template[1] = "";
                    arr_template[2] = "";
                    arr_template[3] = "";
                    arr_template[4] = "D";
                    arr_template[5] = "";
                    arr_template[6] = transfer_code;
                    arr_template[7] = "9";
                    arr_template[8] = Space(44);
                    arr_template[9] = ls_trndate;
                    arr_template[10] = Space(9);
                    arr_template[11] = "";
                    arr_template[12] = Space(4);

                    string[] arr_detail = new string[13];

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

                        try { ldc_moneyamt = dtcbt.GetDecimal("total"); }
                        catch { ldc_moneyamt = 0; }

                        ls_memname = ls_memberno + " " + ls_name + "  " + ls_surname;
                        ls_memname = ls_memname.PadRight(31, ' ');
                        ls_memname = ls_memname.Substring(0, 31);

                        ldc_moneytotal = ldc_moneytotal + ldc_moneyamt;
                        string ls_money = (ldc_moneyamt * 100).ToString("000000000000");

                        arr_detail = arr_template;
                        txt += "\r\n";
                        li_row += 1;

                        arr_detail[1] = li_row.ToString("000000");
                        arr_detail[2] = company_bank;
                        arr_detail[3] = ls_expaccid;
                        arr_detail[5] = ls_money;
                        arr_detail[11] = ls_memname;

                        linetext = arr_detail[0] + arr_detail[1] + arr_detail[2] + arr_detail[3] + arr_detail[4] + arr_detail[5] + arr_detail[6] + arr_detail[7] + arr_detail[8] + arr_detail[9] + arr_detail[10] + arr_detail[11] + arr_detail[12];
                        txt += linetext;
                    }

                    //footer
                    txt += "\r\n";
                    li_row += 1;

                    linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + li_count.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + "0000000" + "000000000000000" + Space(66);

                    txt += linetext;

                    //Download the Text file.                    
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + ls_filename);
                    Response.ContentType = "application/text";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("TIS-620");
                    Response.Output.Write(txt);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ต้องการออก TextFile");
                }
            }
            else
            {
                recv_period = dsCbt.DATA[0].year + dsCbt.DATA[0].month.ToString("00");
                ls_sqlcbt = @"select kp.member_no, 
                        mb.salary_id, 
                        kp.membgroup_code, 
                        exp.expense_accid, 
                        exp.item_payment,
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
                        order by kp.member_no desc";
                ls_sqlcbt = WebUtil.SQLFormat(ls_sqlcbt, state.SsCoopControl, recv_period);
                Sdt dtcbt = WebUtil.QuerySdt(ls_sqlcbt);
                if (dtcbt.Rows.Count > 0)
                {

                    String company_bank = dsCbt.DATA[0].bank_code.Trim();
                    String company_accid = dsCbt.DATA[0].bank_accid.Trim();
                    String company_name = dsCbt.DATA[0].bank_name.Trim();
                    String transfer_type = dsCbt.DATA[0].transfer_type.Trim();
                    String transfer_code = dsCbt.DATA[0].transfer_code.Trim();

                    company_bank = company_bank.PadLeft(3, '0');
                    company_name = company_name.PadRight(25, ' ');

                    filename = "D00803";

                    String ls_memberno, ls_expaccid, ls_memname, ls_surname, ls_name;
                    int li_row, li_count, li_running;
                    Decimal ldc_moneyamt = 0, ldc_moneytotal = 0;

                    ldtm_trndate = dsCbt.DATA[0].operate_date;
                    ls_trndate = ldtm_trndate.ToString("ddMMyy");
                    ls_filename = filename + (ldtm_trndate.Year.ToString()) + ldtm_trndate.ToString("MMdd") + ".txt";

                    //Header
                    li_row = 1;
                    li_running = 1;
                    li_count = dtcbt.GetRowCount();

                    String linetext = "H" + li_running.ToString("000000") + company_bank + company_accid + company_name + ls_trndate + Space(79);
                    txt = linetext;

                    //Detail
                    string[] arr_template = new string[13];
                    arr_template[0] = "D";
                    arr_template[1] = "";
                    arr_template[2] = "";
                    arr_template[3] = "";
                    arr_template[4] = "D";
                    arr_template[5] = "";
                    arr_template[6] = transfer_code;
                    arr_template[7] = "9";
                    arr_template[8] = Space(44);
                    arr_template[9] = ls_trndate;
                    arr_template[10] = Space(9);
                    arr_template[11] = "";
                    arr_template[12] = Space(4);

                    string[] arr_detail = new string[13];

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

                        try { ldc_moneyamt = dtcbt.GetDecimal("total"); }
                        catch { ldc_moneyamt = 0; }

                        ls_memname = ls_memberno + " " + ls_name + "  " + ls_surname;
                        ls_memname = ls_memname.PadRight(31, ' ');
                        ls_memname = ls_memname.Substring(0, 31);

                        ldc_moneytotal = ldc_moneytotal + ldc_moneyamt;
                        string ls_money = (ldc_moneyamt * 100).ToString("000000000000");

                        arr_detail = arr_template;
                        txt += "\r\n";
                        li_row += 1;

                        arr_detail[1] = li_row.ToString("000000");
                        arr_detail[2] = company_bank;
                        arr_detail[3] = ls_expaccid;
                        arr_detail[5] = ls_money;
                        arr_detail[11] = ls_memname;

                        linetext = arr_detail[0] + arr_detail[1] + arr_detail[2] + arr_detail[3] + arr_detail[4] + arr_detail[5] + arr_detail[6] + arr_detail[7] + arr_detail[8] + arr_detail[9] + arr_detail[10] + arr_detail[11] + arr_detail[12];
                        txt += linetext;
                    }

                    //footer
                    txt += "\r\n";
                    li_row += 1;

                    linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + li_count.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + "0000000" + "000000000000000" + Space(66);

                    txt += linetext;

                    //Download the Text file.                    
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + ls_filename);
                    Response.ContentType = "application/text";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("TIS-620");
                    Response.Output.Write(txt);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลที่ต้องการออก TextFile");
                }
            }
        }
    }
}