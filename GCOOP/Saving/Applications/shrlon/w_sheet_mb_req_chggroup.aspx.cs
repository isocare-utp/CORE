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
    public partial class w_sheet_mb_req_chggroup : PageWebSheet, WebSheet
    {
        private DwThDate tdwmain;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String jsPostMember;
        protected String jsPostGroup;
        protected String newClear;
        protected String jsRefresh;



        void WebSheet.InitJsPostBack()
        {
            jsPostMember = WebUtil.JsPostBack(this, "jsPostMember");
            newClear = WebUtil.JsPostBack(this, "newClear");
            tdwmain = new DwThDate(dw_main, this);
            tdwmain.Add("entry_date", "entry_tdate");

        }

        void WebSheet.WebSheetLoadBegin()
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
                    dw_history.RestoreContext();
                }
                catch { }
            }
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
                dw_history.InsertRow(1);

                dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
                dw_main.SetItemString(1, "entry_id", state.SsUsername);
                RetrieveDDDW(); tdwmain.Eng2ThaiAllRow();

            }
        }
        public void RetrieveDDDW()
        {
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "new_group_1", "mb_req_chggroup.pbl", null);


            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }
        void WebSheet.CheckJsPostBack(String eventArg)
        {
            if (eventArg == "jsPostMember")
            {
                JsPostMember();
            }
            else if (eventArg == "newClear")
            {
                //JsNewClear();

            }

        }

        void WebSheet.SaveWebSheet()
        {
            //String member_no = Hfmember_no.Value;
            //String as_xmlreq = dw_main.Describe("DataWindow.Data.XML");
            //String as_entryid = state.SsUsername;
            //DateTime adtm_entrydate = state.SsWorkDate;
            //tdwmain.Eng2ThaiAllRow();
            //int result = shrlonService.SaveRequestChangeGroup(state.SsWsPass, as_xmlreq, as_entryid, adtm_entrydate);
            //if (result == 1)
            //{
            //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            //    dw_main.Reset(); dw_main.InsertRow(0);
            //    RetrieveDDDW();

            //    dw_history.Reset(); dw_history.InsertRow(0);
            //}
        }

        void WebSheet.WebSheetLoadEnd()
        {
            RetrieveDDDW();
        }
        private void JsPostMember()
        {
            //try
            //{

            //    //String member_no = Hfmember_no.Value;
            //    //String as_xmlreq = dw_main.Describe("DataWindow.Data.XML");
            //    //String as_xmlhistory = dw_history.Describe("DataWindow.Data.XML");
            //    //DateTime adtm_datereq = dw_main.GetItemDate(1, "entry_date");
            //    //string memcoop_id = WebUtil.getmemcoopid(state.SsCoopId, member_no);
            //    //int result = shrlonService.InitRequestChangeGroup(state.SsWsPass, memcoop_id,state.SsCoopId, member_no, adtm_datereq, ref as_xmlreq, ref as_xmlhistory);
            //    //if (result == 1)
            //    //{
            //    //    try
            //    //    {
            //    //        dw_main.Reset();
            //    //        dw_main.ImportString(as_xmlreq, FileSaveAsType.Xml);

            //    //        dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            //    //        dw_main.SetItemString(1, "entry_id", state.SsUsername);
            //    //        dw_history.Reset();
            //    //        dw_history.ImportString(as_xmlhistory, FileSaveAsType.Xml);
            //    //        TextDwmain.Text = as_xmlreq;
            //    //        Textdwhistory.Text = as_xmlhistory;

            //    //    }
            //    //    catch
            //    //    {
            //    //        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล กรุณาตรวจสอบใหม่");
            //    //        dw_main.Reset(); dw_main.InsertRow(0);
            //    //        dw_history.Reset(); dw_history.InsertRow(0);

            //    //    }
            //    //    tdwmain.Eng2ThaiAllRow();
            //    //    RetrieveDDDW();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            //}


        }
        //JS-EVENT
        private void JsNewClear()
        {

            dw_main.Reset();
            dw_history.Reset();

            dw_main.InsertRow(1);
            dw_history.InsertRow(1);
            dw_main.SetItemDate(1, "entry_date", state.SsWorkDate);
            dw_main.SetItemString(1, "entry_id", state.SsUsername);
            tdwmain.Eng2ThaiAllRow();
        }
    }
}
