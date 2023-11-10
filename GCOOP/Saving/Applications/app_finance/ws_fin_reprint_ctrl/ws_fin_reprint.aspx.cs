using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DataLibrary;
namespace Saving.Applications.app_finance.ws_fin_reprint_ctrl
{
    public partial class ws_fin_reprint : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostRetrieve { get; set; }
        [JsPostBack]
        public string PostPrint { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DDUsername();
                dsMain.DATA[0].ENTRY_ID = state.SsUsername.Trim();
                dsMain.DATA[0].SLIP_DATE_S = state.SsWorkDate;
                dsMain.DATA[0].SLIP_DATE_E = state.SsWorkDate;
                dsList.Retrieve(dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].ENTRY_ID, dsMain.DATA[0].PAY_RECV_STATUS, "", "", state.SsWorkDate, state.SsWorkDate);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostRetrieve)
            {
                try
                {
                    string member_no = "";
                    string entry_id = "";
                    string slip_no_s = "";
                    string slip_no_e = "";
                    decimal payrecv_status = 0;
                    DateTime slip_date_s = dsMain.DATA[0].SLIP_DATE_S;
                    DateTime slip_date_e = dsMain.DATA[0].SLIP_DATE_E;

                    member_no = dsMain.DATA[0].MEMBER_NO;
                    entry_id = dsMain.DATA[0].ENTRY_ID;
                    payrecv_status = dsMain.DATA[0].PAY_RECV_STATUS;
                    slip_no_s = dsMain.DATA[0].SLIP_NO_S;
                    slip_no_e = dsMain.DATA[0].SLIP_NO_E;
                    slip_date_s = dsMain.DATA[0].SLIP_DATE_S;
                    slip_date_e = dsMain.DATA[0].SLIP_DATE_E;
                    dsList.Retrieve(member_no, entry_id, payrecv_status, slip_no_s, slip_no_e, slip_date_s, slip_date_e);
                }
                catch { }
            }
            else if (eventArg == PostPrint)
            {
                bool chk = true;
                string slip_no = "", ld_dwrec = "", ls_dwpay="";
                decimal payrecv_status = dsMain.DATA[0].PAY_RECV_STATUS; decimal item_amtnet = 0;
                string sql_dw = @"select rtrim(ltrim(dw_receipt)) as dw_receipt,rtrim(ltrim(dw_payreceipt)) as dw_payreceipt from finconstant where coop_id={0} ";
                sql_dw = WebUtil.SQLFormat(sql_dw, state.SsCoopControl);
                Sdt dt_dw = WebUtil.QuerySdt(sql_dw);
                if (dt_dw.Next())
                {
                    ld_dwrec = dt_dw.GetString("dw_receipt");
                    ls_dwpay = dt_dw.GetString("dw_payreceipt");
                }
                for (int j = 0; j < dsList.RowCount && chk; j++)
                {
                    if (dsList.DATA[j].checkselect == 1)
                    {
                        try
                        {
                            slip_no = dsList.DATA[j].SLIP_NO.Trim();
                            if (payrecv_status == 1)
                            {

                                item_amtnet = dsList.DATA[j].ITEM_AMTNET;

                                ////////////////////////////
                                
                                /////////////////////////

                                //Printing.PrintFinRecvSlipIreportExat(this, slip_no);
                                string report_name = "", report_label = "";
                                report_name = ld_dwrec;
                                report_label = "ใบเสร็จรับเงิน";

                                iReportArgument args = new iReportArgument();
                                //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                                args.Add("as_slipno", iReportArgumentType.String, slip_no);
                                iReportBuider report = new iReportBuider(this, "");
                                report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                                report.AutoOpenPDF = true;
                                report.Retrieve();
                            }
                            else if (payrecv_status == 0)
                            {

                                //item_amtnet = dsList.DATA[j].ITEM_AMTNET;

                                //////////////////////////////
                                ////// ทำค่า sum รวมเป็นภาษาไทย
                                //string bahtTxt, n, bahtTH_sum = "";
                                //double amount;
                                //try { amount = Convert.ToDouble(item_amtnet); }
                                //catch { amount = 0; }
                                //bahtTxt = amount.ToString("####.00");
                                //string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                                //string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                                //string[] temp = bahtTxt.Split('.');
                                //string intVal = temp[0];
                                //string decVal = temp[1];
                                //if (Convert.ToDouble(bahtTxt) == 0)
                                //    bahtTH_sum = "ศูนย์บาทถ้วน";
                                //else
                                //{
                                //    for (int k = 0; k < intVal.Length; k++)
                                //    {
                                //        n = intVal.Substring(k, 1);
                                //        if (n != "0")
                                //        {
                                //            if ((k == (intVal.Length - 1)) && (n == "1"))
                                //                bahtTH_sum += "เอ็ด";
                                //            else if ((k == (intVal.Length - 2)) && (n == "2"))
                                //                bahtTH_sum += "ยี่";
                                //            else if ((k == (intVal.Length - 2)) && (n == "1"))
                                //                bahtTH_sum += "";
                                //            else
                                //                bahtTH_sum += num[Convert.ToInt32(n)];
                                //            bahtTH_sum += rank[(intVal.Length - k) - 1];
                                //        }
                                //    }
                                //    bahtTH_sum += "บาท";
                                //    if (decVal == "00")
                                //        bahtTH_sum += "ถ้วน";
                                //    else
                                //    {
                                //        for (int k = 0; k < decVal.Length; k++)
                                //        {
                                //            n = decVal.Substring(k, 1);
                                //            if (n != "0")
                                //            {
                                //                if ((k == decVal.Length - 1) && (n == "1"))
                                //                    bahtTH_sum += "เอ็ด";
                                //                else if ((k == (decVal.Length - 2)) && (n == "2"))
                                //                    bahtTH_sum += "ยี่";
                                //                else if ((k == (decVal.Length - 2)) && (n == "1"))
                                //                    bahtTH_sum += "";
                                //                else
                                //                    bahtTH_sum += num[Convert.ToInt32(n)];
                                //                bahtTH_sum += rank[(decVal.Length - k) - 1];
                                //            }
                                //        }
                                //        bahtTH_sum += "สตางค์";
                                //    }
                                //}
                                /////////////////////////

                                //Printing.PrintFinPaySlipIreportExat(this, slip_no);
                                string report_name = "", report_label = "";
                                report_name = ls_dwpay;
                                report_label = "ใบสำคัญจ่าย";

                                iReportArgument args = new iReportArgument();
                                //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                                args.Add("as_slipno", iReportArgumentType.String, slip_no);
                                //args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                                iReportBuider report = new iReportBuider(this, "");
                                report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                                report.AutoOpenPDF = true;
                                report.Retrieve();
                            }
                            chk = false;
                            LtServerMessage.Text = WebUtil.CompleteMessage("พิมพ์ใบเสร็จสำเร็จ");
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("พิมพ์ใบเสร็จไม่สำเร็จ กรุณาตรวจสอบ" + ex.Message);
                        }
                    }
                }
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