using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.IO;
using CoreSavingLibrary;
using System.Data;
using System.Windows.Forms;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit.ws_dp_upbook_ctrl
{
    public partial class ws_dp_upbook : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostDepAccount { get; set; }
        [JsPostBack]
        public string PostUpBook { get; set; }

        private n_depositClient ndept;
        string deptacc = "";

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                ndept = wcf.NDeposit;
            }
            catch
            { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostDepAccount)
            {
                deptacc = dsMain.DATA[0].DEPTACCOUNT_NO;
                dsMain.OfRetrieveDepAcc(deptacc);
                //กรณีต้องการปริ้นโดยไม่ต้องกดปุ่มพิมพ์
                //of_printpassbook();
            }
            else if (eventArg == PostUpBook)
            {
                of_printpassbook();
            }
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            dsMain.FindTextBox(0, "deptaccount_no").Enabled = true;
            dsMain.FindTextBox(0, "deptpassbook_no").ReadOnly = true;
            dsMain.FindTextBox(0, "TextBox1").ReadOnly = true;
            dsMain.FindTextBox(0, "lastrec_no_pb").ReadOnly = true;
            dsMain.FindTextBox(0, "prnpbkto").ReadOnly = true;
            dsMain.FindTextBox(0, "lastpage_no_pb").ReadOnly = true;
            dsMain.FindTextBox(0, "lastline_no_pb").ReadOnly = true;
            
        }

        private void of_printpassbook()
        {
            String as_xml_return = "", returnValue = "";
            Int16 seq = 0, page = 0, line = 0;
            Int16 ai_status = 1;
            deptacc = dsMain.DATA[0].DEPTACCOUNT_NO.Trim();
            seq = Convert.ToInt16(dsMain.DATA[0].LASTREC_NO_PB);
            page = Convert.ToInt16(dsMain.DATA[0].LASTPAGE_NO_PB);
            line = Convert.ToInt16(dsMain.DATA[0].LASTLINE_NO_PB);
            string printset = "1";

            ndept.of_print_book(state.SsWsPass, deptacc, state.SsCoopControl, seq, page, line, true, printset, ref returnValue, ai_status, ref as_xml_return);
            
            Printing.DeptPrintBook(this, as_xml_return);
        }
    }
}
