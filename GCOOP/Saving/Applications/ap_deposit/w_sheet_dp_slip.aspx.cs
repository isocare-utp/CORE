using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit; // new deposit
using CoreSavingLibrary.WcfNCommon; //new common

using DataLibrary;
using Sybase.DataWindow;


using System.Web.Services.Protocols;
//using Saving.ConstantConfig;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_slip : PageWebSheet, WebSheet
    {
        private DwThDate tDwMain;
        private DwThDate tDwCheque;
        private DwThDate tDwItem;
        private String deptAccountNo = null;
        //private DepositClient depService;
        //private CommonClient cmService;
        private n_depositClient ndept; //new deposit
        private n_commonClient ncommon; //new common

        private bool isException = false;
       // private DepositConfig depConfig;
        private bool IsAutoDeptWith
        {
            get
            {
                try
                {
                    return Session["is_auto_deptwith"].ToString().ToLower() == "true";
                }
                catch { return false; }
            }
            set
            {
                Session["is_auto_deptwith"] = value;
            }
        }
        private bool checkBox1Before;
        private bool completeCheque;


        //POSTBACK
        protected String postPost;
        protected String postNewAccount;
        protected String postDeptWith;
        protected String postRecpPayTypeCode;
        protected String newClear;
        protected String postTotalWidthFixed;
        protected String postItemSelect;
        protected String postSaveNoCheckApv;
        protected String postInsertRowCheque;
        protected String postDeleteRowCheque;
        protected String postBankCode;
        protected String postBankBranchCode;
        protected String CheckCoop;
        protected String setCoopname;
        protected String postchgremark;
        protected String postpassbook;

        private String TryDwMainGetString(String column)
        {
            try
            {
                return DwMain.GetItemString(1, column).Trim();
            }
            catch
            {
                return "";
            }
        }

        private void CallPrintBook(String slipNo, String message)
        {
            HdPrintFlag.Value = "true";
            //get สถานะว่า มีการผ่านรายการการเงินเลยหรือไม่ ถ้าใช่ ให้ทำการ print ใบเสร็จการเงินที่หน้านี้เลย [new]
            if (state.SsCoopId == "018001")
            {
                HdPrintFin.Value = "true";
            }
            else
            {
                HdPrintFin.Value = "false";
            }

            HdPrintBook.Value = deptAccountNo;
            Int32 PrintSlipStatus = 0;
            try
            {
                Decimal nobook_flag = Convert.ToDecimal(HdNoBook_flag.Value);
                if (nobook_flag == 1)
                {
                    PrintSlipStatus = 1;
                }
                else
                {
                    PrintSlipStatus = Convert.ToInt32(WebUtil.GetDpDeptConstant("printslip_status"));
                }
                if (PrintSlipStatus == 1)
                {
                    //depService.PrintSlip(state.SsWsPass, slipNo, state.SsCoopId, state.SsPrinterSet);
                    int printStatus = xmlconfig.DepositPrintMode;
                    string xml_return = "", xml_return_bf = "";
                    //depService.PrintSlip(state.SsWsPass, slipNo, state.SsCoopId, state.SsPrinterSet, 1, ref xml_return);
                    ndept.of_print_slip(state.SsWsPass, slipNo, state.SsCoopId, state.SsPrinterSet, 1, ref xml_return);

                    if (xml_return != "")
                    {
                        Printing.PrintApplet(this, "dept_slip", xml_return);
                    }
                }
                if (HdPrintFin.Value == "true")
                {
                    String recslipNO = "", payslipNO = "";
                    recslipNO = GetFinSlipnoRec(slipNo);
                    payslipNO = GetFinSlipnoPay(slipNo);
                    if (recslipNO != "" || recslipNO != null)
                    {
                        Printing.PrintFinSlipRecv(this, state.SsCoopId, recslipNO);
                    }
                    if (payslipNO != "" || payslipNO != null)
                    {
                        Printing.FinPrintSlipPayNan(this, state.SsCoopId, payslipNO);
                    }

                }

                LtServerMessage.Text = WebUtil.CompleteMessage(message);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.WarningMessage(message + " , ไม่สามารถเชื่อมต่อเครื่องพิมพ์ slip");
            }
            JsNewClear();

            HfCoopid.Value = state.SsCoopId;
            DwMain.SetItemString(1, "deptcoop_id", HfCoopid.Value);
        }

        private String GetDwItemXml(bool forceItem)
        {
            String xml = ""; ;
            if (!forceItem)
            {
                if (DwItem.RowCount > 0)
                {
                    if (DwItem.GetItemDecimal(1, "prnc_no") > 0)
                    {
                        tDwItem.Eng2ThaiAllRow();
                        xml = DwItem.Describe("DataWindow.Data.XML");
                    }
                }
            }
            else
            {
                tDwItem.Eng2ThaiAllRow();
                xml = DwItem.Describe("DataWindow.Data.XML");
            }
            return xml;
        }

        //JS-EVENT
        private void Jspostpassbook()
        {
            string passbook = null;
            string accNo = null;
            passbook = DwMain.GetItemString(1, "passbook");
            accNo = WebUtil.DeptBarcodeToDeptAccount(passbook);
            DwMain.SetItemString(1, "deptformat", accNo);
            JsNewAccountNo();
         }
        private void JsNewAccountNo()
        {
            String accNo = null;
            DwCheque.Reset();
            DwItem.Reset();
            try
            {
                accNo = DwMain.GetItemString(1, "deptformat");
                //accNo = wcf.InterPreter.DeptBarcodeToDeptAccount(state.SsConnectionIndex, state.SsCoopControl, accNo);
                //accNo = WebUtil.DeptBarcodeToDeptAccount(accNo);
                
                //accNo = depService.BaseFormatAccountNo(state.SsWsPass, accNo);
                accNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, accNo);
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                this.deptAccountNo = null;
                return;
            }
            try
            {
                ///////HARD
                isException = false;
                String coopid = state.SsCoopId;
                String deptcoop_id = HfCoopid.Value;

                //String ls_xml = depService.InitDepSlip(state.SsWsPass, state.SsCoopId, accNo, deptcoop_id, state.SsUsername, state.SsWorkDate, state.SsClientIp);
                String ls_xml = ndept.of_init_deptslip(state.SsWsPass, state.SsCoopId, accNo, deptcoop_id, state.SsWorkDate, state.SsUsername, state.SsClientIp);

                DwUtil.ImportData(ls_xml, DwMain, tDwMain, FileSaveAsType.Xml);
                String depformat = WebUtil.ViewAccountNoFormat(accNo);
                DwMain.SetItemString(1, "deptformat", depformat);
                if (DwMain.RowCount != 1)
                {
                    throw new Exception("Import ไม่สำเร็จ ไม่ทราบสาเหตุ");
                }
                try
                {
                    HdAccName.Value = DwMain.GetItemString(1, "deptaccount_name");
                }
                catch { }
                HdNewAccountNo.Value = "true";
            }
            catch (Exception ex)
            {
                isException = true;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                JsNewClear();
            }
            finally
            {
                if (isException)
                {
                    accNo = null;
                    JsNewClear();
                }
            }
            this.deptAccountNo = accNo;
        }

        //JS-EVENT
        private void JsPostDeptWith()
        {
            String accNo = DwMain.GetItemString(1, "deptaccount_no");
            String recpPayGrp, deptgroup_code;
            decimal v_interest = 0, v_principal = 0;
            //edit by BankCm เคลียร์ค่าเก่าออกในกรณีมีการ postdeptwith หลายครั่ง
            try
            {
                DwMain.SetItemDecimal(1, "accuint_amt", 0);
                DwMain.SetItemDecimal(1, "int_amt1", 0);
                DwMain.SetItemDecimal(1, "tax_amt", 0);
                DwMain.SetItemDecimal(1, "int_return", 0);
                DwMain.SetItemDecimal(1, "tax_return", 0);
                DwMain.SetItemDecimal(1, "deptslip_amt", 0);
                DwMain.SetItemDecimal(1, "deptslip_netamt", 0);
                DwMain.SetItemDecimal(1, "other_amt", 0);
            }
            catch { }
            ////////////////////////////////////////////////////////

            try
            {
                isException = false;
                try
                {
                    DwMain.SetItemString(1, "deptitemtype_code", DwMain.GetItemString(1, "recppaytype_code"));
                }
                catch { }
                DwMain.SetItemString(1, "tofrom_accid", "");
                String deptWithFlag = TryDwMainGetString("deptwith_flag");
                String recpPayTypeCode = TryDwMainGetString("recppaytype_code");
                //Edit by dot
                //String accid = depService.of_get_default_accid(state.SsWsPass, state.SsCoopControl, recpPayTypeCode);
                String accid = ndept.of_get_default_accid(state.SsWsPass, state.SsCoopControl, recpPayTypeCode);

                DwMain.SetItemString(1, "tofrom_accid", accid);
                if (accid != "")
                {
                    Sta ta = new Sta(state.SsConnectionString);
                    String sql = "";
                    sql = @"SELECT ACCOUNT_NAME  
                    FROM ACCMASTER  
                    WHERE ( ACCOUNT_ID = '" + accid + @"' ) AND ( COOP_ID = '" + state.SsCoopControl + @"' )";
                    Sdt dt = ta.Query(sql);
                    DwMain.SetItemString(1, "remark", dt.Rows[0]["ACCOUNT_NAME"].ToString());
                    ta.Close();
                }
                // Edit by Phai กำหนดกลุ่มการประเภททำรายการ
                //recpPayGrp = depService.GetRecvPayGroup(state.SsWsPass, recpPayTypeCode);
                //แสดงยอดเงินฝากรายเดือน
                if (state.SsCoopId == "034001") {
                    Sta ta = new Sta(state.SsConnectionString);
                    string sql = @"select deptmonth_amt from dpdeptmaster where deptaccount_no='" + accNo + @"' and deptmonth_status=1";
                    Sdt dt = ta.Query(sql);
                    string ls_deptmonth = "";
                    if (dt.Rows.Count > 0)
                    {
                        ls_deptmonth = "ยอดฝากรายเดือน "+dt.Rows[0]["deptmonth_amt"].ToString()+" บาท";
                    }
                    DwMain.SetItemString(1, "remark", ls_deptmonth);
                }
                recpPayGrp = ndept.of_getrevpaygrp(state.SsWsPass, recpPayTypeCode);

                DwMain.SetItemString(1, "group_itemtpe", recpPayGrp);

                //recpPayGrp = TryDwMainGetString("group_itemtpe"); //Edit By Bank to Change Recppay Code

                String deptTypeCode = TryDwMainGetString("DEPTTYPE_CODE");
                //    String cashType = depService.GetCashType(state.SsWsPass, recpPayTypeCode);// TryDwMainGetString("cash_type");                        
                String cashType = ndept.of_get_cashtype(state.SsWsPass, recpPayTypeCode);
                // กลุ่มเงินฝาก
                //deptgroup_code = depService.GetDeptGroup(state.SsWsPass, deptTypeCode);
                deptgroup_code = ndept.of_get_deptgroup(state.SsWsPass, deptTypeCode);
                if (deptWithFlag != "/")
                {
                    if (cashType == "CHQ" && DwCheque.RowCount < 1)
                    {
                        DwMain.SetItemDecimal(1, "deptslip_amt", 0);
                        DwMain.SetItemDecimal(1, "deptslip_netamt", 0);
                        JsPostInsertRowCheque();
                    }
                }
                if (deptWithFlag == "/")
                {
                    DwMain.SetItemDecimal(1, "deptslip_amt", DwMain.GetItemDecimal(1, "prncbal"));
                }
                //------------------ เช็คว่าประจำรึเปล่า ------------------
                if (deptgroup_code == "01")
                {
                    try
                    {
                        String deptcoop_id = DwMain.GetItemString(1, "deptcoop_id");
                        String accountNo = TryDwMainGetString("deptaccount_no");
                        DateTime calint_date = new DateTime();
                        String xmlSlipDetail = "";
                        
                        xmlSlipDetail = ndept.of_init_deptslip_det(state.SsWsPass, deptTypeCode, accountNo, deptcoop_id, state.SsWorkDate, recpPayGrp);
                        //}
                        DwUtil.ImportData(xmlSlipDetail, DwItem, tDwItem, FileSaveAsType.Xml);
                    }
                    catch (Exception ex)
                    {
                        DwItem.Reset();
                        ex.ToString();
                    }
                }
                //-----------------------------------------------

                if (deptWithFlag == "/")
                {
                    String ls_xml = DwMain.Describe("DataWindow.Data.XML");
                    String ls_xml_det = "", ls_deptcoop_id;
                    if (DwItem.RowCount > 0)
                    {
                        ls_xml_det = DwItem.Describe("DataWindow.Data.XML");
                    }
                    ls_deptcoop_id = DwMain.GetItemString(1, "deptcoop_id");


                    //ถ้าไม่ใช่เงินฝากประเภทประจำ ไม่ต้องส่ง detail ไป
                    if (deptgroup_code != "01") { ls_xml_det = ""; }

                    string errorMessage = "";
                    String[] result = new String[2];
                    Decimal adc_intsum = 0;
                    

                    int res = ndept.of_init_deptslip_calint(state.SsWsPass, accNo, ls_deptcoop_id, state.SsUsername, state.SsWorkDate, state.SsClientIp, ref ls_xml, ref ls_xml_det, ref errorMessage, ref adc_intsum);
                    result[0] = ls_xml;
                    result[1] = ls_xml_det;
                    DwUtil.ImportData(result[0], DwMain, tDwMain, FileSaveAsType.Xml);
                    //}                    
                    DwItem.Reset();
                    if (WebUtil.IsXML(result[1]))
                    {
                        DwUtil.ImportData(result[1], DwItem, tDwItem, FileSaveAsType.Xml);
                    }
                    //bee cls_chq set amt_net
                    if (cashType == "CHQ" && DwCheque.RowCount < 1)
                    {
                        JsPostInsertRowCheque();
                        decimal deptslip_netamt = DwMain.GetItemDecimal(1, "deptslip_netamt");
                        DwCheque.SetItemDecimal(1, "cheque_amt", deptslip_netamt);
                    }
                }
                else if (deptWithFlag == "+")
                {
                    decimal balance = DwMain.GetItemDecimal(1, "prncbal");
                    decimal deptAmt = 0;
                    //Decimal isEqualDept = depService.IsEqualDept(state.SsWsPass, accNo, state.SsCoopId, balance, deptTypeCode, recpPayGrp, ref deptAmt);

                    Decimal isEqualDept = ndept.of_is_equal_dept(state.SsWsPass, accNo, state.SsCoopId, balance, deptTypeCode, recpPayGrp, ref deptAmt);
                    if (isEqualDept > 0)
                    {
                        DwMain.SetItemDecimal(1, "deptslip_amt", (decimal)deptAmt);
                        DwMain.SetItemDecimal(1, "deptslip_netamt", (decimal)deptAmt);
                        JsPostTotalWidthFixed();
                        DwMain.Modify("deptslip_amt.Protect=1");
                    }

                    //HARD BY DOT
                    try
                    {
                        //int re = depService.of_chack_masdue(state.SsWsPass, accNo);

                        int re = ndept.of_chack_masdue(state.SsWsPass, accNo);
                    }
                    catch (Exception ex)
                    {
                        isException = true;
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);

                    }
                }
                //HARD BY DOT
                else if (deptWithFlag == "-") //&& DwMain.GetItemString(1, "Depttype_code") == "50")
                    //{
                    //    JsNewClear();
                    //    LtServerMessage.Text = WebUtil.ErrorMessage("ประเภทบัญชีนี้ไม่สามารถถอนได้");
                    //}

                    // DefaultSendGov(cashType);

                    if (DwMain.GetItemString(1, "depttype_code") == "10" && (DwMain.GetItemString(1, "recppaytype_code") == "DEP" || DwMain.GetItemString(1, "recppaytype_code") == "WID"))
                    {
                        //     LtServerMessage.Text = WebUtil.ErrorMessage("รายการ ฝาก/ถอน ออมทรัพย์(สด) กรุณาใช้หน้าจอใหม่");
                    }
            }
            catch (Exception ex)
            {
                isException = true;
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            finally
            {
                if (isException)
                {
                    accNo = null;
                    JsNewClear();
                }
            }
            this.deptAccountNo = accNo;
        }

        //JS-EVENT
        private void JsPostItemSelect()
        {
            try
            {
                String accNo = DwMain.GetItemString(1, "deptaccount_no");
                Decimal princBal = DwItem.GetItemDecimal(1, "prnc_bal");

                String xmlDwMain = DwMain.Describe("DataWindow.Data.XML");
                String xmlDwItem = DwItem.Describe("DataWindow.Data.XML");
                String deptcoop_id = DwMain.GetItemString(1, "deptcoop_id");
                Decimal adc_intsum = 0;
                String errorMessage = "";
                
                String[] result = new String[2];
                int res = ndept.of_init_deptslip_calint(state.SsWsPass, accNo, deptcoop_id, state.SsUsername, state.SsWorkDate, state.SsClientIp, ref xmlDwMain, ref xmlDwItem, ref errorMessage, ref adc_intsum);
                result[0] = xmlDwMain;
                result[1] = xmlDwItem;
                DwUtil.ImportData(result[0], DwMain, tDwMain, FileSaveAsType.Xml);
                DwUtil.ImportData(result[1], DwItem, tDwItem, FileSaveAsType.Xml);

                //แก้ไขเรื่องกรณีเป็น เช็ค เมื่อทำการคำนวณดอกเบี้ย ให้เซ็ตค่า กลับที่ยอดเช็คใหม่ 23/06/2558
                String cashType = DwMain.GetItemString(1, "cash_type").Trim();
                if (cashType == "CHQ")
                {
                    try
                    {
                        Decimal deptslip_netamt = 0;

                        deptslip_netamt = DwMain.GetItemDecimal(1, "deptslip_netamt");
                        DwCheque.SetItemDecimal(1, "cheque_amt", deptslip_netamt);
                    }
                    catch (Exception ex)
                    { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsNewClear()
        {

            DwMain.Reset();
            DwCheque.Reset();
            DwCheque.SaveDataCache();
            DwItem.Reset();
            DwItem.SaveDataCache();
            DwMain.InsertRow(0);
            DwMain.SetItemString(1, "deptcoop_id", state.SsCoopId);
            DwMain.SetItemDateTime(1, "deptslip_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            HdRequireCalInt.Value = "false";
            HdIsPostBack.Value = "false";
            HdPrintFin.Value = "false";

        }

        //JS-EVENT
        private void JsPostTotalWidthFixed()
        {
            try
            {
                String accNo = DwMain.GetItemString(1, "deptaccount_no");
                String xmlDwMain = DwMain.Describe("DataWindow.Data.XML");
                String xmlDwItem = DwItem.Describe("DataWindow.Data.XML");
                String deptGroupCode = TryDwMainGetString("deptgroup_code");//deptgroup_code 01 = ประจำ
                String deptWithFlag = TryDwMainGetString("deptwith_flag");
                //bee cls_chq
                if (deptWithFlag == "/" && DwMain.GetItemString(1, "cash_type") == "CHQ") {
                    decimal ld_chqamt = 0;
                    decimal deptslip_netamt = DwMain.GetItemDecimal(1, "deptslip_netamt");
                    int cheque_row = DwCheque.RowCount;
                    for (int i = 1; i <= cheque_row; i++)
                    {
                        ld_chqamt += DwCheque.GetItemDecimal(i, "cheque_amt");
                    }
                    if (ld_chqamt > deptslip_netamt)
                    {
                        deptslip_netamt = (cheque_row > 1 ? 0 : deptslip_netamt);                        
                        DwCheque.SetItemDecimal(cheque_row, "cheque_amt", deptslip_netamt);
                    }
                }
                if (deptGroupCode != "01" && deptWithFlag == "-")
                {
                    string ls_itemGrp = "", ls_itemtype_code = "", as_errmessage = "";
                    decimal ldc_intslip_amt, li_count_wtd;
                    string deptTypeCode = TryDwMainGetString("DEPTTYPE_CODE");

                    ls_itemGrp          = DwMain.GetItemString(1, "group_itemtpe");
                    ls_itemtype_code    = DwMain.GetItemString(1, "deptitemtype_code");
                    ldc_intslip_amt     = DwMain.GetItemDecimal(1, "deptslip_amt");
                    li_count_wtd        = DwMain.GetItemDecimal(1, "count_wtd");
                    Int16 count_wtd = Convert.ToInt16(li_count_wtd);
                   
                    decimal ldc_fee = 0;

                    int res = ndept.of_chk_withdrawcount(state.SsWsPass, deptTypeCode, state.SsCoopControl, accNo, ls_itemGrp, ls_itemtype_code, count_wtd, ldc_intslip_amt, ref count_wtd, ref ldc_fee, state.SsWorkDate, ref as_errmessage);
                    DwMain.SetItemDecimal(1, "count_wtd", Convert.ToDecimal(count_wtd));
                    DwMain.SetItemDecimal(1, "other_amt", Convert.ToDecimal(ldc_fee));
                    ///
                    //bank ให้แจ้งเตือนกรณีมีค่าปรับ
                    if (DwMain.GetItemDecimal(1, "other_amt") > 0)
                    { ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('มีค่าปรับ');", true); }
                    ///
                    Int16 feemeth = Convert.ToInt16(DwMain.GetItemString(1, "payfee_meth"));
                    if ((feemeth == 1))
                    {
                        DwMain.SetItemDecimal(1, "deptslip_netamt", DwMain.GetItemDecimal(1, "deptslip_amt"));
                    }
                    else if ((feemeth == 2))
                    {
                        DwMain.SetItemDecimal(1, "deptslip_netamt", ((DwMain.GetItemDecimal(1, "deptslip_amt")) - (DwMain.GetItemDecimal(1, "other_amt"))));
                    }
                    else if ((feemeth == 3))
                    {
                        DwMain.SetItemDecimal(1, "deptslip_netamt", ((DwMain.GetItemDecimal(1, "deptslip_amt"))));
                    }
                }

                if (deptGroupCode == "01" && deptWithFlag == "+")///by kowit  เงินฝาก ประจำ ฝาก
                {
                    DwItem.SetItemDecimal(1, "prnc_bal", DwMain.GetItemDecimal(1, "deptslip_amt"));
                    DwItem.SetItemDecimal(1, "prncslip_amt", DwMain.GetItemDecimal(1, "deptslip_netamt"));
                    tDwItem.Eng2ThaiAllRow();
                }
                else if (DwItem.RowCount > 0 && DwItem.GetItemDecimal(1, "prnc_no") > 0)///by kowit  เงินฝาก ประจำ ถอน หรือ ปิดบัญชี
                {
                    decimal itemAmt;
                    decimal slipAmt = DwMain.GetItemDecimal(1, "deptslip_amt");//GET จาก SLIP
                    for (int i = 1; i <= DwItem.RowCount; i++)
                    {
                        itemAmt = DwItem.GetItemDecimal(i, "prnc_bal");
                        if (slipAmt > itemAmt && itemAmt > 0)
                        {
                           
                            DwItem.SetItemDecimal(i, "select_flag", 1);
                            DwItem.SetItemDecimal(i, "prncslip_amt", itemAmt);
                        }
                        else
                        {

                            if (slipAmt == 0 || itemAmt == 0)
                            {
                                DwItem.SetItemDecimal(i, "select_flag", 0);
                                DwItem.SetItemDecimal(i, "prncslip_amt", 0);
                                DwItem.SetItemDecimal(i, "intslip_amt", 0);
                                DwItem.SetItemDecimal(i, "taxslip_amt", 0);
                                DwItem.SetItemDecimal(i, "fee_amt", 0);
                                DwItem.SetItemDecimal(i, "other_amt", 0);
                                DwItem.SetItemDecimal(i, "int_return", 0);
                                DwItem.SetItemDecimal(i, "intcur_accyear", 0);
                                DwItem.SetItemDecimal(i, "intarr_amt", 0);
                            }
                            else
                            {
                                HdRequireCalInt.Value = "true";
                                DwItem.SetItemDecimal(i, "select_flag", 1);
                                DwItem.SetItemDecimal(i, "prncslip_amt", slipAmt);
                            }
                        }
                        slipAmt -= itemAmt;
                        if (slipAmt < 0) slipAmt = 0;
                    }

                    if (DwItem.RowCount > 0)
                    {
                        xmlDwItem = DwItem.Describe("DataWindow.Data.XML");
                    }
                    else
                    {
                        xmlDwItem = "";
                    }
                    ///by kowit  เงินฝาก ใช้เมื่อ ประจำ ถอน หรือ ปิดบัญชี = ?
                    Decimal adc_intsum = 0;
                    String errorMessage = "";
                    String[] result = new String[2];
                    DateTime calint_date = new DateTime();
                    
                    int res = ndept.of_init_deptslip_calint(state.SsWsPass, accNo, HfCoopid.Value, state.SsUsername, calint_date, state.SsClientIp, ref xmlDwMain, ref xmlDwItem, ref errorMessage, ref adc_intsum);
                    result[0] = xmlDwMain;
                    result[1] = xmlDwItem;
                    DwUtil.ImportData(result[0], DwMain, tDwMain, FileSaveAsType.Xml);
                    String cashType = DwMain.GetItemString(1, "cash_type");
                    DefaultSendGov(cashType);
                    DwUtil.ImportData(result[1], DwItem, tDwItem, FileSaveAsType.Xml);
                }///by kowit  เงินฝาก ประจำ ถอน หรือ ปิดบัญชี

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            // ย้ายจุด focus ไปฟิวอื่น
            this.SetFocusByClientId("ctl00_Image4", this.GetType());
        }

        //JS-EVENT
        private void JspostSaveNoCheckApv()
        {
            SaveSheet();
        }

        //JS-EVENT
        private void JsPostInsertRowCheque()
        {
            DwCheque.InsertRow(0);
            DwCheque.SetItemDateTime(DwCheque.RowCount, "cheque_date", state.SsWorkDate);
            DwCheque.SetItemDecimal(DwCheque.RowCount, "day_float", int.Parse(HdDayPassCheq.Value));
            tDwCheque.Eng2ThaiAllRow();
            HdIsInsertCheque.Value = "true";
        }

        //JS-EVENT
        private void JsPostBankCode()
        {
            try
            {
                Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
                String bankCode = DwCheque.GetItemString(row, "bank_code");
                String sql = "select bank_desc from cmucfbank where bank_code='" + bankCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String bankName = dt.Rows[0][0].ToString().Trim();
                    DwCheque.SetItemString(row, "bank_name", bankName);
                }
                else
                {
                    throw new Exception("ไม่พบรหัสธนาคาร " + bankCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostBankBranchCode()
        {
            try
            {
                Int32 row = Convert.ToInt32(HdDwChequeRow.Value);
                String bankCode = DwCheque.GetItemString(row, "bank_code");
                String branchCode = DwCheque.GetItemString(row, "branch_code");
                String sql = "select branch_name from cmucfbankbranch where bank_code='" + bankCode + "' and branch_id='" + branchCode + "'";
                DataTable dt = WebUtil.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    String branchName = dt.Rows[0][0].ToString().Trim();
                    DwCheque.SetItemString(row, "branch_name", branchName);
                }
                else
                {
                    throw new Exception("ไม่พบรหัสสาขาธนาคารเลขที่ " + branchCode);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //JS-EVENT
        private void JsPostDeleteRowCheque()
        {
            try
            {
                DwCheque.DeleteRow(int.Parse(HdDwChequeRow.Value));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        //INNER FUNCTION
        private void LoopCheque()
        {
            try
            {
                for (int i = 1; i <= DwCheque.RowCount; i++)
                {
                    try
                    {
                        String chequeNo = DwUtil.GetString(DwCheque, i, "cheque_no", "");
                        completeCheque = chequeNo == "" ? false : completeCheque;
                        // int ii = chequeNo == "" ? 0 : int.Parse(chequeNo);
                        int ii = chequeNo == "" ? 0 : int.Parse(chequeNo) > 0 ? int.Parse(chequeNo) : 1;

                        if (ii > 0)
                        {
                            DwCheque.SetItemString(i, "cheque_no", ii.ToString("00000000"));
                        }
                        else
                        {
                            completeCheque = false;
                        }
                    }
                    catch { completeCheque = false; }
                }
            }
            catch { }
        }

        private void DefaultSendGov(String cashType)
        {
            Decimal deptSlipNetAmt = DwMain.GetItemDecimal(1, "deptslip_netamt");
            DwMain.SetItemDecimal(1, "send_gov", 0);
            if (cashType == "CSH" && deptSlipNetAmt >= 2000000)
            {
                DwMain.SetItemDecimal(1, "send_gov", 1);
            }
            else
            {
                DwMain.SetItemDecimal(1, "send_gov", 0);
            }
        }

        //Using Deposit Service
        private void DepositPost()
        {
            try
            {
                String ls_xml_main = DwMain.Describe("DataWindow.Data.XML");
                String ls_xml_cheque = "";
                String ls_xml_item = "";
                String cashType = DwMain.GetItemString(1, "cash_type");
                String as_apvdoc = Hdas_apvdoc.Value;
                if (cashType == "CHQ" && DwCheque.RowCount > 0)
                {
                    for (int i = 1; i <= DwCheque.RowCount; i++)
                    {
                        int ii = int.Parse(DwCheque.GetItemString(i, "cheque_no"));
                        DwCheque.SetItemString(i, "cheque_no", ii.ToString("00000000"));
                    }
                    ls_xml_cheque = DwCheque.Describe("DataWindow.Data.XML");
                }
                if (DwItem.RowCount > 0 && DwItem.GetItemDecimal(1, "prnc_no") > 0)
                {
                    ls_xml_item = DwItem.Describe("DataWindow.Data.XML");
                }
               
                String result = ndept.of_deposit(state.SsWsPass, ls_xml_main, ls_xml_cheque, ls_xml_item, as_apvdoc);
                JsNewClear();
                DwListCoop.Reset();
                DwListCoop.InsertRow(0);
               String endMessage = "บันทึกทำรายการฝากบัญชี " + WebUtil.ViewAccountNoFormat(deptAccountNo) + " เรียบร้อยแล้ว";
                CallPrintBook(result, endMessage);
                Hdas_apvdoc.Value = "";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                String msg = WebUtil.ErrorMessage(ex);
                try
                {
                    msg = msg.Substring(0, msg.LastIndexOf("*"));
                    msg = msg.Substring(msg.LastIndexOf("*") + 1, 10);
                    Hdas_apvdoc.Value = msg.Trim();
                }
                catch
                {
                }


            }
            HdIsPostBack.Value = "false";

        }

        //Using Withdraw Service
        private void Withdraw()
        {
            try
            {
                String ls_xml = DwMain.Describe("DataWindow.Data.XML");
                String ls_xml_det = "";
                String ls_xml_cheque = "";
                String cashType = DwMain.GetItemString(1, "cash_type");
                String as_apvdoc = Hdas_apvdoc.Value;

                if (cashType == "CHQ" && DwCheque.RowCount > 0)
                {
                    for (int i = 1; i <= DwCheque.RowCount; i++)
                    {
                        int ii = int.Parse(DwCheque.GetItemString(i, "cheque_no"));
                        DwCheque.SetItemString(i, "cheque_no", ii.ToString("00000000"));
                    }
                    ls_xml_cheque = DwCheque.Describe("DataWindow.Data.XML");
                }

                if (DwItem.RowCount > 0 && DwItem.GetItemDecimal(1, "prnc_no") > 0)
                {
                    ls_xml_det = DwItem.Describe("DataWindow.Data.XML");
                }
               
                String result = ndept.of_withdraw_close(state.SsWsPass, ls_xml, ls_xml_det, ls_xml_cheque, as_apvdoc);
                JsNewClear();
                DwListCoop.Reset();
                DwListCoop.InsertRow(0);
                String endMessage = "บันทึกทำรายการถอนเงินบัญชี " + WebUtil.ViewAccountNoFormat(deptAccountNo) + " เรียบร้อยแล้ว";
                CallPrintBook(result, endMessage);
                Hdas_apvdoc.Value = "";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                String msg = WebUtil.ErrorMessage(ex);
                try
                {
                    msg = msg.Substring(0, msg.LastIndexOf("*"));
                    msg = msg.Substring(msg.LastIndexOf("*") + 1, 10);
                    Hdas_apvdoc.Value = msg.Trim();
                }
                catch
                {

                }

            }
            HdIsPostBack.Value = "false";
        }

        //Using Close Account Service
        private void CloseAccount()
        {
            try
            {
                String ls_xml = DwMain.Describe("DataWindow.Data.XML");
                String ls_xml_det = "";
                String ls_xml_cheque = "";
                String cashType = DwMain.GetItemString(1, "cash_type");
                String as_apvdoc = Hdas_apvdoc.Value;

                if (cashType == "CHQ" && DwCheque.RowCount > 0)
                {
                    for (int i = 1; i <= DwCheque.RowCount; i++)
                    {
                        int ii = int.Parse(DwCheque.GetItemString(i, "cheque_no"));
                        DwCheque.SetItemString(i, "cheque_no", ii.ToString("00000000"));
                    }
                    ls_xml_cheque = DwCheque.Describe("DataWindow.Data.XML");
                }

                try
                {
                    if (DwItem.RowCount <= 0)
                    {
                        throw new Exception();
                    }
                    ls_xml_det = DwItem.Describe("DataWindow.Data.XML");
                }
                catch
                {
                    ls_xml_det = "";
                }
                //////////////////////ปิดบัญชี    ลำปาง            
                String accNo = DwMain.GetItemString(1, "deptaccount_no");
                DateTime startdate_save = new DateTime();
                DateTime enddate_save = new DateTime();
                String sql_re = "select start_date,end_date from dpdeptmasdue where deptaccount_no = '" + accNo + "'";
                Sdt sql_ck = WebUtil.QuerySdt(sql_re);
                if (sql_ck.Next())
                {
                    startdate_save = sql_ck.GetDate("start_date");
                    enddate_save = sql_ck.GetDate("end_date");
                }
                String result = "";
                String endMessage = "";
                String ck_depttype = accNo.Substring(3, 2);
                String recppaytype = DwMain.GetItemString(1, "recppaytype_code");
                String tofrom_accid = DwMain.GetItemString(1, "tofrom_accid");
                //recppaytype = recppaytype.Substring(0, 1);                

                result = ndept.of_withdraw_close(state.SsWsPass, ls_xml, ls_xml_det, ls_xml_cheque, as_apvdoc);
                JsNewClear();
                DwListCoop.Reset();
                DwListCoop.InsertRow(0);
                endMessage = "บันทึกทำรายการปิดบัญชี " + WebUtil.ViewAccountNoFormat(deptAccountNo) + " เรียบร้อยแล้ว";
                CallPrintBook(result, endMessage);
                Hdas_apvdoc.Value = "";
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                String msg = WebUtil.ErrorMessage(ex);
                try
                {
                    msg = msg.Substring(0, msg.LastIndexOf("*"));
                    msg = msg.Substring(msg.LastIndexOf("*") + 1, 10);
                    Hdas_apvdoc.Value = msg.Trim();
                }
                catch
                {
                }
            }
            HdIsPostBack.Value = "false";
        }

        private void SaveSheet()
        {
            String control = DwMain.GetItemString(1, "deptwith_flag");
            if (control == "+")
            {
                DepositPost();
            }
            else if (control == "-")
            {
                Decimal with = DwMain.GetItemDecimal(1, "withdrawable_amt");
                Decimal dpr = Convert.ToDecimal(DwMain.GetItemString(1, "deptslip_amt"));
                if (with >= dpr)
                {
                    Withdraw();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้ เนื่องจากจำนวนเงินที่ถอนได้ไม่พอ");
                }
            }
            else if (control == "/")
            {
                CloseAccount();
            }
            HdIsPostBack.Value = "false";
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {

            //----------------------------------------------------------------------
            //----------------------------------------------------------------------
            postNewAccount = WebUtil.JsPostBack(this, "postNewAccount");
            postDeptWith = WebUtil.JsPostBack(this, "postDeptWith");
            postRecpPayTypeCode = WebUtil.JsPostBack(this, "postRecpPayTypeCode");
            newClear = WebUtil.JsPostBack(this, "newClear");
            postBankCode = WebUtil.JsPostBack(this, "postBankCode");
            postBankBranchCode = WebUtil.JsPostBack(this, "postBankBranchCode");
            postTotalWidthFixed = WebUtil.JsPostBack(this, "postTotalWidthFixed");
            postItemSelect = WebUtil.JsPostBack(this, "postItemSelect");
            postSaveNoCheckApv = WebUtil.JsPostBack(this, "postSaveNoCheckApv");
            postInsertRowCheque = WebUtil.JsPostBack(this, "postInsertRowCheque");
            postDeleteRowCheque = WebUtil.JsPostBack(this, "postDeleteRowCheque");
            postPost = WebUtil.JsPostBack(this, "postPost");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
            setCoopname = WebUtil.JsPostBack(this, "setCoopname");
            postchgremark = WebUtil.JsPostBack(this, "postchgremark");
            postpassbook = WebUtil.JsPostBack(this, "postpassbook");
            //----------------------------------------------------------------------
            //----------------------------------------------------------------------
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("deptslip_date", "deptslip_date_tdate");
            //----------------------------------------------------------------------
            tDwCheque = new DwThDate(DwCheque, this);
            tDwCheque.Add("cheque_date", "cheque_tdate");
            //----------------------------------------------------------------------
            tDwItem = new DwThDate(DwItem, this);
            tDwItem.Add("calint_from", "calint_from_tdate");
            tDwItem.Add("calint_to", "calint_to_tdate");
            tDwItem.Add("prncdue_date", "prncdue_tdate");//prnc_tdate
            tDwItem.Add("prnc_date", "prnc_tdate");//prnc_tdate
            //----------------------------------------------------------------------
        }

        public void WebSheetLoadBegin()
        {
            HdPrintFlag.Value = "false";
            HdPrintBook.Value = "";
            HdPrintSlip.Value = "";
            HdNewAccountNo.Value = "";
            HdCheckApvAlert.Value = "";
            HdIsInsertCheque.Value = "";
            completeCheque = true;
            try
            {
                //depService = wcf.Deposit;
                ndept = wcf.NDeposit;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            //depConfig = new DepositConfig(ndept);
            if (Session["is_auto_deptwith"] != null)
            {
                checkBox1Before = CheckBox1.Checked;
                CheckBox1.Checked = IsAutoDeptWith;
            }
            //---------------------------------------------------------------------
            if (IsPostBack)
            {
                try
                {
                    this.RestoreContextDw(DwMain);
                    this.RestoreContextDw(DwCheque);
                    this.RestoreContextDw(DwItem);
                    this.RestoreContextDw(DwListCoop);
                }
                catch { }
                HdIsPostBack.Value = "true";
            }
            else
            {
              
                try
                {
                    /// เช็คสถานะระบบการเงินว่ามีการปิดวัน หรือ มีการปิดลิ้นชักหรือยัง
                    Decimal closeday_status = 0, status = 0;

                    String sql = "select closeday_status from amappstatus where coop_id = '" + state.SsCoopId + @"' 
                        and application = '" + state.SsApplication + "'";
                    Sdt ta = WebUtil.QuerySdt(sql);

                    if (ta.Next())
                    {
                        closeday_status = ta.GetDecimal("closeday_status");
                    }
                    if (closeday_status == 1)
                    { throw new Exception("ระบบได้ทำการปิดสิ้นวัน"); }
                    else
                    {
                        //by num PTT ตรวจ เอาแค่เงินสด เงินโอนไม่ต้องตรวจ cashType = 'CSH' และ  state.SsCoopControl = ''
                       if ( state.SsCoopControl != "011001" ) {
                        
                        String sql2 = "select status from fintableusermaster where coop_id = '" + state.SsCoopId + @"' 
                            and user_name = '" + state.SsUsername.Trim() + @"' 
                            and opdatework = convert(datetime,'" + state.SsWorkDate.ToString("yyyy/MM/dd") + "')";
                        Sdt ta2 = WebUtil.QuerySdt(sql2);

                        if (ta2.Next())
                        {
                            status = ta2.GetDecimal("status");
                        }
                        if (Convert.ToInt32(status) == 14)
                        {
                            throw new Exception("ได้ทำการปิดลิ้นชักประจำวัน");
                        }
                        else if (Convert.ToInt32(status) == 0)
                        {
                            throw new Exception("ผู้ใช้งานยังไม่ได้ทำการเปิดลิ้นชักประจำวัน");
                        }
                       }

                    }

                    HdDayPassCheq.Value = WebUtil.GetDpDeptConstant("daypasschq");
                }
                catch
                {
                    HdDayPassCheq.Value = "1";
                }
                HdIsPostBack.Value = "false";
            }
            if (DwMain.RowCount < 1)
            {
                DwMain.InsertRow(0);
                DwMain.SetItemDate(1, "deptslip_date", state.SsWorkDate);
                DwListCoop.InsertRow(0);
                DwMain.SetItemString(1, "deptcoop_id", state.SsCoopId);
                tDwMain.Eng2ThaiAllRow();
                HfCheck.Value = "False";
                HfCoopid.Value = state.SsCoopId;

            }
            LoopCheque();
            setnextdate(); //FN เลือนวันที่ออกไปอีก 1 วัน ใช่สำหรับทำ CHQ ของ PEA

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postBankCode")
            {
                JsPostBankCode();
            }
            else if (eventArg == "postBankBranchCode")
            {
                JsPostBankBranchCode();
            }
            else if (eventArg == "postNewAccount")
            {
                JsNewAccountNo();
            }
            else if (eventArg == "postpassbook")
            {
                Jspostpassbook();
            }
            else if (eventArg == "postDeptWith")
            {
                JsPostDeptWith();
            }
            else if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "postTotalWidthFixed")
            {
                JsPostTotalWidthFixed();
            }
            else if (eventArg == "postItemSelect")
            {
                JsPostItemSelect();
            }
            else if (eventArg == "postSaveNoCheckApv")
            {
                deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                SaveSheet();
            }
            else if (eventArg == "postInsertRowCheque")
            {
                JsPostInsertRowCheque();
            }
            else if (eventArg == "postDeleteRowCheque")
            {
                JsPostDeleteRowCheque();
            }
            else if (eventArg == "CheckCoop")
            {
                checkCoop();
            }
            else if (eventArg == "setCoopname")
            {
                JsSetCoopname();
            }
            else if (eventArg == "postchgremark")
            {
                Jspostchgremark();
            }
        }

        public void SaveWebSheet()
        {
            //if (!completeCheque)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกเลขที่เช็คให้ครบถ้วน!");
            //    return;
            //}
            try
            {
                //for icoopid is 008001 check wct code that is next workday //by num ??
                if ((DwMain.GetItemString(1, "recppaytype_code") == "WCT"
                    || DwMain.GetItemString(1, "recppaytype_code") == "WDT"
                    || DwMain.GetItemString(1, "recppaytype_code") == "CCT"
                    || DwMain.GetItemString(1, "recppaytype_code") == "CDT"

                    ) && state.SsCoopId.Equals("008001"))
                {
                    DateTime deptslip_date = new DateTime();
                    string ddate = Hf_nextdate.Value;
                    string Hdslip_date_new = ddate.Substring(2, 2) + "-" + ddate.Substring(0, 2) + "-" + (Int32.Parse(ddate.Substring(4, 4)) - 543).ToString();
                    deptslip_date = DateTime.Parse(Hdslip_date_new);

                    DwMain.SetItemDateTime(1, "deptslip_date", deptslip_date.Date);
                    //     DwMain.SetItemDateTime(1, "entry_date", deptslip_date.Date);
                    DwMain.SetItemDateTime(1, "calint_to", deptslip_date.Date);

                }


                DwMain.SetItemString(1, "deptitemtype_code", DwMain.GetItemString(1, "recppaytype_code"));
                //String control = DwMain.GetItemString(1, "deptwith_flag");
                deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                String slipXml = DwMain.Describe("datawindow.data.xml");
                String chqXml = DwCheque.Describe("datawindow.data.xml");
                String deptcoop_id = HfCoopid.Value;
                String as_apvdoc = Hdas_apvdoc.Value;
                //String SHdas_apvdoc = WebUtil.Right(as_apvdoc, 10);
                /// HARD
                ////   String[] apv = depService.CheckRightPermissionDep(state.SsWsPass, state.SsUsername, deptcoop_id, slipXml, chqXml, 0, ref as_apvdoc);
                String[] apv = new String[2];
                int res = ndept.of_check_right_permission(state.SsWsPass, state.SsUsername, deptcoop_id, slipXml, chqXml, 0, ref as_apvdoc, ref apv[0], ref apv[1]);
                apv[0] = res == 1 ? "true" : apv[0];

                if (apv[0] == "true")
                {
                    SaveSheet();
                    //if (control == "+")
                    //{
                    //    DepositPost();
                    //}
                    //else if (control == "-")
                    //{
                    //    Withdraw();
                    //}
                    //else if (control == "/")
                    //{
                    //    CloseAccount();
                    //}
                    HdIsPostBack.Value = "false";
                }
                else
                {
                    DataTable dt = WebUtil.Query("select member_no from dpdeptmaster where deptaccount_no='" + deptAccountNo + "'");
                    String memberNo = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
                    Decimal netAmt = DwUtil.GetDec(DwMain, 1, "deptslip_netamt", 0);
                    String accName = DwUtil.GetString(DwMain, 1, "deptaccount_name", "");
                    String itemType = DwUtil.GetString(DwMain, 1, "recppaytype_code", "");

                    String itemTypeDesc = WebUtil.GetRecpPayTypeDesc(TryDwMainGetString("recppaytype_code"));

                    //String reportNo = depService.AddApvTask(state.SsWsPass, state.SsUsername, state.SsApplication, state.SsClientIp, itemType, apv[1], deptAccountNo, memberNo, state.SsWorkDate, netAmt, deptAccountNo, accName, apv[0], "DEP", 1, state.SsCoopId,ref as_apvdoc);
                    // HdProcessId.Value = reportNo;
                    HdAvpCode.Value         = apv[0];
                    HdItemType.Value        = itemType;
                    HdAvpAmt.Value          = netAmt.ToString();
                    HdCheckApvAlert.Value   = "true";
                }

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }
            HdIsPostBack.Value = "false";

        }

        public void WebSheetLoadEnd()
        {
            //เช็ค deptAccountNo
            try
            {
                deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                deptAccountNo = string.IsNullOrEmpty(deptAccountNo) ? null : deptAccountNo;
            }
            catch { deptAccountNo = null; }
            //ใส่ตัวเงินคงเหลือลง text label
            if (!string.IsNullOrEmpty(deptAccountNo))
            {
                DwMain.Modify("prncbal_t.text='" + DwMain.GetItemDecimal(1, "prncbal").ToString("#,##0.00") + "'");
                DwMain.Modify("withdrawable_amt_t.text='" + DwMain.GetItemDecimal(1, "withdrawable_amt").ToString("#,##0.00") + "'");

                String deptWithFlag = TryDwMainGetString("deptwith_flag");
                decimal chk_balance = DwMain.GetItemDecimal(1, "prncbal");

                if (deptWithFlag == "+")
                {
                    chk_balance = chk_balance + DwMain.GetItemDecimal(1, "deptslip_amt");
                    DwMain.Modify("chk_balance.text='" + chk_balance.ToString("#,##0.00") + "'");
                }
                else if (deptWithFlag == "-")
                {
                    chk_balance = chk_balance - DwMain.GetItemDecimal(1, "deptslip_amt");
                    DwMain.Modify("chk_balance.text='" + chk_balance.ToString("#,##0.00") + "'");
                }
                else
                {
                    DwMain.Modify("chk_balance.text='" + DwMain.GetItemDecimal(1, "withdrawable_amt").ToString("#,##0.00") + "'");
                }

                //ใส่ชื่อ นามสกุล ทะเบียนลง  text label
                try
                {
                    DataTable dt = WebUtil.Query("select member_no from dpdeptmaster where deptaccount_no='" + deptAccountNo + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DwMain.Modify("t_member_no.text='" + dt.Rows[0]["member_no"].ToString() + "'");
                        HdMemberNo.Value = dt.Rows[0][0].ToString();
                    }
                }
                catch (Exception ex) { ex.ToString(); }
            }
            //ขั้นตอนเกี่ยวกับรายละเอียดประเภทเงิน
            String cashType = "";
            if (!String.IsNullOrEmpty(deptAccountNo))
            {
                try
                {
                    String sql = "select condforwithdraw from dpdeptmaster where deptaccount_no = '" + deptAccountNo + "'";
                    DataTable dt = WebUtil.Query(sql);
                    String condition = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
                    DwMain.Modify("t_33.text='" + condition + "'");
                    if(state.SsCoopId=="003001"){  //เพิ่มการแสดงรายการฝากถอน  เพื่อ...
                        String sql2 = "select dept_objective from dpdeptmaster where deptaccount_no = '" + deptAccountNo + "'";
                        DataTable dt2 = WebUtil.Query(sql2);
                        String condition2 = dt2.Rows.Count > 0 ? dt2.Rows[0][0].ToString() : "";
                        DwMain.Modify("t_dept_objective.text='" + condition2 + "'");
                    }
                }
                catch { }
                String recPayTypeCode = DwMain.GetItemString(1, "recppaytype_code");
                if (!String.IsNullOrEmpty(recPayTypeCode))
                {
                     DwMain.Modify("recppaytype_desc_t.text='" + recPayTypeCode + "  " + WebUtil.GetRecpPayTypeDesc(TryDwMainGetString("recppaytype_code")) + "'");
                    try
                    {
                        //ใส่ dddw สำหรับคู่บัญชี
                        cashType = ndept.of_get_cashtype(state.SsWsPass, recPayTypeCode);
                        DwMain.SetItemString(1, "cash_type", cashType);
                        WebUtil.RetrieveDDDW(DwMain, "tofrom_accid", "dp_slip.pbl", state.SsCoopControl);
                        DataWindowChild dcToFromAccId = DwMain.GetChild("tofrom_accid");
                        dcToFromAccId.SetFilter("cash_type = '" + cashType + "'");
                        dcToFromAccId.Filter();
                        if (cashType == "CSH")
                        {
                            String accToFromNo1 = dcToFromAccId.GetItemString(1, "account_id");
                            DwMain.SetItemString(1, "tofrom_accid", accToFromNo1);
                        }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text += (":::" + WebUtil.ErrorMessage(ex)).ToString();
                    }
                }
            }
            //ให้ลบแถวถ้าประเภทเงินไม่ใช่เช็ค
            if (cashType != "CHQ")
            {
                int i = 0;
                while (DwCheque.RowCount > 0)
                {
                    DwCheque.DeleteRow(0);
                    if (i++ > 500) break;
                }
            }

            //เงื่อนไขการ protected การคีย์เงิน
            if (cashType == "CHQ")
            {
                DwMain.Modify("deptslip_amt.Protect=1");
            }

            //เครียค่า WebService และเซฟ DataCache
            DwMain.SaveDataCache();
            DwItem.SaveDataCache();
            DwCheque.SaveDataCache();
            DwListCoop.SaveDataCache();
        }

        #endregion

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsAutoDeptWith = checkBox1Before;
            CheckBox1.Checked = checkBox1Before;
        }

        private void checkCoop()
        {
            decimal i = 0;
            decimal crossflag = DwListCoop.GetItemDecimal(1, "cross_coopflag");
            if (crossflag == 1)
            {
                try
                {
                    i = DwListCoop.GetItemDecimal(1, "cross_coopflag");
                }
                catch
                { }
                JsNewClear();
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopControl, state.SsCoopId);
            }
            else
            {
                try
                {
                    JsNewClear();
                    DwMain.SetItemString(1, "deptcoop_id", state.SsCoopId);
                    HfCoopid.Value = state.SsCoopId;
                }
                catch
                { }
            }
        }

        private void JsSetCoopname()
        {
            String Coopid = HfCoopid.Value;
            String Coopname;
            DataTable dt = WebUtil.Query("select coop_name from cmcoopmaster where coop_id ='" + Coopid + "'");
            if (dt.Rows.Count > 0)
            {
                Coopname = dt.Rows[0]["coop_name"].ToString();
                DwListCoop.SetItemDecimal(1, "cross_coopflag", 1);
                DwUtil.RetrieveDDDW(DwListCoop, "dddwcoopname", "cm_constant_config.pbl", state.SsCoopId, state.SsCoopControl);
                DwListCoop.SetItemString(1, "dddwcoopname", Coopname);
            }
            JsNewAccountNo();
        }

        private void Jspostchgremark()
        {

            DataTable dt = WebUtil.Query("select account_name from accmaster where account_id = '" + DwMain.GetItemString(1, "tofrom_accid") + "' and coop_id = '" + state.SsCoopControl + "'");
            if (dt.Rows.Count > 0)
            {
                DwMain.SetItemString(1, "remark", dt.Rows[0]["account_name"].ToString());
            }

        }
        /// <summary>
        /// set next work date for nexdate operation
        /// </summary>
        public void setnextdate()
        {
            //cmService = wcf.NCommon;
            ncommon = wcf.NCommon;
            try
            {
                DateTime nextdate = new DateTime();
                //cmService.GetNextWorkDay(state.SsWsPass, state.SsWorkDate, ref nextdate);

                ncommon.of_getnextworkday(state.SsWsPass, state.SsWorkDate, ref nextdate);
                Hf_nextdate.Value = nextdate.Day.ToString("00") + nextdate.Month.ToString("00") + (nextdate.Year + 543).ToString();
                Hd_slipdate.Value = nextdate.Day.ToString("00") + "/" + nextdate.Month.ToString("00") + "/" + (nextdate.Year + 543).ToString();
            }
            catch (Exception e)
            {
                //for debug
                Console.WriteLine("Error: " + e.StackTrace);
            }
        }

        /// <summary>
        /// find number of principle fixed balance of deposit account
        /// </summary>
        /// <param name="deptaccno">fixed account</param>
        /// <returns>number of fixed principle balance</returns>
        private int getnumprncfixed(String deptaccno)
        {

            int numprncfixed = 0;

            DataTable dt = WebUtil.Query("select count(*) as pcount from dpdeptprncfixed where deptaccount_no = '" + deptaccno + "' and coop_id = '" + state.SsCoopControl + "'");
            if (dt.Rows.Count > 0)
            {
                numprncfixed = int.Parse(dt.Rows[0]["pcount"].ToString());
            }

            return numprncfixed;
        }

        /// <summary>
        /// find duedate for fixed deposit account
        /// </summary>
        /// <param name="deptaccno">fixed deposit account</param>
        /// <returns>due date of fixed prnciple account</returns>
        private DateTime getenddate(String deptaccno)
        {

            DataTable dtdate = WebUtil.Query("select end_date  from dpdeptmasdue where deptaccount_no = '" + deptaccno + "' and coop_id = '" + state.SsCoopControl + "'");
            if (dtdate.Rows.Count > 0)
            {
                String test = dtdate.Rows[0]["end_date"].ToString().Substring(0, 10);
                DateTime run = DateTime.ParseExact(test.Trim(), "M/dd/yyyy", new System.Globalization.CultureInfo("en-US")).Date;
                return DateTime.ParseExact(test.Trim(), "M/dd/yyyy", new System.Globalization.CultureInfo("en-US")).Date;
            }
            return state.SsWorkDate;
        }

        Decimal GetSumByDeptGrp(Decimal LimitDeptByGroupTimeUnit, String DeptGroupCode)
        {

            String SqlGetSumDept = "", MemberNo = "";
            Decimal SumDept = 0;
            MemberNo = HdMemberNo.Value.Trim();
            switch (Convert.ToInt32(LimitDeptByGroupTimeUnit))
            {
                case 0:
                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                   " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                   " where stm.operate_date between CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "') " +
                                   "       and  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "')" +
                                   "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                   "       and itm.sign_flag = 1 " +
                                   "       and stm.cash_type <> 'TRN' " +
                                   "       and itm.deptitemtype_code <> 'DTF' " +
                                   "       and exists (select 1 " +
                                   "                   from dpdeptmaster mas ,dpdepttype dt " +
                                   "                   where mas.deptaccount_no =stm.deptaccount_no " +
                                   "                         and dt.depttype_code = mas.depttype_code " +
                                   "                         and mas.deptclose_status = 0 " +
                                   "                         and mas.prncbal>0 " +
                                   "                         and dt.deptgroup_code = '" + DeptGroupCode + "' " +
                                   "                         and mas.member_no = '" + MemberNo + "' )";
                    break;
                case 1:

                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                    " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                    " where stm.operate_date between add_months( ( last_day( trunc( CONVERT(Datetime,'" + state.SsWorkDate.ToString("yyyy/MM/dd", WebUtil.EN) + "') ) ) + 1 ) , -1 ) " +
                                    "       and  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "')" +
                                    "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                    "       and itm.sign_flag = 1 " +
                                    "       and stm.cash_type <> 'TRN' " +
                                    "       and itm.deptitemtype_code <> 'DTF' " +
                                    "       and exists (select 1 " +
                                    "                   from dpdeptmaster mas ,dpdepttype dt " +
                                    "                   where mas.deptaccount_no =stm.deptaccount_no " +
                                    "                         and dt.depttype_code = mas.depttype_code " +
                                    "                         and mas.deptclose_status = 0 " +
                                    "                         and mas.prncbal>0 " +
                                    "                         and dt.deptgroup_code = '" + DeptGroupCode + "' " +
                                    "                         and mas.member_no = '" + MemberNo + "' )";

                    break;
                case 2:

                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                    " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                    " where stm.operate_date between add_months( ( last_day( trunc(  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "') ) ) + 1 ) , -3 ) " +
                                    "       and  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "')" +
                                    "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                    "       and itm.sign_flag = 1 " +
                                    "       and stm.cash_type <> 'TRN' " +
                                    "       and itm.deptitemtype_code <> 'DTF' " +
                                    "       and exists (select 1 " +
                                    "                   from dpdeptmaster mas ,dpdepttype dt " +
                                    "                   where mas.deptaccount_no =stm.deptaccount_no " +
                                    "                         and dt.depttype_code = mas.depttype_code " +
                                    "                         and mas.deptclose_status = 0 " +
                                    "                         and mas.prncbal>0 " +
                                    "                         and dt.deptgroup_code = '" + DeptGroupCode + "' " +
                                    "                         and mas.member_no = '" + MemberNo + "' )";

                    break;
                case 3:

                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                   " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                   " where stm.operate_date between add_months( ( last_day( trunc(  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "') ) ) + 1 ) , -12 ) " +
                                   "       and  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "')" +
                                   "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                   "       and itm.sign_flag = 1 " +
                                   "       and stm.cash_type <> 'TRN' " +
                                   "       and itm.deptitemtype_code <> 'DTF' " +
                                   "       and exists (select 1 " +
                                   "                    from dpdeptmaster mas ,dpdepttype dt " +
                                   "                    where mas.deptaccount_no =stm.deptaccount_no " +
                                   "                          and dt.depttype_code = mas.depttype_code " +
                                   "                          and mas.deptclose_status = 0 " +
                                   "                          and mas.prncbal>0 " +
                                   "                          and dt.deptgroup_code = '" + DeptGroupCode + "' " +
                                   "                          and mas.member_no = '" + MemberNo + "' )";

                    break;

            }
            DataTable dtSumDept = new DataTable();
            try
            {
                dtSumDept = WebUtil.Query(SqlGetSumDept);
                if (dtSumDept.Rows.Count > 0)
                {
                    SumDept = Convert.ToDecimal(dtSumDept.Rows[0]["sumdept"].ToString());
                }
                else
                {
                    SumDept = 0;
                }

            }
            catch (Exception ex)
            {
                SumDept = 0;
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้ " + ex);
            }

            return SumDept;
        }

        Decimal GetSumByDeptType(Decimal LimitDeptPerunit, Decimal LimitDeptUnit, Decimal LimitdTimeunit, String deptTypeCode)
        {

            String SqlGetSumDept = "", LimitDeptPerunitDesc = "", LimitDeptPerunitVal = "";
            Decimal SumDept = 0;
            switch (Convert.ToInt32(LimitDeptPerunit))
            {
                case 0:
                    LimitDeptPerunitDesc = "deptaccount_no";
                    LimitDeptPerunitVal = DwMain.GetItemString(1, "deptaccount_no");
                    break;
                case 1:
                    LimitDeptPerunitDesc = "member_no";
                    LimitDeptPerunitVal = HdMemberNo.Value.Trim();
                    break;
            }

            switch (Convert.ToInt32(LimitdTimeunit))
            {
                case 0:
                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                   " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                   " where stm.operate_date between CONVERT(DATETIME,'" + state.SsWorkDate.ToString("yyyy/MM/dd", WebUtil.EN) + "') - " + LimitDeptUnit + " " +
                                   "       and CONVERT(DATETIME,'" + state.SsWorkDate.ToString("yyyy/MM/dd", WebUtil.EN) + "')" +
                                   "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                   "       and itm.sign_flag = 1 " +
                                   "       and stm.cash_type <> 'TRN' " +
                                   "       and itm.deptitemtype_code <> 'DTF' " +
                                   "       and exists (select 1 " +
                                   "                   from dpdeptmaster mas ,dpdepttype dt " +
                                   "                   where mas.deptaccount_no =stm.deptaccount_no " +
                                   "                         and dt.depttype_code = mas.depttype_code " +
                                   "                         and mas.deptclose_status = 0 " +
                                   "                         and mas.prncbal>0 " +
                                   "                         and dt.depttype_code = '" + deptTypeCode + "' " +
                                   "                         and mas." + LimitDeptPerunitDesc + " = '" + LimitDeptPerunitVal + "' )";
                    break;
                case 1:

                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                    " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                    " where stm.operate_date between add_months( ( last_day( trunc(  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "') ) ) + 1 ) , -" + LimitDeptUnit + " ) " +
                                    "       and CONVERT(DATETIME,'" + state.SsWorkDate.ToString("yyyy/MM/dd", WebUtil.EN) + "')" +
                                    "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                    "       and itm.sign_flag = 1 " +
                                    "       and stm.cash_type <> 'TRN' " +
                                    "       and itm.deptitemtype_code <> 'DTF' " +
                                    "       and exists (select 1 " +
                                    "                   from dpdeptmaster mas ,dpdepttype dt " +
                                    "                   where mas.deptaccount_no =stm.deptaccount_no " +
                                    "                         and dt.depttype_code = mas.depttype_code " +
                                    "                         and mas.deptclose_status = 0 " +
                                    "                         and mas.prncbal>0 " +
                                    "                         and dt.depttype_code = '" + deptTypeCode + "' " +
                                    "                         and mas." + LimitDeptPerunitDesc + " = '" + LimitDeptPerunitVal + "' )";

                    break;
                case 2:

                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                    " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                    " where stm.operate_date between add_months( ( last_day( trunc(  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "') ) ) + 1 ) , (-3*" + LimitDeptUnit + ") ) " +
                                    "       and CONVERT(DATETIME,'" + state.SsWorkDate.ToString("yyyy/MM/dd", WebUtil.EN) + "')" +
                                    "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                    "       and itm.sign_flag = 1 " +
                                    "       and stm.cash_type <> 'TRN' " +
                                    "       and itm.deptitemtype_code <> 'DTF' " +
                                    "       and exists (select 1 " +
                                    "                   from dpdeptmaster mas ,dpdepttype dt " +
                                    "                   where mas.deptaccount_no =stm.deptaccount_no " +
                                    "                         and dt.depttype_code = mas.depttype_code " +
                                    "                         and mas.deptclose_status = 0 " +
                                    "                         and mas.prncbal>0 " +
                                    "                         and dt.depttype_code = '" + deptTypeCode + "' " +
                                    "                         and mas." + LimitDeptPerunitDesc + " = '" + LimitDeptPerunitVal + "' )";

                    break;
                case 3:

                    SqlGetSumDept = " select  ISNULL(sum(deptitem_amt),0) as sumdept " +
                                   " from dpdeptstatement stm,dpucfdeptitemtype itm " +
                                   " where stm.operate_date between add_months( ( last_day( trunc(  CONVERT(Datetime,'" + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN) + "') ) ) + 1 ) , (-12*" + LimitDeptUnit + ") ) " +
                                   "       and CONVERT(DATETIME,'" + state.SsWorkDate.ToString("yyyy/MM/dd", WebUtil.EN) + "')" +
                                   "       and stm.deptitemtype_code = itm.deptitemtype_code " +
                                   "       and itm.sign_flag = 1 " +
                                   "       and stm.cash_type <> 'TRN' " +
                                   "       and itm.deptitemtype_code <> 'DTF' " +
                                   "       and exists (select 1 " +
                                   "                    from dpdeptmaster mas ,dpdepttype dt " +
                                   "                    where mas.deptaccount_no =stm.deptaccount_no " +
                                   "                          and dt.depttype_code = mas.depttype_code " +
                                   "                          and mas.deptclose_status = 0 " +
                                   "                          and mas.prncbal>0 " +
                                   "                          and dt.depttype_code = '" + deptTypeCode + "' " +
                                   "                          and mas." + LimitDeptPerunitDesc + " = '" + LimitDeptPerunitVal + "' )";

                    break;

            }
            DataTable dtSumDept = new DataTable();
            try
            {
                dtSumDept = WebUtil.Query(SqlGetSumDept);
                if (dtSumDept.Rows.Count > 0)
                {
                    SumDept = Convert.ToDecimal(dtSumDept.Rows[0]["sumdept"].ToString());
                }
                else
                {
                    SumDept = 0;
                }

            }
            catch (Exception ex)
            {
                SumDept = 0;
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถทำรายการได้ " + ex);
            }

            return SumDept;
        }

        String GetFinSlipnoRec(String SlipNo)
        {
            String FinSlipNo = "";
            String sq = "select slip_no from finslip where from_system = 'DEP'"+ 
                        "and payment_status = 1 and itempaytype_code = 'FEE'"+
                        "and ref_slipno = '"+ SlipNo +"'";
            Sdt se = WebUtil.QuerySdt(sq);
            if (se.Next())
            {
                FinSlipNo = se.GetString("slip_no");
            }
            return FinSlipNo;
        }

        String GetFinSlipnoPay(String SlipNo)
        {
            String PaySlipNo = "";
            String sq = "select slip_no from finslip where from_system = 'DEP'" +
            "and payment_status = 1 and accuint_amt > 0" +
            "and ref_slipno = '" + SlipNo + "'";
            Sdt se = WebUtil.QuerySdt(sq);
            if (se.Next())
            {
                PaySlipNo = se.GetString("slip_no");
            }
            return PaySlipNo;
        }
    }
}