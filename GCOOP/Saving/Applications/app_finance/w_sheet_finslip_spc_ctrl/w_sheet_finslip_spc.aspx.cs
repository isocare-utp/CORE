using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl
{
    public partial class w_sheet_finslip_spc : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public string PostInitPayRecv { get; set; }
        [JsPostBack]
        public string PostInitAccid { get; set; }
        [JsPostBack]
        public string PostInitMember { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDeleteRow { get; set; }
        [JsPostBack]
        public string PostCalVat { get; set; }
        [JsPostBack]
        public string PostInitSlipdesc { get; set; }
        [JsPostBack]
        public string PostBankBranch { get; set; }
        [JsPostBack]
        public string PostPrint { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dwTax.InitDwTax(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                HdSlip_no.Value = ""; HdPayrec.Value = "";
                LoadBegin();
            }
        }
        public void LoadBegin()
        {
            HdShowDisplay.Value = "";
            HdSlipno.Value = "";
            HdDlg.Value = "0";
            dsMain.DATA[0].SENDTO_SYSTEM = "";
            HdCoopid.Value = "";
            HdDetailRow.Value = "";
            HdDlgAccid.Value = "0";
            Boolean lbl_fin = of_CheckSave();
            if (lbl_fin == false)
            {
                //don't show display 
                this.SetOnLoadedScript("document.getElementById('F_dsShow').style.display = 'none';");
                HdShowDisplay.Value = "fase";
                return;
            }
            else
            {
                //show display 
                this.SetOnLoadedScript("document.getElementById('F_dsShow').style.display = 'block';");
                HdShowDisplay.Value = "true";
                dsMain.ResetRow();
                dsList.ResetRow();
                dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
                dsMain.DATA[0].ENTRY_DATE = state.SsWorkDate;
                dsMain.DATA[0].ENTRY_ID = state.SsUsername;
                dsMain.DATA[0].COOP_ID = state.SsCoopId;
                dsMain.DATA[0].FROM_SYSTEM = "FIN";
                dsMain.DATA[0].REF_SYSTEM = "FIN";
                PostProtect();
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostInitPayRecv)
            {
                PostProtect();
                dsMain.DDMoney();
                dsMain.DDAccid();
                dsMain.DDTaxType();
                InitAccid();
                if (dsList.RowCount < 1)
                {
                    dsList.InsertAtRow(0);
                }
                dsList.DDAccid();
            }
            else if (eventArg == PostInitAccid)
            {
                InitAccid();
            }
            else if (eventArg == PostInitMember)
            {
                InitMember();
            }

            else if (eventArg == PostCalVat)
            {
                SumCalVat();
            }
            else if (eventArg == PostDeleteRow)
            {
                int row = int.Parse(HdDetailRow.Value);
                dsList.DeleteRow(row);
                dsList.DDAccid();
                SumCalVat();
            }
            else if (eventArg == PostInsertRow)
            {
                dsList.InsertLastRow();
                dsList.DDAccid();
            }
            else if (eventArg == PostInitSlipdesc)
            {
                int ln_row = dsList.GetRowFocus();
                string ls_code = "";
                ls_code = dsList.DATA[ln_row].SLIPITEMTYPE_CODE;
                string ls_desc = "";
                string sql = @"
                              SELECT FINUCFITEMTYPE.ITEM_DESC,  
                                FINUCFITEMTYPE.SLIPITEMTYPE_CODE,                     
                                FINUCFITEMTYPE.ACCOUNT_ID
                        FROM FINUCFITEMTYPE  
                        WHERE FINUCFITEMTYPE.COOP_ID = {0}
                        AND FINUCFITEMTYPE.SLIPITEMTYPE_CODE ={1}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_desc = dt.GetString("ITEM_DESC");
                }
                dsList.DATA[ln_row].SLIPITEM_DESC = ls_desc;
                if (ln_row == 0)
                {
                    dsMain.DATA[0].PAYMENT_DESC = ls_desc;
                }
            }
            else if (eventArg == PostBankBranch)
            {
                dsMain.DDBankBranch(dsMain.DATA[0].BANK_CODE);

            }
            else if (eventArg == PostPrint)
            {
                try
                {
                    bool chk = true;
                    string slip_no = "", ld_dwrec = "", ls_dwpay = "";
                    decimal payrecv_status = dsMain.DATA[0].PAY_RECV_STATUS;
                    string sql_dw = @"select rtrim(ltrim(dw_receipt)) as dw_receipt,rtrim(ltrim(dw_payreceipt)) as dw_payreceipt from finconstant where coop_id={0} ";
                    sql_dw = WebUtil.SQLFormat(sql_dw, state.SsCoopControl);
                    Sdt dt_dw = WebUtil.QuerySdt(sql_dw);
                    if (dt_dw.Next())
                    {
                        ld_dwrec = dt_dw.GetString("dw_receipt");
                        ls_dwpay = dt_dw.GetString("dw_payreceipt");
                    }
                    if (HdPayrec.Value == "1")
                    {
                        slip_no = HdSlip_no.Value.Trim();
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
                    else if (HdPayrec.Value == "0")
                    {
                        slip_no = HdSlip_no.Value.Trim();
                        //Printing.PrintFinRecvSlipIreportExat(this, slip_no);
                        string report_name = "", report_label = "";
                        report_name = ls_dwpay;
                        report_label = "ใบสำคัญจ่าย";

                        iReportArgument args = new iReportArgument();
                        //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                        args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                        args.Add("as_slipno", iReportArgumentType.String, slip_no);
                        iReportBuider report = new iReportBuider(this, "");
                        report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถออกใบเสร็จได้ กรุณาทำรายการก่อนออกใบเสร็จ");
                }
            }
        }
        public void InitAccid()
        {
            //call func.
            string ls_cashtype = dsMain.DATA[0].CASH_TYPE;
            decimal[] ar_func = financeFunction.of_getattribconstant(state.SsCoopControl, ls_cashtype);
            dsMain.DATA[0].TOFROM_ACCID = ar_func[0].ToString();
            if (dsMain.DATA[0].PAY_RECV_STATUS == 1 && ls_cashtype == "CHQ")
            {
                dsMain.DDBankCode();
                dsMain.DATA[0].DATEON_CHQ = state.SsWorkDate;
            }

        }
        public void InitMember()
        {
            if (HdDlg.Value == "0")
            {
                string ls_memno = "";
                ls_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO.Trim());
                //call func.
                string[] ar_func = financeFunction.of_init_payrecv_member(state.SsCoopControl, dsMain.DATA[0].MEMBER_FLAG, ls_memno);
                string ls_nomemberdetail = "";
                ls_nomemberdetail = ar_func[0];
                if (ls_nomemberdetail == "" || ls_nomemberdetail == null)
                {
                    dsMain.DATA[0].NONMEMBER_DETAIL = "";
                    dsMain.DATA[0].MEMBGROUP_CODE = "";
                    dsMain.DATA[0].MEMBER_NO = "";
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
                    return;
                }
                else
                {
                    dsMain.DATA[0].NONMEMBER_DETAIL = ls_nomemberdetail;
                    dsMain.DATA[0].MEMBGROUP_CODE = ar_func[1];
                    dsMain.DATA[0].MEMBER_NO = ls_memno;
                }
            }
            else
            {
                string ls_coop = "", ls_slipno = "";
                dsMain.ResetRow();
                dsList.ResetRow();
                dwTax.ResetRow();
                ls_slipno = HdSlipno.Value.Trim();
                ls_coop = HdCoopid.Value.Trim();
                dsMain.RetrieveFinslip(ls_coop, ls_slipno);
                dsMain.DATA[0].ENTRY_DATE = state.SsWorkDate;
                dsMain.DDAccid();
                dsMain.DDMoney();
                if (dsMain.DATA[0].CASH_TYPE == "CHQ" && dsMain.DATA[0].PAY_RECV_STATUS == 1)
                {
                    dsMain.DDBankCode();
                    dsMain.DDBankBranch(dsMain.DATA[0].BANK_CODE);
                }
                if (dsMain.RowCount > 0)
                {
                    //dsList.InsertAtRow(0);
                    dsList.RetrieveFinslipDet(ls_coop, ls_slipno);
                    dsList.DDAccid();
                }
            }
        }
        protected void PostProtect()
        {
            decimal pay_recv_status = 9;
            pay_recv_status = Convert.ToDecimal(dsMain.DATA[0].PAY_RECV_STATUS);
            if (pay_recv_status == 9) //ยังไม่เลือก
            {
                //dsMain
                dsMain.FindTextBox(0, "member_no").Enabled = false;
                dsMain.FindDropDownList(0, "member_flag").Enabled = false;
                dsMain.FindTextBox(0, "nonmember_detail").Enabled = false;
                dsMain.FindTextBox(0, "operate_date").Enabled = false;
                dsMain.FindDropDownList(0, "cash_type").Enabled = false;
                dsMain.FindDropDownList(0, "tofrom_accid").Enabled = false;
                dsMain.FindTextBox(0, "vat_amt").Enabled = false;
                dsMain.FindTextBox(0, "tax_amt").Enabled = false;

                ///dwTax
                dwTax.FindTextBox(0, "taxpay_id").Enabled = false;
                dwTax.FindTextBox(0, "taxpay_addr").Enabled = false;
                dwTax.FindTextBox(0, "taxpay_desc").Enabled = false;
            }
            else
            {
                //dsMain
                dsMain.FindTextBox(0, "member_no").Enabled = true;
                dsMain.FindDropDownList(0, "member_flag").Enabled = true;
                dsMain.FindTextBox(0, "nonmember_detail").Enabled = true;
                dsMain.FindTextBox(0, "operate_date").Enabled = true;
                dsMain.FindDropDownList(0, "cash_type").Enabled = true;
                dsMain.FindDropDownList(0, "tofrom_accid").Enabled = true;


                ///dwTax
                if (dsMain.DATA[0].TAX_FLAG == 1)
                {
                    dsMain.FindTextBox(0, "vat_amt").Enabled = true;
                    dsMain.FindTextBox(0, "tax_amt").Enabled = true;

                    dwTax.FindTextBox(0, "taxpay_id").Enabled = true;
                    dwTax.FindTextBox(0, "taxpay_addr").Enabled = true;
                    dwTax.FindTextBox(0, "taxpay_desc").Enabled = true;
                }
                else
                {
                    dsMain.FindTextBox(0, "vat_amt").Enabled = false;
                    dsMain.FindTextBox(0, "tax_amt").Enabled = false;

                    dwTax.FindTextBox(0, "taxpay_id").Enabled = false;
                    dwTax.FindTextBox(0, "taxpay_addr").Enabled = false;
                    dwTax.FindTextBox(0, "taxpay_desc").Enabled = false;
                }
            }
            if (pay_recv_status == 0)
            {
                lb_slipdet.Text = "รายการ (DR)";
            }
            else if (pay_recv_status == 1)
            {
                lb_slipdet.Text = "รายการ (CR)";
            }
            else
            {
                lb_slipdet.Text = "รายการ";
            }

        }
        protected void SumCalVat()
        {
            try
            {
                string sqlStr = "";
                decimal sumAmtNet = 0, taxRate = 0;
                Decimal itempay_amt = 0, sumpay_amt = 0, sumAmt = 0, sumVat = 0, ld_includevat_flag = 0;
                Decimal sumTax = 0, payTax = 0, sumpayprice_amt = 0, memberFlag = 0;
                Int16 vatFlag = Convert.ToInt16(dsMain.DATA[0].vat_flag);
                Int16 taxFlag = Convert.ToInt16(dsMain.DATA[0].TAX_FLAG);
                Int16 taxCode = Convert.ToInt16(dsMain.DATA[0].TAX_CODE);
                if (taxFlag == 1)
                {
                    this.SetOnLoadedScript("document.getElementById('F_dwTax').style.display = 'block';");
                    sqlStr = @"  SELECT FINTAXTYPE.TAX_CODE,   
                         FINTAXTYPE.TAX_DESC,   
                         FINTAXTYPE.TAX_RATE,   
                         FINUCFICTAXTYPE.ICTAXTYPE_DESC  
                    FROM FINTAXTYPE,   
                         FINUCFICTAXTYPE  
                   WHERE ( FINTAXTYPE.ICTAXTYPE_CODE = FINUCFICTAXTYPE.ICTAXTYPE_CODE ) and  
                         ( ( FINTAXTYPE.COOP_ID  = {0} ) and
                            (FINTAXTYPE.TAX_CODE ={1}))    ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, taxCode);
                    Sdt dt = WebUtil.QuerySdt(sqlStr);
                    if (dt.Next())
                    {
                        taxRate = dt.GetDecimal("TAX_RATE");
                        dsMain.DATA[0].TAX_RATE = taxRate;
                    }
                }
                else
                {
                    this.SetOnLoadedScript("document.getElementById('F_dwTax').style.display = 'none';");
                }
                decimal[] ar_func = financeFunction.of_getattribconstant(state.SsCoopControl, "");
                ld_includevat_flag = ar_func[2];
                //vat นอก คือ ราคาที่ยังไม่รวม vat
                if (ld_includevat_flag == 0)
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        itempay_amt = dsList.DATA[i].ITEMPAY_AMT;
                        if (dsList.DATA[i].OPERATE_FLAG == 1)
                        {
                            sumpay_amt += dsList.DATA[i].ITEMPAY_AMT;
                        }
                        else
                        {
                            sumpay_amt -= dsList.DATA[i].ITEMPAY_AMT;
                        }
                        if (vatFlag == 1)
                        {
                            dsList.DATA[i].VAT_FLAG = 1;
                            sumAmt = (itempay_amt * 7) / 100;
                            sumAmt = Math.Round(sumAmt, 2);
                            try
                            {
                                dsList.DATA[i].VAT_AMT = sumAmt;
                                //รวม vat
                                dsList.DATA[i].ITEMPAYAMT_NET = itempay_amt + sumAmt;
                            }
                            catch (Exception ex)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                            }

                        }
                        else
                        {
                            dsList.DATA[i].VAT_FLAG = 0;
                            dsList.DATA[i].VAT_AMT = 0;
                        }
                        if (taxFlag == 1)
                        {
                            dsList.DATA[i].TAX_FLAG = 1;
                            dsList.DATA[i].TAX_CODE = dsMain.DATA[0].TAX_CODE;
                            payTax = (itempay_amt * taxRate);
                            sumTax += payTax;
                            dsList.DATA[i].TAX_AMT = payTax;
                        }
                        else
                        {
                            dsList.DATA[i].TAX_FLAG = 0;
                            dsList.DATA[i].TAX_AMT = 0;
                            dsList.DATA[i].TAX_CODE = 0;
                        }
                        dsList.DATA[i].ITEMPAYAMT_NET = itempay_amt + sumAmt - payTax;
                        if (dsList.DATA[i].OPERATE_FLAG == 1)
                        {
                            sumAmtNet += dsList.DATA[i].ITEMPAYAMT_NET;
                        }
                        else
                        {
                            sumAmtNet -= dsList.DATA[i].ITEMPAYAMT_NET;
                        }
                        sumVat += sumAmt;
                    }
                }
                else
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        itempay_amt = dsList.DATA[i].ITEMPAY_AMT;//ค่าของรวมvat
                        if (dsList.DATA[i].OPERATE_FLAG == 1)
                        {
                            sumpay_amt += dsList.DATA[i].ITEMPAY_AMT;
                        }
                        else
                        {
                            sumpay_amt -= dsList.DATA[i].ITEMPAY_AMT;
                        }
                        if (vatFlag == 1)
                        {
                            dsList.DATA[i].VAT_FLAG = 1;
                            sumAmt = (itempay_amt * 100) / 107;
                            sumAmt = Math.Round(sumAmt, 2);//ค่าของไม่รวมvat
                            sumpayprice_amt += sumAmt;//ค่าของรวมไม่รวมvat
                            sumAmt = (sumAmt * 7) / 100;
                            sumAmt = Math.Round(sumAmt, 2);
                            try
                            {
                                dsList.DATA[i].VAT_AMT = sumAmt;
                            }
                            catch (Exception ex)
                            {
                                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                            }

                        }
                        else
                        {
                            dsList.DATA[i].VAT_FLAG = 0;
                            dsList.DATA[i].VAT_AMT = 0;
                        }
                        if (taxFlag == 1)
                        {
                            dsList.DATA[i].TAX_FLAG = 1;
                            payTax = (sumAmt * taxRate);
                            sumTax += payTax;
                            dsList.DATA[i].TAX_AMT = payTax;
                            dsList.DATA[i].TAX_CODE = dsMain.DATA[0].TAX_CODE;
                        }
                        else
                        {
                            dsList.DATA[i].TAX_FLAG = 0;
                            dsList.DATA[i].TAX_AMT = 0;
                            dsList.DATA[i].TAX_CODE = 0;
                        }
                        dsList.DATA[i].ITEMPAYAMT_NET = itempay_amt - payTax;
                        if (dsList.DATA[i].OPERATE_FLAG == 1)
                        {
                            sumAmtNet += dsList.DATA[i].ITEMPAYAMT_NET;
                        }
                        else
                        {
                            sumAmtNet -= dsList.DATA[i].ITEMPAYAMT_NET;
                        }
                        sumVat += sumAmt;
                    }
                }
                dwTax.ResetRow();
                memberFlag = dsMain.DATA[0].MEMBER_FLAG;
                if (taxFlag == 1 && memberFlag != 3)
                {
                    string[] ar_detail = financeFunction.of_getaddress(state.SsCoopControl, memberFlag, dsMain.DATA[0].MEMBER_NO);
                    dwTax.DATA[0].TAXPAY_ID = ar_detail[1];
                    dwTax.DATA[0].TAXPAY_ADDR = ar_detail[0];
                }
                //คิดยอด net
                if (vatFlag == 0) { sumVat = 0; }
                if (taxFlag == 0) { sumTax = 0; }
                dsMain.DATA[0].ITEMPAY_AMT = sumpay_amt;
                dsMain.DATA[0].ITEM_AMTNET = sumAmtNet;
                dsMain.DATA[0].VAT_AMT = sumVat;
                dsMain.DATA[0].TAX_AMT = sumTax;
                //SumItemAmt();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        public void SaveWebSheet()
        {
            try
            {
                of_CheckSave();
                Boolean lbl_fin = of_CheckSave();
                if (lbl_fin == false)
                {
                    return;
                }
                else
                {
                    InsertData();
                    dsList.ResetRow();
                    dsMain.ResetRow();
                    PostProtect();
                    LoadBegin();
                }
            }
            catch (Exception err)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ " + err.Message);
            }
        }
        public void InsertData()
        {
            ExecuteDataSource exedinsert = new ExecuteDataSource(this);
            try
            {
                string ls_receipt = "", ls_payno = "", sqlStr = "", ls_slipno = "", ls_cashdetail = "";
                decimal ln_payrecv = dsMain.DATA[0].PAY_RECV_STATUS;
                string ls_cashtype = dsMain.DATA[0].CASH_TYPE;
                string ls_accid_bank = "";
                decimal bank_status = 0;
                //เลขที่ใบเสร็จ

                //เลขที่ใบเสร็จ
                if (ln_payrecv == 1)
                {
                    if (dsMain.DATA[0].MEMBER_FLAG == 8)
                    {
                        ls_receipt = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNRECEIPTNO_FIN");
                    }
                    else
                    {
                        ls_receipt = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNRECEIPTNO");
                    }
                }
                else
                {
                    ls_payno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNPAYSLIPNO");
                }
                if (dsMain.DATA[0].PAYMENT_STATUS == 8)
                {
                    //รายการที่มาจากระบบอื่น ๆๆ เพื่อ รอยืนยัน 
                    string slip_no = dsMain.DATA[0].SLIP_NO.Trim();
                    HdSlip_no.Value = ls_slipno;
                    sqlStr = @"update	finslip
	                                set			payment_status  	= 1 ,
				                                entry_id			= {2} ,
				                                receive_status		= 1 ,
				                                recvpay_id			= {2} ,
				                                recvpay_time		= {3},
				                                receipt_no			= {4},
				                                cash_type			= {5},
				                                tofrom_accid		= {6},
                                                payslip_no          = {7},
                                                payment_desc        = {8}				
	                                where	rtrim(ltrim (slip_no))		=  {1}
                                            and coop_id         = {0} ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, slip_no, state.SsUsername, DateTime.Now, ls_receipt,
                       ls_cashtype, dsMain.DATA[0].TOFROM_ACCID, ls_receipt, dsMain.DATA[0].PAYMENT_DESC);
                    //WebUtil.ExeSQL(sqlStr);
                    exedinsert.SQL.Add(sqlStr);

                    sqlStr = @" update FINSLIPDET set SLIPITEMTYPE_CODE={3},ACCOUNT_ID={3},slipitem_desc={4}
                    where coop_id={0} and slip_no={1} and SEQ_NO={2}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, slip_no, 1, dsList.DATA[0].SLIPITEMTYPE_CODE, dsList.DATA[0].SLIPITEM_DESC);
                    exedinsert.SQL.Add(sqlStr);
                }
                else
                {
                    //รายการที่เกิดจากระบบการเงิน
                    ls_slipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNRECEIVENO");
                    HdSlip_no.Value = ls_slipno;
                    decimal[] ar_func = financeFunction.of_getattribconstant(state.SsCoopControl, ls_cashtype);
                    string[] ar_func_memgroup = financeFunction.of_init_payrecv_member(state.SsCoopControl, dsMain.DATA[0].MEMBER_FLAG, dsMain.DATA[0].MEMBER_NO);
                    if (dsMain.DATA[0].PAY_RECV_STATUS == 0)
                    {
                        if (ar_func[3] == 1 && ls_cashtype == "CHQ")
                        {
                            dsMain.DATA[0].PAYMENT_STATUS = 1;
                            dsMain.DATA[0].CHEQUE_STATUS = 1;
                        }
                        else
                        {
                            if (ls_cashtype == "CHQ")
                            {
                                dsMain.DATA[0].PAYMENT_STATUS = 8;
                                dsMain.DATA[0].CHEQUE_STATUS = 1;
                            }
                        }
                    }

                    //"sysdate"
                    object[] objArray = new object[44];
                    objArray[0] = state.SsCoopId; objArray[1] = state.SsUsername; objArray[2] = state.SsWorkDate;
                    objArray[3] = dsMain.DATA[0].OPERATE_DATE; objArray[4] = dsMain.DATA[0].FROM_SYSTEM; objArray[5] = dsMain.DATA[0].PAYMENT_STATUS;
                    objArray[6] = ls_cashtype; objArray[7] = dsMain.DATA[0].PAYMENT_DESC; objArray[8] = dsMain.DATA[0].BANK_CODE;
                    objArray[9] = dsMain.DATA[0].BANK_BRANCH; objArray[10] = dsMain.DATA[0].ITEMPAY_AMT; objArray[11] = dsMain.DATA[0].PAY_TOWHOM;
                    objArray[12] = dsMain.DATA[0].MEMBER_NO; objArray[13] = ln_payrecv; objArray[14] = dsMain.DATA[0].NONMEMBER_DETAIL.Trim();
                    objArray[15] = state.SsIsLocalIp; objArray[16] = dsMain.DATA[0].BANKFEE_AMT; objArray[17] = dsMain.DATA[0].BANKSRV_AMT;
                    objArray[18] = dsMain.DATA[0].TOFROM_ACCID; objArray[19] = "refslipno"; objArray[20] = dsMain.DATA[0].REF_SYSTEM;
                    objArray[21] = ls_receipt; objArray[22] = dsMain.DATA[0].REMARK; objArray[23] = dsMain.DATA[0].FROM_BANKCODE;
                    objArray[24] = dsMain.DATA[0].LOANAPPV_AMT; objArray[25] = dsMain.DATA[0].SHARE_AMT; objArray[26] = dsMain.DATA[0].SHARESPX_AMT;
                    objArray[27] = dsMain.DATA[0].TAX_AMT; objArray[28] = dsMain.DATA[0].TAX_FLAG; objArray[29] = dsMain.DATA[0].TAX_RATE;
                    objArray[30] = dsMain.DATA[0].ITEM_AMTNET; objArray[31] = dsMain.DATA[0].MEMBER_FLAG; objArray[32] = dsMain.DATA[0].ACCUINT_AMT;
                    objArray[33] = ls_payno; objArray[34] = dsMain.DATA[0].ENTRY_ID; objArray[35] = ls_slipno;
                    objArray[36] = dsMain.DATA[0].ACCOUNT_NO; objArray[37] = DateTime.Now; objArray[38] = dsMain.DATA[0].CHEQUE_STATUS;
                    objArray[39] = dsMain.DATA[0].DATEON_CHQ; objArray[40] = ar_func_memgroup[1]; objArray[41] = dsMain.DATA[0].VAT_AMT;
                    objArray[42] = dsMain.DATA[0].RETAIL_FLAG; objArray[43] = dsMain.DATA[0].TAX_CODE;
                    sqlStr = @"    INSERT INTO FINSLIP  
                    (		SLIP_NO,			ENTRY_ID,				ENTRY_DATE,				OPERATE_DATE,   
                            FROM_SYSTEM,		PAYMENT_STATUS,		    CASH_TYPE,				PAYMENT_DESC,   
                            BANK_CODE,			BANK_BRANCH,			ITEMPAY_AMT,			PAY_TOWHOM,   
                            DATEON_CHQ,			MEMBER_NO,				ITEMPAYTYPE_CODE,		PAY_RECV_STATUS,   
                            MEMBER_FLAG,		NONMEMBER_DETAIL,	    coop_id,				MACHINE_ID,   
                            CANCEL_ID,			CANCEL_DATE,			BANKFEE_AMT,			BANKSRV_AMT,
                            TOFROM_ACCID,		ACCOUNT_NO,				CHEQUEBOOK_NO,			CHQ_ADVFLAG,
                            REF_SLIPNO,			REF_SYSTEM,				FROM_ACCNO,				RECEIPT_NO,
                            REMARK,				FROM_BANKCODE,			TAX_CODE,		        loanappv_amt,
                            SHARE_AMT,			emer_amt,				norm_amt,				extra_amt,
                            sharespx_amt,		tax_amt,				tax_flag,				tax_rate,
                            item_amtnet,		membgroup_code,		    receive_status,			accuint_amt,
                            retail_flag,		payslip_no,			    recvpay_id ,			vat_amt,
                            print_status,       recvpay_time,           cheque_status
                    )  
                    VALUES
                    ( 		{35},   			{1},					{2},				    {3},
                            {4},		        {5},			        {6},					{7},
                            {8},		        {9},		            {10},					{14},
                            {39},   			{12},			        '',					    {13},
                            {31},			    {14},				    {0},				    {15},   
                            null,				null,					{16},				    {17},
                            {18},		        {36},				    null,					null,
                            {19},	            {20},			        null,			        {21},   
                            {22},				{23},			        {43},					{24},
                            {25},				   0,					   0,					   0,
                            {26},			    {27},					{28},					{29},
                            {30},			    {40},			         '1',					{32},
                            {42},	            {33},				    {34}, 					{41},
                            0,                  {37},                   {38}
                    ) ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, objArray);
                    //WebUtil.ExeSQL(sqlStr);
                    exedinsert.SQL.Add(sqlStr);
                    decimal ld_payrec = 0; int seqno = 0;
                    string slipitemtype_code = "", slipitem_desc = "";
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        seqno = i + 1;
                        decimal itempay_amt = 0;
                        slipitemtype_code = dsList.DATA[i].SLIPITEMTYPE_CODE;
                        itempay_amt = dsList.DATA[i].ITEMPAY_AMT;
                        slipitem_desc = dsList.DATA[i].SLIPITEM_DESC.Trim();
                        ls_cashdetail += "," + dsList.DATA[i].CASH_DETAIL.Trim();

                        sqlStr = @" INSERT INTO FINSLIPDET  
                        (   SLIP_NO,                coop_id,            SEQ_NO,                 SLIPITEMTYPE_CODE,          SLIPITEM_DESC,   
                            SLIPITEM_STATUS,        CANCEL_ID,          CANCEL_DATE,            POSTTOVC_FLAG,              VOUCHER_NO,   
                            CANCELTOVC_FLAG,        CANCELVC_NO,        DISPLAYONLY_STATUS,     ITEMPAY_AMT,                ACCOUNT_ID,   
                            SECTION_ID,             VAT_FLAG  ,         TAX_FLAG,               TAX_CODE,                   TAXWAY_KEEP,   
                            vat_amt,                TAX_AMT,            MEMBGROUP_CODE,         OPERATE_FLAG    ,           ITEMPAYAMT_NET       
                        
                        )  
                        VALUES
                        (   {0} ,                   {1} ,               {2},                    {3} ,                       {4} ,
                            1 ,                     null ,              null ,                  0 ,                         null ,      
                            0   ,                   null ,              0 ,                     {5} ,                       {3} ,
                            {6} ,                   {7},                {8} ,                   {9},                        0 ,                          
                           {10} ,                   {11},               null ,                    1 ,                        {12}                        
                        )  ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_slipno, state.SsCoopId, seqno, slipitemtype_code, slipitem_desc,
                                      dsList.DATA[i].ITEMPAY_AMT,
                                      dsList.DATA[i].SECTION_ID, dsList.DATA[i].VAT_FLAG, dsList.DATA[i].TAX_FLAG, dsList.DATA[i].TAX_CODE,
                                      dsList.DATA[i].VAT_AMT, dsList.DATA[i].TAX_AMT, dsList.DATA[i].ITEMPAYAMT_NET);
                        exedinsert.SQL.Add(sqlStr);

                        //CHKPOSTTOBANK           
                        try
                        {
                            string sql = @"SELECT * FROM finconstant where coop_id	= {0}";
                            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
                            WebUtil.ExeSQL(sql);
                            Sdt dt = WebUtil.QuerySdt(sql);
                            if (dt.Next())
                            {
                                bank_status = dt.GetDecimal("bank_status");
                            }
                        }
                        catch { bank_status = 0; }
                        if (bank_status == 1)
                        {
                            ld_payrec = (ln_payrecv == 1) ? -1 : 1;
                            ls_accid_bank = slipitemtype_code;
                            //if (ls_cashtype != "CSH")
                            //{

                            //}
                            //else { ld_payrec = (ln_payrecv == 0) ? 1 : -1; }

                            decimal ld_af_balance = 0, ld_seqno = 0, result_bankstatus = 0;
                            decimal ld_itemnetamt = dsList.DATA[i].ITEMPAYAMT_NET;
                            decimal[] rerult_bank = financeFunction.of_init_posttobank(state.SsCoopControl, ls_accid_bank, ld_itemnetamt, ld_payrec);
                            result_bankstatus = rerult_bank[0];
                            ld_af_balance = rerult_bank[2];
                            ld_seqno = rerult_bank[3];
                            string sqlStr_bank = "";
                            //POSTTOBANK      
                            if (result_bankstatus == 1)
                            {
                                sqlStr_bank = @"INSERT INTO	FINBANKSTATEMENT
                            (		COOP_ID,                ENTRY_DATE,                 OPERATE_DATE,
                                    ACCOUNT_NO,				BANK_CODE,			        BANKBRANCH_CODE,
		                            SEQ_NO ,		        DETAIL_DESC,				ENTRY_ID,
		                            BALANCE_BEGIN,			item_amt,					BALANCE,
		                            ITEM_STATUS,			refer_slipno,				MACHINE_ID,
		                            sign_operate
                            )
                            (
	                            SELECT
                                    COOP_ID,	            {10},                           {10},
		                            ACCOUNT_NO,		        BANK_CODE,			            BANKBRANCH_CODE,	
		                            {0},				    {1},		                    {2},					  
		                            {3},					{4},					        {5},	
		                            '1',					{6},				            {7},	
                                    {8}		
                                        from FINBANKACCOUNT WHERE ACCOUNT_ID={9}	AND CLOSE_STATUS  = 0	    
	                            )   ";
                                sqlStr_bank = WebUtil.SQLFormat(sqlStr_bank, ld_seqno, dsMain.DATA[0].NONMEMBER_DETAIL.Trim(), state.SsUsername
                                    , rerult_bank[1], ld_itemnetamt, ld_af_balance
                                    , ls_slipno, state.SsIsLocalIp
                                    , ld_payrec, ls_accid_bank, state.SsWorkDate
                                    );
                                WebUtil.ExeSQL(sqlStr_bank);

                                sqlStr_bank = @"UPDATE	FINBANKACCOUNT
                                SET		balance			= {3},
			                            laststm_seq		= {2}
                                WHERE	( ACCOUNT_ID	= {1} ) AND  
			                            ( COOP_ID	    = {0} ) AND
                                        ( CLOSE_STATUS  = 0)";
                                sqlStr_bank = WebUtil.SQLFormat(sqlStr_bank, state.SsCoopControl, ls_accid_bank, ld_seqno, ld_af_balance);
                                WebUtil.ExeSQL(sqlStr_bank);
                            }
                        }
                    }
                }

                //tax บันทึกข้อมูลหักภาษี 
                if (dsMain.DATA[0].TAX_AMT > 0)
                {
                    DateTime ldtm_date = dsMain.DATA[0].OPERATE_DATE;
                    int li_cancelflag = 0;
                    if (ls_cashtype == "CHQ" && dsMain.DATA[0].CHEQUE_STATUS != 8)
                    {
                        ldtm_date = dsMain.DATA[0].DATEON_CHQ;
                        li_cancelflag = 8;
                    }
                    String ls_chqdate = ldtm_date.ToString("dd/MM/yyyy");
                    string ls_taxslipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNTAXATPAY");
                    sqlStr = @"INSERT INTO FINTAX  
	                    (	TAXDOC_NO,					TAXPAY_NAME,			TAXPAY_ADDR,
		                    TAXPAY_ID,					TAXPAY_DATE,			TAXPAY_DESC,   
		                    MONEY_ALLAMT,				MONEY_AFTAX,			MONEY_TAXAMT,
		                    tax_rate,					tax_code,				tax_accno,
		                    tax_branch,					tax_bank,				slip_no,
		                    cancel_flag,				coop_id
	                    )  
	                    VALUES
	                    (	{0},			            {1},			        {2},
		                    {3},				        {4},				    {5},
		                    {6},				        {7},		            {8},
		                    {9},				        {10},			        '',
		                    {11},				    {12},		        {13},
		                    {14},				    {15}
	                    )   ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, ls_taxslipno, dsMain.DATA[0].NONMEMBER_DETAIL, dwTax.DATA[0].TAXPAY_ADDR,
                        dwTax.DATA[0].TAXPAY_ID, ldtm_date, dwTax.DATA[0].TAXPAY_DESC,
                        dsMain.DATA[0].ITEMPAY_AMT, dsMain.DATA[0].ITEM_AMTNET, dsMain.DATA[0].TAX_AMT,
                        '0', dsMain.DATA[0].TAX_CODE,
                        dsMain.DATA[0].BANK_BRANCH, dsMain.DATA[0].BANK_CODE, ls_slipno,
                        li_cancelflag, state.SsCoopId);
                    exedinsert.SQL.Add(sqlStr);
                }
                //CHKPOSTTOBANK   
                if (bank_status == 1)
                {
                    ls_accid_bank = dsMain.DATA[0].TOFROM_ACCID;
                    ln_payrecv = (ln_payrecv == 1) ? 1 : -1;
                    decimal ld_itemnetamt = dsMain.DATA[0].ITEM_AMTNET;
                    decimal ld_af_balance = 0, ld_seqno = 0, result_bankstatus = 0;
                    decimal[] rerult_bank = financeFunction.of_init_posttobank(state.SsCoopControl, ls_accid_bank, ld_itemnetamt, ln_payrecv);
                    result_bankstatus = rerult_bank[0];
                    ld_af_balance = rerult_bank[2];
                    ld_seqno = rerult_bank[3];
                    string sqlStr_bank = "";
                    //POSTTOBANK      
                    if (result_bankstatus == 1)
                    {


                        sqlStr_bank = @"INSERT INTO	FINBANKSTATEMENT
                            (		COOP_ID,                ENTRY_DATE,                 OPERATE_DATE,
                                    ACCOUNT_NO,				BANK_CODE,			        BANKBRANCH_CODE,
		                            SEQ_NO ,		        DETAIL_DESC,				ENTRY_ID,
		                            BALANCE_BEGIN,			item_amt,					BALANCE,
		                            ITEM_STATUS,			refer_slipno,				MACHINE_ID,
		                            sign_operate
                            )
                            (
	                            SELECT
                                    COOP_ID,	            {10},                            {10},
		                            ACCOUNT_NO,		        BANK_CODE,			            BANKBRANCH_CODE,	
		                            {0},				    {1},		                    {2},					  
		                            {3},					{4},					        {5},	
		                            '1',					{6},				            {7},	
                                    {8}		
                                        from FINBANKACCOUNT WHERE ACCOUNT_ID={9}	AND CLOSE_STATUS  = 0	    
	                            )   ";
                        sqlStr_bank = WebUtil.SQLFormat(sqlStr_bank, ld_seqno, dsMain.DATA[0].NONMEMBER_DETAIL.Trim(), state.SsUsername
                            , rerult_bank[1], ld_itemnetamt, ld_af_balance
                            , ls_slipno, state.SsIsLocalIp
                            , ln_payrecv, ls_accid_bank, state.SsWorkDate
                            );
                        WebUtil.ExeSQL(sqlStr_bank);

                        sqlStr_bank = @"UPDATE	FINBANKACCOUNT
                                SET		balance			= {3},
			                            laststm_seq		= {2}
                                WHERE	( ACCOUNT_ID	= {1} ) AND  
			                            ( COOP_ID	    = {0} ) AND
                                        ( CLOSE_STATUS  = 0)";
                        sqlStr_bank = WebUtil.SQLFormat(sqlStr_bank, state.SsCoopControl, ls_accid_bank, ld_seqno, ld_af_balance);
                        WebUtil.ExeSQL(sqlStr_bank);
                    }
                }
                if (ls_cashtype == "CSH")
                {
                    decimal ld_status = 0, ld_balance = 0;
                    decimal[] rerult = financeFunction.of_is_teller(state.SsCoopId, state.SsUsername, state.SsWorkDate);
                    decimal seq_no = rerult[3] + 1;
                    ld_balance = rerult[2];
                    decimal itemamt_net = dsMain.DATA[0].ITEM_AMTNET;
                    if (ln_payrecv == 1)
                    {
                        ld_status = 22;
                        ld_balance += itemamt_net;
                    }
                    else
                    {
                        ld_status = 21;
                        ld_balance -= itemamt_net;
                    }
                    //รายละเอียดจำนวนเงินทำรายการ
                    string ls_infer_cashdetail = "";
                    if (dsMain.DATA[0].FROM_SYSTEM == "FIN")
                    {
                        String[] ls_AllCash = ls_cashdetail.Split(',');
                        ls_infer_cashdetail = CashDetail(ls_AllCash);
                    }
                    else
                    {
                        ls_infer_cashdetail = "";
                    }
                    sqlStr = @"INSERT INTO	FINTABLEUSERDETAIL
	                    (	USER_NAME,				APPLICATION,			OPDATEWORK,				SEQNO,
		                    OPDATE,				    STATUS,					AMOUNT,					ITEMTYPE_CODE,
		                    ITEMTYPE_DESC,			DOC_NO,					MEMBER_NO,				AMOUNT_BALANCE,
		                    MONEYTYPE_CODE,		    MONEYTYPE_DESC,	        NAMEAPPORVE,			NAMETABLE,
		                    group_type,				ref_docno,			    ref_status,				coop_id, cash_detail
	                    )  
	                    VALUES
	                    (	{0},					{1},					{2},			            {3},
		                    {4},		            {5},			        {6},				        {7},
		                    {8},				    {9},		            {10},					    {11},
		                    'CSH',					{8},					{0},					    {0},
		                    null,					{9},				    {5},					    {12},   {13}
	                    )   ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsUsername, dsMain.DATA[0].FROM_SYSTEM, state.SsWorkDate, seq_no,
                        DateTime.Now, ld_status, itemamt_net, dsMain.DATA[0].ITEMPAYTYPE_CODE,
                        dsMain.DATA[0].PAYMENT_DESC, ls_slipno, dsMain.DATA[0].MEMBER_NO, ld_balance,
                        state.SsCoopId, ls_infer_cashdetail);
                    //WebUtil.ExeSQL(sqlStr);
                    exedinsert.SQL.Add(sqlStr);

                    sqlStr = @"update FINTABLEUSERMASTER 
                            set AMOUNT_BALANCE = {3} ,
                                laststm_no = {4}
                            where   USER_NAME 			= {2}
                            AND		OPDATEWORK 			= {1}
                            and		coop_id				= {0}";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, state.SsWorkDate, state.SsUsername
                     , ld_balance, seq_no);
                    //WebUtil.ExeSQL(sqlStr);
                    exedinsert.SQL.Add(sqlStr);

                    //CHKPOSTTOBANK   
                    ls_accid_bank = dsList.DATA[0].SLIPITEMTYPE_CODE;
                }
                else if (ls_cashtype == "CHQ" && ln_payrecv == 1)
                {
                    string ls_bank = "", ls_bankbranch = "", ls_account_no = "";
                    int ln_seqno = 0;
                    string sql = @"SELECT FINBANKACCOUNT.BANK_CODE AS BANK_CODE,
                    FINBANKACCOUNT.BANKBRANCH_CODE AS BANKBRANCH_CODE,FINBANKACCOUNT.ACCOUNT_NO AS ACCOUNT_NO,
                    COUNT(FINCHECKRETRIEVE.SEQ_NO)+1 AS SEQ_NO
                    FROM FINBANKACCOUNT LEFT JOIN FINCHECKRETRIEVE ON 
                    FINBANKACCOUNT.BANK_CODE = FINCHECKRETRIEVE.BANK_CODE AND
                    FINBANKACCOUNT.BANKBRANCH_CODE = FINCHECKRETRIEVE.BANKBRANCH_CODE AND
                    FINBANKACCOUNT.ACCOUNT_NO = FINCHECKRETRIEVE.TOBANK_ACCNO
                    WHERE FINBANKACCOUNT.COOP_ID = {0} AND FINBANKACCOUNT.CLOSE_STATUS=0 AND FINBANKACCOUNT.ACCOUNT_ID = {1}
                    GROUP BY FINBANKACCOUNT.BANK_CODE,FINBANKACCOUNT.BANKBRANCH_CODE,FINBANKACCOUNT.ACCOUNT_NO";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].TOFROM_ACCID);
                    Sdt dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        //คู่บัญชีตรงกับข้อมูลธนาคาร
                        ls_bank = dt.GetString("BANK_CODE");
                        ls_bankbranch = dt.GetString("BANKBRANCH_CODE");
                        ls_account_no = dt.GetString("ACCOUNT_NO");
                        ln_seqno = dt.GetInt32("SEQ_NO");
                        string ls_checkno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNCHECKRETRIEVE");
                        sqlStr = @" INSERT INTO FINCHECKRETRIEVE  
                        (
                        COOP_ID,              	CHECK_NO,           BANK_CODE,      BANKBRANCH_CODE,    SEQ_NO,         CHECKDUE_DATE,   
                        CHECKCLEAR_STATUS,      ENTRY_DATE,         ENTRY_ID,       ENTRY_TIME,         CHEQUE_AMT,     REFER_DOCNO,   
                        REFERDOC_NAME,   		NORMAL_DEPT,	    SLIP_NO, 		TOBANK_ACCNO,       REFER_APP,      POST_TOBANK                        
                        )
                        VALUES
                        ( {0}                   ,{1}                , {2}           , {3}              , {4}            ,{5}
                        , 0                     , {5}               , {6}           , {7}              , {8}            ,{9}
                        , {10}                  , 1                 , {9}           , {11}             , 'FIN'          ,8
                        )";
                        sqlStr = WebUtil.SQLFormat(sqlStr
                        , state.SsCoopId, ls_checkno, ls_bank, ls_bankbranch, ln_seqno, state.SsWorkDate
                        , state.SsUsername, DateTime.Now, dsMain.DATA[0].ITEM_AMTNET, ls_slipno
                        , dsMain.DATA[0].NONMEMBER_DETAIL, ls_account_no);
                        exedinsert.SQL.Add(sqlStr);
                    }
                }
                exedinsert.Execute();
                exedinsert.SQL.Clear();
                HdPrintReceipt.Value = "true";
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");

            }
            catch (Exception err)
            {
                HdPrintReceipt.Value = "false";
                exedinsert.SQL.Clear();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้ " + err.Message);
            }
        }
        private Boolean of_CheckSave()
        {
            financeFunction.ResultClass classChk = new financeFunction.ResultClass();
            try
            {
                string ls_coopid = state.SsCoopId;
                DateTime work_date = state.SsWorkDate;
                classChk = financeFunction.of_is_openday(ls_coopid, work_date);
                if (classChk.text != "")
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(classChk.text); return false;
                }

                decimal[] rerult = financeFunction.of_is_teller(ls_coopid, state.SsUsername, work_date);
                if ((rerult[0] == 0) || rerult[0] == 1 && rerult[1] == 14)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้ ยังไม่ได้เปิดลิ้นชัก หรือได้ปิดไปแล้วของ " + state.SsUsername);
                    return false;
                }

            }
            catch
            {

                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถตรวจสอบการเปิดวันได้"); return false;
            }
            return true;
        }

        /// <summary>
        /// รายละเอียดจำนวนเงินทำรายการ
        /// </summary>
        /// <param name="listcash"></param>
        /// <returns></returns>
        public static string CashDetail(string[] listcash)
        {
            string ls_cashdetail = "";
            int ii = 0;
            int n_b1000 = 0, n_b500 = 0, n_b100 = 0, n_b50 = 0, n_b20 = 0, n_c10 = 0, n_c5 = 0, n_c2 = 0, n_c1 = 0, n_c50 = 0, n_c25 = 0;
            foreach (string myList in listcash)
            {
                string[] ls_numcash = myList.Split('@');
                switch (ls_numcash[0])
                {
                    case "1000":
                        n_b1000 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "500":
                        n_b500 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "100":
                        n_b100 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "50":
                        n_b50 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "20":
                        n_b20 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "10":
                        n_c10 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "5":
                        n_c5 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "2":
                        n_c2 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "1":
                        n_c1 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "050":
                        n_c50 += Convert.ToInt16(ls_numcash[1]);
                        break;
                    case "025":
                        n_c25 += Convert.ToInt16(ls_numcash[1]);
                        break;
                }
                ii = ii + 1;
            }
            ls_cashdetail = "1000@" + n_b1000 + ",500@" + n_b500 + ",100@" + n_b100 + ",50@" + n_b50 + ",20@" + n_b20 + ",10@" + n_c10 + ",5@" + n_c5 + ",2@" + n_c2 + ",1@" + n_c1 + ",0.50@" + n_c50 + ",0.25@" + n_c25;
            return ls_cashdetail;

        }
        public void WebSheetLoadEnd()
        {
            //vat 
            if (dsMain.DATA[0].vat_flag == 1)
            {
                dsMain.FindTextBox(0, "vat_amt").Enabled = true;
            }
            else { dsMain.FindTextBox(0, "vat_amt").Enabled = false; }
            //ภาษี
            if (dsMain.DATA[0].TAX_FLAG == 1)
            {
                dsMain.FindDropDownList(0, "tax_code").Enabled = true;
                dsMain.FindTextBox(0, "tax_amt").Enabled = true;
            }
            else
            {
                dsMain.FindDropDownList(0, "tax_code").Enabled = false;
                dsMain.FindTextBox(0, "tax_amt").Enabled = false;
            }

            //chq
            if (dsMain.DATA[0].PAY_RECV_STATUS == 1 && dsMain.DATA[0].CASH_TYPE == "CHQ")
            {
                dsMain.FindTextBox(0, "account_no").Enabled = true;
                dsMain.FindTextBox(0, "dateon_chq").Enabled = true;
                dsMain.FindDropDownList(0, "bank_code").Enabled = true;
                dsMain.FindDropDownList(0, "bank_branch").Enabled = true;
                dsMain.FindDropDownList(0, "cheque_type").Enabled = true;
                dsMain.FindDropDownList(0, "cheque_status").Enabled = true;
            }
            else
            {
                dsMain.FindTextBox(0, "account_no").Enabled = false;
                dsMain.FindTextBox(0, "dateon_chq").Enabled = false;
                dsMain.FindDropDownList(0, "bank_code").Enabled = false;
                dsMain.FindDropDownList(0, "bank_branch").Enabled = false;
                dsMain.FindDropDownList(0, "cheque_type").Enabled = false;
                dsMain.FindDropDownList(0, "cheque_status").Enabled = false;
            }
        }
    }
}