using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class ws_sl_reprint : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostRetrieve { get; set; }
        [JsPostBack]
        public string PostPrint { get; set; }
        [JsPostBack]
        public string PostPrint2 { get; set; }

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
                    DateTime slip_date_s = dsMain.DATA[0].SLIP_DATE_S;
                    DateTime slip_date_e = dsMain.DATA[0].SLIP_DATE_E;

                    member_no = dsMain.DATA[0].MEMBER_NO;
                    entry_id = dsMain.DATA[0].ENTRY_ID;
                    sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                    document_no_s = dsMain.DATA[0].document_no_e;
                    document_no_e = dsMain.DATA[0].document_no_e;

                    dsList.Retrieve(member_no, entry_id, sliptype_code, document_no_s, document_no_e, slip_date_s, slip_date_e);
                    int row = dsList.RowCount;
                    for (int i = 0; i < row; i++)
                    {
                        decimal slip_status = dsList.DATA[i].SLIP_STATUS;
                        if (slip_status < 0)
                        {
                            dsList.FindTextBox(i, "COMPUTE_1").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "document_no").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "slip_date").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "entry_date").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "member_no").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "COMPUTE_2").BackColor = System.Drawing.Color.DarkGray;
                            dsList.FindTextBox(i, "entry_id").BackColor = System.Drawing.Color.DarkGray;
                        }
                    }
                }
                catch { }
            }
            else if (eventArg == PostPrint)
            {
                string bahtTH_sum = "";
                // ตรวจสอบก่อนว่าพิมพ์แบบไหน
                string sqlprint = "select printslip_type, ireport_obj from lnloanconstant ";
                Sdt dtp = WebUtil.QuerySdt(sqlprint);
                string printtype = "", ireportobj = "r_sl_slip_in_exat_a4_table";

                if (dtp.Next())
                {
                    printtype = dtp.GetString("printslip_type");
                    ireportobj = dtp.GetString("ireport_obj");
                }
                else
                {
                    printtype = "PB";
                    ireportobj = "r_sl_slip_in_exat_a4_table";
                }

                string rslip = ""; decimal slip_amt = 0;
                string ls_payinno = "";
                int[] prt_arr = new int[dsList.RowCount];
               
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        if (dsList.DATA[i].checkselect != 1)
                        {
                            continue;
                        }

                        ls_payinno = dsList.DATA[i].PAYINSLIP_NO;
                        slip_amt = dsList.DATA[i].SLIP_AMT;
                        #region THAIBAHT
                        //// ทำค่า sum รวมเป็นภาษาไทย
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
                            bahtTH_sum = "ศูนย์บาทถ้วน";
                        else
                        {
                            if (printtype != "IR")
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
                            }
                            else
                            {
                                if (printtype != "IR")
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
                        }
                        #endregion

                        /////////////////////////
                        // พิมพ์เป็นแบบการพิมพ์เลย
                        if (printtype == "PBA")
                        {
                            Printing.PrintSlipSlpayin(this, ls_payinno, state.SsCoopControl);
                            continue;
                        }
                        rslip += "," + ls_payinno + "";
                    
                }
                // ถ้าเป็นการพิมพ์เลย ไม่ต้องทำข้างล่างต่อเพราะพิมพ์หมดแล้ว
                if (printtype == "PBA")
                {
                    return;
                }
                // ถ้ามีข้อมูลตัด comma ตัวแรกทิ้ง
                if (rslip.Trim() != "")
                {
                    rslip = rslip.Substring(1);
                }
                if (printtype == "IR")
                {
                    //พิมพ์แบบireport
                    iReportArgument args = new iReportArgument();
                    args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                    args.Add("as_payinslipno", iReportArgumentType.String, rslip);
                    iReportBuider report = new iReportBuider(this, "");
                    report.AddCriteria(ireportobj, "ใบเสร็จรับเงิน", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();
                }
                else if (printtype == "JS" || printtype == "PB")
                {
                    if (state.SsCoopControl == "051001")
                    {
                        try
                        {
                            string slip_no = "";
                            for (int i = 0; i < dsList.RowCount; i++)
                            {
                                if (dsList.DATA[i].checkselect == 1)
                                {
                                    if (slip_no == "")
                                    {
                                        slip_no = "" + dsList.DATA[i].PAYINSLIP_NO + "";
                                    }
                                    else
                                    {
                                        slip_no += "," + dsList.DATA[i].PAYINSLIP_NO + "";
                                    }
                                }
                            }
                            iReportArgument args = new iReportArgument();
                            args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                            args.Add("as_payinslipno", iReportArgumentType.String, slip_no);
                            iReportBuider report = new iReportBuider(this, "");
                            report.AddCriteria("r_sl_slippayin_receipt_slp", "ใบเสร็จรับเงิน", ReportType.pdf, args);
                            report.AutoOpenPDF = true;
                            report.Retrieve();
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("พิมพ์ใบเสร็จไม่สำเร็จ กรุณาตรวจสอบ" + ex.Message);
                        }
                    }
                    // ถ้าเป็นการพิมพ์ผ่าน PB ให้ตัด quote หน้าหลังทิ้ง
                    //ขอกระทรวงตากไม่ต้องตัด quote เนื่องจากไม่มีติดมา hard ไว้ เพราะไม่แน่ใจว่าที่อื่นเป็นยังไง
                    else
                    {
                        if (state.SsCoopControl != "040001")
                        {
                            rslip = rslip.Substring(1, rslip.Length - 2);
                        }                        
                        iReportArgument args = new iReportArgument();
                        args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                        args.Add("payinslip_no", iReportArgumentType.String, rslip);
                        args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                        iReportBuider report = new iReportBuider(this, "");
                        report.AddCriteria("r_sl_slippayin_receipt_stk", "ใบเสร็จรับเงิน", ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();
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
