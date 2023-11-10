using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl
{
    public partial class ws_sl_sharetype_detail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
        [JsPostBack]
        public String PostMembtype { get; set; }
        [JsPostBack]
        public String RetrieveShare { get; set; }
        [JsPostBack]
        public String RetrieveDetail { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
            dsMthrate.InitDsMthrate(this);
            dsMembtype.InitDsMembtype(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.Retrieve();
                dsDetail.Retrieve(dsMain.DATA[0].SHARETYPE_CODE);
                dsMthrate.Retrieve(dsMembtype.DATA[0].membtype, dsMain.DATA[0].SHARETYPE_CODE);
                dsDetail.DATA[0].ADJSALARYCHGSHRPERIOD_FLAG = 1;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostDeleteRow")
            {
                int r = dsMthrate.GetRowFocus();
                dsMthrate.DeleteRow(r);
            }
            else if (eventArg == "PostInsertRow")
            {
                dsMthrate.InsertLastRow();

                int r = dsMthrate.RowCount - 1;
                dsMthrate.DATA[r].COOP_ID = state.SsCoopControl;
                dsMthrate.DATA[r].SHARETYPE_CODE = dsMain.DATA[0].SHARETYPE_CODE;
                dsMthrate.DATA[r].ENTRY_ID = state.SsUsername;
                dsMthrate.DATA[r].ENTRY_DATE = state.SsWorkDate;
                dsMthrate.DATA[r].MEMBER_TYPE = Convert.ToDecimal(dsMembtype.DATA[0].membtype);

                dsMthrate.FindTextBox(r, "start_salary").Focus();
            }
            else if (eventArg == "PostMembtype")
            {
                dsMthrate.Retrieve(dsMembtype.DATA[0].membtype, dsMain.DATA[0].SHARETYPE_CODE);
            }
            else if (eventArg == "RetrieveDetail")
            {
                string shrtype_code = dsMain.DATA[0].SHARETYPE_CODE;
                dsDetail.Retrieve(shrtype_code);
            }
            else if (eventArg == "RetrieveShare")
            {
                dsMain.Retrieve();
                dsDetail.Retrieve(dsMain.DATA[0].SHARETYPE_CODE);
                dsMthrate.Retrieve(dsMembtype.DATA[0].membtype, dsMain.DATA[0].SHARETYPE_CODE);
            }
        }       
        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddFormView(dsMain, ExecuteType.Update);
                exe.AddFormView(dsDetail, ExecuteType.Update);
                exe.AddRepeater(dsMthrate);
                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");


            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ " + ex); }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}