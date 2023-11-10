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

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_proc_divavg_new : PageWebSheet, WebSheet
    {

        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_option;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_proc_divavg.pbl";
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcDivavg;
        protected String postSetAccDate;
        protected String postSetAvgpercent;
        public String outputProcess;
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
            postProcDivavg = WebUtil.JsPostBack(this, "postProcDivavg");
            postSetAccDate = WebUtil.JsPostBack(this, "postSetAccDate");
            postSetAvgpercent = WebUtil.JsPostBack(this, "postSetAvgpercent");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Hd_process.Value = "false";
            if (!IsPostBack)
            {
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
                case "postProcDivavg":
                    JspostProcDivavg();
                    break;
                case "postSetAccDate":
                    JspostSetAccDate();
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
        }
        #endregion
        //=============================================

        //function การหาช่วงวันที่จากข้อมุลปีปันผล
        private void JspostSetAccDate()
        {
            String ls_divyear, ls_sacctdate, ls_eacctdate;
            int li_divyear;
            ls_divyear = Dw_option.GetItemString(1, "div_year");
            ls_divyear = ls_divyear.Substring(0, 4);
            li_divyear = int.Parse(ls_divyear);

            try
            {
                String sql = @"select accstart_date,accend_date from cmaccountyear where account_year = '" + li_divyear.ToString() + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_sacctdate = dt.GetString("accstart_date");
                    ls_eacctdate = dt.GetString("accend_date");
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

        //function การหาปีปันผล จากตารางปีบัญชี
        private void JspostSetDivyear()
        {
            try
            {
                Decimal li_divyear = 0;
                String sql = @"select min(account_year) as account_year from accaccountyear where close_account_stat = 0";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    li_divyear = dt.GetDecimal("account_year");
                    li_divyear = li_divyear + 543;
                    //li_divyear = int.Parse(dt.GetString("min(account_year)")) + 543;
                    Dw_option.SetItemString(1, "div_year", Convert.ToString(li_divyear));
                    JspostSetAccDate();
                }
                else
                {
                    Dw_option.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
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
            Dw_option.SetItemDateTime(1, "slip_date", state.SsWorkDate);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            Dw_loan.Reset();
            DwUtil.RetrieveDataWindow(Dw_loan, pbl, null, state.SsCoopId, "R");

            JsGetYear();

            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
        }

        // function ประมวลผลปันผลเฉลี่ยคืน
        private void JspostProcDivavg()
        {
            try
            {
                //DivavgService = wcf.NDivavg;
                //str_divsrv_proc astr_divsrv_proc = new str_divsrv_proc();
                //astr_divsrv_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //astr_divsrv_proc.xml_option_lntype = Dw_loan.Describe("DataWindow.Data.XML");
                //int result = DivavgService.of_prc_divavg_opt(state.SsWsPass, ref astr_divsrv_proc);
                //if (result == 1)
                //{
                //    LtServerMessage.Text = WebUtil.CompleteMessage("ประมวลผลปันผล เฉลี่ยคืน เสร็จเรียบร้อยแล้ว");
                //    JspostNewClear();
                //}

                //DivavgService = wcf.NDivavg;
                //str_divsrv_proc astr_divsrv_proc = new str_divsrv_proc();
                //astr_divsrv_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //astr_divsrv_proc.xml_option_lntype = Dw_loan.Describe("DataWindow.Data.XML");
                //DivavgService.RunDivavgProc(state.SsWsPass, ref astr_divsrv_proc, state.SsApplication, state.CurrentPage);
                //Hd_process.Value = "true";
                string xml_option = Dw_option.Describe("DataWindow.Data.XML");
                string xml_option_lntype = Dw_loan.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "DIVPROCESSING", xml_option, xml_option_lntype, "");

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
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

            Decimal ldc_div_rate = Dw_option.GetItemDecimal(1, "div_rate");
            Decimal ldc_avg_rate = Dw_option.GetItemDecimal(1, "avg_rate");


            DwUtil.RetrieveDataWindow(Dw_loan, pbl, null, state.SsCoopId, "R");
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
                    if (ldc_div_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate ปันผล");
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
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate เฉลี่ยคืน");
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
                    if (ldc_div_rate == 0)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate ปันผล");
                    }
                    else if (ldc_avg_rate == 0)
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
                            }
                            else
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกข้อมูล Rate เฉลี่ยคืน");
                            }

                        }
                        else
                        {
                            sqlca.Rollback();
                        }
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
            //Decimal ldc_divrate = Dw_option.GetItemDecimal(1, "div_rate");
            //Decimal ldc_avgrate = Dw_option.GetItemDecimal(1, "avg_rate");
            ////ตรวจสอบกรณี rate ปันผลเฉลี่ยคืนมีค่าเป็น 0
            //if (ldc_divrate == 0 && ldc_avgrate == 0)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกอัตราปันผล/เฉลี่ยคืน");
            //}
            //else
            //{
            //    Panel1.Visible = false;
            //    Panel3.Visible = false;
            //    Panel2.Visible = true;
            //    Panel4.Visible = true;
            //}

        }

        //ปุ่มย้อนกลับ
        protected void B_previous_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel3.Visible = true;
            Panel2.Visible = false;
            Panel4.Visible = false;
        }


        private void JsGetYear()
        {
            Decimal account_year = 0;
            try
            {
                String sql = @"select max(current_year) as current_year  from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    //account_year = int.Parse(dt.GetString("max(current_year)"));
                    account_year = dt.GetDecimal("current_year");
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

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }


    }
}