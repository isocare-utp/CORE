using System;
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
using System.Data.OracleClient; //เพิ่มเข้ามา
using Sybase.DataWindow;//เพิ่มเข้ามา
using System.Globalization; //เพิ่มเข้ามา
using DataLibrary; //เพิ่มเข้ามา
using System.Web.Services.Protocols;
using CoreSavingLibrary; //เรียกใช้ service
using CoreSavingLibrary.WcfNAccount;

namespace Saving.Applications.account
{
    public partial class w_acc_amortized : PageWebSheet, WebSheet
    {
        protected String jsPostAssetDoc;
        protected String jsPostAmortized;
        DwThDate tSaleDate;
        string pbl = "asset.pbl";

        #region WebSheet Members
        public void InitJsPostBack()
        {
            tSaleDate = new DwThDate(DwMain, this);
            tSaleDate.Add("sale_date", "sale_tdate");
            jsPostAssetDoc = WebUtil.JsPostBack(this, "jsPostAssetDoc");
            jsPostAmortized = WebUtil.JsPostBack(this, "jsPostAmortized");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostAssetDoc":
                    RetrieveAssetDoc();
                    break;
                case "jsPostAmortized":
                    Amortized();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(DwMain, "type_series", pbl, null);
            }
            catch { }
            tSaleDate.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
        }
        #endregion

        #region Function
        private void RetrieveAssetDoc()
        {
            string asset_doc = DwMain.GetItemString(1, "asset_doc");
            DwUtil.RetrieveDataWindow(DwMain, pbl, null, asset_doc);
            if (DwMain.RowCount == 0)
            {
                DwMain.InsertRow(0);
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียนสินทรัพย์ " + asset_doc);
            }
            else
            {
                DwMain.SetItemDateTime(1, "sale_date", state.SsWorkDate);
                DwMain.SetItemDecimal(1, "delete_amount", DwMain.GetItemDecimal(1, "before_amt"));
            }
        }

        private void Amortized()
        {
            String asset_doc = DwMain.GetItemString(1, "asset_doc");
            Decimal bf_amount = DwMain.GetItemDecimal(1, "before_amt");
            Decimal delete_amount = DwMain.GetItemDecimal(1, "delete_amount");
            Decimal begin_asset = 0;
            Decimal total_amount = bf_amount - delete_amount;
            short ai_year = 0, ai_period = 0;

            int result = wcf.NAccount.of_get_year_period(state.SsWsPass, state.SsWorkDate, state.SsCoopControl, ref ai_year, ref ai_period);

            int dp_year = Convert.ToInt32(ai_year);

            String sql_chkasset = @"SELECT begin_asset FROM acc_asset_year WHERE asset_doc = '" + asset_doc + "' and account_year = '" + dp_year + "'";
            Sdt chkasset = WebUtil.QuerySdt(sql_chkasset);
            try
            {
                if (chkasset.Next())
                {
                    begin_asset = chkasset.GetDecimal("begin_asset");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลทะเบียนสินทรัพย์" + asset_doc);
                }
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลทะเบียนสินทรัพย์" + asset_doc);
            }


            if ((bf_amount - delete_amount) >= 0)
            {

                DwMain.SetItemDecimal(1, "delete_amount", delete_amount);
                DwMain.SetItemDecimal(1, "standing_balance", bf_amount - delete_amount);

                if ((bf_amount - delete_amount) <= 0)
                {
                    DwMain.SetItemDecimal(1, "type_of_caldp", 2);
                    DwMain.SetItemDecimal(1, "status", 0);
                }
                else
                {
                    DwMain.SetItemDecimal(1, "type_of_caldp", 1);
                    DwMain.SetItemDecimal(1, "status", 1);
                    begin_asset = (begin_asset / bf_amount) * delete_amount;
                }

                try
                {
                    DwUtil.UpdateDataWindow(DwMain, pbl, "ACC_DELSERIES");
                    String sql = @"Update acc_asset_year set delete_amount = '" + begin_asset + "' , total_amount = '" + total_amount + "' , standing_balance = '" + total_amount +
                                "' WHERE asset_doc ='" + asset_doc + @"' and account_year = '" + dp_year + "' and coop_id ='" + state.SsCoopId + @"'";
                    WebUtil.ExeSQL(sql);
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    LtServerMessage.Text = WebUtil.CompleteMessage("ตัดจำหน่ายสำเร็จ");
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ตัดจำหน่ายไม่สำเร็จ");
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.WarningMessage("จำนวนรายการที่ตัดจำหน่ายมีมากกว่าจำนวนคงเหลือ กรุณาตรวจสอบ");
            }
        }
        #endregion
    }
}