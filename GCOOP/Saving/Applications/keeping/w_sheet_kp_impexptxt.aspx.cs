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
using DataLibrary;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNKeeping;
using Sybase.DataWindow;
using System.IO;
using System.Web.Services.Protocols;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_impexptxt : PageWebSheet, WebSheet
    {

        private String ls_pbl = "egat_kp_impexpdisk.pbl";

        protected String postRetrieve;
        protected String postExport;
        protected String postImport;

        private DwThDate tDw_main;

        private void JspostNewClear()
        {
            try
            {
                dw_main.Reset();
                dw_data.Reset();
                dw_main.InsertRow(0);
                dw_main.SetItemString(1, "year", Convert.ToString(state.SsWorkDate.Year + 543).Trim());
                dw_main.SetItemString(1, "month", Right( "0" + state.SsWorkDate.Month.ToString().Trim(), 2));
                dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tDw_main.Eng2ThaiAllRow();
            }
            catch { }
        }
        public void InitJsPostBack()
        {
            postRetrieve = WebUtil.JsPostBack(this, "postRetrieve");
            postExport = WebUtil.JsPostBack(this, "postExport");
            postImport = WebUtil.JsPostBack(this, "postImport");

            tDw_main = new DwThDate(dw_main, this);
            tDw_main.Add("operate_date", "operate_tdate");
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_data);
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRetrieve")
            {
                JsPostRetrieve();
            }
            else if (eventArg == "postExport")
            {
                JsPostExport();
            }
            else if (eventArg == "postImport")
            {
                JsPostImport();
            }
        }
        public void SaveWebSheet()
        {
        }
        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "membgroup_code", ls_pbl, null);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            dw_main.SaveDataCache();
            dw_data.SaveDataCache();
        }

        private void JsPostRetrieve()
        {
            try
            {
                dw_data.DataWindowObject = "d_kp_impexp_data";
                dw_data.Reset();

                string ls_year = dw_main.GetItemString(1, "year").Trim();
                string ls_month = dw_main.GetItemString(1, "month").Trim();
                string ls_period = ls_year + ls_month;
                string ls_mbgroup = dw_main.GetItemString(1, "membgroup_code").Trim();
                DwUtil.RetrieveDataWindow(dw_data, ls_pbl, tDw_main, ls_period, ls_mbgroup);
            }
            catch { }
        }
        private void JsPostExport()
        {
            try
            {
                string ls_year = dw_main.GetItemString(1, "year").Trim();
                string ls_month = dw_main.GetItemString(1, "month").Trim();
                string ls_period = ls_year + ls_month;
                string ls_mbgroup = dw_main.GetItemString(1, "membgroup_code").Trim();
                string ls_time = dw_main.GetItemString(1, "time_no").Trim();
                DateTime ldtm_operate = dw_main.GetItemDateTime(1, "operate_date");

                DataStore lds_data;
                lds_data = new DataStore();
                lds_data.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keepingmonth\" + ls_pbl;
                lds_data.DataWindowObject = "d_kp_exptxt_data";

                string ls_smsDesc;
                decimal ldc_total = 0;
                // Header
                lds_data.InsertRow(0);
                ls_smsDesc = "H" + ls_mbgroup + ldtm_operate.Year.ToString().Trim() + Right("0" + ldtm_operate.Month.ToString().Trim(), 2) +
                    Right("0" + ldtm_operate.Day.ToString().Trim(), 2) + ls_period + ls_time;
                lds_data.SetItemString(1, "member_no", "HEADER00");
                lds_data.SetItemString(1, "sms_desc", ls_smsDesc);

                // Detail
                int li_rowcount, li_row;
                li_rowcount = dw_data.RowCount;
                if (li_rowcount > 0)
                {
                    
                    string ls_memberno, ls_salaryid, ls_prename, ls_name, ls_names, ls_surname;
                    decimal ldc_itemPay;
                    for (li_row = 1; li_row <= li_rowcount; li_row++)
                    {
                        ls_smsDesc = "";
                        try { ls_memberno = dw_data.GetItemString(li_row, "member_no").Trim(); }
                        catch { ls_memberno = ""; }
                        try { ls_salaryid = dw_data.GetItemString(li_row, "salary_id").Trim(); }
                        catch { ls_salaryid = ""; }
                        try { ls_prename = dw_data.GetItemString(li_row, "prename_desc").Trim(); }
                        catch { ls_prename = ""; }
                        try { ls_name = dw_data.GetItemString(li_row, "memb_name").Trim(); }
                        catch { ls_name = ""; }
                        ls_names = ls_prename + ls_name;
                        try { ls_surname = dw_data.GetItemString(li_row, "memb_surname").Trim(); }
                        catch { ls_surname = ""; }
                        try { ldc_itemPay = dw_data.GetItemDecimal(li_row, "item_payment"); }
                        catch { ldc_itemPay = 0; }
                        ldc_total += ldc_itemPay;

                        lds_data.InsertRow(0);
                        ls_smsDesc = "D" + ls_memberno.PadRight(8, ' ') + ls_salaryid.PadRight(15, ' ') + ls_names.PadRight(60, ' ') +
                            ls_surname.PadRight(60, ' ') + Right( "00000000"+ ldc_itemPay.ToString("###.00").Trim(),10);
                        lds_data.SetItemString(li_row + 1, "member_no", ls_memberno);
                        lds_data.SetItemString(li_row + 1, "sms_desc", ls_smsDesc);
                    }
                }

                //Sammary
                lds_data.InsertRow(0);
                ls_smsDesc = "T" + Right("0000" + li_rowcount.ToString(), 5) + Right( "0000000000000"+ ldc_total.ToString("###.00"),15);
                lds_data.SetItemString(li_rowcount + 2, "member_no", "SAMMARY0");
                lds_data.SetItemString(li_rowcount + 2, "sms_desc", ls_smsDesc);

                string ls_xml = lds_data.Describe("DataWindow.Data.XML");
                ExportText(ls_xml);
            }
            catch { }

        }
        private void JsPostImport()
        {
            try
            {
                String filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                String save = WebUtil.PhysicalPath + "Saving\\filecommon\\" + filename + ".txt";
                String err_msg = "";
                // String save = "C:\\GCOOP_ALL\\EGAT\\GCOOP\\Saving\\filecommon\\Temp_Disk.txt";
                FileUpload.PostedFile.SaveAs(save);

                //dw_data1.Reset();
                //dw_data1.ImportFile(save, FileSaveAsType.Text);
                Sdt dtReceiveAmt;
                decimal receive_amt = 0;

                DataStore lds_data;
                lds_data = new DataStore();
                lds_data.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keepingmonth\" + ls_pbl;
                lds_data.DataWindowObject = "d_kp_imptxt_data";
                lds_data.ImportFile(save, FileSaveAsType.Text);

                int li_row, li_rowcount;
                li_rowcount = lds_data.RowCount;
                if (li_rowcount > 0)
                {
                    string ls_mbgroup, ls_period, ls_timeno, ls_operatedate,
                        ls_memberno, ls_salaryid, ls_name, ls_surname,
                        ls_smsDesc, ls_sqlCode;
                    decimal ldc_itemPay, ldc_itemBal;
                    bool err_flag = false;
                    ls_mbgroup = ""; ls_period = ""; ls_timeno = ""; ls_operatedate = "";
                  
                   

                    dw_data.DataWindowObject = "d_kp_impexp_data_imp";
                    dw_data.Reset();


                    for (li_row = 1; li_row <= li_rowcount; li_row++)
                    {
                        ls_smsDesc = lds_data.GetItemString(li_row, "sms_desc").Trim();
                        if (ls_smsDesc.Substring(0, 1) == "H")
                        {
                            ls_mbgroup = ls_smsDesc.Substring(1, 8);
                            ls_operatedate = ls_smsDesc.Substring(9, 8);
                            ls_period = ls_smsDesc.Substring(17, 6);
                            ls_timeno = ls_smsDesc.Substring(23, 1);
                            ls_sqlCode = "Delete From KPTEMPSMS WHERE MEMBGROUP_CODE ='" + ls_mbgroup + "' and RECV_PERIOD = '" + ls_period + "' ";
                            WebUtil.Query(ls_sqlCode);
                        }
                        else if (ls_smsDesc.Substring(0, 1) == "D")
                        {
                            int length_str = ls_smsDesc.ToString().Length;
                            if (length_str != 164) 
                            {
                                err_msg = err_msg + "Format ผิดพลาด : " + ls_smsDesc + "<BR>";
                                err_flag = true;
                                continue;
                            }
                            ls_memberno = ls_smsDesc.Substring(1, 8);
                            ls_salaryid = ls_smsDesc.Substring(9, 15);
                            ls_name = ls_smsDesc.Substring(24, 60);
                            ls_surname = ls_smsDesc.Substring(84, 60);
                            ldc_itemPay = Convert.ToDecimal(ls_smsDesc.Substring(144, 10));
                            ldc_itemBal = Convert.ToDecimal(ls_smsDesc.Substring(154, 10));

                            ls_sqlCode = "INSERT INTO KPTEMPSMS( MEMBER_NO, SALARY_ID, RECV_PERIOD, TIME_NO, " +
                                "MEMBGROUP_CODE, OPERATE_DATE, MEMB_NAME, MEMB_SURNAME, ITEM_PAYMENT, ITEM_BALANCE ) " +
                                "VALUES ( '" + ls_memberno + "','" + ls_salaryid + "','" + ls_period + "','" + ls_timeno + 
                                "','" + ls_mbgroup + "', to_date('" + ls_operatedate + "','yyyymmdd'),'" + ls_name +
                                "','" + ls_surname + "'," + ldc_itemPay.ToString().Trim() + "," +
                                ldc_itemBal.ToString().Trim() + ")";
                            WebUtil.Query(ls_sqlCode);


                            string ls_sql_kptemp = "select receive_amt from kptempreceive where member_no = '" + ls_memberno + "' and recv_period = '" + ls_period + "'";
                            dtReceiveAmt = WebUtil.QuerySdt(ls_sql_kptemp);
                            if (dtReceiveAmt.Next())
                            {
                                try
                                {
                                    receive_amt = dtReceiveAmt.GetDecimal("receive_amt");
                                    //ldc_itemBal = Convert.ToDecimal(ls_smsDesc.Substring(154, 10));
                                    if (ldc_itemBal != receive_amt)
                                    {
                                        string SqlUpdate = "update kptempreceive set keeping_status = -9 where member_no = '" + ls_memberno + "' and recv_period = '" + ls_period + "'";
                                        WebUtil.Query(SqlUpdate);
                                        string SqlUpdate1 = "update kptempreceivedet set keepitem_status = -9 where member_no = '" + ls_memberno + "' and recv_period = '" + ls_period + "'";
                                        WebUtil.Query(SqlUpdate1);
                                    }
                                    else
                                    {
                                        string SqlUpdate = "update kptempreceive set keeping_status = 1 where member_no = '" + ls_memberno + "' and recv_period = '" + ls_period + "'";
                                        WebUtil.Query(SqlUpdate);
                                        string SqlUpdate1 = "update kptempreceivedet set keeping_status = 1 where member_no = '" + ls_memberno + "' and recv_period = '" + ls_period + "'";
                                        WebUtil.Query(SqlUpdate1);
                                    }
                                }
                                catch { }
                            }


                            dw_data.InsertRow(0);
                            dw_data.SetItemString(dw_data.RowCount, "member_no", ls_memberno);
                            dw_data.SetItemString(dw_data.RowCount, "memb_name", ls_name);
                            dw_data.SetItemString(dw_data.RowCount, "memb_surname", ls_surname);
                            dw_data.SetItemDecimal(dw_data.RowCount, "item_payment", ldc_itemPay);
                            dw_data.SetItemDecimal(dw_data.RowCount, "itembal_amt", ldc_itemBal);                        

                        }
                        else
                        {
                        }
                    }

                    dw_data.SetSort("member_no ASC");
                    dw_data.Sort();

                    if (err_flag == false ) LtServerMessage.Text = WebUtil.CompleteMessage("นำเข้าข้อมูลเรียบร้อย");
                    else LtServerMessage.Text = WebUtil.ErrorMessage(err_msg);
                }
            }
            catch(Exception ex) 
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("Import Error : " + ex);
            }

        }

        #region PersonalFunction
        private void ExportText(String as_xmlData)
        {
            try
            {
                DataStore lds_data;
                str_rptexcel astr_rptexcel = new str_rptexcel();
                astr_rptexcel.as_xmldw = as_xmlData;
                if (astr_rptexcel.as_xmldw != "")
                {
                    String xml_detail = astr_rptexcel.as_xmldw;
                    String ls_filename = "SMS_.txt";
                    String ls_path = "C:\\TEMP\\" + ls_filename;

                    lds_data = new DataStore();
                    lds_data.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keepingmonth\" + ls_pbl;
                    lds_data.DataWindowObject = "d_kp_exptxt_data";
                    lds_data.ImportString(xml_detail, FileSaveAsType.Xml);
                    int li_rowcount = lds_data.RowCount;
                    int li_row;
                    String ls_sms = "";
                    StreamWriter writer = new StreamWriter(ls_path, false, System.Text.Encoding.GetEncoding(874));
                    for (li_row = 1; li_row <= li_rowcount; li_row++)
                    {
                        try
                        {
                            ls_sms = lds_data.GetItemString(li_row, "sms_desc").Trim();
                        }
                        catch { }
                        writer.WriteLine(ls_sms.Replace(Environment.NewLine, "<br/>"));
                    }
                    writer.Close();
                    JspostNewClear();

                    string fName = @"C:\\TEMP\\" + ls_filename;
                    FileInfo fi = new FileInfo(fName);
                    long sz = fi.Length;

                    Response.ClearContent();
                    Response.ContentType = MimeType(Path.GetExtension(fName));
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)));
                    Response.AddHeader("Content-Length", sz.ToString("F0"));
                    Response.TransmitFile(fName);
                    Response.End();

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }
        public static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;
            string ext = Extension.ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }
        private String Right(String as_string, int ai_length)
        {
            String ls_return;
            if (ai_length <= as_string.Length)
            {
                int li_len = as_string.Length;
                ls_return = as_string.Substring(li_len - ai_length, ai_length);
            }
            else ls_return = as_string;
            return ls_return;
        }
        //private String FillString(string as_string, int ai_length)
        //{
        //    string ls_return;
        //    int li_lenDiff;
        //    if (as_string.Length < ai_length)
        //    {
        //        li_lenDiff = ai_length - as_string.Length;

        //    }
        //    else ls_return = as_string;
        //    return ls_return;
        //}
        #endregion

    }
}