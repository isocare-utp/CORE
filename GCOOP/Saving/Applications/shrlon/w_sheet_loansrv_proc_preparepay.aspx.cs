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
using CoreSavingLibrary.WcfNShrlon;
using System.Web.Services.Protocols;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_loansrv_proc_preparepay : PageWebSheet, WebSheet
    {

        //ประกาศตัวแปร
        #region Variable
        private DwThDate tDw_option;
        private n_shrlonClient ShrlonService;
        private String pbl = "sl_loansrv_proc_preparepay.pbl";
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcPreparepay;
        public string outputProcess;
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //ประกาศฟังก์ชันการใช้วันที่
            tDw_option = new DwThDate(Dw_option, this);
            tDw_option.Add("prepare_date", "prepare_tdate");
            tDw_option.Add("calintto_date", "calintto_tdate");
            //=========================================

            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProcPreparepay = WebUtil.JsPostBack(this, "postProcPreparepay");
        }

        public void WebSheetLoadBegin()
        {
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
                case "postProcPreparepay":
                    JspostProcPreparepay();
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

         
            Dw_option.SaveDataCache();
            Dw_loan.SaveDataCache();
        }
        #endregion
        //=============================================

      
        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemDateTime(1, "prepare_date", state.SsWorkDate);
            Dw_option.SetItemDateTime(1, "calintto_date", state.SsWorkDate);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            // แปลงค่าวันที่จาก Eng เป็น Thai          
            tDw_option.Eng2ThaiAllRow();
            Dw_loan.Reset();
            DwUtil.RetrieveDataWindow(Dw_loan, pbl, null, state.SsCoopId);

            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
        }

        // function ประมวลผลชำระเงินกู้ล่วงหน้า
        private void JspostProcPreparepay()
        {
            try
            {
                //ShrlonService = wcf.NShrlon;
                //str_proclnprepare astr_proclnprepare = new str_proclnprepare();
                //astr_proclnprepare.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //astr_proclnprepare.xml_lntype  = Dw_loan.Describe("DataWindow.Data.XML");
                //ShrlonService.RunProcPreparepay(state.SsWsPass, ref astr_proclnprepare, state.SsApplication, state.CurrentPage);//RunProcPreparepay
                //Hd_process.Value = "true";
                string xml_option = Dw_option.Describe("DataWindow.Data.XML");
                string xml_lntype = Dw_loan.Describe("DataWindow.Data.XML");
                outputProcess = WebUtil.runProcessing(state, "LNPAYPREPARE", xml_option, xml_lntype, "");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
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
            String caltype_code, preparetype_code = "";
            try
            {
                caltype_code = Dw_option.GetItemString(1, "caltype_code").Trim();
            }
            catch { caltype_code = ""; }

            try
            {
                preparetype_code = Dw_option.GetItemString(1, "preparetype_code").Trim();
            }
            catch { preparetype_code = ""; }
         
           
            if (caltype_code == "" || caltype_code == null)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกวิธีคำนวณ");
            }
            else if (preparetype_code == "" || preparetype_code == null)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกระบบที่รับชำระ");
            }
            else
            {
                Panel1.Visible = false;
                Panel3.Visible = false;
                Panel2.Visible = true;
                Panel4.Visible = true;
            }
        }

        //ปุ่มย้อนกลับ
        protected void B_previous_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel3.Visible = true;
            Panel2.Visible = false;
            Panel4.Visible = false;
        }
    }
}