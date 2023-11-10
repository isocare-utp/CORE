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
using DataLibrary;
using System.Globalization;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_adjust_keepother_ins : PageWebSheet, WebSheet
    {
        string pbl = "kp_opr_receive_store_other.pbl";
        Sdt dt = new Sdt();
        protected string jsPostRetrieveIns;

        public void InitJsPostBack()
        {
            jsPostRetrieveIns = WebUtil.JsPostBack(this, "jsPostRetrieveIns");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                ResetNewPage();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostRetrieveIns":
                    PostRetrieveIns();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                string member_no = "", keepitemtype_code = "";
                decimal item_payment = 0;
                if (DwDetail.RowCount <= 0)
                { 
                    throw new Exception("ไม่พบข้อมูลที่ทำการเปลี่ยนแปลง กรุณาตรวจสอบ"); 
                }
                else
                {
                    for (int i = 1; i <= DwDetail.RowCount; i++)
                    {
                        member_no = DwDetail.GetItemString(i, "member_no");
                        keepitemtype_code = DwDetail.GetItemString(i, "keepitemtype_code");
                        item_payment = DwDetail.GetItemDecimal(i, "item_payment");

                        string sqlup = @"update kprcvkeepother set item_payment = {3} where coop_id = {0} and member_no = {1} and keepitemtype_code = {2}";
                        sqlup = WebUtil.SQLFormat(sqlup, state.SsCoopControl, member_no, keepitemtype_code, item_payment);
                        dt = WebUtil.QuerySdt(sqlup);
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเปลี่ยนแปลงยอดประกัน สำเร็จ");
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        //####################<Function>###############################//
        public void ResetNewPage()
        {
            DwMain.Reset();
            DwDetail.Reset();
            DwMain.InsertRow(0);
            DwDetail.InsertRow(0);
            DwMain.SetItemString(1, "year", state.SsWorkDate.ToString("yyyy", CultureInfo.CreateSpecificCulture("th-TH")));
            DwMain.SetItemString(1, "month", state.SsWorkDate.ToString("MM"));
        }

        private void PostRetrieveIns()
        {
            string year = "", month = "";
            year = Convert.ToString(Convert.ToDecimal(DwMain.GetItemString(1, "year")) - 543 );
            month = DwMain.GetItemString(1, "month");
            DwUtil.RetrieveDataWindow(DwDetail, pbl, null, year, month);
            if (DwDetail.RowCount <= 0)
            { LtServerMessage.Text = WebUtil.WarningMessage2("ไม่พบข้อมูลที่ทำการค้นหา"); }
        }
    }
}