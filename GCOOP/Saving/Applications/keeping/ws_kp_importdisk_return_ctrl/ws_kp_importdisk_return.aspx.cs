using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Saving.Applications.keeping.ws_kp_importdisk_return_ctrl
{
    public partial class ws_kp_importdisk_return : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostImport { get; set; }

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                //set งวด             
                year.Text = Convert.ToString(WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate));
                month.Text = DateTime.Now.Month.ToString();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostImport")
            {
                string ls_sql, ls_period, ls_fullname;
                decimal ldc_membertype = 0;
                ExecuteDataSource ex = new ExecuteDataSource(this);

                ls_period = year.Text + Convert.ToDecimal(month.Text).ToString("00");
                ldc_membertype = Convert.ToDecimal(member_type.SelectedValue);

                // ลบข้อมูลทิ้งก่อน
                ex.SQL.Add("delete from kpdiskreturn where moneytype_code = 'SAL' and member_type = " + ldc_membertype);

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
                    string ls_salaryid;
                    decimal ldc_moneyamt;
                    string[] txtindex;

                    foreach (string line in lines)
                    {                        
                        txtLength = line.Length;
                        if (txtLength > 3)
                        {
                            txtindex = line.Split(',');
                            try { ls_salaryid = Convert.ToString(txtindex[0]); }
                            catch { ls_salaryid = ""; }
                            try { ls_fullname = Convert.ToString(txtindex[1]); }
                            catch { ls_fullname = ""; }
                            try { ldc_moneyamt = System.Math.Abs(Convert.ToDecimal(txtindex[2])); }
                            catch { ldc_moneyamt = 0; }

                            //intsert ข้อมูลไปพักไว้ก่อน
                            ls_sql = @"insert into kpdiskreturn values({0},{1},{2},{3},{4},{5},{6},{7})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_period, ls_salaryid, ls_fullname, ldc_membertype, ldc_moneyamt, "CBT", "");
                            ex.SQL.Add(ls_sql);

                            n++;
                        }
                    }
                    //อัปเดตข้อมูลที่สามารถเรียกเก็บได้จริง จากการขึ้นแผ่น
                    ex.SQL.Add(@"update	kpreceiveexpense ke
                            set	ke.diskreturn_amt	= ( select abs( money_amt ) from kpdiskreturn kd, mbmembmaster mb where kd.salary_id = mb.salary_id and kd.period = ke.recv_period and ke.memcoop_id = mb.coop_id and ke.member_no = mb.member_no )
                            where exists ( select 1 from kpdiskreturn kd, mbmembmaster mb where kd.salary_id = mb.salary_id and kd.period = ke.recv_period and ke.memcoop_id = mb.coop_id and ke.member_no = mb.member_no )
                            and ke.moneytype_code = 'SAL'
                            and ke.recv_period = '" + ls_period + "'")                                                    ;
                    ex.Execute();
                    ex.SQL.Clear();

                    LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
                }
                catch (Exception eX)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(eX);
                }
            }
            else if (eventArg == "excel")
            {
                string ls_sql, ls_period, ls_fullname;
                decimal ldc_membertype = 0;
                ExecuteDataSource ex = new ExecuteDataSource(this);

                ls_period = year.Text + Convert.ToDecimal(month.Text).ToString("00");
                ldc_membertype = Convert.ToDecimal(member_type.SelectedValue);

                // ลบข้อมูลทิ้งก่อน
                ex.SQL.Add("delete from kpdiskreturn where member_type = " + ldc_membertype);
                try
                {
                    string saveFolder = @"C:\GCOOP_ALL\CORE\GCOOP\Saving\WSRPDF";
                    string filePath = Path.Combine(saveFolder, txtInput.FileName);
                    File_Name.Text = txtInput.FileName;
                    int n = 1;

                    txtInput.SaveAs(filePath);

                    String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);

                    //Create Connection to Excel work book 
                    using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter("Select salary_id, name, money from [Sheet1$]", excelConnection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        string txtFull = "";
                        string doc_no = String.Format("{0:yyyyMMdd}", state.SsWorkDate);
                        string temp_doc = "";
                        int txtLength;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string cell, txt = "";

                            temp_doc = doc_no + n.ToString("0000000");

                            for (int j = 0; j < dt.Columns.Count; j++)
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
                                decimal ldc_moneyamt;
                                try { ls_salaryid = Convert.ToString(gettxt[0]); }
                                catch { ls_salaryid = ""; }
                                try { ls_fullname = Convert.ToString(gettxt[1]); }
                                catch { ls_fullname = ""; }
                                try { ldc_moneyamt = System.Math.Abs(Convert.ToDecimal(gettxt[2])); }
                                catch { ldc_moneyamt = 0; }

                                //intsert ข้อมูลไปพักไว้ก่อน
                                ls_sql = @"insert into kpdiskreturn values({0},{1},{2},{3},{4},{5},{6})";
                                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_period, ls_salaryid, ls_fullname, ldc_membertype, ldc_moneyamt, "SAL");
                                ex.SQL.Add(ls_sql);

                                n++;
                            }
                        }

                        //อัปเดตข้อมูลที่สามารถเรียกเก็บได้จริง จากการขึ้นแผ่น
                        ex.SQL.Add(@"update	kpreceiveexpense ke
                            set	ke.diskreturn_amt	= ( select abs( money_amt ) from kpdiskreturn kd, mbmembmaster mb where kd.salary_id = mb.salary_id and kd.period = ke.recv_period and ke.memcoop_id = mb.coop_id and ke.member_no = mb.member_no )
                            where exists ( select 1 from kpdiskreturn kd, mbmembmaster mb where kd.salary_id = mb.salary_id and kd.period = ke.recv_period and ke.memcoop_id = mb.coop_id and ke.member_no = mb.member_no )
                            and ke.recv_period = '" + ls_period + "'");
                        ex.Execute();
                        ex.SQL.Clear();

                        LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
                    }
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