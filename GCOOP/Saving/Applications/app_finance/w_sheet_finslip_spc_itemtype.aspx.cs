using System;
using CoreSavingLibrary;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finslip_spc_itemtype : PageWebSheet, WebSheet
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
                }
                else
                {
                    this.RestoreContextDw(DwMain);
                    this.RestoreContextDw(DwItem);
                    this.RestoreContextDw(DwTax);
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
                case "postRefresh":
                    Refresh();
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
            }
        }
        public void SaveWebSheet()
        {
            try
            {
                Int16       taxFlag, recvpay_status  ;
                Decimal     sumTax;
                String      mainXml = "", itemXml = "", taxXml = "", tofromAccid = "", cashtype = "", member_flag = "";

                CheckSave();

                try
                {
                    recvpay_status = Convert.ToInt16(DwMain.GetItemDecimal(1, "pay_recv_status"));
                }
                catch { recvpay_status = 9; }

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

                    if (taxdesc == "" || taxaddr == "" || taxpay_id == "")
                    {
                        throw new Exception("ป้อนรายละเอียดภาษีไม่ครบ");
                    }

                    taxXml = DwTax.Describe("Datawindow.Data.Xml");

                    Htaxpay_id.Value = "";
                    Htaxpay_desc.Value = "";
                    Htaxpay_addr.Value = "";

                    HprintTax.Value = "true";
                }

                sumTax  =    Convert.ToInt16(DwMain.GetItemDecimal(1, "tax_flag"));
                if (sumTax > 0)
                {
                    HprintTax.Value = "true";
                }
                else
                {
                    HprintTax.Value = "false";
                }

                cashtype    = DwMain.GetItemString(1, "cash_type");
                tofromAccid = DwMain.GetItemString(1, "tofrom_accid");
                member_flag = DwMain.GetItemString(1, "member_flag");

                mainXml     = DwMain.Describe("Datawindow.Data.Xml");
                itemXml     = DwItem.Describe("Datawindow.Data.Xml");

                String result;
                try
                {
                    result = fin.of_payslip(state.SsWsPass, mainXml, itemXml, taxXml, state.SsApplication);
                }
                catch (  Exception err ) 
                    {
                        MessageBox("คำเตือน", err.ToString() );
                        result = "";
                    }

                if (result != "")
                {
                    Hslipno.Value = result;
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");

                    Hprintslip.Value = "true";
                    HClear.Value = "true";
                    HCashtype.Value = cashtype;
                    HAccid.Value = tofromAccid;
                    Hmember_flag.Value = member_flag ;
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

                resultXml = fin.of_init_payrecv_slip_1(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsClientIp, state.SsWorkDate, ai_recvpay, ref slipmain_Xml);

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
                string member_no        = DwMain.GetItemString(1, "member_no");
                string nonmember_detail = DwMain.GetItemString(1, "nonmember_detail");
                string payment_desc     = DwMain.GetItemString(1, "payment_desc");
                Decimal item_amtnet     = DwMain.GetItemDecimal(1, "item_amtnet");
                string account_id       = DwMain.GetItemString(1, "tofrom_accid");
                string slipitem_desc    = DwMain.GetItemString(1, "payment_desc");
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

            DwMain.SetItemString(1, "from_bankcode", bankcode);
            GetBankBranch();

            this.DisConnectSQLCA();
            return bankcode;
        }
        protected void Refresh()
        {
            Int16 row = 0, countTaxflag = 0, memberFlag = 0;
            row = Convert.ToInt16(HfRow.Value);
            String memberNo = "", columnName = "";
            columnName = HColumName.Value.Trim();

            Int16 taxFlag = Convert.ToInt16(DwMain.GetItemDecimal(row, "tax_flag"));

            if (taxFlag == 0)
            {
                DwMain.SetItemDecimal(row, "vat_flag", 0);
                DwMain.SetItemDecimal(row, "vat_amt", 0);
                DwMain.SetItemDecimal(row, "tax_amt", 0);
                DwMain.SetItemDecimal(row, "itempay_amt", 0);               
                SumItemAmt();
            }
            else
            {
                Int32 resultXml;
                String taxaddr = "", taxid = "";

                resultXml = fin.of_getaddress(state.SsWsPass,ref taxaddr,ref taxid ,state.SsCoopId, memberNo, memberFlag);
                DwTax.SetItemString(1, "taxpay_id", taxid);
                DwTax.SetItemString(1, "taxpay_addr", taxaddr);
                ItemtCalTax();
            }

        }
        protected void ItemtCalTax()
        {
            int row;
            Decimal itemPayAmt = 0, taxFlag = 0;
            Int32 result;
            Int16 taxCode = 0, vatflag = 0, recvpay = 0;

            try
            {
                try
                {
                    row = Convert.ToInt32(HfRow.Value);
                }
                catch { row = 0; }
                HfRow.Value = "";

                try
                {
                    taxCode = Convert.ToInt16(DwItem.GetItemString(row, "tax_code"));
                }
                catch
                { taxCode = 0; }

                try
                {
                    vatflag = Convert.ToInt16(DwItem.GetItemString(row, "vat_flag"));
                }
                catch { vatflag = 0; }

                try
                {
                    recvpay = Convert.ToInt16(DwMain.GetItemString(row, "pay_recv_status"));
                }
                catch { recvpay = 0; }

                try
                {
                    itemPayAmt = DwItem.GetItemDecimal(row, "itempay_amt");
                    itemPayAmt = Math.Round(itemPayAmt, 2);
                }
                catch { itemPayAmt = 0; }

                if (taxCode != 0 && itemPayAmt > 0)
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
                        result = fin.of_itemcaltax(state.SsWsPass, state.SsCoopId, recvpay, vatflag, taxCode, itemPayAmt,ref taxamt,ref itemamt,ref vatamount);
                    }
                    catch
                    {
                    }
                    if (recvpay == 1 && vatamount > 0)
                    {
                        vat_amt = Math.Round(vatamount, 2);
                        itemPayAmt = itemPayAmt - vat_amt;

                        DwItem.SetItemDecimal(row, "itempay_amt", itemPayAmt);
                        DwItem.SetItemDecimal(row, "vat_amt", vatamount);

                    }
                    DwItem.SetItemDecimal(row, "itempayamt_net", itemamt);
                }
            }
            
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            SumItemAmt();
        }
        protected void PostCalTax()
        {
            try
            {
                Int32 result;
                String mainXml = "",taxdet = "";
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
                    result = fin.of_caltax(state.SsWsPass,ref mainXml,ref taxdet);
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
            //DwUtil.RetrieveDDDW(DwMain, "coop_id", "finslip_spc.pbl", null); // สาขาสหกรณ์
            DwUtil.RetrieveDDDW(DwMain, "cash_type", "finslip_spc.pbl", null);// ประเภทเงินทำรายการ
            DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", "finslip_spc.pbl", state.SsCoopControl);  // คู่บัญชีของ DwMain
            DwUtil.RetrieveDDDW(DwMain, "tax_code", "finslip_spc.pbl", state.SsCoopControl); // ประเภท ภาษี
            //DwUtil.RetrieveDDDW(DwMain, "from_bankcode", "finslip_spc.pbl", null);            
            DwUtil.RetrieveDDDW(DwItem, "slipitemtype_code", "finslip_spc.pbl", state.SsCoopControl); //รหัสรายการ 
            //DwUtil.RetrieveDDDW(DwItem, "account_id", "finslip_spc.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(DwMain, "tax_code", "finslip_spc.pbl", state.SsCoopControl);
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
                lb_slipdet.Text = "รายการ (DR)";
            }
            else if (pay_recv_status == 1)//รับ 
            {
                lb_slipdet.Text = "รายการ (CR)";
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
                DwMain.Modify("vat_amt.protect=1");
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

                if (moneytype == "CBT" && (accid != "" || accid != null))
                {
                    GetBankCode();
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
                Int16 memberFlag;
                String mainXml = "", memberNo,taxaddr = "",taxid = "";
                Int32 result,main;

                DwMain.SetItemString(1,"member_no",HdMemberNo.Value.ToString());
                mainXml = DwMain.Describe("Datawindow.Data.Xml");
                main = fin.of_init_payrecv_member(state.SsWsPass,ref mainXml);
                DwMain.Reset();
                DwMain.ImportString(mainXml, FileSaveAsType.Xml);

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

                // Edit By Bank เรื่อง Confirm รับชำระที่ การเงิน //
                Decimal item_amtnet = DwMain.GetItemDecimal(1,"item_amtnet");
                String pay_recv_status = DwMain.GetItemString(1,"pay_recv_status");
                if ((item_amtnet <= 0) && (pay_recv_status=="1"))
                {
                    try
                    {
                        String member_flag = DwMain.GetItemString(1, "member_flag");
                        if (member_flag == "1")
                        {
                            string coopid = state.SsCoopId;
                            string slip_no = HdSlipNo.Value;
                            string entryid = state.SsUsername;
                            string machine = state.SsClientIp;
                            DateTime adtm_wdate = state.SsWorkDate;
                            string xmlfinslip = "";
                            string xmlfinslipdet = "";
                            int resultvalue = fin.of_init_payrecv_slipcfm(state.SsWsPass, coopid, slip_no, entryid, machine, adtm_wdate, ref xmlfinslip, ref xmlfinslipdet);
                            if (resultvalue == 1)
                            {
                                // import string
                                DwMain.Reset();
                                DwItem.Reset();
                                DwMain.ImportString(xmlfinslip, FileSaveAsType.Xml);
                                tDwMain.Eng2ThaiAllRow();
                                DwItem.ImportString(xmlfinslipdet, FileSaveAsType.Xml);
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
                    itemCode = DwItem.GetItemString(Convert.ToInt16(HfRow.Value), "slip_code_key");
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
                    Printing.FinPrintSlipReceive(this, state.SsCoopId, slipno, state.SsWorkDate, xmlconfig.FinancePrintMode);
                }
                else if (recOrPay == "pay")
                {
                    Printing.FinPrintSlipPay(this, state.SsCoopId, slipno, state.SsWorkDate, xmlconfig.FinancePrintMode);
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
       
    }
}
