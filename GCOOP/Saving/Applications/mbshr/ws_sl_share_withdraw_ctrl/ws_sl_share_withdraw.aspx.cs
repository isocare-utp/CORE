using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CoreSavingLibrary.WcfShrlon;
using CoreSavingLibrary.WcfNShrlon;
using System.Drawing;
using CoreSavingLibrary;
using DataLibrary;


namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl
{
    public partial class ws_sl_share_withdraw : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostCheckSelect { get; set; }
        [JsPostBack]
        public string PostShowData { get; set; }
        [JsPostBack]
        public string PostListMemberNo { get; set; }
        [JsPostBack]
        public string Postmember_serach { get; set; }
        [JsPostBack]
        public string PostPrintSlippayin { get; set; }
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
                //String xmlList = wcf.NShrlon.InitShareWithdrawList(state.SsWsPass, state.SsCoopControl);
                //String xmlList = wcf.NShrlon.of_initlist_shrwtd(state.SsWsPass, state.SsCoopControl);
                //dsList.ImportData(xmlList);
                dsMain.RetrieveCoop();
                dsMain.DATA[0].coop_id = state.SsCoopId;
                dsList.RetrieveList(dsMain.DATA[0].coop_id);
                Hdrow_member.Value = "-1";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostShowData)
            {
                //String xmlList = wcf.NShrlon.InitShareWithdrawList(state.SsWsPass, state.SsCoopControl);
                //String xmlList = wcf.NShrlon.of_initlist_shrwtd(state.SsWsPass, state.SsCoopControl);
                //dsList.ImportData(xmlList);
                if (HdSlipno.Value.Trim() != "" || HdSlipno.Value != "")
                {
                    PostPrintSlip(state.SsCoopControl); 
                }
                dsList.RetrieveList(dsMain.DATA[0].coop_id);
            }
            else if (eventArg == PostCheckSelect)
            {
                if (dsMain.DATA[0].checkselect == 0)
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        dsList.SetItem(i, dsList.DATA.OPERATE_FLAGColumn, 0);
                    }
                }
                else if (dsMain.DATA[0].checkselect == 1)
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        dsList.SetItem(i, dsList.DATA.OPERATE_FLAGColumn, 1);
                    }
                }
            }
            else if (eventArg == Postmember_serach)
            {
                int index_row = Convert.ToInt16(Hdrow_member.Value);
                setcolordefault();
                setcolor_row(index_row);
                Hdrow_member.Value = Convert.ToString(index_row);
                dsMain.DATA[0].MEMBER_NO = Hdmember_no.Value;
            }
            else if (eventArg == PostPrintSlippayin)
            {
                string payinslip_no = HdSlipno.Value;
                Printing.PrintSlipSlInIreportTnt(this, payinslip_no, state.SsCoopId);
            }
            else if (eventArg == PostPrint) // ออกใบสำคัญจ่ายหุ้น
            {
                string payoutslip_no = HdPayoutNo.Value;
                string payinslip_no = HdPayinNo.Value;

                if (state.SsCoopControl == "040001") { // ของกระทรวงตาก

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





                
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

            if (state.SsCoopControl == "013001")//GSB
            {
                if (HdSlipno.Value == "")
                {
                    dsMain.FindButton(0, "b_print").Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                }
                else
                {
                    dsMain.FindButton(0, "b_print").Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                }
            }
            else
            {
                dsMain.FindButton(0, "b_print").Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            }
        }

        private void setcolor_row(int index_row)
        {
            Color myRgbColor = new Color();
            myRgbColor = Color.FromArgb(92, 172, 238);

            dsList.FindTextBox(index_row, "sharetype_code").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "member_no").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "cp_mbname").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "membgroup_code").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "resign_date").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "cp_shareamt").BackColor = myRgbColor;
            dsList.FindTextBox(index_row, "cp_sharemasterstatus").BackColor = myRgbColor;
            dsList.FindCheckBox(index_row, "operate_flag").BackColor = myRgbColor;

        }

        private void setcolordefault()
        {
            Color myRgbColor = new Color();
            myRgbColor = Color.FromArgb(255, 255, 255);
            for (int i = 0; i < dsList.RowCount; i++)
            {
                dsList.FindTextBox(i, "sharetype_code").BackColor = myRgbColor;
                dsList.FindTextBox(i, "member_no").BackColor = myRgbColor;
                dsList.FindTextBox(i, "cp_mbname").BackColor = myRgbColor;
                dsList.FindTextBox(i, "membgroup_code").BackColor = myRgbColor;
                dsList.FindTextBox(i, "resign_date").BackColor = myRgbColor;
                dsList.FindTextBox(i, "cp_shareamt").BackColor = myRgbColor;
                dsList.FindTextBox(i, "cp_sharemasterstatus").BackColor = myRgbColor;
                dsList.FindCheckBox(i, "operate_flag").BackColor = myRgbColor;
            }
        }

        private void PostPrintSlip(string coopId)
        {
            string[] reportobj = WebUtil.GetIreportObjPrintLoan();
            string printslip_type = reportobj[0];
            string ireport_obj = reportobj[1].Trim();
            string ireportpayout_obj = reportobj[2].Trim();

            if (coopId == "022001")
            {
                Printing.PrintSlipSlInOutIrExat(this, HdSlipno.Value, state.SsCoopControl);
            }
            else if (coopId == "020001")
            {
                Printing.PrintIRSlippayOutPBN(this, coopId, HdSlipno.Value, ireportpayout_obj);
            }
        }

        public String GetIrobjectPrint(string coopid)
        {
            String irreportobj = "";

            String se = "select ireport_obj from lnloanconstant where coop_id = '"+ coopid +"'";
            return irreportobj;
        }

    }
}