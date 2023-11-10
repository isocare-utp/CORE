using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;
using DataLibrary;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_finance : PageWebSheet, WebSheet
    {
        n_financeClient fin;
        private DwThDate tDwMain;
        private Decimal cashAmt;
        private Decimal moneyRemain;
        protected String postInitUser;
        protected String postPrintSlip;
        protected String postChangeTeller;
        DataStore DStore;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("operate_date", "operate_tdate");

            postInitUser = WebUtil.JsPostBack(this, "postInitUser");
            postPrintSlip = WebUtil.JsPostBack(this, "postPrintSlip");
            postChangeTeller = WebUtil.JsPostBack(this, "postChangeTeller");
        }

        public void WebSheetLoadBegin()
        {
            LtAlert.Text = "";
            String fincashctrl_info = "";
            Int32 resultXml;
            fin = wcf.NFinance;

            this.ConnectSQLCA();
            DStore = new DataStore();
            DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\app_finance\finquery.pbl";
            DStore.DataWindowObject = "d_finquery_userid";

            if (!IsPostBack)
            {
                try
                {
                    resultXml = fin.of_init_fincashcontrol(state.SsWsPass, state.SsCoopId, state.SsWorkDate, state.SsUsername, ref fincashctrl_info);
                    string rexml = resultXml.ToString();
                    DwMain.Reset();
                    DwMain.ImportString(fincashctrl_info, Sybase.DataWindow.FileSaveAsType.Xml);
                    tDwMain.Eng2ThaiAllRow();

                    //ส่งค่า coop_id และวันที่ทำการ ไปเพื่อแสดงรายละเอียดสมาชิก
                    DwUserList.SetTransaction(sqlca);
                    DwUserList.Retrieve(state.SsCoopId, state.SsWorkDate);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
            DwUtil.RetrieveDDDW(DwMain, "coop_id", "finance.pbl", null);

            cashAmt = DwMain.GetItemDecimal(1, "cash_amt");
            DwMain.Modify("cash_amt_t.text='" + cashAmt.ToString("#,##0.00") + "'");

        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postInitUser":
                    InitUser();
                    break;
                case "postChangeTeller":
                    ChangeTeller();
                    break;
                case "postPrintSlip":
                    PostPrintSlip();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            Int32 result, resultXml;
            String mainXml, finctrl_info = "";
            this.ConnectSQLCA();
            DStore = new DataStore();
            DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\app_finance\finquery.pbl";
            DStore.DataWindowObject = "d_finquery_userid";

            try
            {
                mainXml = DwMain.Describe("DataWindow.Data.XML");
                String coop_id = state.SsCoopId;

                result = fin.of_fincashcontrol_process(state.SsWsPass, mainXml, state.SsClientIp, state.SsApplication);

                if (result > 0)
                {
                    Hprintslip.Value = "true";

                    Happname.Value = DwMain.GetItemString(1, "application");
                    Husername.Value = DwMain.GetItemString(1, "entry_id");
                    Hseqno.Value = Convert.ToString(result);
                    //fincashdetail
                    if (Convert.ToDecimal(Hdamount.Value) > 0)
                    {
                        //insert cashdetail
                        string sql = @"select isnull( max(seq_no) +1,1) as seqno from fincashdetail where entry_id={0} and coop_id={1} and entry_date={2} ";
                        sql = WebUtil.SQLFormat(sql, state.SsUsername, state.SsCoopId, state.SsWorkDate);
                        Sdt dt = WebUtil.QuerySdt(sql);
                        dt.Next();
                        int ln_seq_no = dt.GetInt32("seqno");
                        sql = @"select max(cash_slipno) as cash_slipno from fincash_control where coop_id={0}  ";
                        sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                        Sdt dt_slip = WebUtil.QuerySdt(sql);
                        dt_slip.Next();
                        string cash_slipno = dt_slip.GetString("cash_slipno");
                        string sqlStr = @"insert into fincashdetail
                        (COOP_ID 			        , entry_id 		        , entry_date      , seq_no          , status 
                        , entry_time               , b_1000 	            , b_500            , b_100            , b_50 
                        , b_20 			            , c_10 		            , c_5              , c_2              , c_1 
                        , c_50 			            , c_25 		   ,ref_cashslipno )
                        values
                        ( {0}                      ,{1}             , {2}            , {3}               , {4}
                        , {5}                      , {6}            , {7}            , {8}               , {9}
                        , {10}                     , {11}      , {12}           , {13}              , {14}
                        , {15}                          , {16}   ,{17})";
                        sqlStr = WebUtil.SQLFormat(sqlStr
                    , state.SsCoopId, state.SsUsername, state.SsWorkDate, ln_seq_no, DwMain.GetItemDecimal(1, "item_type")
                    , state.SsWorkDate, Hdb1000.Value, Hdb500.Value, Hdb100.Value, Hdb50.Value
                    , Hdb20.Value, Hdc10.Value, Hdc5.Value, Hdc2.Value, Hdc1.Value
                    , Hdc50.Value, Hdc25.Value, cash_slipno);
                        WebUtil.ExeSQL(sqlStr);
                        Hdamount.Value = ""; Hdb1000.Value = ""; Hdb500.Value = ""; Hdb100.Value = ""; Hdb50.Value = "";
                        Hdb20.Value = ""; Hdc10.Value = ""; Hdc5.Value = ""; Hdc2.Value = ""; Hdc1.Value = ""; Hdc50.Value = ""; Hdc25.Value = "";

                        /* string report_name = "", report_label = "";
                         report_name = "ir_fin_cashdetail";
                         report_label = "รายงานการเบิกจ่าย";

                         iReportArgument args = new iReportArgument();
                         args.Add("as_slip_no", iReportArgumentType.String, slip_no);
                         args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                         args.Add("as_date", iReportArgumentType.Date, state.SsWorkDate);
                         args.Add("as_seqno", iReportArgumentType.String, ln_seq_no);
                         args.Add("as_usename", iReportArgumentType.String, state.SsUsername);
                         iReportBuider report = new iReportBuider(this, "");
                         report.AddCriteria(report_name, report_label, ReportType.pdf, args);
                         report.AutoOpenPDF = true;
                         report.Retrieve();*/
                    }
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");

                    resultXml = fin.of_init_fincashcontrol(state.SsWsPass, state.SsCoopId, state.SsWorkDate, state.SsUsername, ref finctrl_info);
                    DwMain.Reset();
                    DwMain.ImportString(finctrl_info, Sybase.DataWindow.FileSaveAsType.Xml);
                    tDwMain.Eng2ThaiAllRow();

                    //ส่งค่า coop_id และวันที่ทำการ ไปเพื่อแสดงรายละเอียดสมาชิก
                    DwUserList.SetTransaction(sqlca);
                    DwUserList.Retrieve(state.SsCoopId, state.SsWorkDate);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion


        protected void InitUser()
        {
            String DwMainXml, fullname = "";
            Int32 resultXml;

            try
            {
                DwMainXml = DwMain.Describe("DataWindow.Data.XML");
                resultXml = fin.of_init_fincashcontrol_user(state.SsWsPass, ref DwMainXml, ref fullname);

                DwMain.Reset();
                DwMain.ImportString(DwMainXml, FileSaveAsType.Xml);
                DwMain.Modify("t_fullname.text='" + fullname + "'");

                DwUtil.RetrieveDDDW(DwMain, "item_type", "finance.pbl", state.SsCoopControl);

                DataWindowChild DcItemType = DwMain.GetChild("item_type");
                if (DcItemType.RowCount == 0)
                {
                    DcItemType.ImportString(fin.of_dddwfinitemtype(state.SsWsPass), FileSaveAsType.Xml);
                }

                if (DwMain.GetItemDecimal(1, "status") == 11)
                {
                    DwMain.SetItemDecimal(1, "item_type", 15);
                }
                else if (DwMain.GetItemDecimal(1, "status") == 14)
                {
                    DwMain.SetItemDecimal(1, "item_type", 11);
                }
                else { DwMain.SetItemDecimal(1, "item_type", 11); }

                cashAmt = DwMain.GetItemDecimal(1, "cash_amt");
                HfCashAmt.Value = Convert.ToString(cashAmt);

                moneyRemain = DwMain.GetItemDecimal(1, "money_remain");
                HfMoneyRemain.Value = Convert.ToString(moneyRemain);
                DwMain.Modify("money_remain_text.text='" + moneyRemain.ToString("#,##0.00") + "'");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        protected void ChangeTeller()
        {
            Int32 resultXml;
            String mainXml, columnName, fullname = "";
            Decimal status, NewValue, ItemType;

            mainXml = DwMain.Describe("DataWindow.Data.XML");
            resultXml = fin.of_init_fincashcontrol_user(state.SsWsPass, ref mainXml, ref fullname);

            DwMain.Modify("t_fullname.text='" + HfFullName.Value + "'");
            DwMain.Modify("t_fullname.text='" + fullname + "'");

            columnName = HfColumn.Value;
            status = DwMain.GetItemDecimal(1, "status");

            try
            {
                NewValue = Convert.ToDecimal(HfValue.Value);
            }
            catch { NewValue = 0; }

            if (NewValue < 0)
            {
                NewValue = 0;
                LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินติดลบไม่ได้");
            }

            ItemType = Convert.ToInt16(DwMain.GetItemDecimal(1, "item_type"));

            if (ItemType != status)
            {
                cashAmt = Convert.ToDecimal(HfCashAmt.Value);
                moneyRemain = Convert.ToDecimal(HfMoneyRemain.Value);

                try
                {
                    if (ItemType == 14) //14 ปิดลิ้นชัก
                    {
                        if (moneyRemain >= 0)
                        {
                            cashAmt = cashAmt + moneyRemain;
                            DwMain.SetItemDecimal(1, "money_remain", 0);
                            DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                            DwMain.SetItemDecimal(1, "operate_amt", moneyRemain);
                            DwMain.Modify("t_moneyreturn.text='" + "ยอดเงินที่ต้องส่งคืนทั้งหมด =" + moneyRemain.ToString("#,##0.00") + "'");
                        }
                        else
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินสดคงเหลือติดลบ");
                            DwMain.SetItemDecimal(1, "item_type", 15);
                        }
                    }
                    else if (ItemType == 11 && columnName == "operate_amt")//11 เปิดลิ้นชัก
                    {
                        cashAmt = cashAmt - NewValue;
                        DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                        DwMain.SetItemDecimal(1, "operate_amt", NewValue);
                        DwMain.SetItemDecimal(1, "money_remain", NewValue);
                        DwMain.Modify("t_moneyreturn.text='" + "" + "'");
                    }
                    else if (ItemType == 15)//15  เบิกเงินเพิ่ม
                    {
                        if (status == 11 && columnName == "operate_amt")
                        {
                            if (NewValue <= cashAmt)
                            {
                                cashAmt = cashAmt - NewValue;
                                moneyRemain = moneyRemain + NewValue;
                                DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                                DwMain.SetItemDecimal(1, "operate_amt", NewValue);
                                DwMain.SetItemDecimal(1, "money_remain", moneyRemain);
                                DwMain.Modify("t_moneyreturn.text='" + "" + "'");
                            }
                            else if (NewValue > cashAmt)
                            {
                                DwMain.SetItemDecimal(1, "operate_amt", 0);
                                LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินที่เบิกมากกว่าจำนวนเงินคงเหลือ");
                            }
                        }
                        else if (status == 11 && columnName == "item_type")
                        {
                            DwMain.SetItemDecimal(1, "operate_amt", 0);
                            DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                            DwMain.SetItemDecimal(1, "money_remain", moneyRemain);
                        }
                        else
                        {
                            //LtAlert.Text = "<script>AlertFin('" + ItemType + "','" + status + "')</script>";
                            DwMain.SetItemDecimal(1, "item_type", 11);
                            LtServerMessage.Text = WebUtil.ErrorMessage("ต้องมีสถานะเปิดลิ้นชักเท่านั้น");
                        }
                    }
                    else if (ItemType == 16)//16 ส่งเงินคืน
                    {
                        if (status == 11 && columnName == "operate_amt")
                        {
                            if (NewValue <= moneyRemain)
                            {
                                cashAmt = cashAmt + NewValue;
                                moneyRemain = moneyRemain - NewValue;
                                DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                                DwMain.SetItemDecimal(1, "operate_amt", NewValue);
                                DwMain.SetItemDecimal(1, "money_remain", moneyRemain);
                                DwMain.Modify("t_moneyreturn.text='" + "" + "'");
                            }
                            else if (NewValue > moneyRemain)
                            {
                                DwMain.SetItemDecimal(1, "operate_amt", 0);
                                LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนเงินที่คืนมากกว่าจำนวนเงินคงเหลือ");
                            }
                        }
                        else if (status == 11 && columnName == "item_type")
                        {
                            DwMain.SetItemDecimal(1, "operate_amt", 0);
                            DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                            DwMain.SetItemDecimal(1, "money_remain", moneyRemain);
                        }
                        else
                        {
                            //LtAlert.Text = "<script>AlertFin('" + ItemType + "','" + status + "')</script>";
                            DwMain.SetItemDecimal(1, "item_type", 11);
                            LtServerMessage.Text = WebUtil.ErrorMessage("ต้องมีสถานะเปิดลิ้นชักเท่านั้น");
                        }
                    }
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else if (ItemType == status)
            {
                cashAmt = Convert.ToDecimal(HfCashAmt.Value);
                moneyRemain = Convert.ToDecimal(HfMoneyRemain.Value);

                if (ItemType == 11)// เปิดลิ้นชัก
                {
                    DwMain.SetItemDecimal(1, "item_type", 15);
                    DwMain.SetItemDecimal(1, "operate_amt", 0);
                    DwMain.SetItemDecimal(1, "cash_amt", cashAmt);
                    DwMain.SetItemDecimal(1, "money_remain", moneyRemain);
                    LtServerMessage.Text = WebUtil.ErrorMessage("มีสถานะเปิดอยู่แล้ว  ไม่สามารถเปิดได้อีก");
                }
                else if (ItemType == 14)// ปิดลิ้นชัก
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่ทำการเปิดลิ้นชัก หรือ มีสถานะปิดลิ้นชักแล้ว");
                }
            }
        }

        protected void PostPrintSlip()
        {
            Int16 ai_seqno = 0;
            String as_app = "", as_username = "";
            Hprintslip.Value = "false";
            try
            {
                as_app = Happname.Value;
                as_username = Husername.Value;
                ai_seqno = Convert.ToInt16(Hseqno.Value);
                //fin.OfPostPrintCashCrtl(state.SsWsPass, state.SsCoopId, state.SsUsername, as_username, state.SsWorkDate, ai_seqno, state.SsPrinterSet);
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
    }
}
