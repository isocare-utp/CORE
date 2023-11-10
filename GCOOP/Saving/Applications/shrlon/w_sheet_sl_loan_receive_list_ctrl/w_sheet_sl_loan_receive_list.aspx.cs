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

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl
{
    public partial class w_sheet_sl_loan_receive_list : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostShowData { get; set; }
        [JsPostBack]
        public string PostTypeShow { get; set; }
        [JsPostBack]
        public string PostSearchLoan { get; set; }
        [JsPostBack]
        public string PostShowDataGroup { get; set; }
        [JsPostBack]
        public string PostShowDataGroupEntry { get; set; }
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostCheck { get; set; }
        [JsPostBack]
        public string PostPrintpay { get; set; }
        [JsPostBack]
        public string PostLoopSave { get; set; }
        [JsPostBack]
        public string PostReport { get; set; }
        [JsPostBack]
        public string PostPrintReceiptPay { get; set; }
        [JsPostBack]
        public string PostPrintReceipt { get; set; }

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
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostShowData)
            {
                dsList.RetrieveList();
            }
            else if (eventArg == PostShowDataGroup)
            {
                string group = dsMain.DATA[0].GROUP;
                dsList.RetrieveTypeGroup(group);
            }
            else if (eventArg == PostShowDataGroupEntry)
            {
                decimal payavd_flag = 0;
                string group = "", entry = "";
                string status = "";
                DateTime slip_date = new DateTime();

                try
                {
                    status = dsMain.DATA[0].STATUS;
                    payavd_flag = dsMain.DATA[0].PAYADVANCE_FLAG;
                    slip_date = dsMain.DATA[0].SLIP_DATE;
                }
                catch { }

                if (dsMain.DATA[0].GROUP == "0")
                {
                    group = "%";
                }
                else
                {
                    group = dsMain.DATA[0].GROUP + "%";
                }

                entry = "%" + dsMain.DATA[0].ENTRY_ID + "%";

                if (status == "0")
                {
                    dsList.RetrieveListEntryGroup(group, entry);
                }
                else if (status == "1")
                {
                    dsList.RetrieveListEntryApvContno(group, entry);
                }
                else if (status == "2")
                {
                    dsList.RetrieveListEntryApvNoContno(group, entry);
                }
                else if (status == "3")
                {
                    if (payavd_flag == 0)
                    {
                        dsList.RetrieveListEntryPrintPay(group, entry);
                    }else if(payavd_flag==1){
                        dsList.RetrieveListEntryPrintPayDate(group, entry, slip_date);
                    }
                }
                else
                {
                    dsList.RetrieveListEntryGroup(group, entry);
                }

            }
            else if (eventArg == PostTypeShow)
            {

                string type_show = dsMain.DATA[0].TYPE_SHOW;
                if (type_show == "0")
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        dsList.DATA[i].OPERATE_FLAG = 1;
                    }
                }
                else if (type_show == "1")
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        dsList.DATA[i].OPERATE_FLAG = 0;
                    }
                }
            }
            else if (eventArg == PostSearchLoan)
            {
                string sqlext_con = HdCon.Value;
                string sqlext_req = HdReq.Value;
                HdCon.Value = "";
                HdReq.Value = "";

                dsList.RetrieveSearch(sqlext_con, sqlext_req);
            }
            else if (eventArg == PostMemberNo)
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].MEMBER_NO = memb_no;
                dsList.RetrieveByMembno(memb_no);

            }
            else if (eventArg == PostPrintpay)
            {
                PrintPay();
            }
            else if (eventArg == PostLoopSave)
            {
                LoopSave();
            }
            else if (eventArg == PostReport)
            {
                ProcessReport();
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            decimal payadv_flag = dsMain.DATA[0].PAYADVANCE_FLAG;
            if (payadv_flag == 1)
            {

            }
            else if (payadv_flag == 0)
            {
                dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
            }


            try //กำหนดค่าสีพื้นหลัง
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].LOANREQUEST_STATUS == 12)
                    {
                        setcolor_row(i);
                    }
                }
            }
            catch { }

            try
            {
                if (state.SsCoopControl != "008001")
                {
                    dsMain.IsShow = "hidden";
                    dsMain.FindButton(0, "b_loop_save").Style.Add(HtmlTextWriterStyle.Visibility, "hidden");//.Visible = false;
                    dsMain.FindTextBox(0, "slip_date").Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                }

            }
            catch { }
        }

        public void PrintPay()
        {
            try
            {
                string sqlSelLnreqClr = "", sqlUpdateLnreq = "", loanrequest_docno = "", cont_no = "", sqlUpdateInt = "";
                DateTime calint_to;
                decimal intclear_amt = 0, principal_balance = 0, sum_clear = 0;
                if (dsMain.DATA[0].PAYADVANCE_FLAG == 1)
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        if (dsList.DATA[i].OPERATE_FLAG == 1 && dsList.DATA[i].LNRCVFROM_CODE == "REQ")
                        {
                            sum_clear = 0;
                            calint_to = dsMain.DATA[0].SLIP_DATE;
                            loanrequest_docno = dsList.DATA[i].LOANCONTRACT_NO; //loancontract_no ของ dslist ที่เป็น REQ คือ loanrequest_docno จาก db

                            //ค้นหาสัญญาเก่าที่จะหักกลบมาคำวนวนดอกเบี้ย
                            sqlSelLnreqClr = "select * from lnreqloanclr where coop_id={0} and loanrequest_docno={1} and clear_status=1";
                            sqlSelLnreqClr = WebUtil.SQLFormat(sqlSelLnreqClr, state.SsCoopId, loanrequest_docno);
                            Sdt dtSelLnreqClr = WebUtil.QuerySdt(sqlSelLnreqClr);
                            while (dtSelLnreqClr.Next())
                            {
                                cont_no = dtSelLnreqClr.GetString("loancontract_no");
                                intclear_amt = wcf.NShrlon.of_computeinterest(state.SsWsPass, state.SsCoopId, cont_no, calint_to); //คำนวณดอกเบี้ย
                                principal_balance = dtSelLnreqClr.GetDecimal("principal_balance");
                                sum_clear += (principal_balance + intclear_amt); //รวมยอดเงินต้นและดอกเบี้ยของสัญญาเก่าที่จะหักกลบ

                                //อัพเดท ดอกเบี้ยของแต่ละสัญญา
                                sqlUpdateInt = "update lnreqloanclr set intclear_amt={0} where coop_id={1} and loancontract_no={2} and loanrequest_docno={3}";
                                sqlUpdateInt = WebUtil.SQLFormat(sqlUpdateInt, intclear_amt, state.SsCoopId, cont_no, loanrequest_docno);
                                Sdt dtUpdateInt = WebUtil.QuerySdt(sqlUpdateInt);

                            }

                            // อัพเดทสถานะให้ใบคำขอ สถานะ 12 และยอดรวมที่จะหักกลบทั้งหมด
                            sqlUpdateLnreq = "update lnreqloan set loanrequest_status=12 ,sum_clear={0} where coop_id={1} and loanrequest_docno={2}";
                            sqlUpdateLnreq = WebUtil.SQLFormat(sqlUpdateLnreq, sum_clear, state.SsCoopId, loanrequest_docno);
                            Sdt dtUpdateLnreq = WebUtil.QuerySdt(sqlUpdateLnreq);

                        }

                    }
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่ได้เลือกจ่ายล่วงหน้ากรุณาตรวจสอบ");
                }

            }
            catch { LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ"); }

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
            dsList.FindTextBox(index_row, "slip_date").BackColor = myRgbColor;
        }

        //วนเซฟ
        private void LoopSave()
        {
            //วนฟอลูปเพื่อดุว่าเลือกแถวไหนบ้าง
            //ถ้าเลือกให้เรียกinitและเรียกเซฟเลย
            try
            {
                int ii = 0;
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].OPERATE_FLAG == 1)
                    {
                        str_slippayout sSlipPayOut = new str_slippayout();
                        sSlipPayOut.coop_id = state.SsCoopId;
                        sSlipPayOut.contcoop_id = state.SsCoopControl;
                        sSlipPayOut.operate_date = state.SsWorkDate;
                        sSlipPayOut.loancontract_no = dsList.DATA[i].LOANCONTRACT_NO;
                        sSlipPayOut.slip_date = state.SsWorkDate;

                        String initfrom_type = dsList.DATA[i].LNRCVFROM_CODE;
                        sSlipPayOut.initfrom_type = initfrom_type;

                        wcf.NShrlon.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);

                        DataSet ds0 = new DataSet();
                        StringReader strd = new StringReader(sSlipPayOut.xml_sliphead);
                        ds0.ReadXml(strd);
                        DataTable dt0 = ds0.Tables[0];
                        dt0.TableName = "sliphead";
                        bool isCSH = false;
                        try
                        {
                            if (dt0.Rows[0]["MONEYTYPE_CODE"].ToString() == "CSH")
                            {
                                isCSH = true;
                            }
                        }
                        catch { }

                        if (isCSH)
                        {
                            try
                            {
                                int fincash_status = 1;
                                string sql = @"select count(1) from fincash_control where entry_id = {0} and operate_date = {1} and status = 14";
                                sql = WebUtil.SQLFormat(sql, state.SsUsername, state.SsWorkDate);
                                Sdt dt = WebUtil.QuerySdt(sql);
                                if (dt.Next())
                                {
                                    fincash_status = dt.GetInt32("count(1)");
                                    if (fincash_status != 1)
                                    {
                                        //NextLoan();
                                    }
                                    else
                                    {
                                        LoopSave2(ref sSlipPayOut);
                                        ii++;
                                    }
                                }
                            }
                            catch (Exception ex) { throw new Exception("ไม่สามารถตรวจสอบสถานะลิ้นชักได้ " + ex.Message); }
                        }
                        else
                        {
                            LoopSave2(ref sSlipPayOut);
                            ii++;
                        }

                    }

                }

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ " + ii + " รายการ");

                try
                {
                    this.SetOnLoadedScript("GetShowData();");
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        private void LoopSave2(ref str_slippayout sSlipPayOut)
        {

            int result = wcf.NShrlon.of_saveslip_lnrcv(state.SsWsPass, ref sSlipPayOut);
            //if (result == 1)
            //{
            //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");

            //}

        }

        private void ProcessReport()
        {

            try
            {
                decimal payavd_flag = 0;
                string report_name = "", report_label = "";
                string str_ref = "";

                str_ref = HdRefno.Value;
                payavd_flag = dsMain.DATA[0].PAYADVANCE_FLAG;
                if (payavd_flag == 0)
                {
                    report_name = "r_fin_slippayout";
                }

                iReportArgument args = new iReportArgument();
                args.Add("slippayout_no", iReportArgumentType.String, str_ref);
                //args.Add("bankCode", iReportArgumentType.String, rbankcode[0]);
                //args.Add("bankBranch", iReportArgumentType.String, rbranchcode[0]);
                iReportBuider report = new iReportBuider(this, "");
                report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();
            }
            catch { }

        }
        public void JsPrintSlippayOut()
        {
            string rslip = "";
            string printtype = "iReport";

            rslip = "'" + HdPayoutNo.Value + "'";

            if (rslip != "" && rslip.Length > 8)
            {
                try
                {
                    if (printtype == "iReport")
                    {
                        string sql = "";
                        iReportArgument args = new iReportArgument(sql);
                        iReportBuider report = new iReportBuider(this, "พิมพ์ใบเสร็จ");
                        report.AddCriteria("sr_slip_payment", "PDF พิมพ์ใบเสร็จ", ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();
                    }
                    else if (printtype == "JsSlip")
                    {

                        string coop_id = state.SsCoopControl;
                        Printing.PrintSlipSlpayin(this, rslip, coop_id);
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

            }

        }
        public void PrintSlippayout(string payoutslip_no)
        {

            string sql = @"select
mp.prename_desc,
mb.memb_name,
mb.memb_surname,
so.loancontract_no,
so.shrlontype_code,
so.payoutslip_no,
so.member_no,
so.payout_amt,
so.returnetc_amt ,
so.payoutnet_amt,
so.moneytype_code,
so.expense_bank,
cb.bank_desc,
so.expense_accid,
trim(TO_CHAR( so.slip_date, 'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI')) as slip_date ,
nvl(si.payinslip_no,'n/a') as payinslip_no,
nvl(si.document_no,'n/a') as document_no,
nvl(sum(sid.principal_payamt),0) as sum_princ,
nvl(sum(sid.interest_payamt),0) as sum_int,
sum(CASE sid.slipitemtype_code WHEN 'LON' THEN sid.item_payamt ELSE 0 end   ) as item_lon,
sum(CASE sid.slipitemtype_code WHEN 'MUT' THEN sid.item_payamt ELSE 0 end   ) as item_mut,
nvl((LN_SRV.OF_GETCONTCLR(so.payoutslip_no)),'n/a') as list_cont,
nvl((LN_SRV.OF_GETCONTCLRDAY(so.payoutslip_no)),'n/a') as list_contday

from slslippayout so,slslippayin si ,slslippayindet sid,mbmembmaster mb ,mbucfprename mp,cmucfbank cb
where so.payoutslip_no = {0}
and so.slipclear_no = si.payinslip_no(+)
and si.payinslip_no = sid.payinslip_no(+)
and so.coop_id = si.coop_id(+)
and so.member_no = si.member_no(+)
and si.coop_id = sid.coop_id (+)
and so.member_no = mb.member_no
and mb.prename_code = mp.prename_code
and so.expense_bank = cb.bank_code(+)
group by mp.prename_desc,
mb.memb_name,
mb.memb_surname,
so.loancontract_no,
so.shrlontype_code,
so.payoutslip_no,
so.member_no,
so.payout_amt,
so.returnetc_amt ,
so.payoutnet_amt,
so.moneytype_code,
so.expense_bank,
cb.bank_desc,
so.expense_accid,
so.slip_date,
si.payinslip_no,
si.document_no
,(LN_SRV.OF_GETCONTCLR(so.payoutslip_no))
,(LN_SRV.OF_GETCONTCLRDAY(so.payoutslip_no))";

            sql = WebUtil.SQLFormat(sql, payoutslip_no);
            DataTable data = WebUtil.Query(sql);
            DataTable data2 = WebUtil.Query(sql);
            string shrlontype_code = "";
            foreach (DataRow row in data2.Rows)
            {
                shrlontype_code = row["shrlontype_code"].ToString();

            }


            if (shrlontype_code.Trim() == "10")
            {
                Printing.PrintAppletPB(this, "sl_slip_payout_emer", data);
            }
            else
            {
                Printing.PrintAppletPB(this, "sl_slip_payout", data);
            }
        }

        public void PrintSlippayin(string payinslip_no)
        {
            string sql = "";
            sql = @"SELECT pin.PAYINSLIP_NO,
TO_CHAR( pin.SLIP_DATE, 'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') as TSLIPDATE,
pre.PRENAME_DESC || mas.MEMB_NAME || ' ' || mas.MEMB_SURNAME AS FULLNAME,
mas.MEMBER_NO,
mgrp.MEMBGROUP_DESC as memgroup,
pin.INTACCUM_AMT,
decode(pindet.slipitemtype_code,
'SHR','ซื้อหุ้นพิเศษ',
'LON','ชำระหนี้ '||nvl(pindet.loancontract_no,''),
'MUT',pindet.SLIPITEM_DESC,'') as slip_desc,
pindet.PERIOD,
pindet.PRINCIPAL_PAYAMT,
pindet.INTEREST_PAYAMT,
pindet.ITEM_PAYAMT,
decode(pindet.slipitemtype_code,'SHR',shr.sharestk_amt*10,pindet.ITEM_BALANCE) as ITEM_BALANCE,
pindet.Shrlontype_Code,
pin.slip_amt as sumitempay,
ftreadtbaht(pin.slip_amt) as tbaht

FROM
SLSLIPPAYIN pin,
MBMEMBMASTER mas,
SHSHAREMASTER shr,
MBUCFPRENAME pre,
SLSLIPPAYINDET pindet,
MBUCFMEMBGROUP mgrp
WHERE (mas.MEMBER_NO=shr.MEMBER_NO ) AND
( mas.MEMBER_NO = pin.MEMBER_NO ) AND
( mas.COOP_ID = pin.MEMCOOP_ID ) AND
( mas.PRENAME_CODE = pre.PRENAME_CODE ) AND
( mas.MEMBGROUP_CODE = mgrp.MEMBGROUP_CODE ) AND
( mas.COOP_ID = mgrp.COOP_ID ) AND
( pin.PAYINSLIP_NO = pindet.PAYINSLIP_NO ) AND
( pin.COOP_ID = pindet.COOP_ID ) AND
( ( pin.PAYINSLIP_NO = {0} ) AND
( pin.COOP_ID = {1}) )
ORDER BY pindet.Shrlontype_Code
";
            sql = WebUtil.SQLFormat(sql, payinslip_no, state.SsCoopId);
            DataTable data = WebUtil.Query(sql);
            Printing.PrintAppletPB(this, "sl_slip_payin", data);
        }

        public void PrintReceiptPay()
        {
            try
            {
                string slippayout_no = "";
                slippayout_no = HdPayoutNo.Value;
                if (slippayout_no != "")
                {
                    PrintSlippayout(slippayout_no);
                    //HdPayoutNo.Value = "";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

        public void PrintReciept()
        {
            try
            {
                string slippayin_no = "";
                slippayin_no = HdPayinslipno.Value;
                if (slippayin_no != "")
                {
                    PrintSlippayin(slippayin_no);
                    //Printing.PrintSlipSlpayin(this, slippayin_no, state.SsCoopId);
                    //HdPayinNo.Value="";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }


        }
    }
}