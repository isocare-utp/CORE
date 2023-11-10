using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_reprint_payout_ctrl
{
    public partial class ws_sl_reprint_payout : PageWebSheet, WebSheet
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
                dsMain.DdCode();
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
                    string entry_id = "";
                    string sliptype_code = "";
                    string document_no_s = "";
                    string document_no_e = "";
                    DateTime slip_date_s = dsMain.DATA[0].slip_date_s;
                    DateTime slip_date_e = dsMain.DATA[0].slip_date_e;

                    member_no = dsMain.DATA[0].member_no;
                    entry_id = dsMain.DATA[0].entry_id;
                    sliptype_code = dsMain.DATA[0].sliptype_code;
                    document_no_s = dsMain.DATA[0].slip_no_e;
                    document_no_e = dsMain.DATA[0].slip_no_e;

                    dsList.Retrieve(member_no, entry_id, sliptype_code, document_no_s, document_no_e, slip_date_s, slip_date_e);
                    int row = dsList.RowCount;
                    for (int i = 0; i < row; i++)
                    {
                        decimal slip_status = dsList.DATA[i].slip_status;
                        if (slip_status < 0)
                        {
                            dsList.FindTextBox(i, "sliptype_code").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "payoutslip_no").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "slip_date").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "member_no").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "mbname").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "payout_amt").BackColor = System.Drawing.Color.DarkGray;
                        }
                    }
                }
                catch { }
            }
            else if (eventArg == PostPrint)
            {
                string rslip = ""; string bahtTH_sum_out = ""; string bahtTH_sum_payin = "";

               /* int[] prt_arr = new int[dsList.RowCount];

                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].checkselect == 1)
                    {
                        if (rslip == "")
                        {
                            rslip = dsList.DATA[i].payoutslip_no;
                        }
                        else
                        {
                            rslip += "," + dsList.DATA[i].payoutslip_no;
                        }
                    }
                }*/

                /*string sqlprint = "select printslip_type, ireportpayout_obj from lnloanconstant ";
                Sdt dtp = WebUtil.QuerySdt(sqlprint);
                string printtype = "", ireportobj = "";
                if (dtp.Next())
                {
                    printtype = dtp.GetString("printslip_type");
                    ireportobj = dtp.GetString("ireportpayout_obj");
                }

                if (!(string.IsNullOrEmpty(ireportobj.Trim())))
                {
                    try
                    {
                        Printing.PrintIRSlippayout(this, state.SsCoopControl, rslip, ireportobj);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    }

                }*/
                //try
                //{
                //    //Printing.PrintSlipSlIreportGsb(this, payoutslip_no, payinslip_no, state.SsCoopId);
                //    Printing.PrintSlipSlInOutIreportExat(this, rslip, state.SsCoopControl, ireportobj);
                //}
                //catch (Exception ex)
                //{
                //    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                //}

                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].checkselect == 1)
                    {

                        rslip = dsList.DATA[i].payoutslip_no;

                        decimal payoutnet_amt = 0; decimal slip_amt = 0; string payinslip_no = ""; decimal cl_amt = 0;

                        string sql = @" 
                               select slslippayout.payoutnet_amt , slslippayout.payout_amt - slslippayout.payoutnet_amt as cl_amt , slslippayin.slip_amt , slslippayin.payinslip_no
                                from slslippayout left join slslippayin on slslippayout.slipclear_no = slslippayin.payinslip_no
                                where slslippayout.payoutslip_no = {0} ";
                        sql = WebUtil.SQLFormat(sql, rslip);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            payoutnet_amt = dt.GetDecimal("payoutnet_amt");
                            cl_amt = dt.GetDecimal("cl_amt");
                            slip_amt = dt.GetDecimal("slip_amt");
                            payinslip_no = dt.GetString("payinslip_no");
                        }

                        if (payoutnet_amt > 0  && cl_amt == 0)
                        {

                            //// ทำค่า sum รวมเป็นภาษาไทย
                            string bahtTxt, n, bahtTH_sum = "";
                            double amount;
                            try { amount = Convert.ToDouble(payoutnet_amt); }
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
                            string report_name = "", report_label = "";
                            report_name = "ir_printfin_patout_clound_a4";
                            report_label = "ใบสำคัญจ่าย";

                            iReportArgument args = new iReportArgument();
                            //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("as_slipno", iReportArgumentType.String, rslip);
                            args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                        }

                        if (cl_amt > 0 && (payoutnet_amt > 0 || slip_amt > 0))
                        {

                            //// ทำค่า sum รวมเป็นภาษาไทย

                            if (cl_amt > 0)
                            {
                               
                                string bahtTxt, n = "";
                                double amount;
                                try { amount = Convert.ToDouble(cl_amt); }
                                catch { amount = 0; }
                                bahtTxt = amount.ToString("####.00");
                                string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                                string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                                string[] temp = bahtTxt.Split('.');
                                string intVal = temp[0];
                                string decVal = temp[1];
                                if (Convert.ToDouble(bahtTxt) == 0)
                                    bahtTH_sum_payin = "ศูนย์บาทถ้วน";
                                else
                                {
                                    for (int j = 0; j < intVal.Length; j++)
                                    {
                                        n = intVal.Substring(j, 1);
                                        if (n != "0")
                                        {
                                            if ((j == (intVal.Length - 1)) && (n == "1"))
                                                bahtTH_sum_payin += "เอ็ด";
                                            else if ((j == (intVal.Length - 2)) && (n == "2"))
                                                bahtTH_sum_payin += "ยี่";
                                            else if ((j == (intVal.Length - 2)) && (n == "1"))
                                                bahtTH_sum_payin += "";
                                            else
                                                bahtTH_sum_payin += num[Convert.ToInt32(n)];
                                            bahtTH_sum_payin += rank[(intVal.Length - j) - 1];
                                        }
                                    }
                                    bahtTH_sum_payin += "บาท";
                                    if (decVal == "00")
                                        bahtTH_sum_payin += "ถ้วน";
                                    else
                                    {
                                        for (int j = 0; j < decVal.Length; j++)
                                        {
                                            n = decVal.Substring(j, 1);
                                            if (n != "0")
                                            {
                                                if ((j == decVal.Length - 1) && (n == "1"))
                                                    bahtTH_sum_payin += "เอ็ด";
                                                else if ((j == (decVal.Length - 2)) && (n == "2"))
                                                    bahtTH_sum_payin += "ยี่";
                                                else if ((j == (decVal.Length - 2)) && (n == "1"))
                                                    bahtTH_sum_payin += "";
                                                else
                                                    bahtTH_sum_payin += num[Convert.ToInt32(n)];
                                                bahtTH_sum_payin += rank[(decVal.Length - j) - 1];
                                            }
                                        }
                                        bahtTH_sum_payin += "สตางค์";
                                    }
                                }
                            }
                            /////////////////////////

                            if (payoutnet_amt > 0 || slip_amt > 0)
                            {

                                if (payoutnet_amt == 0)
                                {

                                    payoutnet_amt = slip_amt;

                                }

                                //// ทำค่า sum รวมเป็นภาษาไทย
                                string bahtTxt, n = "";
                                double amount;
                                try { amount = Convert.ToDouble(payoutnet_amt); }
                                catch { amount = 0; }
                                bahtTxt = amount.ToString("####.00");
                                string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
                                string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
                                string[] temp = bahtTxt.Split('.');
                                string intVal = temp[0];
                                string decVal = temp[1];
                                if (Convert.ToDouble(bahtTxt) == 0)
                                    bahtTH_sum_out = "ศูนย์บาทถ้วน";
                                else
                                {
                                    for (int j = 0; j < intVal.Length; j++)
                                    {
                                        n = intVal.Substring(j, 1);
                                        if (n != "0")
                                        {
                                            if ((j == (intVal.Length - 1)) && (n == "1"))
                                                bahtTH_sum_out += "เอ็ด";
                                            else if ((j == (intVal.Length - 2)) && (n == "2"))
                                                bahtTH_sum_out += "ยี่";
                                            else if ((j == (intVal.Length - 2)) && (n == "1"))
                                                bahtTH_sum_out += "";
                                            else
                                                bahtTH_sum_out += num[Convert.ToInt32(n)];
                                            bahtTH_sum_out += rank[(intVal.Length - j) - 1];
                                        }
                                    }
                                    bahtTH_sum_out += "บาท";
                                    if (decVal == "00")
                                        bahtTH_sum_out += "ถ้วน";
                                    else
                                    {
                                        for (int j = 0; j < decVal.Length; j++)
                                        {
                                            n = decVal.Substring(j, 1);
                                            if (n != "0")
                                            {
                                                if ((j == decVal.Length - 1) && (n == "1"))
                                                    bahtTH_sum_out += "เอ็ด";
                                                else if ((j == (decVal.Length - 2)) && (n == "2"))
                                                    bahtTH_sum_out += "ยี่";
                                                else if ((j == (decVal.Length - 2)) && (n == "1"))
                                                    bahtTH_sum_out += "";
                                                else
                                                    bahtTH_sum_out += num[Convert.ToInt32(n)];
                                                bahtTH_sum_out += rank[(decVal.Length - j) - 1];
                                            }
                                        }
                                        bahtTH_sum_out += "สตางค์";
                                    }
                                }
                                /////////////////////////

                            }

                            string report_name = "", report_label = "";
                            report_name = "ir_printfin_clound_a4";
                            report_label = "ใบสำคัญจ่ายเเละรับ";

                            iReportArgument args = new iReportArgument();
                            //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("as_slipno", iReportArgumentType.String, rslip);
                            args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum_out);
                            args.Add("bahtTH_sumin", iReportArgumentType.String, bahtTH_sum_payin);                            
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();

                        }
                    }
                }


            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        private static string XmlReadVar(string responseData, string szVar)
        {
            int i1stLoc = responseData.IndexOf("<" + szVar + ">");
            if (i1stLoc < 0)
                return string.Empty;
            int ilstLoc = responseData.IndexOf("</" + szVar + ">");
            if (ilstLoc < 0)
                return string.Empty;
            int len = szVar.Length;
            return responseData.Substring(i1stLoc + len + 2, ilstLoc - i1stLoc - len - 2);
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}
