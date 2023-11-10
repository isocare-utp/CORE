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
using Sybase.DataWindow;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loanrcv_order_cancel_day : PageWebSheet, WebSheet
    {
        private DwThDate tdwhead;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostLnrcvList;
        protected String newClear;

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

                try
                {
                    dw_main.RestoreContext();
                    dw_list.RestoreContext();
                    dw_head.RestoreContext();
                    this.RestoreContextDw(dw_detail);
                }
                catch { }

            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(0);
                dw_list.InsertRow(0);
                dw_head.InsertRow(0);
                dw_detail.InsertRow(0);

                DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);
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

               //บันทึก datawindow ด้าน list
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        //เป็น method สุดท้ายของ web sheet นี้
        public void WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);
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

        //
        private void JsPostMember()
        {
            try
            {
               //อ่านข้อมูลมาแสดง dw_list retreive ตรงเลย



            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
          

        }

        //
        private void JsPostLnrcvList()
        {
            try
            {
                //เลือกรายการด้านซ้าย  ให้แสดงรายละเอียด้านขวา

                
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }

        //JS-EVENT
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

            DwUtil.RetrieveDDDW(dw_head, "moneytype_code", "sl_loanrcv_cancel.pbl", null);
        }

    }
}
