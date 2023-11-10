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
//using CoreSavingLibrary.WcfShrlon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_apv_chggrp : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        //private n_shrlonClient shrlonService;
        private DwThDate tDwOption;
        private DwThDate tDwList;
        public String pbl = "mb_apbchg_group.pbl";
        protected String postInit;
        protected String postSetStatus;
        protected String postRequestStatus;
        //===========================
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
                //shrlonService = wcf.NShrlon;
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
            }

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
            else if(eventArg == "postRequestStatus")
            {
                JspostRequestStatus();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                astr_mbreqchggrp.xml_reqlist = Dw_list.Describe("DataWindow.Data.XML");
                //int result = shrlonService.SaveApvChangeGroup(state.SsWsPass, ref astr_mbreqchggrp);
                int result = shrlonService.of_saveapv_chggrp(state.SsWsPass, ref astr_mbreqchggrp);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
                    JspostNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            Dw_option.SaveDataCache();
            Dw_list.SaveDataCache();
        }

        //=====================
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

                str_mbreqchggrp astr_mbreqchggrp = new str_mbreqchggrp();
                astr_mbreqchggrp.xml_reqopt = Dw_option.Describe("DataWindow.Data.XML");
                //short result = shrlonService.InitApvChangeGrouplist(state.SsWsPass, ref astr_mbreqchggrp);
                short result = shrlonService.of_initlist_apvchggrp(state.SsWsPass, ref astr_mbreqchggrp);
                if (result == 1)
                {
                    Dw_list.Reset();                    
                    DwUtil.ImportData(astr_mbreqchggrp.xml_reqlist, Dw_list, null, FileSaveAsType.Xml);
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
                        Dw_list.SetItemDecimal(i, "request_status", 8);
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
                        Dw_list.SetItemDecimal(i, "request_status", 1);
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
                        Dw_list.SetItemDecimal(i, "request_status", 0);
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
                Decimal request_status = Dw_list.GetItemDecimal(rowcurrent, "request_status");
                if (request_status == 1)
                {
                    Dw_list.SetItemString(rowcurrent, "apv_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "request_status", 1);
                    Dw_list.SetItemDate(rowcurrent, "apv_date", state.SsWorkDate);
                    tDwList.Eng2ThaiAllRow();
                }
                else if (request_status == 0)
                {
                    Dw_list.SetItemString(rowcurrent, "apv_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "request_status", 0);
                    Dw_list.SetItemDate(rowcurrent, "apv_date", state.SsWorkDate);
                    tDwList.Eng2ThaiAllRow();
                }
                else
                {
                    Dw_list.SetItemString(rowcurrent, "apv_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "request_status", 8);
                    Dw_list.SetItemString(rowcurrent, "apv_tdate", "00/00/0000");
                    tDwList.Thai2EngAllRow();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

    }
}