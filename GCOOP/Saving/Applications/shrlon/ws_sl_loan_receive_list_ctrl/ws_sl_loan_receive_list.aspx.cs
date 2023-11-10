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

namespace Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl
{
    public partial class ws_sl_loan_receive_list : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostShowData { get; set; }
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostPrintSlip { get; set; }
        [JsPostBack]
        public string Printslip_stk { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            dsMain.RetriveEntryid();

            if (!IsPostBack)
            {
                dsMain.DATA[0].GROUP = "01";
                dsList.RetrieveList("01", "%", "");
                dsMain.DATA[0].LIST_QUANTITY = "0";
                this.SetOnLoadedScript(" parent.Setfocus();");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostShowData)
            {
                this.ShowData();

            }
            else if (eventArg == PostMemberNo)
            {
                string ls_membno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);

                dsMain.DATA[0].MEMBER_NO = ls_membno;
                setcolordefault();
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].MEMBER_NO == ls_membno)
                    {
                        setcolor_row(i);
                        dsList.FindTextBox(i, "member_no").Focus();
                    }
                }
            }
            else if (eventArg == PostPrintSlip)
            {
                string payoutslip_no = HdPayoutNo.Value.Trim();
                string payinslip_no = HdPayinNo.Value.Trim();

                LtServerMessage.Text = payoutslip_no + " : " + payinslip_no;
                try
                {
                    string[] reportobj = WebUtil.GetIreportObjPrintLoan();
                    string printslip_type = reportobj[0];
                    string ireport_obj = reportobj[1];
                    string ireportpayout_obj = reportobj[2];

                    if (state.SsCoopControl == "013001") // ของออมสิน
                    {
                        Printing.PrintSlipSlOutIreportGsb(this, payoutslip_no, state.SsCoopId);
                    }
                    else if (state.SsCoopControl == "020001") // ของเพชรบูรณ์
                    {
                        Printing.PrintIRSlippayOutPBN(this, state.SsCoopControl, payoutslip_no, ireportpayout_obj);
                    }
                    else if (state.SsCoopControl == "040001") // ของกระทรวงตาก
                    {
                        decimal payoutnet_amt = 0; decimal slip_amt = 0; string bahtTH_sum_payin = ""; string bahtTH_sum_out = "";

                        string sql = @" 
                               select slslippayout.payoutnet_amt , slslippayin.slip_amt
                                from slslippayout left join slslippayin on slslippayout.slipclear_no = slslippayin.payinslip_no
                                where slslippayout.payoutslip_no = {0} ";
                        sql = WebUtil.SQLFormat(sql, payoutslip_no);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            payoutnet_amt = dt.GetDecimal("payoutnet_amt");
                            slip_amt = dt.GetDecimal("slip_amt");
                        }
                            //100000>0
                        if (payoutnet_amt > 0 && slip_amt == 0)
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
                            /////////////////////////
                            string report_name = "", report_label = "";
                            report_name = "ir_printfin_patout_clound_a4";
                            report_label = "ใบสำคัญจ่าย";

                            iReportArgument args = new iReportArgument();
                            //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("as_slipno", iReportArgumentType.String, payoutslip_no);
                            args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                           // Printing.PrintSlipSlpayOut_sqlserver(this, payoutslip_no, state.SsCoopControl, bahtTH_sum);
                        }

                        if (slip_amt > 0 && payoutnet_amt > 0)
                        {

                            //// ทำค่า sum รวมเป็นภาษาไทย

                            if (slip_amt > 0)
                            {

                                string bahtTxt, n = "";
                                double amount;
                                try { amount = Convert.ToDouble(slip_amt); }
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

                            if (payoutnet_amt > 0)
                            {

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
                            args.Add("as_slipno", iReportArgumentType.String, payoutslip_no);
                            args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum_out);
                            args.Add("bahtTH_sumin", iReportArgumentType.String, bahtTH_sum_payin);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();

                           // Printing.PrintSlipMix_sqlserver(this, payinslip_no, state.SsCoopControl, bahtTH_sum_payin, bahtTH_sum_out);

                        }


                    }
                    else // ของพวกที่ใช้ส่วนกลาง
                    {

                        if (printslip_type == "IR")
                        {
                            Printing.PrintIRSlip(this, state.SsCoopControl, payoutslip_no, ireportpayout_obj);
                        }
                        else if (printslip_type == "PB" || printslip_type == "JS")
                        {
                            // ถ้าไม่มีใบเสร็จส่งเข้ามาให้ออก
                            if (payinslip_no == null || payinslip_no.Trim() == "")
                            {
                                return;
                            }

                            // ถ้าเป็นการพิมพ์ผ่าน PB ให้ตัด quote หน้าหลังทิ้ง
                            payinslip_no = payinslip_no.Substring(1, payinslip_no.Length - 2);
                            Printing.PrintSlipSlpayin(this, payinslip_no, state.SsCoopControl);
                        }
                        else if (printslip_type == "PBA")
                        {
                            Printing.PrintIRSlippayout(this, state.SsCoopControl, payoutslip_no, ireportpayout_obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }

                if (state.SsCoopControl == "028001")
                {
                    this.ShowData();
                }
            }

            else if (eventArg == Printslip_stk) {

                string slipout_no = Hdslipno.Value;

            }
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        private void setcolordefault()
        {
            Color myRgbColor = new Color();
            myRgbColor = Color.FromArgb(255, 255, 255);
            for (int index_row = 0; index_row < dsList.RowCount; index_row++)
            {
                dsList.FindTextBox(index_row, "lnrcvfrom_code").BackColor = myRgbColor;
                dsList.FindTextBox(index_row, "loancontract_no").BackColor = myRgbColor;
                dsList.FindTextBox(index_row, "prefix").BackColor = myRgbColor;
                dsList.FindTextBox(index_row, "member_no").BackColor = myRgbColor;
                dsList.FindTextBox(index_row, "name").BackColor = myRgbColor;
                dsList.FindTextBox(index_row, "membgroup_code").BackColor = myRgbColor;
                dsList.FindTextBox(index_row, "withdrawable_amt").BackColor = myRgbColor;
            }
        }

        private void setcolor_row(int index_row)
        {
            Color myRgbColor = new Color();
            myRgbColor = Color.FromArgb(92, 172, 238);

            dsList.FindTextBox(index_row, "lnrcvfrom_code").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "loancontract_no").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "prefix").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "member_no").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "name").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "membgroup_code").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "withdrawable_amt").BackColor = myRgbColor;
        }

        private void ShowData()
        {
            try
            {
                string group = "", entry = "", str_query = "";
                decimal list_quantity = Convert.ToDecimal(dsMain.DATA[0].LIST_QUANTITY);
                if (list_quantity > 0)
                {
                    str_query = " where rownum <= " + list_quantity;
                }

                if (dsMain.DATA[0].GROUP == "0")
                {
                    group = "%";
                }
                else
                {
                    group = dsMain.DATA[0].GROUP + "%";
                }

                entry = "%" + dsMain.DATA[0].ENTRY_ID + "%";

                dsList.RetrieveList(group, entry, str_query);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void SaveWebSheet()
        {
            this.SetOnLoadedScript(" parent.Setfocus();");
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}