using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Saving.Applications.mbshr.ws_mbshr_update_expaccid_ctrl
{
    public partial class ws_mbshr_update_expaccid : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostImport { get; set; }
        [JsPostBack]
        public string PostReport { get; set; }
        [JsPostBack]
        public string PostUpdate { get; set; }

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostImport")
            {
                string ls_sql;
                ExecuteDataSource ex = new ExecuteDataSource(this);

                // ลบข้อมูลทิ้งก่อน
                ex.SQL.Add("delete from mbadjexpensetemp");

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
                    string ls_salaryid, ls_expaccid;
                    string[] txtindex;

                    foreach (string line in lines)
                    {
                        txtLength = line.Length;
                        if (txtLength > 3)
                        {
                            txtindex = line.Split(',');
                            try { ls_salaryid = Convert.ToString(txtindex[0]); }
                            catch { ls_salaryid = ""; }
                            try { ls_expaccid = Convert.ToString(txtindex[1]); }
                            catch { ls_expaccid = ""; }

                            //intsert ข้อมูลไปพักไว้ก่อน
                            ls_sql = @"insert into mbadjexpensetemp values({0},{1})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, ls_salaryid, ls_expaccid);
                            ex.SQL.Add(ls_sql);

                            n++;
                        }
                    }
                    ex.Execute();
                    ex.SQL.Clear();

                    LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
                }
                catch (Exception eX)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
            else if (eventArg == "PostReport")
            {
                try
                {
                    iReportArgument args = new iReportArgument();
                    
                    args.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                    iReportBuider report = new iReportBuider(this, "รายงานการปรับเลขบัญชี");
                    report.AddCriteria("r_010_mb_update_expense_accid", "ดาวน์โหลด รายงานการปรับเลขบัญชี", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == "PostUpdate")
            {
                ExecuteDataSource ex = new ExecuteDataSource(this);                

                try
                {
                 //อัปเดตข้อมูลเลขที่บัญชี
                    ex.SQL.Add(@"update mbmembmaster mb set ( mb.expense_code, mb.expense_bank, mb.expense_accid ) = ( select 'CBT', '034', mt.expense_accid from mbadjexpensetemp mt where mb.salary_id = mt.salary_id and mb.resign_status = 0 )
                        where exists ( select 1 from mbadjexpensetemp mt where mb.salary_id = mt.salary_id and mb.resign_status = 0 )");

                    ex.Execute();
                    ex.SQL.Clear();

                    LtServerMessage.Text = WebUtil.CompleteMessage("Update Complete");
                }
                catch (Exception eX)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}