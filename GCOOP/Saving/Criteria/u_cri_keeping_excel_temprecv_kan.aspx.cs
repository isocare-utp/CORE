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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.Web.Services.Protocols;
using Sybase.DataWindow;
using System.Data.OracleClient;
using System.IO;

namespace Saving.Criteria
{
    public partial class u_cri_keeping_excel_temprecv_kan : PageWebSheet, WebSheet
    {
        DataStore DStore;
        private n_commonClient commonService;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String postRefresh;

        private DwThDate tdw_criteria;
        public String pbl = "criteria.pbl";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
        }


        public void WebSheetLoadBegin()
        {
            try
            {
                commonService = wcf.NCommon;
            }
            catch { LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ WebService ไม่ได้"); }

            InitJsPostBack();


            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                JspostNewClear();
            }

            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "postRefresh")
            { 
            
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion
        #region Report Process

        private void RunProcess()
        {
            //ออก Report เป็น Excel
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String coop_id = dw_criteria.GetItemString(1, "coop_id");
            String year = dw_criteria.GetItemString(1, "year");
            String month = dw_criteria.GetItemString(1, "month");
            String recvperiod = year + month;
            String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup").Trim();
            String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup").Trim();

            try
            {
               ExportTemp();
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลรายงานตามเงื่อนไขที่ระบุ กรุุณาทำรายการใหม่");
                //JspostNewClear();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }


        }

        public void PopupReport()
        {
            ////เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('" + pdf + "')";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        private void JspostNewClear()
        {
            dw_criteria.Reset();
            dw_criteria.InsertRow(0);
            DwUtil.RetrieveDDDW(dw_criteria, "coop_id", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(dw_criteria, "start_membgroup_1", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(dw_criteria, "end_membgroup_1", pbl, state.SsCoopId);

            dw_criteria.SetItemString(1, "coop_id", state.SsCoopId);
            dw_criteria.SetItemString(1, "year", Convert.ToString(DateTime.Now.Year + 543));
            String month = Convert.ToString(DateTime.Now.Month);
            if (month.Length != 2)
            {
                month = "0" + month;
            }
            dw_criteria.SetItemString(1, "month", month);
            string[] minmax = ReportUtil.GetMinMaxMembgroup();
            dw_criteria.SetItemString(1, "start_membgroup", minmax[0]);
            dw_criteria.SetItemString(1, "end_membgroup", minmax[1]);
        }

        private void ExportTemp()
        {
            string choice = dw_criteria.GetItemString(1, "choice");
            String ls_sqlcode= "";
            String ls_HeaderText = "";
            DataTable dt;
            try
            {
                String coop_id = dw_criteria.GetItemString(1, "coop_id");
                String year = dw_criteria.GetItemString(1, "year");
                String month = dw_criteria.GetItemString(1, "month");
                String recvperiod = year + month;
                String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup").Trim();
                String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup").Trim();
               
                //กรณีเลือกเป็นช่วงสังกัด
                if (choice == "1")
                {
                    ls_sqlcode = "SELECT m.member_no as เลขประจำตัว,  to_number(m.card_person) as เลขบัตรประชาชน, k.membgroup_code as รหัสหน่วย," +
                                                    " nvl( mp.prename_desc , '' ) || '  ' || nvl( m.memb_name , '' ) || '   ' || nvl( m.memb_surname , '' ) as ชื่อนามสกุล," +
                                                    " k.receive_amt as จำนวนเงิน" +
                                                    " FROM mbmembmaster m,mbucfprename mp,kptempreceive k" +
                                                    " where (m.member_no = k.member_no) and" +
                                                    " (m.prename_code = mp.prename_code) and" +
                                                    " ((k.coop_id = '" + coop_id + "') and" +
                                                    " (k.recv_period = '" + recvperiod + "') and" +
                                                    " (k.membgroup_code between '" + start_membgroup + "' and '" + end_membgroup + "')) Order by k.membgroup_code,m.member_no";
                   
                }
                else
                {
                    string datamembgroup = dw_criteria.GetItemString(1,"text").Trim();
                    string[] textData = datamembgroup.Split(',');
                    String ls_membgroup = "";
                    String ls_data = "";
                    for (int i = 1; i <= textData.Length; i++)
                    {

                        ls_data = textData[i - 1];
                        ls_data = "'" + ls_data + "'";
                        //กรณีที่เป็นแถวสุดท้าย
                        if (i == textData.Length)
                        {
                            ls_membgroup = ls_membgroup + ls_data;
                        }
                        else
                        {
                            ls_membgroup = ls_membgroup + ls_data + ",";
                        }
                    }

                    //sql กรณีกรอกข้อมูล
                    ls_sqlcode = "SELECT m.member_no as เลขประจำตัว,    cast ((m.card_person ) as number) as เลขบัตรประชาชน, k.membgroup_code as รหัสหน่วย," +
                                                    " nvl( mp.prename_desc , '' ) || '  ' || nvl( m.memb_name , '' ) || '   ' || nvl( m.memb_surname , '' ) as ชื่อนามสกุล," +
                                                    " k.receive_amt as จำนวนเงิน" +
                                                    " FROM mbmembmaster m,mbucfprename mp,kptempreceive k" +
                                                    " where (m.member_no = k.member_no) and" +
                                                    " (m.prename_code = mp.prename_code) and" +
                                                    " ((k.coop_id = '" + coop_id + "') and" +
                                                    " (k.recv_period = '" + recvperiod + "') and" +
                                                    " (k.membgroup_code in (" + ls_membgroup + "))) Order by k.membgroup_code,m.member_no";
                }

                dt = WebUtil.Query(ls_sqlcode);
                
                String ls_month = "";

                //หาชื่อเดือน 
                if (month == "01")
                {
                    ls_month = "มกราคม";
                }
                else if (month == "02")
                {
                    ls_month = "กุมภาพันธ์";
                }
                else if (month == "03")
                {
                    ls_month = "มีนาคม";
                }
                else if (month == "04")
                {
                    ls_month = "เมษายน";
                }
                else if (month == "05")
                {
                    ls_month = "พฤษภาคม";
                }
                else if (month == "06")
                {
                    ls_month = "มิถุนายน";
                }
                else if (month == "07")
                {
                    ls_month = "กรกฎาคม";
                }
                else if (month == "08")
                {
                    ls_month = "สิงหาคม";
                }
                else if (month == "09")
                {
                    ls_month = "กันยายน";
                }
                else if (month == "10")
                {
                    ls_month = "ตุลาคม";
                }
                else if (month == "11")
                {
                    ls_month = "พฤศจิกายน";
                }
                else if (month == "12")
                {
                    ls_month = "ธันวาคม";
                }

                ls_HeaderText = "รายงานเรียกเก็บประจำเดือน " + ls_month + " " + year;
                EngnExpExcel(dt, ls_HeaderText);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private int EngnExpExcel(DataTable idt_data, String is_Header)
        {
            try
            {
                String year = dw_criteria.GetItemString(1, "year");
                String month = dw_criteria.GetItemString(1, "month");
                String recvperiod = year + month;

                if (idt_data.Rows.Count > 0)
                {
                    
                    Response.Clear();
                    String ls_productName = "EXP0001" + recvperiod;
                    String ls_attachment = "attachment; filename=" + ls_productName + ".xls";
                    Response.AddHeader("content-disposition", ls_attachment);
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Charset = "windows-874";
                    Response.ContentType = "application/vnd.ms-excel";




                    StringWriter ls_StringWriter = new StringWriter();
                    HtmlTextWriter ls_htmlWriter = new HtmlTextWriter(ls_StringWriter);
                    //String ls_underLine = "&nbsp;&nbsp;";
                    DataGrid ldg_dataGrid = new DataGrid();
                    ldg_dataGrid.DataSource = idt_data;
                    ldg_dataGrid.DataBind();
                    ldg_dataGrid.Caption = "Export Excel" + " " + is_Header;
                    ldg_dataGrid.RenderControl(ls_htmlWriter);
                    Response.Write(ls_StringWriter.ToString());
                    
                    Response.End();
                }
            }
            catch// (Exception ex)
            {
                return -1;
            }

            return 1;
        }
        #endregion
    }
}