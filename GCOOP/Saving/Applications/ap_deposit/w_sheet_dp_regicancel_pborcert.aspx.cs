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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit;  // new deposit
using CoreSavingLibrary.WcfNCommon; // new common
using System.Web.Services.Protocols;
using Sybase.DataWindow;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_regicancel_pborcert : PageWebSheet, WebSheet
    {
        //private DepositClient depService;
        private n_depositClient ndept; //new
        protected String newRecord;
        protected String ItemChangedJS;
        protected String GetBeginBkNo;
        protected String FilterBookType;
        private String pbl = "dp_regicancel_pborcert.pbl";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            newRecord = WebUtil.JsPostBack(this, "newRecord");
            ItemChangedJS = WebUtil.JsPostBack(this, "ItemChangedJS");
            GetBeginBkNo = WebUtil.JsPostBack(this, "GetBeginBkNo");
            FilterBookType = WebUtil.JsPostBack(this, "FilterBookType");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            this.ConnectSQLCA();
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
            }
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain, "as_coopid", "dp_regicancel_pborcert.pbl", state.SsCoopControl);
                DwMain.SetItemString(1, "as_coopid", state.SsCoopId);
                GetNewStartBkNo();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "newRecord")
            {
                JsNewClear();
            }
            else if (eventArg == "ItemChangedJS")
            {
                JSITemchanged();
            }
            else if (eventArg == "GetBeginBkNo")
            {
                GetNewStartBkNo();
            }
            else if (eventArg == "FilterBookType")
            {
                JsFilterBookType();
            }
        }

        public void SaveWebSheet()
        {
            int result = 0;
            try
            {
                String ls_xml = DwMain.Describe("DataWindow.Data.XML");
                //result = depService.RegisBookNo(state.SsWsPass, ls_xml, HfCoopid.Value, state.SsUsername, state.SsClientIp, state.SsWorkDate);
                result = ndept.of_register_book(state.SsWsPass, ls_xml, HfCoopid.Value, state.SsUsername, state.SsClientIp, state.SsWorkDate); //new

                if (result == 1)
                {
                    try
                    {
                        JsNewClear();
                    }
                    catch { }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว...");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                String bookType = DwUtil.GetString(DwMain, 1, "as_booktype", "");
                if (!string.IsNullOrEmpty(bookType))
                {
                    DataWindowChild dc = DwMain.GetChild("as_bookgrp");
                    DwUtil.RetrieveDDDW(DwMain, "as_bookgrp", pbl, null);
                    dc.SetFilter("book_type='" + bookType + "'");
                    dc.Filter();
                }
            }
            catch { }
            DwMain.SaveDataCache();
        }

        #endregion

        private void JSITemchanged()
        {
            String as_start = "";
            long an_start = 0;
            String as_end = "";
            long an_end = 0;
            long amt = 0;
            try
            {
                as_start = DwMain.GetItemString(1, "as_pbstart").Trim();
                an_start = Convert.ToInt64(as_start);
                amt = Convert.ToInt64(DwMain.GetItemString(1, "an_amt"));
                try
                {
                    as_end = DwMain.GetItemString(1, "as_pbend").Trim();
                    an_end = Convert.ToInt64(as_end);
                }
                catch (Exception)
                {
                    an_end = 0;
                }

                if (Hcol.Value == "as_pbend" || Hcol.Value == "as_pbstart")
                {
                    //amt = (an_end - an_start) + 1;
                    an_end = (an_start + amt) - 1;
                    DwMain.SetItemString(1, "as_pbstart", GetFormat(Convert.ToInt64(as_start)));
                    an_end = (an_start + amt) - 1;
                    //if (an_end > an_start)//{                         
                    //}
                    if (an_end < an_start)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด เลขที่เริ่มต้นมากกว่าหรือเท่ากับ เลขที่สิ้นสุด");
                        throw new Exception();
                    }
                }
                else if (Hcol.Value == "an_amt")
                {
                    an_end = (an_start + amt) - 1;
                }

                if (amt > 100)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาด จำนวนในการลงทะเบียนครั้งละ ไม่เกิน 100");
                    throw new Exception();
                }
                else
                {
                    as_end = GetFormat(an_end);
                    DwMain.SetItemSqlDecimal(1, "an_amt", amt);
                    DwMain.SetItemString(1, "as_pbend", as_end);
                }
            }
            catch
            {
            }
        }

        private String GetFormat(long i)
        {
            string s = "0000000000" + Convert.ToString(i);
            s = WebUtil.Right(s, 10);
            return s;
        }

        private void JsNewClear()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            GetNewStartBkNo();
        }

        private void JsFilterBookType()
        {
            //String bookType = DwMain.GetItemString(1, "as_booktype");
            //DataWindowChild dc = DwMain.GetChild("as_bookgrp");
            //dc.SetTransaction(sqlca);
            //dc.Retrieve();
            //dc.SetFilter("book_type='" + bookType + "'");
            //dc.Filter();
        }

        private void GetNewStartBkNo()
        {
            String StartBkNo = "";
            String BookType = "";
            String BookGroup = "";
            BookType = DwUtil.GetString(DwMain, 1, "as_booktype", "");//.GetItemString(1, "as_booktype");
            BookGroup = DwUtil.GetString(DwMain, 1, "as_bookgrp", "");// DwMain.GetItemString(1, "as_bookgrp");
            try
            {
                if ((BookType == null) || (BookGroup == null) || (BookType == "") || (BookGroup == ""))
                {
                    //LtServerMessage.Text = WebUtil.ErrorMessage("กรอกข้อมูลไม่ครบ กรุณาตรวจสอบ");
                }
                else
                {
                    //StartBkNo = depService.GetNewStartBkNo(state.SsWsPass, BookType, BookGroup, HfCoopid.Value);
                    StartBkNo = ndept.of_get_new_startbook_no(state.SsWsPass, BookType, BookGroup, HfCoopid.Value); //new                  
                    DwMain.SetItemString(1, "as_pbstart", StartBkNo);
                    DwMain.SetItemString(1, "as_pbend", "");
                    DwMain.SetItemDecimal(1, "an_amt", 0);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = ex.Message;
            }
        }
    }
}
