using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_setcheque_format : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        protected String reFresh;
        protected String postSave;
        protected String postExample;
        protected String postRetrieveMain;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            reFresh = WebUtil.JsPostBack(this, "reFresh");
            postSave = WebUtil.JsPostBack(this, "postSave");
            postExample = WebUtil.JsPostBack(this, "postExample");
            postRetrieveMain = WebUtil.JsPostBack(this, "postRetrieveMain");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            this.ConnectSQLCA();
            DwList.SetTransaction(sqlca);
            DwMain.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                //รายการ
                DwList.Retrieve(state.SsCoopId);

                //String listXml = fin.of_retrievelistformatchq(state.SsWsPass, state.SsCoopId);
                //if (listXml != "")
                //{
                //    DwList.ImportString(listXml, FileSaveAsType.Xml);
                //}

                DwMain.InsertRow(0);
                DwPrev.InsertRow(0);

                SetPositionBegin();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwPrev);
                this.RestoreContextDw(DwList);
            }

            DwUtil.RetrieveDDDW(DwMain, "bank_code", "setcheque_format.pbl", null);
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postSave":
                    Save();
                    break;
                case "Update":
                    Save();
                    break;
                case "postRetrieveMain":
                    RetrieveMain();
                    break;
                case "postExample":
                    SetApplyPosition();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            DwMain.UpdateData();
        }

        public void WebSheetLoadEnd()
        {
            this.DisConnectSQLCA();
            DwMain.SaveDataCache();
            DwPrev.SaveDataCache();
        }

        #endregion


        private void Save()
        {
            try
            {
                DwMain.UpdateData(true);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void RetrieveMain()
        {
            int row;
            String bankCode, branchId;
            Int16 chqType, printType;

            row = Convert.ToInt32(HdRow.Value);

            bankCode = DwList.GetItemString(row, "bank_code");
            branchId = DwList.GetItemString(row, "branch_id");
            chqType = Convert.ToInt16(DwList.GetItemDecimal(row, "cheque_type"));
            printType =  Convert.ToInt16(DwList.GetItemString(row, "printer_type"));

            //DwMain.Reset();
            //DwMain.Retrieve(bankCode, chqType, state.SsCoopId, printType);

            //String mainXml = fin.OfRetrieveFormatChq(state.SsWsPass, state.SsCoopId, bankCode, chqType, printType);
            //if (mainXml != "")
            //{
            //    DwMain.ImportString(mainXml, FileSaveAsType.Xml);
            //}

            SetApplyPosition();
        }

        protected void SetPositionBegin()
        {
            if (DwPrev.RowCount < 0)
            {
                DwPrev.InsertRow(0);
            }
           /* DwPrev.SetItemString(1, "headdate", "DD MMMM YYYY");
            DwPrev.SetItemString(1, "headname", "ชื่อต้นสมาชิก");
            DwPrev.SetItemString(1, "headsname", "ชื่อสกุลสมาชิก");
            //DwPrev.SetItemString(1,headamt				, right( "*****************" + "1,234,567,899.33" , 17)
            DwPrev.SetItemString(1, "headamt", "***" + "1,234,567,899.33" + "***");// by Phai;
            DwPrev.SetItemString(1, "remark", "หมายเหตุ");
            DwPrev.SetItemString(1, "detaildate", "D D M M Y Y Y Y");
            DwPrev.SetItemString(1, "payname", "จ่าหน้าเช็ค สั่งจ่าย");
            DwPrev.SetItemString(1, "killer", "///////////////");
            DwPrev.SetItemString(1, "moneythai", "*** หนึ่งพันสองร้อยสามสิบสี่ล้านห้าแสนหกหมื่นเจ็ดพันแปดร้อยเก้าสิบเก้าบาทสามสิบสามสตางค์ ***");
            //DwPrev.SetItemString(1,moneynumberic	, right( "*****************" + "1,234,567,899.33" , 17)
            DwPrev.SetItemString(1, "moneynumberic", "***" + "1,234,567,899.33" + "***");// by Phai
            //DwPrev.accepttext();*/
        }

        protected void SetApplyPosition()
        {
            Int16 li_x = 0, li_y = 0;

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "headdate_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "headdate_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("headdate.x = " + Convert.ToString(li_x));
            DwPrev.Modify("headdate.y = " + Convert.ToString(li_y));

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "headname_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "headname_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("headname.x = " + (li_x).ToString());
            DwPrev.Modify("headname.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "headsname_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "headsname_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("headsname.x = " + (li_x).ToString());
            DwPrev.Modify("headsname.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "headamt_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "headamt_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("headamt.x = " + (li_x).ToString());
            DwPrev.Modify("headamt.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "remark_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "remark_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("remark.x = " + (li_x.ToString()));
            DwPrev.Modify("remark.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "detaildate_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "detaildate_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("detaildate.x = " + (li_x).ToString());
            DwPrev.Modify("detaildate.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "payname_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "payname_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("payname.x = " + (li_x).ToString());
            DwPrev.Modify("payname.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "killer_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "killer_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("killer.x = " + (li_x).ToString());
            DwPrev.Modify("killer.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "moneynumberic_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "moneynumberic_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("moneynumberic.x = " + (li_x).ToString());
            DwPrev.Modify("moneynumberic.y = " + (li_y).ToString());

            try
            {
                li_x = Convert.ToInt16(DwMain.GetItemDecimal(1, "moneythai_x"));
            }
            catch { li_x = 0; }
            try
            {
                li_y = Convert.ToInt16(DwMain.GetItemDecimal(1, "moneythai_y"));
            }
            catch { li_y = 0; }
            DwPrev.Modify("moneythai.x = " + (li_x).ToString());
            DwPrev.Modify("moneythai.y = " + (li_y).ToString());


            //DwPrev.setredraw( true )
        }
    }
}
