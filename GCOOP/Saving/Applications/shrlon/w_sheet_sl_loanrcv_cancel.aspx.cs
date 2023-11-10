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
using CoreSavingLibrary.WcfNShrlon;
using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loanrcv_cancel : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostLnrcvList;
        protected String newClear;
        DateTime cancel_date;
        //register event สำหรับการใช้งานในหน้าจ
        public void InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            jsPostLnrcvList = WebUtil.JsPostBack(this, "jsPostLnrcvList");
            tdwhead = new DwThDate(dw_head, this);
            tdwhead.Add("slip_date", "slip_tdate");
            tdwhead.Add("operate_date ", "operate_tdate ");
            newClear = WebUtil.JsPostBack(this, "newClear");
        }

        //method แรกเมื่อ sheet ดังกล่าวถูกเปิดขึ้น
        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (IsPostBack)
            {
                HdIsPostBack.Value = "false";
                try
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_head.RestoreContext();
                    this.RestoreContextDw(dw_detail);

                }
                catch { }

            }
            else { HdIsPostBack.Value = "true"; }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_list.InsertRow(0);
                dw_head.InsertRow(0);
                dw_detail.InsertRow(0);

                //DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_slipall.pbl", null);
            }

        }

        //เป็นฟังก์ชันไว้สำหรับตรวจสอบ event ที่มีการ register ไว้ กรณีมีการเรียกใช้งาน event นั้นๆ
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "jsPostLnrcvList")
            {
                JsPostLnrcvList();
                checkclsday();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
        }

        //เป็น method สำหรับการบันทึกข้อมูลของ sheet นั้นๆ 
        public void SaveWebSheet()
        {
            try
            {
                DateTime dt_canceldate = dw_head.GetItemDate(1, "cancel_date");
                //String dwhead_XML = dw_head.Describe("DataWindow.Data.XML");
                //String dwdetail_XML = dw_detail.Describe("DataWindow.Data.XML");
                //String slip_no = dw_head.GetItemString(1, "payoutslip_no");
                //String cancel_id = dw_head.GetItemString(1, "entry_id");
                //tdwhead.Eng2ThaiAllRow();
                //DateTime cancel_date = state.SsWorkDate;
                dw_head.SetItemString(1, "entry_id", state.SsUsername);
                str_slipcancel slipcancle = new str_slipcancel();

                slipcancle.xml_sliphead = dw_head.Describe("DataWindow.Data.XML");
                slipcancle.xml_slipdetail = dw_detail.Describe("DataWindow.Data.XML");
                slipcancle.slip_no = dw_head.GetItemString(1, "payoutslip_no");
                slipcancle.cancel_id = state.SsUsername;
                if (dw_head.GetItemDate(1, "slip_date") > dt_canceldate)
                {
                    slipcancle.cancel_date = dw_head.GetItemDate(1, "slip_date");
                }
                else
                {
                    slipcancle.cancel_date = dt_canceldate;
                }
                slipcancle.operateccl_date = dt_canceldate;
                slipcancle.slipcoop_id = state.SsCoopId;
                slipcancle.memcoop_id = state.SsCoopId;
                slipcancle.cancel_coopid = state.SsCoopId;

                int result = shrlonService.of_saveccl_lnrcv(state.SsWsPass,ref slipcancle);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    ///สอ.ครูเพรชบูรณ์ให้ทำการยกเลิกเก็บกองทุน
                    if (state.SsCoopControl == "020001")
                    {
                        PostCancelFund(dw_main.GetItemString(1, "member_no").Trim());
                    }
                    JsNewClear();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        //เป็น method สุดท้ายของ web sheet นี้
        public void WebSheetLoadEnd()
        {
            //DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_slipall.pbl", null);
            if (dw_head.RowCount > 1)
            {
                dw_head.DeleteRow(dw_head.RowCount);
            }
            if (dw_main.RowCount > 1)
            {
                dw_main.DeleteRow(dw_main.RowCount);
            }
            dw_detail.SaveDataCache();
            dw_head.SaveDataCache();
        }

        private void JsPostMember()
        {
            try
            {
                dw_list.Reset();
                dw_head.Reset();
                dw_head.InsertRow(0);
                dw_detail.Reset();

                String member_no = WebUtil.MemberNoFormat(Hfmember_no.Value);
                DwUtil.RetrieveDataWindow(dw_main, "sl_slipall.pbl", null, state.SsCoopControl, member_no);

                if (dw_main.RowCount < 1)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิก กรุณาตรวจสอบใหม่");
                }
                else
                {
                    try
                    {
                        DwUtil.RetrieveDataWindow(dw_list, "sl_slipall.pbl", null, state.SsCoopControl, member_no);

                        if (dw_list.RowCount < 1)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JsPostLnrcvList()
        {
            try
            {
                //String payoutslip_no = dw_list.GetItemString(1, "payoutslip_no");
                String payoutslip_no = HfSlipNo.Value;
                String coop_id = state.SsCoopControl;
                String slipclear_no = "";

                String sql = "select slipclear_no from slslippayout where payoutslip_no = '" + payoutslip_no + "'";
                sql = WebUtil.SQLFormat(sql, payoutslip_no);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    slipclear_no = dt.GetString("slipclear_no");
                }

                dw_head.Reset();
                dw_detail.Reset();
                
                DwUtil.RetrieveDataWindow(dw_head, "sl_slipall.pbl", null, coop_id, payoutslip_no);
                DwUtil.RetrieveDataWindow(dw_detail, "sl_slipall.pbl", null, coop_id, slipclear_no);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        //เช็คปิดวัน
        public void checkclsday()
        {
            CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            if (state.SsCloseDayStatus == 1)
            {
                try
                {
                    DateTime adtm_nextworkdate = new DateTime();
                    int result = wcf.NCommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref adtm_nextworkdate);
                    if (result == 1)
                    {
                        this.SetOnLoadedScript("alert('ระบบได้ทำการปิดวันไปแล้ว เปลี่ยนวันใบเสร็จเป็น " + adtm_nextworkdate.ToString("dd/MM/yyyy", th) + " ')");
                        dw_head.SetItemDate(1, "cancel_date", adtm_nextworkdate);
                    }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            }
            else
            {
                cancel_date = state.SsWorkDate;
                dw_head.SetItemDate(1, "cancel_date", cancel_date);
            }
        }
   
        private void JsNewClear()
        {
            dw_main.Reset();
            dw_list.Reset();
            dw_head.Reset();
            dw_detail.Reset();
            dw_main.InsertRow(0);
            dw_list.InsertRow(0);
            dw_head.InsertRow(0);
            dw_detail.InsertRow(0);
        }

        private void PostCancelFund( string memberNo)
        {
            decimal seq_no = 0, fundbalance = 0, itempay_amt = 0;
            string payoutslip_no = "", loancontract_no = "";
            try
            {
                for (int i = 1; i < dw_list.RowCount; i++)
                {
                    if (dw_list.GetItemDecimal(i, "operate_flag") == 1)
                    {
                        payoutslip_no = dw_list.GetItemString(1, "payoutslip_no");
                        loancontract_no = dw_list.GetItemString(1, "loancontract_no").Trim();
                    }
                }
                
                seq_no = GetSeqFundstate(state.SsCoopControl, memberNo);
                itempay_amt = GetFundPay(memberNo, loancontract_no);
                fundbalance = GetFundBalance(memberNo);
                fundbalance -= itempay_amt;

                //insert fundcollstatement
                String sqlInsertfundstateInt = @"INSERT INTO FUNDCOLLSTATEMENT (FUNDMEMBER_NO, COOP_ID, SEQ_NO, ITEMTYPE_CODE, OPERATE_DATE, 
                        REF_DOCNO, ITEMPAY_AMT, FUNDBALANCE, ENTRY_ID, ENTRY_DATE, INT_RATE, INT_AMT, 
                        INT_ACCUM, PRNTOPB_STATUS, PAGE_PB, PAGE_CARD) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8},{9},{10},{11},
                        {12},{13},{14},{15})";
                sqlInsertfundstateInt = WebUtil.SQLFormat(sqlInsertfundstateInt, memberNo, state.SsCoopControl, seq_no, "RPX", state.SsWorkDate,
                        payoutslip_no, itempay_amt, fundbalance, state.SsUsername, DateTime.Now, 0, 0,
                        0, 0, 0, 0);
                Sdt dt2 = WebUtil.QuerySdt(sqlInsertfundstateInt);

                //update fundcollmaster
                String sqlUpfundmast = @"update fundcollmaster set fundbalance = {2}, last_stm_no = {4}, fund_status = {5}, lastaccess_date = {6}
                        where coop_id = {0} and fundmember_no = {1}";
                sqlUpfundmast = WebUtil.SQLFormat(sqlUpfundmast, state.SsCoopControl, memberNo, fundbalance, seq_no, -1, state.SsWorkDate);
                Sdt dt3 = WebUtil.QuerySdt(sqlUpfundmast);

                //update dpdepttran
                decimal tranStatus = CheckTranStatus(memberNo, loancontract_no);
                if (tranStatus == 0)
                {
                    String sqlUpdepttran = @"update dpdepttran set tran_status = -9 where coop_id = {0} and system_code = 'LON'
                        and member_no = {1} and lncont_no = {2}";
                    sqlUpfundmast = WebUtil.SQLFormat(sqlUpdepttran, state.SsCoopControl, memberNo, loancontract_no);
                    Sdt dt4 = WebUtil.QuerySdt(sqlUpdepttran);
                }
                else if(tranStatus == 1)
                {
                    this.SetOnLoadedScript("alert('รายการสัญญาเลขที่ "+ loancontract_no +" ได้มีการผ่านรายการไปแล้ว ให้ทำการยกเลิกที่ระบบเงินฝาก')");
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.WarningMessage(ex.Message); }
        }

        private decimal GetSeqFundstate(string coop_id, string fundmembno)
        {
            decimal maxseq_no = 0;
            String sqlgetmaxseq = @"select last_stm_no from fundcollmaster where coop_id = {0} and fundmember_no = {1}";
            sqlgetmaxseq = WebUtil.SQLFormat(sqlgetmaxseq, coop_id, fundmembno);
            Sdt ta = WebUtil.QuerySdt(sqlgetmaxseq);
            if (ta.Next())
            {
                maxseq_no = ta.GetDecimal("last_stm_no");
            }
            maxseq_no++;
            return maxseq_no;
        }

        private decimal GetFundBalance(string fundmembno)
        {
            decimal fundbalance = 0;
            string sqlgetfundbal = @"select fundbalance from fundcollmaster where fundmember_no = {0}";
            sqlgetfundbal = WebUtil.SQLFormat(sqlgetfundbal, fundmembno);
            Sdt ta = WebUtil.QuerySdt(sqlgetfundbal);
            if (ta.Next())
            {
                fundbalance = ta.GetDecimal("fundbalance");
            }
            return fundbalance;
        }

        private decimal GetFundPay(string fundmembno, string loancontractNo)
        {
            decimal itempay_amt = 0;
            string sqlgetfundpay = @"select itempay_amt from fundcollstatement where fundmember_no = {0} 
                    and itemtype_code = 'FPX' and trim(loancontract_no) = {1}";
            sqlgetfundpay = WebUtil.SQLFormat(sqlgetfundpay, fundmembno, loancontractNo);
            Sdt ta = WebUtil.QuerySdt(sqlgetfundpay);
            if (ta.Next())
            {
                itempay_amt = ta.GetDecimal("itempay_amt");
            }
            return itempay_amt;
        }

        private decimal CheckTranStatus(string memberNo, string loancontractNo)
        {
            decimal tran_status = 0;
            string sqlgettranstatus = @"select tran_status from dpdepttran where coop_id = {0} and system_code = 'LON'
                        and member_no = {1} and lncont_no = {2}";
            sqlgettranstatus = WebUtil.SQLFormat(sqlgettranstatus, state.SsCoopControl, memberNo, loancontractNo);
            Sdt ta = WebUtil.QuerySdt(sqlgettranstatus);
            if (ta.Next())
            {
                tran_status = ta.GetDecimal("tran_status");
            }
            return tran_status;
        }
    }
}
