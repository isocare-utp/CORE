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
    public partial class w_sheet_kp_opr_impfile : PageWebSheet,WebSheet
    {

        protected String postSetCriteria;
        public String pbl = "kp_opr_impfile";
        //====================================================
        public void InitJsPostBack()
        {
            postSetCriteria = WebUtil.JsPostBack(this, "postSetCriteria");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_choice);
                this.RestoreContextDw(Dw_detail);
                this.RestoreContextDw(Dw_linetext);
                this.RestoreContextDw(Dw_cri);
            }
            
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSetCriteria")
            {
                JspostSetCriteria();
            }
        }

        public void SaveWebSheet()
        {
            try 
            {
                string imp_choice = Dw_choice.GetItemString(1, "imp_choice");
                string member_no = "";
                string sql = "";
                //กรณีเป็นเงินเดือน
                if (imp_choice == "01")
                {
                    decimal salary = 0;
                    if (Dw_detail.RowCount > 0)
                    {
                        for (int i = 1; i <= Dw_detail.RowCount; i++)
                        {
                            member_no = Dw_detail.GetItemString(i, "member_no");
                            salary = Dw_detail.GetItemDecimal(i, "salary");
                            sql = " UPDATE MBMEMBMASTER " +
                                  " SET salary_amount = '" + salary + "'" +
                                  " WHERE member_no = '" + member_no + "'";
                            WebUtil.ExeSQL(sql);
                        }
                    }
                }
                //กรณีเป็น imp เรียกเก็บ
                else
                {
                    decimal receipt_amt = 0;
                    string year = "";
                    string month = "";
                    string recv_period = "";
                    year = Dw_cri.GetItemString(1,"year");
                    month = Dw_cri.GetItemString(1,"month");
                    if(month.Length !=2)
                    {
                        month = "0"+month;
                    }
                    recv_period = year+month;

                    if (Dw_detail.RowCount > 0)
                    {
                        for (int i = 1; i <= Dw_detail.RowCount; i++)
                        {
                            member_no = Dw_detail.GetItemString(i, "member_no");
                            receipt_amt = Dw_detail.GetItemDecimal(i, "receipt_amt");
                            sql = " UPDATE KPTEMPRECEIVE " +
                                  " SET receipt_amt = " + receipt_amt + "" +
                                  " WHERE member_no = '" + member_no + "'" +
                                  " And recv_period = '" + recv_period + "'";
                            WebUtil.ExeSQL(sql);
                            
                        }
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                JspostNewClear();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_choice.SaveDataCache();
            Dw_detail.SaveDataCache();
            Dw_linetext.SaveDataCache();
            Dw_cri.SaveDataCache();
        }

        //===============================
        private void JspostNewClear()
        {
            try 
            {
                string recv_period = "";
                string month = "";
                string year = "";

                Dw_choice.Reset();
                Dw_choice.InsertRow(0);
                Dw_detail.Reset();
                Dw_cri.Reset();
                Dw_cri.InsertRow(0);

                try
                {
                    String sql = @"select max(recv_period) from kptempreceive";
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        recv_period = dt.GetString("max(recv_period)");
                        year = recv_period.Substring(0, 4);
                        month = recv_period.Substring(4, 2);
                        Dw_cri.SetItemString(1, "year", year);
                        Dw_cri.SetItemString(1, "month", month);
                    }
                    else
                    {
                        sqlca.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    year = Convert.ToString(DateTime.Now.Year + 543);
                    month = Convert.ToString(DateTime.Now.Month);
                    if (month.Length != 2)
                    {
                        month = "0" + month;
                    }
                    Dw_cri.SetItemString(1, "year", year);
                    Dw_cri.SetItemString(1, "month", month);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        protected void b_import_Click(object sender, EventArgs e)
        {
            try 
            {
                string imp_choice = Dw_choice.GetItemString(1, "imp_choice");
                String filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                String save = WebUtil.PhysicalPath + "Saving\\filecommon\\Temp_Disk.txt";
                FileUpload.PostedFile.SaveAs(save);
                string[] textData = System.IO.File.ReadAllLines(save);
                string[] data;

                //กรณีเป็น imp เงินเดือน
                if (imp_choice == "01")
                {
                    for (int i = 1; i <= textData.Length; i++)
                    {
                        data = textData[i - 1].Split('\t');
                        Dw_detail.InsertRow(i);
                        Dw_detail.SetItemString(i, "member_no", data[0]);
                        Dw_detail.SetItemDecimal(i, "salary", Convert.ToDecimal(data[1]));
                    }
                }
                //กรณีเป็น imp เรียกเก็บ
                else
                {
                    for (int i = 1; i <= textData.Length; i++)
                    {
                        data = textData[i - 1].Split(',');
                        Dw_detail.InsertRow(i);
                        Dw_detail.SetItemString(i, "member_no", data[0]);
                        Dw_detail.SetItemDecimal(i, "receipt_amt", Convert.ToDecimal(data[1]));
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetCriteria()
        {
            try 
            {
                string imp_choice = Dw_choice.GetItemString(1, "imp_choice");
                //กรณี imp ไฟล์ เรียกเก็บ
                if (imp_choice == "02")
                {
                    Dw_cri.Visible = true;
                    Dw_cri.DataWindowObject = "d_kp_opr_impfile_kptemp";
                    Dw_detail.Reset();
                    Dw_detail.DataWindowObject = "d_kp_opr_impfile_kptemp_text";
                }
                //กรณี imp ไฟล์เงินเดือน
                else
                { 
                    Dw_cri.Visible = false;
                }



            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        
    }
}