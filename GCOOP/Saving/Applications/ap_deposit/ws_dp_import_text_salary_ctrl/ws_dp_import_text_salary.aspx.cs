using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Saving.Applications.ap_deposit.ws_dp_import_text_salary_ctrl
{
    public partial class ws_dp_import_text_salary : PageWebSheet, WebSheet
    {
        ExecuteDataSource exc;
        Sdt dt = new Sdt();
        Sdt dt2 = new Sdt();
        [JsPostBack]
        public string PostImport { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            dsMain.DATA[0].tran_date = state.SsWorkDate;
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostImport")
            {
                string[] segments = txtInput.FileName.Split('.');
                string fileExt = segments[segments.Length - 1].ToLower();
                if (fileExt == "txt")
                {
                    PostImpText();
                }
                else
                {

                }
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }

        private void PostImpText()
        {
            exc = new ExecuteDataSource(this);
            string ls_sql = "", ls_salaryid = "", ls_membname = "", ls_systemcode = "";
            string ls_membsurname = "", ls_personid = "", ls_deptacc = "", ls_olddeptacc = "", ls_membno = "";
            decimal ldm_salaryamt = 0, ldm_seqno = 0;
            DateTime tran_date = new DateTime();
            try
            {

                FileUpload fu = txtInput;
                string filname = txtInput.FileName.ToString().Trim();
                Stream stream = fu.PostedFile.InputStream;
                byte[] bt = new byte[stream.Length];
                stream.Read(bt, 0, (int)stream.Length);
                string txt = Encoding.Unicode.GetString(bt);
                txt = Regex.Replace(txt, "\r", "");
                string[] lines = Regex.Split(txt, "\n");
                int txtLength;
                int n = 1;
                string[] txtindex;
                tran_date = dsMain.DATA[0].tran_date;
                ls_systemcode = "TMP";

                foreach (string line in lines)
                {
                    ldm_seqno = 1;
                    txtLength = line.Length;
                    txtindex = line.Split('\t');
                    try { ls_salaryid = Convert.ToString(txtindex[0]); }
                    catch { ls_salaryid = ""; }
                    ls_salaryid = Regex.Replace(ls_salaryid, @"[^\w\d]", "");
                    try { ls_membname = Convert.ToString(txtindex[1]).Trim(); }
                    catch { ls_membname = ""; }
                    try { ls_membsurname = Convert.ToString(txtindex[2]).Trim(); }
                    catch { ls_membsurname = ""; }
                    try { ls_personid = Convert.ToString(txtindex[3].Trim()); }
                    catch { ls_personid = ""; }
                    try { ls_deptacc = Convert.ToString(txtindex[4].Trim()); }
                    catch { ls_deptacc = ""; }
                    try { ldm_salaryamt = System.Math.Abs(Convert.ToDecimal(txtindex[5])); }
                    catch { ldm_salaryamt = 0; }

                    ls_membno = GetMemberNo(ls_deptacc.Trim());
                    if (ls_olddeptacc == ls_deptacc)
                    {
                        ldm_seqno++;
                    }
                    if (ls_salaryid == "Total")
                    {
                        return;
                    }
                    else
                    {
                        //intsert ข้อมูลไปพักไว้ก่อน
                        ls_sql = @"insert into dpdepttran 
                        ( coop_id, deptaccount_no, member_no, memcoop_id, system_code, tran_year, tran_date, seq_no, deptitem_amt, tran_status, branch_operate )
                        values ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} , {10} )";
                        ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_deptacc, ls_membno, state.SsCoopControl, ls_systemcode,
                            (tran_date.Year + 543), tran_date, ldm_seqno, ldm_salaryamt, 0, "001");
                        exc.SQL.Add(ls_sql);

                        ls_olddeptacc = ls_deptacc;
                    }
                }
                exc.Execute();
                exc.SQL.Clear();
                LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดในการ IMPORT ข้อมูล" + ex); }
        }

        private string GetMemberNo(string deptaccno)
        {
            string member_no = "";
            string sqlmbno = @"select member_no from dpdeptmaster where coop_id = {0} and deptaccount_no = {1}";
            sqlmbno = WebUtil.SQLFormat(sqlmbno, state.SsCoopControl, deptaccno);
            Sdt ta = WebUtil.QuerySdt(sqlmbno);
            if (ta.Next())
            {
                member_no = ta.GetString("member_no");
            }
            return member_no;
        }
    }
}