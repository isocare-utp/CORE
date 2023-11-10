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
//using CoreSavingLibrary.WcfDivavg;
using CoreSavingLibrary.WcfNCommon;
using System.Web.Services.Protocols;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_excel : PageWebSheet,WebSheet
    {
        public String pbl = "divsrv_reportexcel.pbl";
        private n_commonClient commonService;
        private DwThDate tDw_option;
        protected String postNewClear;
        /// <summary>
        /// ============================================================
        /// </summary>

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            //==============
            DwUtil.RetrieveDDDW(Dw_option, "start_membgroup_1", pbl, state.SsCoopControl);
            DwUtil.RetrieveDDDW(Dw_option, "end_membgroup_1", pbl, state.SsCoopControl);
            DwUtil.RetrieveDDDW(Dw_option, "coop_id", pbl, null);
            //==========================
            tDw_option = new DwThDate(Dw_option, this);
            tDw_option.Add("methpay_date", "methpay_tdate");
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
                this.RestoreContextDw(Dw_option);
            }
        }


        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_choice.SaveDataCache();
            Dw_option.SaveDataCache();
        }
        //==============================
        private void JspostNewClear()
        {
            Dw_choice.Reset();
            Dw_choice.InsertRow(0);

            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            Dw_option.SetItemDate(1, "methpay_date", state.SsWorkDate);
            tDw_option.Eng2ThaiAllRow();

            string[] minmax = ReportUtil.GetMinMaxMembgroup();
            Dw_option.SetItemString(1, "start_membgroup", minmax[0]);
            Dw_option.SetItemString(1, "end_membgroup", minmax[1]);
        }

        protected void B_report_Click(object sender, EventArgs e)
        {
            String report_id =Dw_choice.GetItemString(1,"report_id");
            if(report_id!= "" || report_id != null)
            {
                String coop_id = Dw_option.GetItemString(1, "coop_id");
                String div_year = Dw_option.GetItemString(1, "div_year");
                String start_membgroup = Dw_option.GetItemString(1, "start_membgroup");
                String end_membgroup = Dw_option.GetItemString(1, "end_membgroup");
                DwUtil.RetrieveDataWindow(Dw_report, pbl, null, coop_id, div_year, start_membgroup, end_membgroup);
            }

            
        }

        protected void B_excel_Click(object sender, EventArgs e)
        {
            try 
            {
                String div_year = Dw_option.GetItemString(1, "div_year");
                String coop_id = Dw_option.GetItemString(1,"coop_id");
                String start_membgroup = Dw_option.GetItemString(1,"start_membgroup");
                String end_membgroup = Dw_option.GetItemString(1,"end_membgroup");

                commonService = wcf.NCommon;
                String filename = "ReportReceiveExcel_" + DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN) + ".xls";
                str_rptexcel astr_rptexcel = new str_rptexcel();
                astr_rptexcel.as_path = WebUtil.PhysicalPath + "Saving//filecommon//" + filename;
                astr_rptexcel.as_dwobject = "r_divsrv_tobank_excel";
                astr_rptexcel.as_argument01 = "S" + coop_id;
                astr_rptexcel.as_argument02 = "S" + div_year;
                astr_rptexcel.as_argument03 = "S" + start_membgroup;
                astr_rptexcel.as_argument03 = "S" + end_membgroup;
                //astr_rptexcel.as_argument04 = "S" + end_membgroup;
                int result = commonService.of_dwexportexcel_rpt(state.SsWsPass,ref astr_rptexcel);
                LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่<br /><a href=\"" + state.SsUrl + "filecommon/" + filename + "\" target='_blank'>" + filename + "</a>");

                //ExportExcel(Dw_report);
                //String disk_option = Dw_main.GetItemString(1, "disk_option");
                //String recv_period1 = Dw_main.GetItemString(1, "recv_period");
               //tring filename = "divavg_bank";
               
                //String file_tmp = Dw_report.Describe("DataWindow.Data");

                //Response.Clear();
                //String ls_productName = "EXP0001";
                //String ls_attachment = "attachment; filename=" + ls_productName + ".xls";
                //Response.AddHeader("content-disposition", ls_attachment);
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Charset = "windows-874";
                //Response.ContentType = "application/vnd.ms-excel";
                //StringWriter ls_StringWriter = new StringWriter();
                //HtmlTextWriter ls_htmlWriter = new HtmlTextWriter(ls_StringWriter);
                //Response.Write(ls_StringWriter.ToString());
                //LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่<br /><a href=\"" + state.SsUrl + "filecommon/" + ls_attachment + "\" target='_blank'>" + ls_attachment + "</a>");
                //Response.End();

               // Response.Clear();
                //Response.AddHeader("content-disposition", ls_attachment);
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Charset = "windows-874";
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
                //Response.Write(file_tmp);
                //LtServerMessage.Text = WebUtil.CompleteMessage("ออกรายงานในรูปแบบ Excel คุณสามารถดาวน์โหลดไฟล์ได้ที่นี่<br /><a href=\"" + state.SsUrl + "filecommon/" + filename + "\" target='_blank'>" + filename + "</a>");
                ////LtServerMessage.Text = WebUtil.CompleteMessage("Export ข้อมูลเสร็จเรียบร้อยแล้ว");
                //Response.End();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private int ExportExcel(DataTable idt_data)
        {
            try
            {
                if (idt_data.Rows.Count > 0)
                {
                    Response.Clear();
                    String ls_productName = "EXP0001";
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
                    ldg_dataGrid.Caption = "Export Excel";
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
    }
}