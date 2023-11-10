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
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;
//using Saving.ConstantConfig;
using CoreSavingLibrary.WcfNDeposit;  // new deposit
using CoreSavingLibrary.WcfNCommon; // new common

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_enquiry_pborcert : PageWebSheet,WebSheet
    {
        protected String FilterBookType;
        protected String postBookGrp;
        protected String postBookNoDetail;
        protected String SearchBook;
        protected String CheckCoop;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            FilterBookType = WebUtil.JsPostBack(this, "FilterBookType");
            postBookGrp = WebUtil.JsPostBack(this, "postBookGrp");
            postBookNoDetail = WebUtil.JsPostBack(this, "postBookNoDetail");
            SearchBook = WebUtil.JsPostBack(this, "SearchBook");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwListCoop.InsertRow(0);
                dw_master.InsertRow(0);
                dw_goto.InsertRow(0);

                HfCoopid.Value = state.SsCoopId;
            }
            else 
            {
                this.RestoreContextDw(dw_master);
                this.RestoreContextDw(dw_dplist);
                this.RestoreContextDw(dw_dpdetail);
                this.RestoreContextDw(dw_goto);
                this.RestoreContextDw(DwListCoop);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "FilterBookType")
            {
                JsFilterBookType();
            }
            else if (eventArg == "postBookGrp")
            {
                JspostBookGrp();
            }
            else if (eventArg == "postBookNoDetail")
            {
                JspostBookNoDetail();
            }
            else if (eventArg == "SearchBook")
            {
                JsSearchBook();
            }
            else if (eventArg == "CheckCoop") 
            {
                checkCoop();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_master.SaveDataCache();
            dw_dplist.SaveDataCache();
            dw_dpdetail.SaveDataCache();
            dw_goto.SaveDataCache();
            DwListCoop.SaveDataCache();
        }

        #endregion

        private void JsFilterBookType()
        {
            String bookType = dw_master.GetItemString(1, "book_type");
            DataWindowChild dc = dw_master.GetChild("book_grp");
            DwUtil.RetrieveDDDW(dw_master, "book_grp", "dp_enquiry_pborcert.pbl", null);
            dc.SetFilter("book_type = '" + bookType + "'");
            dc.Filter();
        }

        private void JspostBookGrp()
        {
            string bookType = dw_master.GetItemString(1, "book_type");
            string bookGrp = dw_master.GetItemString(1, "book_grp");
            dw_master.Reset();
            dw_dplist.Reset();
            object[] Argument = new object[3] { bookType, bookGrp, state.SsCoopControl };
            DwUtil.RetrieveDataWindow(dw_master, "dp_enquiry_pborcert.pbl", null, Argument);
            Argument = new object[3] { bookType, bookGrp, HfCoopid.Value };
            DwUtil.RetrieveDataWindow(dw_dplist, "dp_enquiry_pborcert.pbl", null, Argument);
        }

        private void JspostBookNoDetail()
        {
            string bookNo = HdBookNo.Value;
            string bookType = dw_master.GetItemString(1, "book_type");
            string bookGrp = dw_master.GetItemString(1, "book_grp");
            dw_dpdetail.Reset();
            object[] Argument = new object[4] { bookType, HfCoopid.Value, bookGrp, bookNo };
            DwUtil.RetrieveDataWindow(dw_dpdetail, "dp_enquiry_pborcert.pbl", null, Argument);
            //DwUtil.RetrieveDDDW(dw_master, "reson_ofnew", "dp_slip.pbl", null);
        }

        private void JsSearchBook()
        {
            String bookNo = "";
            try
            {
                bookNo = dw_goto.GetItemString(1, "book_no");
            }
            catch
            {
                bookNo = "";
            }           
            string bookType = dw_master.GetItemString(1, "book_type");
            string bookGrp = dw_master.GetItemString(1, "book_grp");
            object[] Argument = new object[3] { bookType, bookGrp, HfCoopid.Value };
            if (bookNo != "" && bookNo != null)
            {
                dw_dpdetail.Reset();
                String bookNoR = "0000000000" + dw_goto.GetItemString(1, "book_no");
                bookNoR = WebUtil.Right(bookNoR, 10);
                dw_goto.SetItemString(1, "book_no", bookNoR);
                try
                {
                    DwUtil.RetrieveDataWindow(dw_dplist, "dp_enquiry_pborcert.pbl", null, Argument);
                    dw_dplist.SetFilter("book_no = '" + bookNoR + "'");
                    dw_dplist.Filter();
                    object[] Args = new object[4] { bookType, HfCoopid.Value, bookGrp, bookNoR };
                    DwUtil.RetrieveDataWindow(dw_dpdetail, "dp_enquiry_pborcert.pbl", null, Args);
                }
                catch(Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขที่ระบุ : " + bookNoR);
                }

            }
            else
            {
                dw_dpdetail.Reset();
                DwUtil.RetrieveDataWindow(dw_dplist, "dp_enquiry_pborcert.pbl", null, Argument);
            }
        }

        private void checkCoop()
        {

            decimal i = 0;
            decimal crossflag = DwListCoop.GetItemDecimal(1, "cross_coopflag");
            if (crossflag == 1)
            {
                try
                {
                    i = DwListCoop.GetItemDecimal(1, "cross_coopflag");
                }
                catch
                { }
                JsClear();
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopId, state.SsCoopControl);
            }
            else
            {
                try
                {
                    JsClear();
                    HfCoopid.Value = state.SsCoopId + "";
                }
                catch
                { }
            }

        }

        private void JsClear()
        {
            dw_dpdetail.Reset();
            dw_dplist.Reset();
            dw_goto.Reset();
            dw_master.Reset();

            dw_dpdetail.InsertRow(0);
            dw_dplist.InsertRow(0);
            dw_goto.InsertRow(0);
            dw_master.InsertRow(0);
        }
    }
}
