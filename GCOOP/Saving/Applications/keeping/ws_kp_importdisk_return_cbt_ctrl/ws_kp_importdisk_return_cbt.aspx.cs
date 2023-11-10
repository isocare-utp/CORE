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

namespace Saving.Applications.keeping.ws_kp_importdisk_return_cbt_ctrl
{
    public partial class ws_kp_importdisk_return_cbt : PageWebSheet, WebSheet
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
                string ls_sql, ls_period, ls_salaryid = "", ls_expaccid = "";
                decimal ldc_membertype = 0;
                ExecuteDataSource ex = new ExecuteDataSource(this);

                ls_period = year.Text + Convert.ToDecimal(month.Text).ToString("00");
                ldc_membertype = Convert.ToDecimal(member_type.SelectedValue);

                // ลบข้อมูลทิ้งก่อน
                ex.SQL.Add("delete from kpdiskreturn where moneytype_code = 'CBT' and member_type = " + ldc_membertype);

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
                    int li_keepstatus = 0;

                    foreach (string line in lines)
                    {
                        if (n > 1 && n < lines.Length)
                        {
                            txtLength = line.Length;
                            li_keepstatus = Convert.ToInt32(line.Substring(37, 1));
                            if (li_keepstatus == 0)
                            {

                                try { ls_salaryid = "00" + Convert.ToString(line.Substring(97, 6)); }
                                catch { ls_salaryid = ""; }
                                ls_expaccid = line.Substring(10, 12);

                                //intsert ข้อมูลไปพักไว้ก่อน
                                ls_sql = @"insert into kpdiskreturn values({0},{1},{2},{3},{4},{5},{6},{7})";
                                ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_period, ls_salaryid, "", ldc_membertype, 0, "CBT", ls_expaccid);
                                ex.SQL.Add(ls_sql);
                            }
                        }
                        n++;
                    }
                    //อัปเดตข้อมูลที่สามารถเรียกเก็บได้จริง จากการขึ้นแผ่น
                    ex.SQL.Add(@"update kpreceiveexpense kpe set kpe.diskreturn_amt = kpe.item_payment
                        where kpe.moneytype_code = 'CBT'
                        and kpe.recv_period = '" + ls_period + @"'
                        and exists ( select 1 from kpdiskreturn kpr where kpe.member_no = kpr.salary_id and kpr.moneytype_code = 'CBT' )");
                    ex.Execute();
                    ex.SQL.Clear();

                    LtServerMessage.Text = WebUtil.CompleteMessage("Import Complete");
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