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

namespace Saving.Applications.mbshr.ws_mbshr010_update_salary_ctrl
{
    public partial class ws_mbshr010_update_salary : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        [JsPostBack]
        public string postDatatxt { get; set; }
        [JsPostBack]
        public string CMSaveSalary { get; set; }

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            Hd_process.Value = "false";
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
            string ls_sql;
            ExecuteDataSource ex = new ExecuteDataSource(this);

            ls_sql = ("truncate table mbadjsalarytemp");
            Sta ta = new Sta(state.SsConnectionString);
            ta.Exe(ls_sql);

            //เช็คว่าเลือกรายการเป็น txt หรือ excel
            if (selectedValue == "txtFile")
            {
                try
                {
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
                    string ls_docno = "", ls_salaryid;
                    decimal ldc_salaryamt;
                    string[] txtindex;

                    foreach (string line in lines)
                    {
                        ls_docno = String.Format("{0:yyyyMMdd}", state.SsWorkDate) + n.ToString("0000000");

                        txtLength = line.Length;
                        if (txtLength > 3)
                        {
                            txtindex = line.Split(',');
                            try { ls_salaryid = Convert.ToString(txtindex[0]); }
                            catch { ls_salaryid = ""; }
                            try { ldc_salaryamt = Convert.ToDecimal(txtindex[1]); }
                            catch { ldc_salaryamt = 0; }

                            //intsert ข้อมูลไปพักไว้ก่อน
                            ls_sql = @"insert into mbadjsalarytemp values({0},{1},{2})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_salaryid, ldc_salaryamt);
                            ex.SQL.Add(ls_sql);

                            n++;
                        }
                    }
                    ex.Execute();
                    ex.SQL.Clear();

                    LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");

                    SetValue();
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
                                string ls_salaryid;
                                decimal ldc_salaryamt;
                                try { ls_salaryid = Convert.ToString(gettxt[0]); }
                                catch { ls_salaryid = ""; }
                                try { ldc_salaryamt = Convert.ToDecimal(gettxt[1]); }
                                catch { ldc_salaryamt = 0; }

                                //intsert ข้อมูลไปพักไว้ก่อน
                                ls_sql = @"insert into mbadjsalarytemp values({0},{1},{2})";
                                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_salaryid, ldc_salaryamt);
                                ex.SQL.Add(ls_sql);

                                n++;
                            }
                        }
                        ex.Execute();
                        ex.SQL.Clear();

                        LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
                        SetValue();
                    }
                }
                catch (Exception eX)
                {
                    //เอาไว้ดู err เวลามีปัญหาตอน import
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
        }

        public void SetValue()
        {
            string ls_sql;
            ls_sql = "select count(1) from mbadjsalarytemp where coop_id = '" + state.SsCoopControl + "'";
            Sdt dt = WebUtil.QuerySdt(ls_sql);

            if (dt.Next())
            {
                all_disk.Text = dt.GetInt32("count(1)").ToString("##,#");
            }

            ls_sql = @"select count(tmp.salary_id) as match_item,
                        nvl( sum( case when tmp.salary_amount > mast.salary_amount then 1 else 0 end ) , 0 ) as inc_amt ,
                        nvl( sum( case when tmp.salary_amount < mast.salary_amount then 1 else 0 end ) , 0 ) as dec_amt ,
                        nvl( sum( case when tmp.salary_amount <> mast.salary_amount then 1 else 0 end ) , 0 ) as chg_amt,
                        nvl( sum( case when tmp.salary_amount = mast.salary_amount then 1 else 0 end ) , 0 ) as notchg_amt 
                        from mbadjsalarytemp tmp 
                        join mbmembmaster mast 
                        on mast.coop_id = tmp.coop_id
                        and trim(mast.salary_id) = tmp.salary_id 
                        where mast.resign_status = 0
                        and mast.coop_id = '" + state.SsCoopControl + "'";
            dt = WebUtil.QuerySdt(ls_sql);

            if (dt.Next())
            {
                match_item.Text = dt.GetInt32("match_item").ToString("##,#");
                inc_amt.Text = dt.GetInt32("inc_amt").ToString("##,#");
                dec_amt.Text = dt.GetInt32("dec_amt").ToString("##,#");
                chg_amt.Text = dt.GetInt32("chg_amt").ToString("##,#");
            }
        }
        
        //save salary
        public void SaveSalary()
        {
            try
            {
                str_mbaudit astr_mbaudit = new str_mbaudit();
                astr_mbaudit.operate_date = state.SsWorkDate;
                astr_mbaudit.userid = state.SsUsername;
                int result = shrlonService.RunSaveSalaryProcess(state.SsWsPass, ref astr_mbaudit, state.SsApplication, state.CurrentPage);
                Hd_process.Value = "true";
                //LtServerMessage.Text = WebUtil.CompleteMessage("Upload Complete");
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
    }
}