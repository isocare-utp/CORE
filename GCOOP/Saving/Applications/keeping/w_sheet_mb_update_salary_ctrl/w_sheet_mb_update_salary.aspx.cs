using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNShrlon;
using System.IO;
using System.Data;
using DataLibrary;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfReport;
using CoreSavingLibrary;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Script.Serialization;

namespace Saving.Applications.keeping.w_sheet_mb_update_salary_ctrl
{
    public partial class w_sheet_mb_update_salary : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        [JsPostBack]
        public string postDatatxt { get; set; }
        [JsPostBack]
        public string LoadDatatxt { get; set; }
        [JsPostBack]
        public string CMSaveSalary { get; set; }

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postDatatxt")
            {
                JsPostDatatxt();
            }
            else if (eventArg == "LoadDatatxt")
            {
                Runreport();
            }
            else if (eventArg == "CMSaveSalary")
            {
                SaveSalary();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        // ajax แบบง่าย By maxim
        [WebMethod]
        public static string GetText()
        {
            //กรณี return string ธรรมดา
            string msg = "By maxim";

            return msg;
        }

        [WebMethod]
        public static string GetData()
        {
            //กรณี return string [] 
            string msg = "";
            msg = @"memo = [['txt001',2],['txt002',2]];";
            return msg;
        }

        //อ่านข้อมูลไฟล์ txt 
        public void JsPostDatatxt()
        {
            string selectedValue = rdoPriceRange.SelectedValue;

            ExecuteDataSource ex = new ExecuteDataSource(this);
            ExecuteDataSource bx = new ExecuteDataSource(this);

            //เช็คว่าเลือกรายการเป็น txt หรือ excel
            if (selectedValue == "txtFile")
            {
                try
                {
                    //
                    FileUpload fu = txtInput;
                    string filename = txtInput.FileName.ToString().Trim();
                    Stream stream = fu.PostedFile.InputStream;
                    byte[] b = new byte[stream.Length];
                    stream.Read(b, 0, (int)stream.Length);
                    string txt = Encoding.GetEncoding("TIS-620").GetString(b); //sr.ReadToEnd();
                    txt = Regex.Replace(txt, "\r", "");
                    string[] lines = Regex.Split(txt, "\n");
                    int txtLength;
                    int n = 1;
                    string doc_no = String.Format("{0:yyyyMMdd}", state.SsWorkDate);
                    string temp_doc = "";
                    string member_no;
                    decimal salary_amt;
                    string[] txtindex;

                    foreach (string line in lines)
                    {
                        temp_doc = doc_no + n.ToString("0000000");

                        txtLength = line.Length;
                        if (txtLength > 3)
                        {
                            txtindex = line.Split(',');
                            member_no = txtindex[0];
                            member_no = GetSQL_Cond("select member_no as sql_value from mbmembmaster where trim(salary_id) = '" + member_no + "'");

                            if (member_no != "")
                            {
                                member_no = WebUtil.MemberNoFormat(member_no);
                                try { salary_amt = Convert.ToDecimal(txtindex[1]); }
                                catch { salary_amt = 0; }

                                //intsert ข้อมูลไปพักไว้ก่อน
                                string sql_inskptempimp = @"insert into mbadjsalary(coop_id, sharetype_code, member_no, new_salary, adjsal_type, operate_date, posting_flag, adjslip_no, entry_id) values({0},{1},{2},{3},{4},{5},{6},{7},{8})";
                                object[] argslist_mbadjsalary = new object[] { state.SsCoopControl, "01", member_no, salary_amt, "PRC", state.SsWorkDate, 0, temp_doc, state.SsUsername };

                                sql_inskptempimp = WebUtil.SQLFormat(sql_inskptempimp, argslist_mbadjsalary);
                                ex.SQL.Add(sql_inskptempimp);

                                n++;
                            }
                        }
                    }

                    ex.Execute();
                    ex.SQL.Clear();

                    try
                    {
                        string sql_initmbadjsalary = @"select mas.member_no , payment_status , last_period ,
                            nvl( m.salary_amount , 0 ) as old_salary , nvl( sm.periodbase_amt , 0 ) as old_sharebase , nvl( sm.periodshare_amt , 0 ) as old_shareperiod ,
                            nvl( mas.new_salary , 0 ) as new_salary , nvl( stm.minshare_amt , 0 ) as minshare_amt , nvl( stm.maxshare_amt , 99999 ) as maxshare_amt ,
                            ( ( nvl( mas.new_salary , 0 ) * nvl( stm.sharerate_percent , 0 ) ) / ( 100 * st.unitshare_value ) ) as new_shareperiod ,
                            nvl( st.minshare_hold , 0 ) as minshare_hold , nvl( st.maxshare_hold , 0 ) as maxshare_hold , 
                            nvl( st.minshare_low , 0 ) as minshare_low , nvl( st.minshare_stop , 0 ) as minshare_stop , 
                            nvl( st.timeminshare_low , 0 ) as timeminshare_low , nvl( st.timeminshare_stop , 0 ) as timeminshare_stop
                            from mbadjsalary mas , mbmembmaster m , shsharemaster sm , shsharetypemthrate stm , shsharetype st
                            where mas.coop_id = st.coop_id
                            and mas.sharetype_code = st.sharetype_code
                            and mas.coop_id = m.coop_id
                            and mas.member_no = m.member_no
                            and mas.coop_id = sm.coop_id
                            and mas.member_no = sm.member_no
                            and mas.sharetype_code = sm.sharetype_code
                            and m.member_type = stm.member_type
                            and mas.new_salary between stm.start_salary and stm.end_salary
                            and mas.coop_id = {0}
                            and mas.operate_date = {1}
                            and mas.adjsal_type = 'PRC'
                            and mas.posting_flag = 0";
                        sql_initmbadjsalary = WebUtil.SQLFormat(sql_initmbadjsalary, state.SsCoopControl, state.SsWorkDate);
                        Sdt dtBPM = WebUtil.QuerySdt(sql_initmbadjsalary);

                        decimal payment_status, new_shareperiod, old_shareperiod, minshare_amt, maxshare_amt, newshrbase = 0, old_salary, old_sharebase;
                        string member_no_tmp;

                        while (dtBPM.Next())
                        {
                            payment_status = dtBPM.GetDecimal("payment_status");
                            new_shareperiod = dtBPM.GetDecimal("new_shareperiod");
                            old_shareperiod = dtBPM.GetDecimal("old_shareperiod");
                            minshare_amt = dtBPM.GetDecimal("minshare_amt");
                            maxshare_amt = dtBPM.GetDecimal("maxshare_amt");
                            old_salary = dtBPM.GetDecimal("old_salary");
                            old_sharebase = dtBPM.GetDecimal("old_sharebase");
                            member_no_tmp = dtBPM.GetString("member_no");

                            if (payment_status == 1)
                            {
                                if (new_shareperiod < minshare_amt)
                                {
                                    new_shareperiod = minshare_amt;
                                }

                                if (new_shareperiod > minshare_amt)
                                {
                                    new_shareperiod = maxshare_amt;
                                }

                                newshrbase = new_shareperiod;  //หุ้นฐาน

                                if (new_shareperiod < old_shareperiod)
                                {
                                    new_shareperiod = old_shareperiod;
                                }
                            }
                            else
                            {
                                //งดส่งไม่ต้องสนใจ
                                new_shareperiod = old_shareperiod;
                            }

                            string sql_updatembadjsalary = @"update mbadjsalary mas
                        		                set mas.old_salary 		= {0} , 
                        		                mas.old_sharebase 		= {1} , 
                        		                mas.old_shareperiod		= {2} ,
                        		                mas.new_sharebase		= {3} ,
                        		                mas.new_shareperiod 	= {4}
                        		                where mas.coop_id = {5}
                        		                and mas.operate_date = {6}
                        		                and mas.member_no = {7}
                        		                and mas.adjsal_type = 'PRC'
                        		                and mas.posting_flag = 0";
                            object[] argslist_updatembadjsalary = new object[] { old_salary, old_sharebase, old_shareperiod, newshrbase, new_shareperiod, state.SsCoopControl, state.SsWorkDate, member_no_tmp };

                            sql_updatembadjsalary = WebUtil.SQLFormat(sql_updatembadjsalary, argslist_updatembadjsalary);
                            bx.SQL.Add(sql_updatembadjsalary);
                        }

                        bx.Execute();
                        bx.SQL.Clear();

                        LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
                    }
                    catch (Exception exx)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(exx.Message);
                    }
                }
                catch (Exception eX)
                {
                    //เอาไว้ดู err เวลามีปัญหาตอน import
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
            else
            {
                //เลือกเป็นไฟล์ excel
                try
                {
                    //
                    string saveFolder = @"C:\GCOOP_ALL\CORE\GCOOP\Saving\WSRPDF";
                    string filePath = Path.Combine(saveFolder, txtInput.FileName);
                    File_Name.Text = txtInput.FileName;
                    int n = 1;

                    txtInput.SaveAs(filePath);

                    String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);

                    //Create Connection to Excel work book 
                    using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
                    {
                        //OleDbDataAdapter da = new OleDbDataAdapter("Select [member_no],[salary_amt] from [Sheet1$]", excelConnection);
                        OleDbDataAdapter da = new OleDbDataAdapter("Select [salary_id],[salary_amt] from [Sheet1$]", excelConnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        string txtFull = "";
                        string doc_no = String.Format("{0:yyyyMMdd}", state.SsWorkDate);
                        string temp_doc = "";
                        int txtLength;

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            string cell, txt = "";

                            temp_doc = doc_no + n.ToString("0000000");

                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                cell = dt.Rows[i][j].ToString();
                                txt = txt + dt.Rows[i][j].ToString() + "\r";
                            }

                            txtFull = txt;
                            txtLength = txtFull.Length;

                            if (txtLength > 5)
                            {
                                string[] gettxt = txtFull.Split('\r');
                                string member_no;
                                decimal salary_amt;

                                member_no = gettxt[0].ToString();
                                member_no = GetSQL_Cond("select member_no as sql_value from mbmembmaster where trim(salary_id) = '" + member_no + "'");
                                member_no = WebUtil.MemberNoFormat(member_no);
                                try { salary_amt = Convert.ToDecimal(gettxt[1]); }
                                catch { salary_amt = 0; }

                                //intsert ข้อมูลไปพักไว้ก่อน
                                string sql_inskptempimp = @"insert into mbadjsalary(coop_id, sharetype_code, member_no, new_salary, adjsal_type, operate_date, posting_flag, adjslip_no, entry_id) values({0},{1},{2},{3},{4},{5},{6},{7},{8})";
                                object[] argslist_mbadjsalary = new object[] { state.SsCoopControl, "01", member_no, salary_amt, "PRC", state.SsWorkDate, 0, temp_doc, state.SsUsername };

                                sql_inskptempimp = WebUtil.SQLFormat(sql_inskptempimp, argslist_mbadjsalary);
                                ex.SQL.Add(sql_inskptempimp);

                                n++;

                            }
                        }

                        ex.Execute();
                        ex.SQL.Clear();

                        try
                        {
                            str_mbaudit astr_mbaudit = new str_mbaudit();
                            astr_mbaudit.operate_date = state.SsWorkDate;
                            int result = shrlonService.of_init_sal(state.SsWsPass, ref astr_mbaudit);

                            LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
                        }
                        catch (Exception exx)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(exx.Message);
                        }
                    }
                }
                catch (Exception eX)
                {
                    //เอาไว้ดู err เวลามีปัญหาตอน import
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
        }

        //save salary
        public void SaveSalary()
        {
            try
            {
                str_mbaudit astr_mbaudit = new str_mbaudit();
                astr_mbaudit.operate_date = state.SsWorkDate;
                int result = shrlonService.of_save_sal(state.SsWsPass, ref astr_mbaudit);

                LtServerMessage.Text = WebUtil.CompleteMessage("Upload Complete");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        //Funtion นี้เอาไว้ get เลขเอกสารต่างๆ รายละเอียดดูใน table cmdocumentcontrol
        public string Get_NumberDOC(string typecode)
        {
            string coop_id = state.SsCoopControl;
            Sta ta = new Sta(state.SsConnectionString);
            string postNumber = "";
            try
            {
                ta.AddInParameter("AVC_COOPID", coop_id, System.Data.OracleClient.OracleType.VarChar);
                ta.AddInParameter("AVC_DOCCODE", typecode, System.Data.OracleClient.OracleType.VarChar);
                ta.AddReturnParameter("return_value", System.Data.OracleClient.OracleType.VarChar);
                ta.ExePlSql("N_PK_DOCCONTROL.OF_GETNEWDOCNO");
                postNumber = ta.OutParameter("return_value").ToString();
                ta.Close();
            }
            catch
            {
                ta.Close();
            }
            return postNumber.ToString();
        }

        //เอาไว้เพื่อได้ออกรายงาน ireport
        public void Runreport()
        {
            DateTime imp_date = state.SsWorkDate;

            //Add Arg[]
            iReportArgument args = new iReportArgument();
            iReportBuider report = new iReportBuider(this, "กำลังสร้างรายงาน");

            args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopControl);
            args.Add("as_impdate", iReportArgumentType.Date, imp_date);

            report.AddCriteria("r_mb_import_salary", "รายงาน importfile (ทั้งหมด)", ReportType.pdf, args);

            report.AutoOpenPDF = true;
            report.Retrieve();
        }

        public string GetSQL_Cond(string Select_Condition)
        {
            string sql_value = "";
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                sql_value = dt.GetString("sql_value");
            }
            return sql_value;
        }
    }
}