using System;
using CoreSavingLibrary;
using System.Web.Services.Protocols;
//using CoreSavingLibrary.WcfFinance;
using CoreSavingLibrary.WcfNFinance;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finslip_spc : PageWebSheet, WebSheet
    {
        n_financeClient fin;
        private DwThDate tDwMain;
        protected String postCalTax;
        protected String postPrintTax;
        protected String postRefresh;
        protected String postProtect;
        protected String postPrintSlip;
        protected String postSumItemAmt;
        protected String postInitMember;
        protected String postItemtCalTax;
        protected String postSetItemDesc;
        protected String postDefaultAccid;
        protected String postItemDeleteRow;
        protected String postInsertRow;
        protected String postDeleteRow;
        protected String postBankBranch;
        protected String postFilterItem;
        protected String postInitBegin;
        protected String postGetBankCode;
        protected String postCalVat;
        protected String PostPrint;
        protected String postPrintRecPea;
        protected String postInitMembgrp;
        protected String postNewTaxSum;
        protected String postNewVatSum;

        #region WebSheet Members
        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("entry_date", "entry_tdate");
            tDwMain.Add("operate_date", "operate_tdate");
            tDwMain.Add("dateon_chq", "dateon_chq_tdate");

            postInsertRow = WebUtil.JsPostBack(this, "postInsertRow");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postCalTax = WebUtil.JsPostBack(this, "postCalTax");
            postPrintTax = WebUtil.JsPostBack(this, "postPrintTax");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProtect = WebUtil.JsPostBack(this, "postProtect");
            postPrintSlip = WebUtil.JsPostBack(this, "postPrintSlip");
            postInitMember = WebUtil.JsPostBack(this, "postInitMember");
            postSumItemAmt = WebUtil.JsPostBack(this, "postSumItemAmt");
            postBankBranch = WebUtil.JsPostBack(this, "postBankBranch");
            postItemtCalTax = WebUtil.JsPostBack(this, "postItemtCalTax");
            postSetItemDesc = WebUtil.JsPostBack(this, "postSetItemDesc");
            postDefaultAccid = WebUtil.JsPostBack(this, "postDefaultAccid");
            postItemDeleteRow = WebUtil.JsPostBack(this, "postItemDeleteRow");
            postFilterItem = WebUtil.JsPostBack(this, "postFilterItem");
            postInitBegin = WebUtil.JsPostBack(this, "postInitBegin");
            postGetBankCode = WebUtil.JsPostBack(this, "postGetBankCode");
            postCalVat = WebUtil.JsPostBack(this, "postCalVat");
            PostPrint = WebUtil.JsPostBack(this, "PostPrint");
            postInitMembgrp = WebUtil.JsPostBack(this, "postInitMembgrp");
            postPrintRecPea = WebUtil.JsPostBack(this, "postPrintRecPea");
            postNewTaxSum = WebUtil.JsPostBack(this, "postNewTaxSum");
            postNewVatSum = WebUtil.JsPostBack(this, "postNewVatSum");
        }
        public void WebSheetLoadBegin()
        {
            try
            {
                fin = wcf.NFinance;

                if (!IsPostBack)
                {
                    String recOrPay = "";
                    InitBegin(0, ref recOrPay);
                    PostProtect();
                    SetChildDW();
                    DwTax.InsertRow(0);
                }
                else
                {
                    this.RestoreContextDw(DwMain);
                    this.RestoreContextDw(DwItem);
                    this.RestoreContextDw(DwTax);
                    this.RestoreContextDw(DwMain, tDwMain);
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postSumItemAmt":
                    SumItemAmt();
                    break;
                case "DeleteRow":
                    SumItemAmt();
                    break;
                case "postDefaultAccid":
                    DefaultAccid();
                    break;
                case "postProtect":
                    PostProtect();
                    break;
                case "postCalTax":
                    PostCalTax();
                    break;
                case "postSetItemDesc":
                    PostSetItemDesc();
                    break;

                case "postInitMember":
                    PostInitMember();
                    break;
                case "postPrintSlip":
                    PostPrintSlip();
                    break;
                case "postPrintTax":
                    PrintTax();
                    break;
                case "postItemtCalTax":
                    ItemtCalTax();
                    break;
                case "postCalVat":
                    SumCalVat();
                    break;
                case "postRefresh":
                    //Refresh();
                    SumCalVat();
                    break;
                case "postInsertRow":
                    JsPostInsertRow();
                    break;
                case "postDeleteRow":
                    JsPostDeleteRow();
                    break;
                case "postBankBranch":
                    GetBankBranch();
                    break;
                case "postFilterItem":
                    PostFilterItemtype();
                    break;
                case "postInitBegin":
                    String recOrPay = "";
                    InitBegin(Convert.ToInt16(HStatus.Value), ref recOrPay);
                    break;
                case "postGetBankCode":
                    GetBankCode();
                    break;
                case "postPrintRecPea":
                    PrintRecPea();
                    break;
                case "PostPrint":
                    PostPrintSlip();
                    break;
                case "postInitMembgrp":
                    PostInitMembgrp();
                    break;
                case "postNewTaxSum":
                    PostNewTaxSum();
                    break;
                case "postNewVatSum":
                    PostNewVatSum();

                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                Int16 taxFlag, recvpay_status;
                Decimal sumTax;
                String mainXml = "", itemXml = "", taxXml = "", tofromAccid = "", cashtype = "", member_flag = "", cash_type = "";

                CheckSave();

                try
                {
                    recvpay_status = Convert.ToInt16(DwMain.GetItemDecimal(1, "pay_recv_status"));
                    cash_type = DwMain.GetItemString(1, "cash_type");
                }
                catch { recvpay_status = 9; }

                //recvpay_status รายการจ่าย 0 รายการรับ 1 ไม่เลือก 9
                if (cash_type == "CHQ" && recvpay_status == 0)
                {
                    DwMain.SetItemSqlDecimal(1, "payment_status", 8);
                }

                if (recvpay_status == 9)
                {
                    throw new Exception("กรุณาเลือกประเภทการทำรายการ");
                }

                try
                {
                    taxFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "tax_flag"));
                }
                catch { taxFlag = 0; }

                if (taxFlag == 1)
                {
                    String taxdesc = "", taxpay_id = "", taxaddr = "";
                    try
                    {
                        taxdesc = DwTax.GetItemString(1, "taxpay_desc");
                    }

                    catch { taxdesc = ""; }
                    try
                    {
                        taxpay_id = DwTax.GetItemString(1, "taxpay_id");
                    }
                    catch { taxpay_id = ""; }

                    try
                    {
                        taxaddr = DwTax.GetItemString(1, "taxpay_addr");
                    }
                    catch { taxaddr = ""; }

                    //ว่างๆ เดี๋ยวมาแก้ให้
                    //if (taxdesc == "" || taxaddr == "" || taxpay_id == "")
                    //{
                    //    throw new Exception("ป้อนรายละเอียดภาษีไม่ครบ");
                    //}

                    taxXml = DwTax.Describe("Datawindow.Data.Xml");

                    Htaxpay_id.Value = "";
                    Htaxpay_desc.Value = "";
                    Htaxpay_addr.Value = "";

                    HprintTax.Value = "true";
                }

                sumTax = Convert.ToInt16(DwMain.GetItemDecimal(1, "tax_flag"));
                if (sumTax > 0)
                {
                    HprintTax.Value = "true";
                }
                else
                {
                    HprintTax.Value = "false";
                }

                cashtype = DwMain.GetItemString(1, "cash_type");
                tofromAccid = DwMain.GetItemString(1, "tofrom_accid");
                member_flag = DwMain.GetItemString(1, "member_flag");



                String result;
                if (HDChkDeptFEE.Value == "1")
                {
                    if (recvpay_status == 0)//ตรวจสอบรายการที่ดึงจาก dlg ว่าเป็นรับ/จ่าย
                    {
                        HStatus.Value = "0";
                    }
                    try
                    {
                        mainXml = DwMain.Describe("Datawindow.Data.Xml");
                        itemXml = DwItem.Describe("Datawindow.Data.Xml");

                        result = fin.of_payslip_pea(state.SsWsPass, mainXml, itemXml, taxXml, state.SsApplication);
                    }
                    catch (Exception err)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage(err.Message);
                        result = "";
                    }
                }
                else
                {
                    try
                    {
                        mainXml = DwMain.Describe("Datawindow.Data.Xml");
                        itemXml = DwItem.Describe("Datawindow.Data.Xml");

                        //result = wcf.NFinance.of_payslip(state.SsWsPass, mainXml, itemXml, taxXml, state.SsApplication);
                        result = fin.of_payslip(state.SsWsPass, mainXml, itemXml, taxXml, state.SsApplication);
                    }
                    catch (Exception err)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage(err.Message);
                        //MessageBox("คำเตือน", err.ToString() );
                        result = "";
                    }
                }

                if (result != "")
                {
                    Hslipno.Value = result;
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    HdCoopid.Value = state.SsCoopId;

                    DwMain.Reset();
                    DwItem.Reset();
                    DwTax.Reset();

                    DwMain.InsertRow(0);
                    DwItem.InsertRow(0);

                    Hprintslip.Value = "true";
                    HClear.Value = "true";
                    HCashtype.Value = cashtype;
                    HAccid.Value = tofromAccid;
                    Hmember_flag.Value = member_flag;
                    if (state.SsCoopControl == "009001" || state.SsCoopControl == "006001")
                    { PostPrintSlip(); }

                    String recOrPay = "";
                    InitBegin(0, ref recOrPay);
                    PostProtect();
                    SetChildDW();
                    DwTax.InsertRow(0);
                    //try
                    //{
                    //    DwMain.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                    //    DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                    //    tDwMain.Eng2ThaiAllRow();
                    //}
                    //catch { }
                }
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwItem.SaveDataCache();
            DwTax.SaveDataCache();
            tDwMain.Eng2ThaiAllRow();
            String slipitem_desc;
            String ColumnName, payStatus;

            try
            {
                slipitem_desc = DwItem.GetItemString(1, "slipitem_desc");
            }
            catch
            {
                slipitem_desc = "";
            }

            DwMain.SetItemString(1, "payment_desc", slipitem_desc);
            if (DwItem.RowCount < 1)
            {
                DwMain.SetItemDecimal(1, "item_amtnet", 0);
                DwMain.SetItemDecimal(1, "tax_amt", 0);
                DwMain.SetItemDecimal(1, "vat_amt", 0);
                DwMain.SetItemDecimal(1, "itempay_amt", 0);
            }

            ColumnName = HColumName.Value;
            payStatus = HStatus.Value;

            if (ColumnName == "CHQ" && payStatus == "1")
            {
                DwMain.Modify("datawindow.detail.Height=1104");
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "finslip_spc.pbl", null);
                DwMain.SetItemDateTime(1, "dateon_chq", state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                DwMain.Modify("datawindow.detail.Height=884");
            }
        }
        #endregion

        private void InitBegin(Int16 ai_recvpay, ref String recOrPay)
        {
            try
            {
                Int32 resultXml;
                String mainXml = "";
                try
                {
                    ai_recvpay = 9;
                }
                catch { ai_recvpay = 0; }

                resultXml = fin.of_init_payrecv_slip_1( state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, ai_recvpay,ref mainXml);

                DwTax.Reset();
                DwMain.Reset();
                DwItem.Reset();

                DwMain.ImportString( mainXml, FileSaveAsType.Xml );
                tDwMain.Eng2ThaiAllRow();

                if (HCashtype.Value.Trim() != "")
                {
                    DwMain.SetItemString(1, "cash_type", HCashtype.Value.Trim());
                }

                if (HAccid.Value.Trim() != "")
                {
                    DwMain.SetItemString(1, "tofrom_accid", HAccid.Value.Trim());
                }

                if (Hmember_flag.Value.Trim() != "")
                {
                    DwMain.SetItemDecimal(1, "member_flag", Convert.ToInt16(Hmember_flag.Value.Trim()));
                }

                if (DwItem.RowCount < 1)
                {
                    DwItem.InsertRow(0);
                }

                recOrPay = DwMain.GetItemDecimal(1, "pay_recv_status") == 1 ? "rec" : "pay";

            }
            catch (Exception ex)
            {
                recOrPay = "";
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.WarningMessage(ex));
            }
        }
        private void JsPostDeleteRow()
        {
            int row = int.Parse(HdDetailRow.Value);
            DwItem.DeleteRow(row);
            SumItemAmt();
        }
        private void JsPostInsertRow()
        {
            DwItem.InsertRow(0);
        }
        private void CheckSave()
        {
            try
            {
                try { string member_no = DwMain.GetItemString(1, "member_no"); }
                catch { string membgrp_code = DwMain.GetItemString(1, "membgroup_code").Trim(); }
                string nonmember_detail = DwMain.GetItemString(1, "nonmember_detail");
                string payment_desc = DwMain.GetItemString(1, "payment_desc");
                Decimal item_amtnet = DwMain.GetItemDecimal(1, "item_amtnet");
                string account_id = DwMain.GetItemString(1, "tofrom_accid");
                string slipitem_desc = DwMain.GetItemString(1, "payment_desc");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        private void PostFilterItemtype()
        {
            try
            {
                String accID = "";
                accID = DwItem.GetItemString(Convert.ToInt16(HfRow.Value), "account_id"); // คู่บัญชีที่เลือกใน DwItem
                DataWindowChild dcItem = DwItem.GetChild("slipitemtype_code"); // คู่บัญชีของ DwItem
                // หา row ของคู่บัญชีที่เลือก ใน datawindow Child 
                // แล้ว get ค่าออกมาตาม row ที่หาได้
                DwItem.SetItemString(Convert.ToInt16(HfRow.Value), "slipitemtype_code", "");
                DwItem.SetItemString(Convert.ToInt16(HfRow.Value), "slipitem_desc", "");
                dcItem.SetFilter("account_id='" + accID.Trim() + "'");
                dcItem.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        private String GetBankCode()
        {
            this.ConnectSQLCA();
            String sqlStr, bankcode;

            Sta ta = new Sta(sqlca.ConnectionString);
            Sdt dt = new Sdt();
            String account_id = "";

            account_id = DwMain.GetItemString(1, "tofrom_accid").Trim();

            sqlStr = "select distinct bank_code from finbankaccount where account_id = '" + account_id + "'";

            dt = ta.Query(sqlStr);
            dt.Next();
            try
            {
                bankcode = dt.GetString("bank_code").Trim();
            }
            catch { bankcode = ""; }

            //DwMain.SetItemString(1, "from_bankcode", bankcode);
            DwMain.SetItemString(1, "bank_code", bankcode);

            GetBankBranch();

            this.DisConnectSQLCA();
            return bankcode;
        }
        protected void Refresh()
        {
            Int16 countTaxflag = 0, memberFlag = 0, rownum = 0;
            rownum = Convert.ToInt16(DwItem.RowCount);
            String memberNo = "", columnName = "";
            columnName = HColumName.Value.Trim();
            memberFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "member_flag"));

            if (memberFlag == 4)
            {

            }
            else if (memberFlag != 3)
            {
                memberNo = DwMain.GetItemString(1, "member_no");
            }

            for (int row = 1; row <= rownum; row++)
            {
                try
                {
                    //Int16 taxFlag = Convert.ToInt16(DwMain.GetItemDecimal(row, "tax_flag"));
                    decimal taxFlag = DwMain.GetItemDecimal(row, "tax_flag");
                    decimal taxCode = DwMain.GetItemDecimal(row, "tax_code");

                    if (taxFlag == 0)
                    {
                        DwMain.SetItemDecimal(row, "vat_flag", 0);
                        DwMain.SetItemDecimal(row, "vat_amt", 0);
                        DwMain.SetItemDecimal(row, "tax_amt", 0);
                        DwMain.SetItemDecimal(row, "itempay_amt", 0);
                        //DwMain.Modify("tax_amt.protect=1");
                        //DwMain.Modify("tax_code.protect=1");
                        ItemtCalTax();
                    }
                    else
                    {
                        Int32 resultXml;
                        String tax_addr = "", tax_id = "";
                        DwTax.Reset();
                        DwTax.InsertRow(0);
                        if (memberFlag != 3)
                        {

                            resultXml = fin.of_getaddress(state.SsWsPass, ref tax_addr, ref tax_id, state.SsCoopId, memberNo, memberFlag);
                            DwTax.SetItemString(1, "taxpay_id", tax_id);
                            DwTax.SetItemString(1, "taxpay_addr", tax_addr);
                        }
                        else {

                            DwTax.SetItemString(1, "taxpay_id", Htaxpay_id.Value);
                            DwTax.SetItemString(1, "taxpay_addr", Htaxpay_addr.Value);
                        }
                        ItemtCalTax();
                       
                    }

                    PostProtect();
                }
                catch
                {

                }
            }
        }
        protected void ItemtCalTax()
        {
            int row;
            Int16 rownum = Convert.ToInt16(DwItem.RowCount);
            decimal itemPayAmt = 0, taxFlag = 0, vatamt, taxamt = 0, itemamt_det = 0, vatamt2 = 0;
            Int32 result;
            Int16 taxCode, vatflag, recvpay, tax_flag;

            for (row = 1; row <= rownum; row++)
            {
                try
                {
                    tax_flag = Convert.ToInt16(DwMain.GetItemDecimal(1, "tax_flag"));
                }
                catch
                { tax_flag = 0; }

                try
                {
                    taxCode = Convert.ToInt16(DwMain.GetItemDecimal(1, "tax_code"));
                }
                catch
                { taxCode = 0; }

                try
                {
                    vatflag = Convert.ToInt16(DwMain.GetItemDecimal(1, "vat_flag"));
                }
                catch { vatflag = 0; }

                try
                {
                    recvpay = Convert.ToInt16(DwMain.GetItemDecimal(1, "pay_recv_status"));
                }
                catch { recvpay = 0; }

                try
                {
                    if (vatflag == 1)
                    {
                        //vatamt = DwMain.GetItemDecimal(row, "vat_amt");
                        vatamt = DwItem.GetItemDecimal(row, "vat_amt");
                        itemPayAmt = DwItem.GetItemDecimal(row, "itempay_amt");
                        itemPayAmt = itemPayAmt - vatamt;
                        itemPayAmt = Math.Round(itemPayAmt, 2);
                    }
                    else
                    {
                        //vatamt = DwMain.GetItemDecimal(row, "vat_amt");
                        vatamt = DwItem.GetItemDecimal(row, "vat_amt");
                        itemPayAmt = DwItem.GetItemDecimal(row, "itempay_amt");
                        itemPayAmt = itemPayAmt - vatamt;
                        itemPayAmt = Math.Round(itemPayAmt, 2);
                    }
                }
                catch { itemPayAmt = 0; }

                if (tax_flag != 0 && itemPayAmt > 0)
                {
                    try
                    {
                        try
                        {
                            result = fin.of_itemcaltax(state.SsWsPass, state.SsCoopId, recvpay, vatflag, taxCode, itemPayAmt,ref taxamt,ref itemamt_det,ref vatamt2);
                        }
                        catch
                        {

                        }

                        taxFlag = DwMain.GetItemDecimal(1, "tax_flag");
                        if (taxFlag == 1)
                        {
                            DwItem.SetItemDecimal(row, "tax_amt", taxamt);
                            DwItem.SetItemDecimal(row, "itempayamt_net", itemamt_det);
                            DwItem.SetItemDecimal(row, "vat_amt", vatamt2);
                        }
                    }

                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    }
                }
                else
                {
                    Decimal vat_amt = 0, temp_amt = 0;
                    try
                    {
                        vat_amt = DwItem.GetItemDecimal(row, "vat_amt");
                        temp_amt = DwItem.GetItemDecimal(row, "itempayamt_net");
                    }
                    catch { vat_amt = 0; }
                    if (vat_amt > 0)
                    {
                        itemPayAmt = itemPayAmt + vat_amt;

                        if (itemPayAmt != temp_amt)
                        {
                            itemPayAmt = itemPayAmt - vat_amt;
                        }
                    }
                    try
                    {
                        //HC By Maxim
                        taxCode = 0;
                        result = fin.of_itemcaltax(state.SsWsPass, state.SsCoopId, recvpay, vatflag, taxCode, itemPayAmt,ref taxamt,ref itemamt_det,ref vatamt2);
                    }
                    catch
                    {

                    }

                    taxFlag = DwMain.GetItemDecimal(1, "tax_flag");
                    if (taxFlag == 0)
                    {
                        DwItem.SetItemDecimal(row, "tax_amt", taxamt);
                        DwItem.SetItemDecimal(row, "itempayamt_net", itemamt_det);
                        DwItem.SetItemDecimal(row, "vat_amt", vatamt2);
                    }
                }
            }

            SumItemAmt();
        }
        protected void PostCalTax()
        {
            try
            {
                Int32 result;
                String mainXml = "",detXml = "";
                Int16 taxWayKeep = 0, vatFlag = 0, taxFlag = 0;
                Decimal vatFlagNum, netAmt = 0, taxAmt = 0, itempayAmt = 0;

                try
                {
                    vatFlagNum = Convert.ToDecimal(HVatFlag.Value);
                }
                catch { vatFlagNum = 0; }

                netAmt = DwMain.GetItemDecimal(1, "item_amtnet");
                taxWayKeep = Convert.ToInt16(DwMain.GetItemDecimal(1, "taxwaykeep"));

                try
                {
                    itempayAmt = DwMain.GetItemDecimal(1, "itempay_amt");
                }
                catch { itempayAmt = 0; }

                try
                {
                    vatFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "vat_flag"));
                }
                catch { vatFlag = 0; }

                try
                {
                    taxAmt = DwMain.GetItemDecimal(1, "tax_amt");
                }
                catch { taxAmt = 0; }

                try
                {
                    taxFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "tax_flag"));
                }
                catch { taxFlag = 0; }

                // คิดภาษี -- ยอดสุทธิมากกว่า 0 คิดตามประกาศ
                // หากคิด Vat และคิดภาษีเป็นคงที่ ให้ดู ยอดภาษีที่กรอกเข้าไป 
                // จะคำนวณ Vat ให้
                //if ((netAmt > 0 && taxWayKeep == 0) || (vatFlag == 1))
                if ((netAmt > 0) || (vatFlag == 1))
                {
                    mainXml = DwMain.Describe("Datawindow.Data.Xml");
                    result = fin.of_caltax(state.SsWsPass,ref mainXml,ref detXml);
                    DwMain.Reset();
                    DwMain.ImportString(mainXml, FileSaveAsType.Xml);
                }
                else if ((netAmt > 0 && taxWayKeep == 1))
                {

                    netAmt = itempayAmt - taxAmt;
                    DwMain.SetItemDecimal(1, "item_amtnet", netAmt);
                }
                else
                {
                    DwMain.SetItemDecimal(1, "tax_amt", 0);
                    DwMain.SetItemDecimal(1, "vat_amt", 0);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        protected void SetChildDW()
        {
            try
            {
                //DwUtil.RetrieveDDDW(DwMain, "coop_id", "finslip_spc.pbl", null); // สาขาสหกรณ์
                DwUtil.RetrieveDDDW(DwMain, "cash_type", "finslip_spc.pbl", null);// ประเภทเงินทำรายการ
                DwUtil.RetrieveDDDW(DwMain, "sendto_system", "finslip_spc.pbl", null);// ประเภทเงินทำรายการโอน
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "finslip_spc.pbl", state.SsCoopControl);  // คู่บัญชีของ DwMain
                DwUtil.RetrieveDDDW(DwMain, "tax_code", "finslip_spc.pbl", null); // ประเภท ภาษี
                //DwUtil.RetrieveDDDW(DwMain, "from_bankcode", "finslip_spc.pbl", null);            
                DwUtil.RetrieveDDDW(DwItem, "slipitemtype_code", "finslip_spc.pbl", state.SsCoopControl); //รหัสรายการ 
                //DwUtil.RetrieveDDDW(DwItem, "account_id", "finslip_spc.pbl", state.SsCoopControl);
            }
            catch (Exception ex) { }
        }

        protected void SumCalVat()
        {
            try
            {
                Int16 rownum = 0;
                Decimal sumAmt = 0, sumVat = 0, vatAmt = 0, temp = 0;
                Int16 vatFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "vat_flag"));

                if (vatFlag == 1)
                {
                    rownum = Convert.ToInt16(DwItem.RowCount);

                    for (int i = 1; i <= rownum; i++)
                    {
                        sumAmt = DwItem.GetItemDecimal(i, "itempay_amt");
                        sumAmt = (sumAmt * 7) / 107;
                        sumAmt = Math.Round(sumAmt, 2);
                        try
                        {
                            DwMain.SetItemDecimal(1, "vat_amt", sumAmt);
                            DwItem.SetItemDecimal(i, "vat_amt", sumAmt);
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                        }
                    }
                    Refresh();
                }
                else
                {
                    DwMain.SetItemDecimal(1, "vat_amt", sumVat);
                    rownum = Convert.ToInt16(DwItem.RowCount);

                    for (int i = 1; i <= rownum; i++)
                    {
                        try
                        {
                            DwItem.SetItemDecimal(i, "vat_amt", sumVat);
                        }
                        catch (Exception ex)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                        }
                    }
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        protected void PostNewTaxSum()
        {
            decimal tax_amt = 0, item_amtnet = 0, itempay_amt = 0;
            try { tax_amt = DwMain.GetItemDecimal(1, "tax_amt"); }
            catch { tax_amt = 0; }
            try { item_amtnet = DwMain.GetItemDecimal(1, "item_amtnet"); }
            catch { item_amtnet = 0; }
            for (int i = 1; i <= DwItem.RowCount; i++)
            {
                itempay_amt += DwItem.GetItemDecimal(i, "itempay_amt");
            }
            item_amtnet = itempay_amt - tax_amt;
            DwMain.SetItemDecimal(1, "tax_amt", tax_amt);
            DwMain.SetItemDecimal(1, "item_amtnet", item_amtnet);
        }
        protected void PostNewVatSum()
        {
            decimal vat_amt = 0, item_amtnet = 0, tax_amt = 0, itempay_amt = 0, tax_rate = 0, tax_code = 0;
            try { vat_amt = DwMain.GetItemDecimal(1, "vat_amt"); }
            catch { vat_amt = 0; }
            try { tax_amt = DwMain.GetItemDecimal(1, "tax_amt");}
            catch { tax_amt = 0; }
            try { item_amtnet = DwMain.GetItemDecimal(1, "item_amtnet"); }
            catch { item_amtnet = 0; }
            try { tax_code = DwMain.GetItemDecimal(1, "tax_code"); }
            catch { throw new Exception("ไม่สามารถดึงประเภทเสียภาษีได้ กรุณาระบุ !!!"); }
            for (int i = 1; i <= DwItem.RowCount; i++)
            {
                itempay_amt += DwItem.GetItemDecimal(i, "itempay_amt");
            }
            //get value from taxtype
            string sefintax = @"select tax_rate from fintaxtype where coop_id = {0} and tax_code = {1}";
            sefintax = WebUtil.SQLFormat(sefintax, state.SsCoopControl, tax_code);
            Sdt dtse = WebUtil.QuerySdt(sefintax);
            if (dtse.Next())
            {
                tax_rate = dtse.GetDecimal("tax_rate");
            }
            tax_amt = Math.Round((itempay_amt - vat_amt) * tax_rate, 2);
            item_amtnet = itempay_amt - tax_amt;
            DwMain.SetItemDecimal(1, "tax_amt", tax_amt);
            DwMain.SetItemDecimal(1, "item_amtnet", item_amtnet);
        }
        protected void SumItemAmt()
        {
            try
            {
                Int16 rownum = 0;
                Decimal sumAmt = 0, sumAmtNet = 0, sumTax = 0, sumVat = 0, vatAmt = 0, temp = 0;

                rownum = Convert.ToInt16(DwItem.RowCount);

                for (int i = 1; i <= rownum; i++)
                {
                    sumAmt += DwItem.GetItemDecimal(i, "itempay_amt");
                    sumAmtNet += DwItem.GetItemDecimal(i, "itempayamt_net");
                    sumTax += DwItem.GetItemDecimal(i, "tax_amt");

                    try
                    {
                        vatAmt = DwItem.GetItemDecimal(i, "vat_amt");
                    }
                    catch
                    {
                        vatAmt = 0;
                    }
                    sumVat = sumVat + vatAmt;
                }
                DwMain.SetItemDecimal(1, "item_amtnet", sumAmtNet);
                DwMain.SetItemDecimal(1, "tax_amt", sumTax);
                DwMain.SetItemDecimal(1, "vat_amt", sumVat);
                DwMain.SetItemDecimal(1, "itempay_amt", sumAmt);

                sumAmt = 0;
                sumTax = 0;
                sumVat = 0;
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        protected void PostProtect()
        {
            decimal pay_recv_status = 0;
            try
            {
                 pay_recv_status = Convert.ToDecimal(DwMain.GetItemString(1, "pay_recv_status"));
                //decimal pay_rex_status = Convert.ToDecimal(pay_recv_status);
            }
            catch
            {
                pay_recv_status = 1;
            }

            if (pay_recv_status == 0) //จ่าย
            {
                lb_slipdet.Text = "รายการ (DR)";
                DwMain.Modify("member_no.protect=0");
            }
            else if (pay_recv_status == 1)//รับ 
            {
                lb_slipdet.Text = "รายการ (CR)";
                DwMain.Modify("member_no.protect=0");
            }
            else if (pay_recv_status == 9) //ยังไม่เลือก
            {
                DwMain.Modify("member_no.protect=1");
                DwMain.Modify("member_flag.protect=1");
                DwMain.Modify("nonmember_detail.protect=1");
                DwMain.Modify("operate_tdate.protect=1");
                DwMain.Modify("cash_type.protect=1");
                DwMain.Modify("tofrom_accid.protect=1");
                DwMain.Modify("coop_id.protect=1");
                DwMain.Modify("vat_flag.protect=1");
                DwMain.Modify("tax_amt.protect=1");
                DwMain.Modify("payment_desc.protect=1");
                DwItem.Modify("slipitemtype_code.protect=1");
                DwItem.Modify("slipitem_desc.protect=1");
                DwItem.Modify("itempay_amt.protect=1");
                DwItem.Modify("account_id.protect=1");
            }

        }
        protected void DefaultAccid()
        {
            //DwMain.SetItemString(1, "entry_id", null);
            //DwMain.SetItemString(1, "coopbranch_id", null);
            try
            {
                // กำหนด คู่บัญชีเบื้องต้น ตามประเภทเงินทำรายการ ที่กำหนดในค่าคงที่ระบบการเงิน
                String moneytype = DwMain.GetItemString(1, "cash_type");
                String accid = "";
                accid = fin.of_defaultaccid(state.SsWsPass, moneytype).Trim();
                DwMain.SetItemString(1, "tofrom_accid", accid);

                if (state.SsCoopControl == "008001")
                {
                    //service มันเรียกตัวเดียวกันเลยต้อง HC ไว้ก่อน
                    if (moneytype == "DRF")
                    {
                        accid = "11030100";
                        DwMain.SetItemString(1, "tofrom_accid", accid);
                    }
                    else if (moneytype == "CBT" && (accid != "" || accid != null))
                    {
                        DwMain.SetItemString(1, "tofrom_accid", accid);
                        GetBankCode();
                    }
                    else
                    {
                        DwMain.SetItemString(1, "tofrom_accid", accid);
                    }
                }
                else
                {
                    if (moneytype == "CBT" && (accid != "" || accid != null))
                    {
                        GetBankCode();
                    }
                }
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        protected void PostInitMember()
        {
            try
            {
                if (HDChkDeptFEE.Value == "1")
                {
                    String mainXml = "";
                    String[] result = new String[2];
                    mainXml = DwMain.Describe("Datawindow.Data.Xml");
                    DwMain.Reset();

                    //DwMain.ImportString(mainXml, FileSaveAsType.Xml);
                    DwUtil.ImportData(mainXml, DwMain, tDwMain, FileSaveAsType.Xml);
                }
                else
                {
                    Int16 memberFlag;
                    String mainXml = "", memberNo, ls_addr = "", ls_taxid = "";
                    String[] result = new String[2];
                    Int32 re,xml;
                    DwMain.SetItemString(1, "member_no", HdMemberNo.Value.ToString());
                    mainXml = DwMain.Describe("Datawindow.Data.Xml");
                    xml = fin.of_init_payrecv_member(state.SsWsPass, ref mainXml);
                    DwMain.Reset();

                    //DwMain.ImportString(mainXml, FileSaveAsType.Xml);
                    DwUtil.ImportData(mainXml, DwMain, tDwMain, FileSaveAsType.Xml);
                    if (DwTax.RowCount == 1)
                    {
                        try
                        {
                            memberNo = DwMain.GetItemString(1, "member_no");
                        }
                        catch { memberNo = ""; }

                        try
                        {
                            memberFlag = Convert.ToInt16(DwMain.GetItemDecimal(1, "member_flag"));
                        }
                        catch { memberFlag = 0; }

                        re = fin.of_getaddress(state.SsWsPass, ref ls_addr, ref ls_taxid, state.SsCoopId, memberNo, memberFlag);

                        DwTax.SetItemString(1, "taxpay_id", ls_taxid);
                        DwTax.SetItemString(1, "taxpay_addr", ls_addr);

                    }
                }

                // Edit By Bank เรื่อง Confirm รับชำระที่ การเงิน //
                Decimal item_amtnet = 0;
                String pay_recv_status = "";

                try
                {
                    item_amtnet = DwMain.GetItemDecimal(1, "item_amtnet");
                }
                catch (Exception ex)
                {
                    item_amtnet = 0;
                }

                try
                {
                    pay_recv_status = DwMain.GetItemString(1, "pay_recv_status");
                }
                catch (Exception ex)
                {
                    pay_recv_status = "1";
                }

                if ((item_amtnet <= 0) && (pay_recv_status == "1"))
                {
                    try
                    {
                        String member_flag;

                        try
                        {
                            member_flag = DwMain.GetItemString(1, "member_flag");
                        }
                        catch (Exception ex)
                        {
                            member_flag = "1";
                        }

                        if (member_flag == "1")
                        {
                            string coopid = state.SsCoopId;
                            //string memno = DwMain.GetItemString(1, "member_no");
                            string slip_no = HdSlipNo.Value;
                            string entryid = state.SsUsername;
                            string machine = state.SsClientIp;
                            DateTime adtm_wdate = state.SsWorkDate;
                            string xmlfinslip = "";
                            string xmlfinslipdet = "";
                            int resultvalue;
                            xmlfinslip = DwMain.Describe("Datawindow.Data.Xml");
                            //xmlfinslipdet = DwItem.Describe("Datawindow.Data.Xml"); 
                            
                            if (HDChkDeptFEE.Value == "1")
                            {
                                //resultvalue = wcf.Finance.of_init_payrecv_slipcfm_pea(state.SsWsPass, coopid, slip_no, entryid, machine, adtm_wdate, ref xmlfinslip, ref xmlfinslipdet);
                                resultvalue = fin.of_init_payrecv_slipdlg(state.SsWsPass, coopid, slip_no, entryid, machine, adtm_wdate, ref xmlfinslip, ref xmlfinslipdet);
                            }
                            else
                            {
                                resultvalue = fin.of_init_payrecv_slipcfm(state.SsWsPass, coopid, slip_no, entryid, machine, adtm_wdate, ref xmlfinslip, ref xmlfinslipdet);
                            }

                            if (resultvalue == 1)
                            {
                                // import string
                                DwMain.Reset();
                                DwItem.Reset();
                                DwUtil.ImportData(xmlfinslip, DwMain, tDwMain, FileSaveAsType.Xml);
                                //DwMain.ImportString(xmlfinslip, FileSaveAsType.Xml);
                                tDwMain.Eng2ThaiAllRow();
                                DwUtil.ImportData(xmlfinslipdet, DwItem, tDwMain, FileSaveAsType.Xml);
                                //DwItem.ImportString(xmlfinslipdet, FileSaveAsType.Xml);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                    // End Edit By Bank
                }
            }
            catch (Exception ex)
            {
                DwMain.SetItemSqlString(1, "member_no", "");
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        protected void PostInitMembgrp()
        {
            try
            {
                string membgroup_code = "", membgroup_desc = "";
                membgroup_code = HdMembGrp.Value.Trim();
                String se = @"select membgroup_desc from mbucfmembgroup where membgroup_code = '"+ membgroup_code+"'";
                Sdt dtt = WebUtil.QuerySdt(se);
                if (dtt.Next())
                {
                    membgroup_desc = dtt.GetString("membgroup_desc").Trim();
                }
                DwMain.SetItemString(1, "nonmember_detail", membgroup_desc);
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

 

        protected void PostSetItemDesc()
        {
            try
            {
                int found = 0;
                String accDesc = "", accID = "", itemCode = "";
                try
                {
                    // คู่บัญชีที่เลือกใน DwItem กรณี คีย์เลขคู่บัญชี
                    //itemCode = DwItem.GetItemString(Convert.ToInt16(HfRow.Value), "slip_code_key");
                    itemCode = DwItem.GetItemString(Convert.ToInt16(HfRow.Value), "slipitemtype_code");
                    if (itemCode == "" || itemCode == null)
                    {
                        itemCode = DwItem.GetItemString(Convert.ToInt16(HfRow.Value), "slipitemtype_code");
                    }
                    DwItem.SetItemString(Convert.ToInt16(HfRow.Value), "slipitemtype_code", itemCode);
                }
                catch
                {
                    // คู่บัญชีที่เลือกใน DwItem
                    itemCode = DwItem.GetItemString(Convert.ToInt16(HfRow.Value), "slipitemtype_code");
                    DwItem.SetItemString(Convert.ToInt16(HfRow.Value), "slip_code_key", itemCode);
                }


                // คู่บัญชีของ DwItem
                DataWindowChild dcAccidItem = DwItem.GetChild("slipitemtype_code");

                // หา row ของคู่บัญชีที่เลือก ใน datawindow Child 
                // แล้ว get ค่าออกมาตาม row ที่หาได้
                found = dcAccidItem.FindRow("slipitemtype_code='" + itemCode.Trim() + "'", 1, dcAccidItem.RowCount);
                accDesc = dcAccidItem.GetItemString(found, "item_desc");
                accID = dcAccidItem.GetItemString(found, "account_id");
                DwItem.SetItemString(Convert.ToInt16(HfRow.Value), "slipitem_desc", accDesc.Trim());
                DwItem.SetItemString(Convert.ToInt16(HfRow.Value), "account_id", accID.Trim());
                if (Haccrow.Value == "1")
                {
                    DwMain.SetItemString(1, "payment_desc", accDesc.Trim());
                }
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        protected void PostPrintSlip()
        {
            String recOrPay = "",ld_dwrec="",ls_dwpay="";
            InitBegin(Convert.ToInt16(HStatus.Value), ref recOrPay);

            Int16 status = Convert.ToInt16(HStatus.Value);
            if (status == 1)
            {
                recOrPay = "rec";
            }
            else { recOrPay = "pay"; }
            string sql_dw = @"select rtrim(ltrim(dw_receipt)) as dw_receipt,rtrim(ltrim(dw_payreceipt)) as dw_payreceipt from finconstant where coop_id={0} ";
            sql_dw = WebUtil.SQLFormat(sql_dw,state.SsCoopControl);
            Sdt dt_dw = WebUtil.QuerySdt(sql_dw);
            if (dt_dw.Next())
            {
                ld_dwrec = dt_dw.GetString("dw_receipt");
                ls_dwpay = dt_dw.GetString("dw_payreceipt");
            }
            String slipno;
            slipno = Hslipno.Value;
            slipno = slipno.Trim();
            //FinancePrintMode
            if (xmlconfig.FinancePrintMode == 0)
            {
                //fin.OfPostPrintSlip(state.SsWsPass, slipno, state.SsCoopId, state.SsPrinterSet);
            }
            else
            {
                if (recOrPay == "rec")
                {
                    try
                    {

                        Decimal itempay_amt = 0;

                        // slipno

                        string sql = @" 
                               select itempay_amt from finslip where slip_no = {0} ";
                        sql = WebUtil.SQLFormat(sql, slipno);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            itempay_amt = dt.GetDecimal("itempay_amt");
                        }

                        ////////////////////////////
                        //// ทำค่า sum รวมเป็นภาษาไทย
                        string bahtTxt, n, bahtTH_sum = "";
                        double amount;
                        try { amount = Convert.ToDouble(itempay_amt); }
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
                        report_name = ld_dwrec;
                        report_label = "ใบเสร็จรับเงิน";

                        iReportArgument args = new iReportArgument();
                        //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                        args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                        args.Add("as_slipno", iReportArgumentType.String, slipno);
                        //args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                        iReportBuider report = new iReportBuider(this, "");
                        report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();

                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถสร้างใบเสร็จรับเงินได้");
                        //return false;
                    }
                }
                else if (recOrPay == "pay")
                {
                    try
                    {

                        Decimal itempay_amt = 0;

                        // slipno

                        string sql = @" 
                               select itempay_amt from finslip where slip_no = {0} ";
                        sql = WebUtil.SQLFormat(sql, slipno);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        if (dt.Next())
                        {
                            itempay_amt = dt.GetDecimal("itempay_amt");
                        }

                        ////////////////////////////
                        //// ทำค่า sum รวมเป็นภาษาไทย
                        string bahtTxt, n, bahtTH_sum = "";
                        double amount;
                        try { amount = Convert.ToDouble(itempay_amt); }
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
                        report_name = ls_dwpay;
                        report_label = "ใบสำคัญจ่าย";

                        iReportArgument args = new iReportArgument();
                        //args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                        args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                        args.Add("as_slipno", iReportArgumentType.String, slipno);
                        //args.Add("bahtTH_sum", iReportArgumentType.String, bahtTH_sum);
                        iReportBuider report = new iReportBuider(this, "");
                        report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                        report.AutoOpenPDF = true;
                        report.Retrieve();
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถสร้างใบสำคัญจ่ายได้");
                    }
                }
            }
        }
        protected void PrintTax()
        {
            String slipno;
            slipno = Hslipno.Value;
            slipno = slipno.Trim();
            //fin.OfPostPrintTaxPay(state.SsWsPass, state.SsCoopId, slipno, state.SsPrinterSet);
        }
        protected void GetBankBranch()
        {
            String ls_bank;
            try
            {
                ls_bank = "";
                try
                {
                    ls_bank = DwMain.GetItemString(1, "bank_code");
                    DwUtil.RetrieveDDDW(DwMain, "bank_branch", "finslip_spc.pbl", ls_bank);

                }
                catch
                {
                    LtServerMessage.Text = "กรุณาเลือกธนาคาร";
                }
                //DataWindowChild DcBankBranch = DwMain.GetChild("bank_branch");
                //String BankBranchXml = fin.OfGetChildBankbranch(state.SsWsPass, ls_bank);
                //DcBankBranch.ImportString(BankBranchXml, FileSaveAsType.Text);
                //DcBankBranch.SetFilter("bank_code = '" + ls_bank + "'");
                //DcBankBranch.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        private void MessageBox(string title, string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), title, "alert('" + msg + "');", true);
        }

        public decimal GetSql_Value(string Select_Condition)
        {
            decimal max_value = 0;
            string sqlf = Select_Condition;
            Sdt dt = WebUtil.QuerySdt(sqlf);

            if (dt.Next())
            {
                max_value = dt.GetDecimal("sql_value");
            }
            return max_value;
        }

        public void PrintRecPea()
        {
            try
            {
                String slipno;
                slipno = Hslipno.Value;
                Printing.FinPrintSlipReceive(this, state.SsCoopId, slipno, state.SsWorkDate, xmlconfig.FinancePrintMode);
                Hslipno.Value = "";
                HStatus.Value = "";
            }
            catch { }
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
    }
}
