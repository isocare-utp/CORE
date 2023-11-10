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
    public partial class w_sheet_kp_opr_exptextfile : PageWebSheet, WebSheet
    {
        DataStore DStore;
        public String pbl = "kp_opr_exptextfile.pbl";
        protected String postInit;
        protected String postChangeOption;
        
        //==============================
        public void InitJsPostBack()
        {
       
            postInit = WebUtil.JsPostBack(this, "postInit");

            //===================================
           
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_detail.SetTransaction(sqlca);

            try 
            {
                if (!IsPostBack)
                {
                    JspostNewClear();
                }
                else
                {
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
        }

        public void SaveWebSheet()
        {
            try 
            {
                String ls_filename = "", ls_path = "";
                String filename = "";
                
                    DStore = new DataStore();
                    DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\keeping\kp_opr_exptextfile.pbl";
                    DStore.DataWindowObject = "d_kp_opr_linetext";

                    String ls_memberno,ls_memgroup, ls_prename,ls_name,ls_surname, ls_memname, linetext, ls_char, ls_memcard;
                    int li_row, li_count, li_running;
                    Decimal ldc_receiveamt = 0;
                    String year = Dw_main.GetItemString(1, "year");
                    String month = Dw_main.GetItemString(1, "month");
                    if (month.Length != 2)
                    {
                        month = "0" + month;
                    }

                    String recv_period = year + month;

                    ls_filename = "KPTEMP" + recv_period + ".txt";
                    ls_path = WebUtil.PhysicalPath + @"Saving\filecommon\" + ls_filename;
                    ls_char = ",";
                    //Detail
                    for (int i = 1; i <= Dw_detail.RowCount; i++)
                    {
                        try { ls_memberno = Dw_detail.GetItemString(i, "member_no").Trim(); }
                        catch { ls_memberno = ""; }

                        try { ls_memcard = Dw_detail.GetItemString(i, "card_person").Trim(); }
                        catch { ls_memcard = ""; }

                        try { ls_memgroup = Dw_detail.GetItemString(i, "membgroup_code").Trim(); }
                        catch { ls_memgroup = ""; }

                        try { ls_prename = Dw_detail.GetItemString(i, "prename_desc").Trim(); }
                        catch { ls_prename = ""; }

                        try { ls_name = Dw_detail.GetItemString(i, "memb_name").Trim(); }
                        catch { ls_name = ""; }

                        try { ls_surname = Dw_detail.GetItemString(i, "memb_surname").Trim(); }
                        catch { ls_surname = ""; }
                        ls_memname = ls_prename + ls_name + "  " + ls_surname;

                        try { ldc_receiveamt = Dw_detail.GetItemDecimal(i, "receive_amt"); }
                        catch { ldc_receiveamt = 0; }

                        li_row = DStore.InsertRow(0);
                        linetext = ls_memberno + ls_char + ls_memcard+ls_char+ls_memgroup + ls_char + ls_memname + ls_char + ldc_receiveamt;
                        DStore.SetItemString(li_row, "line_text", linetext);
                    }
                    DStore.SaveAs(ls_path, FileSaveAsType.Text, false);
                
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จแล้ว <a href=\"" + state.SsUrl + "filecommon/" + ls_filename + "\" target='_blank'>" + ls_filename + "</a>");
                JspostNewClear();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }

        
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_detail.Reset();
            string recv_period = "";
            string month = "";
            string year = "";
            try
            {
                String sql = @"select max(recv_period) from kptempreceive";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    recv_period = dt.GetString("max(recv_period)");
                    year = recv_period.Substring(0, 4);
                    month = recv_period.Substring(4, 2);
                    Dw_main.SetItemString(1, "year", year);
                    Dw_main.SetItemString(1, "month", month);

                    DwUtil.RetrieveDDDW(Dw_main, "start_memgroup", pbl, null);
                    DwUtil.RetrieveDDDW(Dw_main, "end_memgroup", pbl, null);

                    string[] minmax = ReportUtil.GetMinMaxMembgroup();//.GetMinMaxMembgroupCoopid(state.SsCoopControl);
                    Dw_main.SetItemString(1, "start_memgroup", minmax[0]);
                    Dw_main.SetItemString(1, "end_memgroup", minmax[1]);

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
                Dw_main.SetItemString(1, "year", year);
                Dw_main.SetItemString(1, "month", month);
            }
        }

        private void JspostInit()
        {
            try 
            {
                string s_memgroup = Dw_main.GetItemString(1,"start_memgroup");
                string e_memgroup = Dw_main.GetItemString(1,"end_memgroup");

                String year = Dw_main.GetItemString(1, "year");
                String month = Dw_main.GetItemString(1, "month");
                if (month.Length != 2)
                {
                    month = "0" + month;
                }

                String recv_period = year + month;
                 DwUtil.RetrieveDataWindow(Dw_detail, pbl, null, state.SsCoopId,recv_period,s_memgroup,e_memgroup);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
   
    }
}