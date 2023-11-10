using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_auditloan_history_ctrl
{
    public partial class ws_sl_auditloan_history : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostSearch { get; set; }
        [JsPostBack]
        public String PostDetail { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.Visible = false;
                dsDetail.Visible = false;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostSearch")
            {
                dsList.ResetRow();
                dsDetail.ResetRow();
                String search = "";


                int sdate = dsMain.DATA[0].START_DATE.Year;
                int edate = dsMain.DATA[0].END_DATE.Year;
                //1/1/2043 0:00:00
                if ((sdate > 1900) && (edate > 1900))
                {
                    search += "and convert(date,sys.entry_date) between CONVERT(DATETIME,'" + dsMain.DATA[0].START_DATE.ToString("yyyy/MM/dd", WebUtil.EN) + @"') 
                               and CONVERT(DATETIME,'" + dsMain.DATA[0].END_DATE.ToString("yyyy/MM/dd", WebUtil.EN) + "')";
                }

                if (dsMain.DATA[0].DOC_NO != "")
                {
                    search += "and lnc.member_no like '%" + dsMain.DATA[0].DOC_NO + "%' ";
                }

                if (dsMain.DATA[0].USER_ID != "")
                {
                    search += "and sys.entry_id like '%" + dsMain.DATA[0].USER_ID + "%' ";
                }

                dsList.Visible = true;
                dsList.RetrieveList(search);
            }
            else if (eventArg == "PostDetail")
            {
                dsDetail.Visible = true;
                int row = Convert.ToInt32(HdCheckRow.Value);
                String doc_no = dsList.DATA[row].MODTBDOC_NO;
                dsDetail.RetrieveDetail(doc_no);
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}