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
    public partial class w_sheet_divsrv_req_chg_refrain : PageWebSheet, WebSheet
    {
        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_main;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_req_chg_frain.pbl";

        protected String postNewClear;
        protected String postInit;
        protected String postRefresh;
        protected String postInitMemberNo;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_main = new DwThDate(Dw_main, this);
            tDw_main.Add("reqchg_date", "reqchg_tdate");

            //=========================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postInitMemberNo = WebUtil.JsPostBack(this, "postInitMemberNo");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                this.ConnectSQLCA();
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_detail);

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
                case "postInit":
                    JspostInit();
                    break;
                case "postRefresh":
                    break;
                case "postInitMemberNo":
                    JspostInitMemberNo();
                    break;
            }
        }

        //function บันทึกข้อมูล
        public void SaveWebSheet()
        {
            try
            {
                DivavgService = wcf.NDivavg;
                str_divsrv_req astr_divsrv_req = new str_divsrv_req();
                astr_divsrv_req.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_req.xml_detail_refrain = Dw_detail.Describe("DataWindow.Data.XML");

                int result = DivavgService.of_save_reqchg(state.SsWsPass, ref astr_divsrv_req);
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
                    JspostNewClear();
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            // Retrieve DropDown
            //DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, null);
            //DwUtil.RetrieveDDDW(Dw_detail, "moneytype_code", pbl, null);
            ////DwUtil.RetrieveDDDW(Dw_detail, "paytype_code", pbl, null);

            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_main.Eng2ThaiAllRow();

            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
        }
        #endregion

        // function InitData
        private void JspostInit()
        {
            try
            {
                String member_no = Dw_main.GetItemString(1, "member_no");

                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_req astr_divsrv_req = new str_divsrv_req();
                astr_divsrv_req.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_req.xml_detail_refrain = Dw_detail.Describe("DataWindow.Data.XML");

                int result = DivavgService.of_init_reqchg(state.SsWsPass, ref astr_divsrv_req);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_main, Dw_main, tDw_main, FileSaveAsType.Xml);
                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_detail_refrain, Dw_detail, null, FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(Dw_detail, "reqchgtype_code", pbl, null);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        // ฟังก์ชัน Load หน้าจอแรก
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            //Dw_main.SetItemString(1, "membranch_id", state.SsCoopControl);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            Dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            Dw_main.SetItemDateTime(1, "reqchg_date", state.SsWorkDate);
            


            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();
            }
            else
            {
                Dw_main.SetItemString(1, "div_year", divyear);
            }

            //  Dw_main.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            Dw_detail.Reset();
        }

        private void JspostInitMemberNo()
        {
            try
            {
                String member_no = Hdmember_no.Value.Trim();
                Dw_main.SetItemString(1, "member_no", member_no);

                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);
                DivavgService = wcf.NDivavg;
                str_divsrv_req astr_divsrv_req = new str_divsrv_req();
                astr_divsrv_req.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divsrv_req.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");

                int result = DivavgService.of_init_reqchg(state.SsWsPass, ref astr_divsrv_req);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_main, Dw_main, tDw_main, FileSaveAsType.Xml);
                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divsrv_req.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
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
                    Dw_main.SetItemString(1, "div_year", Convert.ToString(account_year));
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
                Dw_main.SetItemString(1, "div_year", account_year.ToString());
            }
        }
    }
}