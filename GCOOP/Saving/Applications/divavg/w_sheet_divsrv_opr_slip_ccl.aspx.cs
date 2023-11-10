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
    public partial class w_sheet_divsrv_opr_slip_ccl : PageWebSheet,WebSheet
    {

        //ประกาศตัวแปร
        #region Variable
        
        private DwThDate tDw_option;
        private n_divavgClient DivavgService;
        private String pbl = "divsrv_opr_slip_ccl.pbl";
    //===========
        protected String postNewClear;
        protected String postInit;
        protected String postInitMemberNo;
     
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_option = new DwThDate(Dw_main, this);
            tDw_option.Add("operate_date", "operate_tdate");

            //=========================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postInit = WebUtil.JsPostBack(this, "postInit");
            postInitMemberNo = WebUtil.JsPostBack(this, "postInitMemberNo");
          

            DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, state.SsCoopId);
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
                this.RestoreContextDw(Dw_list);
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
                str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                astr_divavg_oper.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_list = Dw_list.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_save_slip_ccl(state.SsWsPass, ref astr_divavg_oper);
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
           

            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_option.Eng2ThaiAllRow ();

            Dw_main.SaveDataCache();
            Dw_detail.SaveDataCache();
            Dw_list.SaveDataCache();
        }
        #endregion

        // function InitData
        private void JspostInit()
        {
            try
            {
                string member_no = Dw_main.GetItemString(1, "member_no");
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                astr_divavg_oper.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_list = Dw_list.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_slip_ccl(state.SsWsPass, ref astr_divavg_oper);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_main, Dw_main, tDw_option, FileSaveAsType.Xml);

                    Dw_list.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_list, Dw_list, null, FileSaveAsType.Xml);
                    
                    
                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                 //   DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, null);
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
            Dw_detail.Reset();
            Dw_list.Reset();
            Dw_main.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_main.SetItemString(1, "entry_id", state.SsUsername);
            Dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            Dw_main.SetItemDateTime(1, "slip_date", state.SsWorkDate);

            //Dw_main.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            String divyear = Hddiv_year.Value.Trim();
            if (divyear == "" || divyear == null)
            {
                JsGetYear();
                // Dw_main.SetItemString(1, "div_year", Convert.ToString(DateTime.Now.Year + 543));
            }
            else
            {
                Dw_main.SetItemString(1, "div_year", divyear);
            }
        }

        protected void JspostInitMemberNo()
        {
            try
            {
                string member_no = Hdmember_no.Value.Trim();
                member_no = WebUtil.MemberNoFormat(member_no);
                Dw_main.SetItemString(1, "member_no", member_no);

                DivavgService = wcf.NDivavg;
                str_divsrv_oper astr_divavg_oper = new str_divsrv_oper();
                astr_divavg_oper.xml_main = Dw_main.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_detail = Dw_detail.Describe("DataWindow.Data.XML");
                astr_divavg_oper.xml_list = Dw_list.Describe("DataWindow.Data.XML");
                int result = DivavgService.of_init_slip_ccl(state.SsWsPass, ref astr_divavg_oper);
                if (result == 1)
                {
                    Dw_main.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_main, Dw_main, tDw_option, FileSaveAsType.Xml);

                    Dw_list.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_list, Dw_list, null, FileSaveAsType.Xml);

                    Dw_detail.Reset();
                    DwUtil.ImportData(astr_divavg_oper.xml_detail, Dw_detail, null, FileSaveAsType.Xml);
                 //   DwUtil.RetrieveDDDW(Dw_detail, "methpaytype_code", pbl, null);
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