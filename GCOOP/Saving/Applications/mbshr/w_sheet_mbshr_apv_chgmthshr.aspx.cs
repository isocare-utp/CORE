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
    public partial class w_sheet_mbshr_apv_chgmthshr : PageWebSheet, WebSheet
    {
        //private ShrlonClient shrlonService;
        private n_shrlonClient shrlonService;

        private DwThDate tDwOption;
        private DwThDate tDwList;
        public String pbl = "mb_apvchgmthshr.pbl";
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
            tDwOption.Add("docreq_sdate", "docreq_stdate");
            tDwOption.Add("docreq_edate", "docreq_etdate");

            tDwList = new DwThDate(Dw_list, this);
            tDwList.Add("approve_date", "approve_tdate");
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
            else if (eventArg == "postRequestStatus")
            {
                JspostRequestStatus();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                str_mbreqchgmthpay astr_mbreqchgmthpay = new str_mbreqchgmthpay();
                astr_mbreqchgmthpay.xml_reqlist = Dw_list.Describe("DataWindow.Data.XML");
                //short result = shrlonService.PostApvlistShrPayment(state.SsWsPass, ref astr_mbreqchgmthpay);
                short result = shrlonService.of_saveapv_chgmthshr(state.SsWsPass, ref astr_mbreqchgmthpay);
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
                Dw_option.SetItemDate(1, "docreq_sdate", state.SsWorkDate);
                Dw_option.SetItemDate(1, "docreq_edate", state.SsWorkDate);
                tDwOption.Eng2ThaiAllRow();


                DwUtil.RetrieveDDDW(Dw_option, "membgroup_scode_1", pbl, state.SsCoopId);
                DwUtil.RetrieveDDDW(Dw_option, "membgroup_ecode_1", pbl, state.SsCoopId);
                string[] minmaxmemgroup = ReportUtil.GetMinMaxMembgroup();
                Dw_option.SetItemString(1, "membgroup_scode_1", minmaxmemgroup[0]);
                Dw_option.SetItemString(1, "membgroup_ecode_1", minmaxmemgroup[1]);

                string[] minmaxmember_no = ReportUtil.GetMinMaxMembno();
                Dw_option.SetItemString(1, "member_sno", minmaxmember_no[0]);
                Dw_option.SetItemString(1, "member_eno", minmaxmember_no[1]);

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

                str_mbreqchgmthpay astr_mbreqchgmthpay = new str_mbreqchgmthpay();
                astr_mbreqchgmthpay.xml_reqopt = Dw_option.Describe("DataWindow.Data.XML");
                //short result = shrlonService.InitApvlistShrPayment(state.SsWsPass, ref astr_mbreqchgmthpay);
                short result = shrlonService.of_initlist_apvchgmthshr(state.SsWsPass, ref astr_mbreqchgmthpay);

                if (result == 1)
                {
                    Dw_list.Reset();
                    DwUtil.ImportData(astr_mbreqchgmthpay.xml_reqlist, Dw_list, null, FileSaveAsType.Xml);
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
                        Dw_list.SetItemString(i, "approve_id", state.SsUsername);
                        Dw_list.SetItemString(i, "entry_id", state.SsUsername);
                        Dw_list.SetItemString(i, "coop_id", state.SsCoopId);
                        Dw_list.SetItemDecimal(i, "shrpayadj_status", 8);
                        Dw_list.SetItemString(i, "approve_tdate", "00/00/0000");
                        tDwList.Thai2EngAllRow();
                    }
                }
                //อนุมติ
                else if (btn_name == "b_apv")
                {
                    for (int i = 1; i <= Dw_list.RowCount; i++)
                    {
                        Dw_list.SetItemString(i, "approve_id", state.SsUsername);
                        Dw_list.SetItemString(i, "entry_id", state.SsUsername);
                        Dw_list.SetItemString(i, "coop_id", state.SsCoopId);
                        Dw_list.SetItemDecimal(i, "shrpayadj_status", 1);
                        Dw_list.SetItemDate(i, "approve_date", state.SsWorkDate);
                        Dw_list.SetItemDate(i, "entry_date", state.SsWorkDate);
                        tDwList.Eng2ThaiAllRow();
                    }
                }
                //ไม่อนุมัติ
                else
                {
                    for (int i = 1; i <= Dw_list.RowCount; i++)
                    {
                        Dw_list.SetItemString(i, "approve_id", state.SsUsername);
                        Dw_list.SetItemString(i, "entry_id", state.SsUsername);
                        Dw_list.SetItemString(i, "coop_id", state.SsCoopId);
                        Dw_list.SetItemDecimal(i, "shrpayadj_status", 0);
                        Dw_list.SetItemDate(i, "approve_date", state.SsWorkDate);
                        Dw_list.SetItemDate(i, "entry_date", state.SsWorkDate);
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
                Decimal request_status = Dw_list.GetItemDecimal(rowcurrent, "shrpayadj_status");
                if (request_status == 1)
                {
                    Dw_list.SetItemString(rowcurrent, "approve_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "entry_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "shrpayadj_status", 1);
                    Dw_list.SetItemDate(rowcurrent, "approve_date", state.SsWorkDate);
                    Dw_list.SetItemDate(rowcurrent, "entry_date", state.SsWorkDate);
                    tDwList.Eng2ThaiAllRow();
                }
                else if (request_status == 0)
                {
                    Dw_list.SetItemString(rowcurrent, "approve_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "entry_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "shrpayadj_status", 0);
                    Dw_list.SetItemDate(rowcurrent, "approve_date", state.SsWorkDate);
                    Dw_list.SetItemDate(rowcurrent, "entry_date", state.SsWorkDate);
                    tDwList.Eng2ThaiAllRow();
                }
                else
                {
                    Dw_list.SetItemString(rowcurrent, "approve_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "entry_id", state.SsUsername);
                    Dw_list.SetItemString(rowcurrent, "coop_id", state.SsCoopId);
                    Dw_list.SetItemDecimal(rowcurrent, "shrpayadj_status", 8);
                    Dw_list.SetItemString(rowcurrent, "approve_tdate", "00/00/0000");
                    tDwList.Thai2EngAllRow();

                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}
