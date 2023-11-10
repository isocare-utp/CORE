using System;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;
using DataLibrary;
using Sybase.DataWindow;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finslip_acc : PageWebSheet, WebSheet
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
        protected String postDate;
        decimal taxamt, itemamt, vatamount;
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
            postDate = WebUtil.JsPostBack(this, "postDate");
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
                case "postDate":
                    SetFormatDate();
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
                //if (cash_type == "CHQ" && recvpay_status == 0)
                //{
                //    DwMain.SetItemSqlDecimal(1, "payment_status", 8);
                //}

                DwMain.SetItemSqlDecimal(1, "payment_status", 8);

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

                    catch { taxdesc = "หักภาษี ณ ที่จ่าย"; }
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
                    try
                    {
                        DwMain.SetItemSqlDecimal(1, "payment_status", 1);
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

                        result = fin.of_payslip_pia(state.SsWsPass, mainXml, itemXml, taxXml, state.SsApplication);
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
                String slipmain_Xml = "";
                try
                {
                    ai_recvpay = 9;
                }
                catch { ai_recvpay = 0; }

                resultXml = fin.of_init_payrecv_slip_1(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, ai_recvpay,ref slipmain_Xml);

                DwTax.Reset();
                DwMain.Reset();
                DwItem.Reset();

                DwMain.ImportString(slipmain_Xml, FileSaveAsType.Xml);
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
                string member_no = DwMain.GetItemString(1, "member_no");
                string nonmember_detail = DwMain.GetItemString(1, "nonmember_detail");
                string payment_desc = DwMain.GetItemString(1, "payment_desc");
                Decimal item_amtnet = DwMain.GetItemDecimal(1, "item_amtnet");
                string account_id = DwMain.GetItemString(1, "tofrom_accid");
                string slipitem_desc = DwMain.GetItemString(1, "payment_desc");
                string operate_tdate = DwMain.GetItemString(1, "operate_tdate");
                string entry_tdate = DwMain.GetItemString(1, "entry_tdate");
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

            if (memberFlag != 3)
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
                        String[] resultXml = new String[2];

                        //resultXml = fin.OfGetAddress(state.SsWsPass, state.SsCoopId, memberNo, memberFlag);
                        //DwTax.SetItemString(1, "taxpay_id", resultXml[1]);
                        //DwTax.SetItemString(1, "taxpay_addr", resultXml[0]);
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
            decimal itemPayAmt = 0, taxFlag = 0, vatamt;
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
                            result = fin.of_itemcaltax(state.SsWsPass, state.SsCoopId, recvpay, vatflag, taxCode, itemPayAmt,ref taxamt,ref itemamt,ref vatamount);
                        }
                        catch
                        {

                        }

                        taxFlag = DwMain.GetItemDecimal(1, "tax_flag");
                        if (taxFlag == 1)
                        {
                            DwItem.SetItemDecimal(row, "tax_amt", taxamt);
                            DwItem.SetItemDecimal(row, "itempayamt_net", itemamt);
                            DwItem.SetItemDecimal(row, "vat_amt", vatamount);
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
                        result = fin.of_itemcaltax(state.SsWsPass, state.SsCoopId, recvpay, vatflag, taxCode, itemPayAmt,ref taxamt,ref itemamt,ref vatamount);
                    }
                    catch
                    {

                    }

                    taxFlag = DwMain.GetItemDecimal(1, "tax_flag");
                    if (taxFlag == 0)
                    {
                        DwItem.SetItemDecimal(row, "tax_amt", taxamt);
                        DwItem.SetItemDecimal(row, "itempayamt_net", itemamt);
                        DwItem.SetItemDecimal(row, "vat_amt", vatamount);
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
                String mainXml = "",taxdet_Xml = "";
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
                    result = fin.of_caltax(state.SsWsPass,ref mainXml, ref taxdet_Xml);
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
            Int16 pay_recv_status = Convert.ToInt16(DwMain.GetItemDecimal(1, "pay_recv_status"));
            if (pay_recv_status == 0) //จ่าย
            {
                lb_slipdet.Text = "รายการ (CR)";
            }
            else if (pay_recv_status == 1)//รับ 
            {
                lb_slipdet.Text = "รายการ (DR)";
            }
            else if (pay_recv_status == 9) //ยังไม่เลือก
            {
                //DwMain.Modify("cash_type.protect=1");
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
            try
            {
                // กำหนด คู่บัญชีเบื้องต้น ตามประเภทเงินทำรายการ ที่กำหนดในค่าคงที่ระบบการเงิน
                String moneytype = DwMain.GetItemString(1, "cash_type");
                String accid = "";
                accid = fin.of_defaultaccid(state.SsWsPass, moneytype).Trim();

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
                    String mainXml = "", memberNo,taxaddr = "",taxid = "";
                    Int32 main, result;
                    DwMain.SetItemString(1, "member_no", HdMemberNo.Value.ToString());
                    mainXml = DwMain.Describe("Datawindow.Data.Xml");
                    main = fin.of_init_payrecv_member(state.SsWsPass, ref mainXml);
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

                        result = fin.of_getaddress(state.SsWsPass,ref taxaddr,ref taxid, state.SsCoopId, memberNo, memberFlag);
                        DwTax.SetItemString(1, "taxpay_id", taxid);
                        DwTax.SetItemString(1, "taxpay_addr", taxaddr);
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

                            if (HDChkDeptFEE.Value == "1")
                            {
                                resultvalue = fin.of_init_payrecv_slipcfm(state.SsWsPass, coopid, slip_no, entryid, machine, adtm_wdate, ref xmlfinslip, ref xmlfinslipdet);
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
                                //DwMain.ImportString(xmlfinslip, FileSaveAsType.Xml);
                                DwUtil.ImportData(xmlfinslip, DwMain, tDwMain, FileSaveAsType.Xml);
                                tDwMain.Eng2ThaiAllRow();
                                //DwItem.ImportString(xmlfinslipdet, FileSaveAsType.Xml);
                                DwUtil.ImportData(xmlfinslipdet, DwItem, tDwMain, FileSaveAsType.Xml);
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
            String recOrPay = "";
            InitBegin(Convert.ToInt16(HStatus.Value), ref recOrPay);

            Int16 status = Convert.ToInt16(HStatus.Value);
            if (status == 1)
            {
                recOrPay = "rec";
            }
            else { recOrPay = "pay"; }
            String slipno, member_flag;
            slipno = Hslipno.Value;
            slipno = slipno.Trim();
            member_flag = Hmember_flag.Value.Trim();

            if (state.SsCoopControl == "013001")
            {
                //Add Arg[]
                iReportArgument args = new iReportArgument();
                iReportBuider report = new iReportBuider(this, "");

                if (recOrPay == "pay")
                {
                    args.Add("as_slipno", iReportArgumentType.String, slipno);

                    if (member_flag == "1")
                    {
                        report.AddCriteria("slip_fin_etc_cr", "as_slipno", ReportType.pdf, args);
                    }
                    else {
                        report.AddCriteria("slip_fin_etc_cr_", "as_slipno", ReportType.pdf, args);
                    }
                }
                //
                report.AutoOpenPDF = true;
                report.Retrieve();
            }
            else
            {
                //FinancePrintMode
                XmlConfigService xml = new XmlConfigService();
                if (xml.FinancePrintMode == 0)
                {
                    //fin.OfPostPrintSlip(state.SsWsPass, slipno, state.SsCoopId, state.SsPrinterSet);
                }
                else
                {
                    if (recOrPay == "rec")
                    {
                        Printing.FinPrintSlipReceive(this, state.SsCoopId, slipno, state.SsWorkDate, xml.FinancePrintMode);
                    }
                    else if (recOrPay == "pay")
                    {
                        Printing.FinPrintSlipPay(this, state.SsCoopId, slipno, state.SsWorkDate, xml.FinancePrintMode);
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

        protected void SetFormatDate()
        {
            string operate_tdate, entry_tdate;
            operate_tdate = DwMain.GetItemString(1, "operate_tdate");
            entry_tdate = DwMain.GetItemString(1, "entry_tdate");

            operate_tdate = operate_tdate.Replace("/", "");
            entry_tdate = entry_tdate.Replace("/", "");

            operate_tdate = string.Format("{0:ddMMyyyy}", operate_tdate);
            entry_tdate = string.Format("{0:ddMMyyyy}", entry_tdate);

            DwMain.SetItemString(1, "operate_tdate", operate_tdate);
            DwMain.SetItemString(1, "entry_tdate", entry_tdate);

            DateTime operate_tdate_ = DateTime.ParseExact(operate_tdate, "ddMMyyyy", new System.Globalization.CultureInfo("en-US"));
            DateTime entry_tdate_ = DateTime.ParseExact(entry_tdate, "ddMMyyyy", new System.Globalization.CultureInfo("en-US"));

            operate_tdate_ = operate_tdate_.AddYears(-543);
            entry_tdate_ = entry_tdate_.AddYears(-543);

            DwMain.SetItemDateTime(1, "operate_date", operate_tdate_);
            DwMain.SetItemDateTime(1, "entry_date", entry_tdate_);
        }
    }
}
