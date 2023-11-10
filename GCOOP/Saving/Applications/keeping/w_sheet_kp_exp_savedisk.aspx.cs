using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNKeeping;
using DataLibrary;
using System.Web.Services.Protocols;
using System.IO;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_exp_savedisk  : PageWebSheet,WebSheet
    {
        DataStore DStore;
        public String pbl = "kp_exp_savedisk.pbl";
        protected String postInit;
        protected String postChangeOption;
        private DwThDate tDw_main;
        //==============================
        public void InitJsPostBack()
        {
       
            postInit = WebUtil.JsPostBack(this, "postInit");
            postChangeOption = WebUtil.JsPostBack(this, "postChangeOption");
            //==================================
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
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
        }

        public void SaveWebSheet()
        {
            try 
            {
                String ls_filename = "", ls_path = "", ls_trndate = "";
                String format_text = Dw_choice.GetItemString(1, "format_text");
                String member_type = Dw_main.GetItemString(1, "member_type");
                String filename = "";
                DateTime ldtm_trndate = new DateTime();
                //ออก Text File แบบเงินเดือน TMT
                if (format_text == "TMT")
                {
                    if (member_type == "1")
                    {
                        filename = "TMT_1_";
                    }
                    else
                    {
                        filename = "TMT_2_";
                    }

                    DStore = new DataStore();
                    DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_exp_savedisk.pbl";
                    DStore.DataWindowObject = "d_kp_dsksrv_hrtext";

                    String ls_datacode, ls_salaryid;
                    int li_row, li_count;
                    Decimal ldc_moneyamt = 0;
                   

                    ls_datacode = Dw_main.GetItemString(1, "data_code");
                    ldtm_trndate = Dw_main.GetItemDate(1, "operate_date");
                    ls_trndate = ldtm_trndate.ToString("dd.MM.yyyy");
                    ls_filename = filename + (ldtm_trndate.Year + 543 - 2500).ToString() + ldtm_trndate.ToString("dd.MM") + ".txt";
                    ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + filename + (ldtm_trndate.Year + 543 - 2500).ToString() + ldtm_trndate.ToString("dd.MM") + ".txt";

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
                        ldc_moneyamt = Dw_detail.GetItemDecimal(i, "item_payment");

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
                    

                    if (member_type == "1")
                    {
                        filename = "TDP_1_";
                    }
                    else
                    {
                        filename = "TDP_2_";
                    }

                    DStore = new DataStore();
                    DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_exp_savedisk.pbl";
                    DStore.DataWindowObject = "d_kp_dsksrv_linetext";

                    String ls_memberno, ls_space,ls_space1, ls_space2, ls_space3, ls_space4, ls_expaccid, ls_memname;
                    int li_row, li_count, li_running;
                    Decimal ldc_moneyamt = 0,ldc_moneytotal = 0;

                    ldtm_trndate = Dw_main.GetItemDate(1, "operate_date");
                    ls_trndate = ldtm_trndate.ToString("ddMMyy");
                    ls_filename = filename + (ldtm_trndate.Year + 543 - 2500).ToString() + ldtm_trndate.ToString("dd.MM") + ".txt";
                    ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + filename + (ldtm_trndate.Year + 543 - 2500).ToString() + ldtm_trndate.ToString("dd.MM") + ".txt";

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
                        try { ls_expaccid = Dw_detail.GetItemString(i, "expense_accid").Trim(); }
                        catch { ls_expaccid = ""; }
                        try { ls_memname = Dw_detail.GetItemString(i, "memname").Trim(); }
                        catch { ls_memname = ""; }


                        ldc_moneyamt = Dw_detail.GetItemDecimal(i, "item_payment");
                        ldc_moneytotal = ldc_moneytotal + ldc_moneyamt;
                        li_row = DStore.InsertRow(0);
                        linetext = "D" + li_row.ToString("000000") + company_bank + ls_expaccid + "D" + (ldc_moneyamt * 100).ToString("0000000000") +transfer_code + "9" + ls_space2 + ldtm_trndate.ToString("yyMMdd") + ls_space3 + ls_memberno + " " + ls_memname;
                        DStore.SetItemString(li_row, "line_text", linetext);
                    }
                    
                    //footer
                    li_row = DStore.InsertRow(0);

                    if (transfer_type == "D")
                    {
                        linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + Dw_detail.RowCount.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + "0000000" + "000000000000000" + ls_space4;
                    }
                    else
                    {
                        linetext = "T" + li_row.ToString("000000") + company_bank + company_accid + "0000000" + "000000000000000" + Dw_detail.RowCount.ToString("0000000") + (ldc_moneytotal * 100).ToString("000000000000000") + ls_space4;
                    }
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
            tDw_main.Eng2ThaiAllRow();
        }

        private void JspostInit()
        {
            try 
            {
                //เปลี่ยน datawindow ก่อน
                JsChangeDataWindow();

                String year = Dw_main.GetItemString(1, "year");
                String month = Dw_main.GetItemString(1, "month");
                if (month.Length != 2)
                {
                    month = "0" + month;
                }
                String recv_period = year + month;
                Dw_detail.Reset();
                DwUtil.RetrieveDataWindow(Dw_detail, pbl, null, recv_period);
                HdRowCount.Value = Dw_detail.RowCount.ToString();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JsChangeDataWindow()
        {
            try
            {
                String format_text = Dw_choice.GetItemString(1, "format_text");
                String member_type = Dw_main.GetItemString(1, "member_type");
                //กรณีเป็นเงินเดือน สมาชิกปกติ
                if (format_text == "TMT" && member_type == "1")
                {
                    Dw_detail.DataWindowObject = "d_kp_exp_savedisk_data_tmt_1";
                }
                //กรณีเป็นเงินเดือน สมาชิกสมทบ
                else if (format_text == "TMT" && member_type == "2")
                {
                    Dw_detail.DataWindowObject = "d_kp_exp_savedisk_data_tmt_2";
                }
                //กรณีเป็นเงินฝาก สมาชิกปกติ
                else if (format_text == "TDP" && member_type == "1")
                {
                    Dw_detail.DataWindowObject = "d_kp_exp_savedisk_data_tdp_1";
                }
                //กรณีเป็นเงินฝาก สมาชิกสมทบ
                else if (format_text == "TDP" && member_type == "2")
                {
                    Dw_detail.DataWindowObject = "d_kp_exp_savedisk_data_tdp_2";
                }
              //  Dw_detail.Reset();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostChangeOption()
        {
            try
            {
                String format_text = Dw_choice.GetItemString(1, "format_text");
                //กรณีเป็นเงินเดือน
                if (format_text == "TMT")
                {
                    Dw_main.DataWindowObject = "d_kp_exp_savedisk_option";
                }
                //กรณีเป็นเงินฝาก
                else
                {
                    Dw_main.DataWindowObject = "d_kp_exp_savedisk_option_tdp";
                }

                JspostSetData();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetData()
        {
            try 
            {
                String format_text = Dw_choice.GetItemString(1, "format_text");
                if (format_text == "TMT")
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
                    Dw_main.SetItemString(1, "transfer_code", "01");
                    Dw_detail.Reset();
                    tDw_main.Eng2ThaiAllRow();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}