using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.ws_fin_reprintchq_ctrl
{
    public partial class ws_fin_reprintchq : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostInitchqno { get; set; }
        [JsPostBack]
        public string PostGetBank { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
                dsMain.DATA[0].ai_prndate = "1";
                dsMain.DATA[0].ai_killer = "1";
                dsMain.DATA[0].ai_payee = "0";
                RetriList();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostSearch)
            {
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
                RetriList();
            }
            else if (eventArg == PostInitchqno)
            {
                Initchqno();
                RetriList();
            }
            else if (eventArg == PostGetBank)
            {
                string bank_code = dsMain.DATA[0].bank_code;
                dsMain.Ddbank();
                dsMain.Ddbankbranch(bank_code);
                RetriList();
            }
        }

        public void SaveWebSheet()
        {
            ExecuteDataSource exe = new ExecuteDataSource(this);
            try
            {
                int row = dsList.RowCount;
                for (int i = 0; i < row; i++)
                {
                    decimal ai_flag = dsList.DATA[i].AI_FLAG;
                    if (ai_flag == 1)
                    {
                        string as_bank = dsList.DATA[i].BANK_CODE;
                        string as_bankbranch = dsList.DATA[i].BANK_BRANCH;
                        string as_chqbookno = dsList.DATA[i].CHEQUEBOOK_NO;
                        string as_chqstart_no = dsList.DATA[i].CHEQUE_NO;
                        //gen ireport
                        string report_name = "";
                        report_name = WebUtil.GetNamePrintChq(as_bank);
                        if (state.SsCoopControl != "" || state.SsCoopControl != null)
                        {
                            string report_label = "เช็ค";
                            iReportArgument args = new iReportArgument();
                            args.Add("ai_killer", iReportArgumentType.String, dsMain.DATA[0].ai_killer);
                            args.Add("ai_payee", iReportArgumentType.String, dsMain.DATA[0].ai_payee);
                            args.Add("ai_prndate", iReportArgumentType.String, dsMain.DATA[0].ai_prndate);
                            args.Add("as_bankbranch", iReportArgumentType.String, as_bankbranch);
                            args.Add("as_bankcode", iReportArgumentType.String, as_bank);
                            args.Add("as_chequebookno", iReportArgumentType.String, as_chqbookno);
                            args.Add("as_chequeno", iReportArgumentType.String, as_chqstart_no);
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                        }
                        else {
                            LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลธนาคารการออกเช็ค"); return;
                        }
                        i = row; //ไม่เช็คต่อแล้ว
                    }  
                }
                RetriList();
                LtServerMessage.Text = WebUtil.CompleteMessage("reprint cheque สำเร็จ");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถ reprint cheque ได้");
            }
        }

        public void WebSheetLoadEnd()
        {

        }
        public void Initchqno()
        {
            string chq_no = dsMain.DATA[0].chq_no;
            while (chq_no.Length < 8)
            {
                chq_no = "0" + chq_no;
            }
            dsMain.DATA[0].chq_no = chq_no;

        }
        public void RetriList()
        {
            string chq_no = dsMain.DATA[0].chq_no.Trim();
            string bank_code = dsMain.DATA[0].bank_code;
            dsMain.Ddbank();
            dsMain.Ddbankbranch(bank_code);
            string sqlwhere = "";
            if (chq_no != "" && chq_no != null)
            {
                sqlwhere += " and  FINCHQEUESTATEMENT.CHEQUE_NO ='" + dsMain.DATA[0].chq_no + "' ";
            }
            else { sqlwhere += ""; }
            if (dsMain.DATA[0].bank_code != "")
            {
                sqlwhere += " and  FINCHQEUESTATEMENT.BANK_CODE ='" + dsMain.DATA[0].bank_code + "' ";
            }
            else { sqlwhere += ""; }
            if (dsMain.DATA[0].branch_code != "")
            {
                sqlwhere += " and  FINCHQEUESTATEMENT.BANK_BRANCH ='" + dsMain.DATA[0].branch_code + "' ";
            }
            else { sqlwhere += ""; }
            
            //if ((dsMain.DATA[0].start_date.ToString("dd/MM/yyyy") != "01/01/1500") && (dsMain.DATA[0].end_date.ToString("dd/MM/yyyy") != "01/01/1500"))
            //{
            //    sqlwhere += " and FINCHQEUESTATEMENT.DATE_ONCHQ between " + dsMain.DATA[0].start_date + " and " + dsMain.DATA[0].end_date;
            //}
            dsList.RetrieveList(sqlwhere, dsMain.DATA[0].start_date, dsMain.DATA[0].end_date);
        }
    }
}