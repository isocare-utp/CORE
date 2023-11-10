using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon_const.ws_sl_const_lnucfloanobjective_ctrl
{
    public partial class ws_sl_const_lnucfloanobjective : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostProbitemtype { get; set; }
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostLoanObject { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);   
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.TypeLoan();
               //dsMain.DATA[0].LOANTYPE_CODE = "11";
               //dsList.Retrieve(dsMain.DATA[0].LOANTYPE_CODE.Trim());
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertAtRow(0);
                dsList.DATA[0].COOP_ID = state.SsCoopControl;
                dsList.DATA[0].LOANTYPE_CODE = dsMain.DATA[0].LOANTYPE_CODE;
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
            }
            else if (eventArg == PostLoanObject)
            {
                dsList.Retrieve(dsMain.DATA[0].LOANTYPE_CODE.Trim());
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                int result = exe.Execute();
                dsList.Retrieve(dsMain.DATA[0].LOANTYPE_CODE);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ (" + result + ")");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            
        }
    }
}