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

namespace Saving.Applications.shrlon.ws_sl_approve_reprint_ctrl
{
    public partial class ws_sl_approve_reprint_ctrl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostRetrieve { get; set; }
        [JsPostBack]
        public string PrintCont { get; set; }
        [JsPostBack]
        public string PrintColl { get; set; }
        static string apvlist = "";
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DdLntype();
                this.SetOnLoadedScript(" parent.Setfocus();");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrieve)
            {
                try
                {
                    string member_no = "";
                    string approve_id = "";
                    string lntypecode = "";
                    string contno_s = "";
                    string contno_e = "";
                    DateTime apv_date_s = dsMain.DATA[0].apv_date_s;
                    DateTime apv_date_e = dsMain.DATA[0].apv_date_e;
                    string loan_docno = "";

                    member_no = dsMain.DATA[0].member_no;
                    approve_id = dsMain.DATA[0].approve_id;
                    lntypecode = dsMain.DATA[0].loantype_code;
                    contno_s = dsMain.DATA[0].contno_e;
                    contno_e = dsMain.DATA[0].contno_e;
                    //loan_docno = dsList.DATA[0].loan_docno;

                    dsList.Retrieve(member_no, approve_id, lntypecode, contno_s, contno_e, apv_date_s, apv_date_e,loan_docno);
                }
                catch { }
            }
            else if (eventArg == PrintCont)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                             apvlist = dsList.DATA[i].loan_docno.Trim();
                            
                        }
                        else
                        {
                             apvlist = apvlist + "," + dsList.DATA[i].loan_docno.Trim();
                        }
                    }
                }
                PrintCont_Click(apvlist);

            }
            else if (eventArg == PrintColl)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                            //apvlist = dsList.DATA[i].loancontract_no.Trim();
                            apvlist = dsList.DATA[i].loan_docno.Trim();
                        }
                        else
                        {
                            //apvlist = apvlist + "," + dsList.DATA[i].loancontract_no.Trim();
                            apvlist = apvlist + "," + dsList.DATA[i].loan_docno.Trim();
                        }
                    }
                }
                PrintColl_Click(apvlist);
            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        public void PrintCont_Click(string loan_docno)
        {

            if (loan_docno != "")
            {
                        string member_no = "";
                        string lntypecode = ""; decimal loanrequest_amt = 0;
                        
                        member_no = dsMain.DATA[0].member_no;
                        string mem_no = WebUtil.MemberNoFormat(member_no);


                        string sql_loanrequest = @"    SELECT LOANREQUEST_AMT as LOANREQUEST_AMT  FROM LNREQLOAN 
                                            where LOANREQUEST_DOCNO = {0}";

                        sql_loanrequest = WebUtil.SQLFormat(sql_loanrequest, loan_docno);
                        Sdt dt_loanrequest = WebUtil.QuerySdt(sql_loanrequest);
                        if (dt_loanrequest.Next())
                        {
                            loanrequest_amt = dt_loanrequest.GetDecimal("LOANREQUEST_AMT");

                        }


                        //// วนยิง ไทยบาทไป ireport

                        string bahtTxt, n, bahtTH_sum = "";
                        double amount;
                        try { amount = Convert.ToDouble(loanrequest_amt); }
                        catch { amount = 0; }
                        bahtTxt = amount.ToString("####.00");
                        string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                        string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                        string[] temp = bahtTxt.Split('.');
                        string intVal = temp[0];
                        string decVal = temp[1];
                        if (Convert.ToDouble(bahtTxt) == 0)
                            bahtTH_sum = "ศูนย์บาทถ้วน";
                        else
                        {
                            for (int j = 0; j < intVal.Length; j++)
                            {
                                n = intVal.Substring(j, 1);
                                if (n != "0")
                                {
                                    if ((j == (intVal.Length - 1)) && (n == "1"))
                                        bahtTH_sum += "เอ็ด";
                                    else if ((j == (intVal.Length - 2)) && (n == "2"))
                                        bahtTH_sum += "ยี่";
                                    else if ((j == (intVal.Length - 2)) && (n == "1"))
                                        bahtTH_sum += "";
                                    else
                                        bahtTH_sum += num[Convert.ToInt32(n)];
                                    bahtTH_sum += rank[(intVal.Length - j) - 1];
                                }
                            }
                            bahtTH_sum += "บาท";
                            if (decVal == "00")
                                bahtTH_sum += "ถ้วน";
                            else
                            {
                                for (int j = 0; j < decVal.Length; j++)
                                {
                                    n = decVal.Substring(j, 1);
                                    if (n != "0")
                                    {
                                        if ((j == decVal.Length - 1) && (n == "1"))
                                            bahtTH_sum += "เอ็ด";
                                        else if ((j == (decVal.Length - 2)) && (n == "2"))
                                            bahtTH_sum += "ยี่";
                                        else if ((j == (decVal.Length - 2)) && (n == "1"))
                                            bahtTH_sum += "";
                                        else
                                            bahtTH_sum += num[Convert.ToInt32(n)];
                                        bahtTH_sum += rank[(decVal.Length - j) - 1];
                                    }
                                }
                                bahtTH_sum += "สตางค์";
                            }
                        }
                        /////////////////////////




                        lntypecode = dsMain.DATA[0].loantype_code;
                        string report_name = "";
                        if (lntypecode == "20") { report_name = "irpt_loancontract_Living"; }
                        else if (lntypecode == "23") { report_name = "irpt_loancontract_no_healthyloan"; }
                        else if (lntypecode == "24") { report_name = "irpt_loancontract_fcmt"; }
                        else if (lntypecode == "26" || lntypecode == "31" || lntypecode == "40") { report_name = "irpt_loancontract_vdp_reqloan"; }
                        else if (lntypecode == "33") { report_name = "irpt_loancontract_special"; }
                        string report_label = "สัญญาเงินกู้";
                        iReportArgument args = new iReportArgument();
                        //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopControl);
                        //args.Add("as_loanreqdocno", iReportArgumentType.String, lncontno);
                        args.Add("as_membno", iReportArgumentType.String, mem_no);
                        args.Add("loantype_code", iReportArgumentType.String, lntypecode);
                        args.Add("loan_docno", iReportArgumentType.String, loan_docno);
                        args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);

                        iReportBuider report = new iReportBuider(this, "");
                        //report.AddCriteria("r_ln_print_loan_req_doc_rbt", "สัญญาเงินกู้", ReportType.pdf, args);
                        report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();
                    }
            }

        public void PrintColl_Click(string lncontno)
        {
            if (lncontno != "")
            {
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                args.Add("as_loanrequest_docno", iReportArgumentType.String, lncontno);

                iReportBuider report = new iReportBuider(this, "");
                report.AddCriteria("r_ln_print_loan_coll_doc_rbt", "หนังสือค้ำประกัน", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();

            }
        }
    }
}