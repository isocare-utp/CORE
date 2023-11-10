using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;
using CoreSavingLibrary.WcfNShrlon;
using System.Data;
using System.IO;

namespace Saving.Applications.shrlon.ws_sl_checkpermiss_reprint_ctrl
{
    public partial class ws_sl_checkpermiss_reprint : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string Reprint { get; set; }
        [JsPostBack]
        public string PostPrintFlag { get; set; }
        static string apvlist = "";
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no);
                dsList.Retrieve(memb_no);
                dsMain.DATA[0].member_no = memb_no;
                dsMain.DATA[0].membname = dsList.DATA[0].fullname;
                dsMain.DATA[0].entry_id = dsList.DATA[0].entry_id;
            }
            else if (eventArg == Reprint)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                            apvlist = dsList.DATA[i].loanrequest_docno.Trim();
                        }
                        else
                        {
                            apvlist = apvlist + "," + dsList.DATA[i].loanrequest_docno.Trim();
                        }
                    }
                }
                Reprint_Click(apvlist);
            }
            else if (eventArg == PostPrintFlag)
            {
                Int32 row = Convert.ToInt32(hdrow.Value.ToString().Trim());
                decimal flag = dsList.DATA[row].print_flag;
                if (flag == 1)
                {
                    dsList.DATA[row].print_flag = 1;
                }
                else if (flag == 0)
                {
                    dsList.DATA[row].print_flag = 0;
                }

            }


        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        
        public void Reprint_Click(string loanrequest_docno)
        {

            try
            {
                String memberNo = dsMain.DATA[0].member_no.Trim();
                wcf.NShrlon.of_genintestimate(state.SsWsPass, memberNo, state.SsWorkDate);
                iReportArgument args = new iReportArgument();
                args.Add("lnreqdocno", iReportArgumentType.String, apvlist);
                iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้า...");
                report.AddCriteria("r_ln_print_loan_req_checkpermiss", "ใบปะหน้า...", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

    }
}