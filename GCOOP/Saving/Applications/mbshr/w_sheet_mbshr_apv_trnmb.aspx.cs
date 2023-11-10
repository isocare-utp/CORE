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
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;


namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_apv_trnmb : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        private DwThDate tDwOption;
        private DwThDate tDwList;
        public String pbl = "mb_apbchg_group.pbl";
        protected String postInit;
        protected String postSetStatus;
        protected String postRequestStatus;
        public string outputProcess;

        public void InitJsPostBack()
        {
            postInit = WebUtil.JsPostBack(this, "postInit");
            postSetStatus = WebUtil.JsPostBack(this, "postSetStatus");
            postRequestStatus = WebUtil.JsPostBack(this, "postRequestStatus");
            ///===========================
            tDwOption = new DwThDate(Dw_option, this);
            tDwOption.Add("req_sdate", "req_stdate");
            tDwOption.Add("req_edate", "req_etdate");

            tDwList = new DwThDate(Dw_list, this);
            tDwList.Add("apv_date", "apv_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
            }
            Server.ScriptTimeout = 3600;
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_option);
                this.RestoreContextDw(Dw_list);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postInit")
            {
                JspostInit();
            }
            else if (eventArg == "postSetStatus")
            {
                JspostSetStatus();
            }
            else if (eventArg == "postRequestStatus")
            {
                JspostRequestStatus();
            }
        }

        public void SaveWebSheet()
        {
            string ls_apvlist = "";
            string ls_mbnew = "", ls_mbold = "", ls_apvdate = "";

            ls_apvdate = state.SsWorkDate.ToString("yyyyMMdd");

            for (int i = 1; i <= Dw_list.RowCount; i++)
            {
                ls_mbold = Dw_list.GetItemString(i, "memold_no");
                ls_mbnew = Dw_list.GetItemString(i, "memnew_no");

                ls_apvlist += ";" + ls_mbold + "," + ls_mbnew + "," + ls_apvdate;
            }

            if (ls_apvlist != "")
            {
                ls_apvlist = ls_apvlist.Substring(1);
            }

            try
            {

                outputProcess = WebUtil.runProcessing(state, "MBTRANS", ls_apvlist, "", "");
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e);
            }

            //try
            //{
            //    str_mbreqtrnmb astr_mbreqtrnmb = new str_mbreqtrnmb();
            //    astr_mbreqtrnmb.entry_id = state.SsUsername;
            //    astr_mbreqtrnmb.xml_reqlist = Dw_list.Describe("DataWindow.Data.XML");
            //    int result = shrlonService.of_saveapv_trnmb(state.SsWsPass, ref astr_mbreqtrnmb);
            //    if (result == 1)
            //    {
            //        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            //        JspostNewClear();
            //    }
            //}
            //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_option.SaveDataCache();
            Dw_list.SaveDataCache();
        }

        private void JspostNewClear()
        {
            try
            {
                Dw_option.Reset();
                Dw_option.InsertRow(0);
                Dw_option.SetItemDate(1, "req_sdate", state.SsWorkDate);
                Dw_option.SetItemDate(1, "req_edate", state.SsWorkDate);
                tDwOption.Eng2ThaiAllRow();

                string[] minmax = ReportUtil.GetMinMaxMembno();
                Dw_option.SetItemString(1, "member_sno", minmax[0]);
                Dw_option.SetItemString(1, "member_eno", minmax[1]);

                Dw_list.Reset();
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostInit()
        {
            try
            {
                string membsno = WebUtil.MemberNoFormat(Dw_option.GetItemString(1, "member_sno"));
                Dw_option.SetItemString(1, "member_sno", membsno);
                string membeno = WebUtil.MemberNoFormat(Dw_option.GetItemString(1, "member_eno"));
                Dw_option.SetItemString(1, "member_eno", membeno);

                str_mbreqtrnmb astr_mbreqtrnmb = new str_mbreqtrnmb();
                astr_mbreqtrnmb.xml_reqopt = Dw_option.Describe("DataWindow.Data.XML");
                int result = shrlonService.of_initlist_apvtrnmb(state.SsWsPass, ref astr_mbreqtrnmb);
                if (result == 1)
                {
                    Dw_list.Reset();
                    DwUtil.ImportData(astr_mbreqtrnmb.xml_reqlist, Dw_list, null, FileSaveAsType.Xml);
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostSetStatus()
        {
            try
            {
                String btn_name = Hdbutton.Value.Trim();
                //รออนุมัติ
                if (btn_name == "b_wait")
                {
                    for (int i = 1; i <= Dw_list.RowCount; i++)
                    {
                        Dw_list.SetItemString(i, "apv_id", state.SsUsername);
                        Dw_list.SetItemString(i, "coop_id", state.SsCoopId);
                        Dw_list.SetItemDecimal(i, "trnmbreq_status", 8);
                        Dw_list.SetItemString(i, "apv_tdate", "00/00/0000");
                        tDwList.Thai2EngAllRow();
                    }
                }
                //อนุมติ
                else if (btn_name == "b_apv")
                {
                    for (int i = 1; i <= Dw_list.RowCount; i++)
                    {
                        Dw_list.SetItemString(i, "apv_id", state.SsUsername);
                        Dw_list.SetItemString(i, "coop_id", state.SsCoopId);
                        Dw_list.SetItemDecimal(i, "trnmbreq_status", 1);
                        Dw_list.SetItemDate(i, "apv_date", state.SsWorkDate);
                        tDwList.Eng2ThaiAllRow();
                    }
                }
                //ไม่อนุมัติ
                else
                {
                    for (int i = 1; i <= Dw_list.RowCount; i++)
                    {
                        Dw_list.SetItemString(i, "apv_id", state.SsUsername);
                        Dw_list.SetItemString(i, "coop_id", state.SsCoopId);
                        Dw_list.SetItemDecimal(i, "trnmbreq_status", 0);
                        Dw_list.SetItemDate(i, "apv_date", state.SsWorkDate);
                        tDwList.Eng2ThaiAllRow();
                    }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        private void JspostRequestStatus()
        {
            try
            {
                int rowcurrent = int.Parse(HdRow.Value);
                Decimal trnmbreq_status = Dw_list.GetItemDecimal(rowcurrent, "trnmbreq_status");
                if (trnmbreq_status == 1)
                {
                    Dw_list.SetItemString(rowcurrent, "apv_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "trnmbreq_status", 1);
                    Dw_list.SetItemDate(rowcurrent, "apv_date", state.SsWorkDate);
                    tDwList.Eng2ThaiAllRow();
                }
                else if (trnmbreq_status == 0)
                {
                    Dw_list.SetItemString(rowcurrent, "apv_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "trnmbreq_status", 0);
                    Dw_list.SetItemDate(rowcurrent, "apv_date", state.SsWorkDate);
                    tDwList.Eng2ThaiAllRow();
                }
                else
                {
                    Dw_list.SetItemString(rowcurrent, "apv_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "trnmbreq_status", 8);
                    Dw_list.SetItemString(rowcurrent, "apv_tdate", "00/00/0000");
                    tDwList.Thai2EngAllRow();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}