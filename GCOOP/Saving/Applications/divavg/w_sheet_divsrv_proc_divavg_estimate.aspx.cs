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
using CoreSavingLibrary.WcfNDivavg;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfReport;
using System.ServiceModel;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_proc_divavg_estimate : PageWebSheet, WebSheet
    {

        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_option;
        private n_divavgClient DivavgService;
        private n_commonClient CommonService;
        private String pbl = "divsrv_proc_estdivavg.pbl";
        protected String postNewClear;
        protected String postRefresh;
        protected String postPrcEstdivavg;
        protected String postSetAccDate;
        protected String postShowReport;
        protected String postSetAvgpercent;

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;

        public string outputProcess;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_option = new DwThDate(Dw_option, this);
            tDw_option.Add("operate_date", "operate_tdate");
            tDw_option.Add("eacc_date", "eacc_tdate");
            tDw_option.Add("sacc_date", "sacc_tdate");
            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postPrcEstdivavg = WebUtil.JsPostBack(this, "postPrcEstdivavg");
            postSetAccDate = WebUtil.JsPostBack(this, "postSetAccDate");
            postShowReport = WebUtil.JsPostBack(this, "postShowReport");
            postSetAvgpercent = WebUtil.JsPostBack(this, "postSetAvgpercent");
        }

        public void WebSheetLoadBegin()
        {
            HdOpenIFrame.Value = "False";
            Hd_process.Value = "false";
            Dw_report.Visible = false;
            B_report.Visible = false;


            if (!IsPostBack)
            {

                this.ConnectSQLCA();
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_option);
                this.RestoreContextDw(Dw_loan);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            // Event ที่เกิดจาก JavaScript
            switch (eventArg)
            {
                case "postNewClear":
                    JspostNewClear();
                    break;
                case "postRefresh":
                    //Refresh();
                    break;
                case "postPrcEstdivavg":
                    JsProcEstdivavg();
                    break;
                case "postSetAccDate":
                    JspostSetAccDate();
                    break;
                case "postShowReport":
                    JspostShowReport();
                    break;
                case "postSetAvgpercent":
                    JspostSetAvgpercent();
                    break;


            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            // Retrieve DropDown
            // DwUtil.RetrieveDDDW(Dw_option, "proc_type", pbl, null);

            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_option.Eng2ThaiAllRow();
            Dw_option.SaveDataCache();
            Dw_loan.SaveDataCache();

            if (Dw_report.RowCount > 0)
            {
                //   B_Print.Visible = true;

            }
            else
            {
                B_Print.Visible = false;
            }

            HDCoop_ID.Value = state.SsCoopControl;
        }
        #endregion
        //=============================================
        private void JspostShowReport()
        {
            try
            {
                DwUtil.RetrieveDataWindow(Dw_report, pbl, null, state.SsCoopId);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        //function การหาช่วงวันที่จากข้อมุลปีปันผล
        private void JspostSetAccDate()
        {
            String ls_divyear, ls_sacctdate, ls_eacctdate;
            int li_divyear;
            ls_divyear = Dw_option.GetItemString(1, "div_year");
            ls_divyear = ls_divyear.Substring(0, 4);
            li_divyear = int.Parse(ls_divyear) - 543;

            try
            {
                String sql = @"select beginning_of_accou,ending_of_account from accaccountyear where account_year = '" + li_divyear.ToString() + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_sacctdate = dt.GetString("beginning_of_accou");
                    ls_eacctdate = dt.GetString("ending_of_account");
                    Dw_option.SetItemDateTime(1, "sacc_date", Convert.ToDateTime(ls_sacctdate));
                    Dw_option.SetItemDateTime(1, "eacc_date", Convert.ToDateTime(ls_eacctdate));
                    tDw_option.Eng2ThaiAllRow();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลปีบัญชี : " + ls_divyear);
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }





        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            Dw_option.SetItemDateTime(1, "eacc_date", state.SsWorkDate);
            Dw_option.SetItemDateTime(1, "sacc_date", state.SsWorkDate);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);

            Dw_loan.Reset();
            DwUtil.RetrieveDataWindow(Dw_loan, pbl, null, state.SsCoopId, "E");

            JsGetYear();

            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;

        }

        // function ประมาณผลปันผลเฉลี่ยคืน
        private void JsProcEstdivavg()
        {
            try
            {
                string xml_option = Dw_option.Describe("DataWindow.Data.XML");
                string xml_option_lntype = Dw_loan.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "DIVESTIMATEBFCLS", xml_option, xml_option_lntype, "");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }
        //ปุ่มยกเลิก
        protected void B_cancel_Click(object sender, EventArgs e)
        {
            JspostNewClear();
        }

        //ปุ่มปิด
        protected void B_close_Click(object sender, EventArgs e)
        {
            JspostNewClear();
        }

        //ปุ่มต่อไป
        protected void B_next_Click(object sender, EventArgs e)
        {
            string sql = "";
            string avgtype_code = "";

            Decimal ldc_div_flag = Dw_option.GetItemDecimal(1, "div_flag");
            Decimal ldc_avg_flag = Dw_option.GetItemDecimal(1, "avg_flag");

            Decimal ldc_sdiv_rate = Dw_option.GetItemDecimal(1, "sdiv_rate");
            Decimal ldc_ediv_rate = Dw_option.GetItemDecimal(1, "ediv_rate");
            Decimal ldc_divstep_rate = Dw_option.GetItemDecimal(1, "divstep_rate");
            Decimal ldc_savg_rate = Dw_option.GetItemDecimal(1, "savg_rate");
            Decimal ldc_eavg_rate = Dw_option.GetItemDecimal(1, "eavg_rate");
            Decimal ldc_avgstep_rate = Dw_option.GetItemDecimal(1, "avgstep_rate");

            DwUtil.RetrieveDataWindow(Dw_loan, pbl, null, state.SsCoopId, "E");
            //Dw_loan.Retrieve(state.SsCoopId);
            //ตรวจสอบการกรอกข้อมูล
            if (ldc_div_flag == 0 && ldc_avg_flag == 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกการประมาณปันผล เฉลี่ยคืน");
            }
            else
            {
                //กรณีเลือกปันผลอย่างเดียว
                if (ldc_div_flag == 1 && ldc_avg_flag == 0)
                {
                    if (ldc_sdiv_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate เริ่มต้นปันผล");
                    }
                    else if (ldc_ediv_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate สิ้นสุดปันผล");
                    }
                    else if (ldc_divstep_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Step ปันผล");
                    }
                    else
                    {
                        Panel1.Visible = false;
                        Panel3.Visible = false;
                        Panel2.Visible = false;
                        Panel4.Visible = true;
                    }
                }
                //กรณีเลือกเฉลี่ยคืนอย่างเดียว
                else if (ldc_avg_flag == 1 && ldc_div_flag == 0)
                {
                    sql = @"select avgtype_code from yrcfconstant";
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        avgtype_code = dt.GetString("avgtype_code");
                        if (avgtype_code == "TYP")
                        {
                            Panel1.Visible = false;
                            Panel3.Visible = false;
                            Panel2.Visible = true;
                            Panel4.Visible = true;
                                                        
                            // set ค่า rate ตามที่ตั้งไว้ข้างหน้า
                            decimal avg_rate = Dw_option.GetItemDecimal(1, "savg_rate");
                            if (avg_rate == 0)
                            {

                            }
                            else
                            {
                                int dw_loan = Dw_loan.RowCount;
                                for (int i = 1; i <= dw_loan; i++)
                                {
                                    decimal calavg_flag = Dw_loan.GetItemDecimal(i, "calavg_flag");
                                    if (calavg_flag == 1)
                                    {
                                        Dw_loan.SetItemDecimal(i, "avgpercent_rate", avg_rate);
                                    }
                                }
                                
                            }
                          }
                        else
                        {
                            if (ldc_savg_rate == 0)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate เริ่มต้นเฉลี่ยคืน");
                            }
                            else if (ldc_eavg_rate == 0)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate สิ้นสุดเฉลี่ยคืน");
                            }
                            else if (ldc_avgstep_rate == 0)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Step เฉลี่ยคืน");
                            }
                            else
                            {
                                Panel1.Visible = false;
                                Panel3.Visible = false;
                                Panel2.Visible = true;
                                Panel4.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        sqlca.Rollback();
                    }



                }
                // กรณีเลือกทั้งสอง
                else
                {

                    if (ldc_sdiv_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate เริ่มต้นปันผล");
                    }
                    else if (ldc_ediv_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate สิ้นสุดปันผล");
                    }
                    else if (ldc_divstep_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Step ปันผล");
                    }

                    sql = @"select avgtype_code from yrcfconstant";
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        avgtype_code = dt.GetString("avgtype_code");
                        if (avgtype_code == "TYP")
                        {
                            Panel1.Visible = false;
                            Panel3.Visible = false;
                            Panel2.Visible = true;
                            Panel4.Visible = true;

                            // set ค่า rate ตามที่ตั้งไว้ข้างหน้า
                            decimal avg_rate = Dw_option.GetItemDecimal(1, "savg_rate");
                            if (avg_rate == 0)
                            {

                            }
                            else
                            {
                                int dw_loan = Dw_loan.RowCount;
                                for (int i = 1; i <= dw_loan; i++)
                                {
                                    decimal calavg_flag = Dw_loan.GetItemDecimal(i, "calavg_flag");
                                    if (calavg_flag == 1)
                                    {
                                        Dw_loan.SetItemDecimal(i, "avgpercent_rate", avg_rate);
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (ldc_savg_rate == 0)
                            {

                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate เริ่มต้นเฉลี่ยคืน");
                            }
                            else if (ldc_eavg_rate == 0)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate สิ้นสุดเฉลี่ยคืน");
                            }
                            else if (ldc_avgstep_rate == 0)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Step เฉลี่ยคืน");
                            }
                            else
                            {
                                Panel1.Visible = false;
                                Panel3.Visible = false;
                                Panel2.Visible = true;
                                Panel4.Visible = true;

                                // set ค่า rate ตามที่ตั้งไว้ข้างหน้า
                                decimal avg_rate = Dw_option.GetItemDecimal(1, "savg_rate");
                                if (avg_rate == 0)
                                {

                                }
                                else
                                {
                                    int dw_loan = Dw_loan.RowCount;
                                    for (int i = 1; i <= dw_loan; i++)
                                    {
                                        decimal calavg_flag = Dw_loan.GetItemDecimal(i, "calavg_flag");
                                        if (calavg_flag == 1)
                                        {
                                            Dw_loan.SetItemDecimal(i, "avgpercent_rate", avg_rate);
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        sqlca.Rollback();
                    }
                }
            }
        }

        //ปุ่มย้อนกลับ
        protected void B_previous_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel3.Visible = true;
            Panel2.Visible = false;
            Panel4.Visible = false;
        }

        protected void B_Print_Click(object sender, EventArgs e)
        {
            try
            {
               RunProcessDetail();
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }

            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }


        protected void B_report_Click(object sender, EventArgs e)
        {
            try
            {
                String div_year = Dw_option.GetItemString(1, "div_year");
                DwUtil.RetrieveDataWindow(Dw_report, pbl, null, state.SsCoopId, div_year);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JsGetYear()
        {
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    Dw_option.SetItemString(1, "div_year", Convert.ToString(account_year));
                    JspostSetAccDate();
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                account_year = DateTime.Now.Year;
                account_year = account_year + 543;
                Dw_option.SetItemString(1, "div_year", account_year.ToString());
            }
        }

        private void RunProcessDetail()
        {
            String print_id = "DIV_ESTIMATE03"; //"d_reqdept_fixed"
            String branch_id = state.SsCoopId;
            String div_year = Dw_option.GetItemString(1, "div_year");

            app = state.SsApplication;
            gid = "DIV_ESTIMATE";
            rid = print_id;

            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(branch_id, ArgumentType.String);
            lnv_helper.AddArgument(div_year, ArgumentType.String);

            //****************************************************************
            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();
            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //ReportClient lws_report = wcf.Report;
                ////String criteriaXML = lnv_helper.PopArgumentsXML();
                ////this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                //// String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
        }

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        private void JspostSetAvgpercent()
        {
            try
            {
                int rowcurrent = int.Parse(Hd_row.Value);
                decimal calavg_flag = Dw_loan.GetItemDecimal(rowcurrent, "calavg_flag");
                if (calavg_flag == 0)
                {
                    Dw_loan.SetItemDecimal(rowcurrent, "avgpercent_rate", 0);
                }
                else
                {
                    decimal avg_rate = Dw_option.GetItemDecimal(1, "savg_rate");
                    Dw_loan.SetItemDecimal(rowcurrent, "avgpercent_rate", avg_rate);
                }

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }


    }
}