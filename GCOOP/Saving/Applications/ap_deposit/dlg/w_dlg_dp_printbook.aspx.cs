using System;
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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;

using System.Web.Services.Protocols;
using DataLibrary;
using CoreSavingLibrary;


using System.Collections.Generic;
using System.Globalization;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_printbook : PageWebDialog, WebDialog
    {

        //private DepositClient dep;
        private n_depositClient ndept;
        private String deptAccountNo;
        protected String postPrintBook;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postPrintBook = WebUtil.JsPostBack(this, "postPrintBook");
        }

        public void WebDialogLoadBegin()
        {
            ndept = wcf.NDeposit;
            this.ConnectSQLCA();
            DwNewBook.SetTransaction(sqlca);
            DwStm.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                HdIsPostBack.Value = "false";
                try
                {

                    HdAccountNo.Value = Request["deptAccountNo"];
                    deptAccountNo = HdAccountNo.Value;
                    DwStm.Retrieve(deptAccountNo, state.SsCoopControl);
                    String xmlInitDwNewBook = "";
                    ndept.of_init_printbook(state.SsWsPass, deptAccountNo, state.SsCoopId, ref xmlInitDwNewBook );
                    //DwPrintPrompt.InsertRow(0);
                    string smltest = DwPrintPrompt.Describe("Datawindow.Data.Xml");
                    //DwPrintPrompt.ImportString( xmlInitDwNewBook, Sybase.DataWindow.FileSaveAsType.Xml);
                    DwUtil.ImportData(xmlInitDwNewBook, DwPrintPrompt, null, Sybase.DataWindow.FileSaveAsType.Xml); 
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                    DwPrintPrompt.InsertRow(0);
                    DwStm.InsertRow(0);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    DwPrintPrompt.InsertRow(0);
                    DwStm.InsertRow(0);
                }
                //ใบคำสัญจ่ายลำปาง
                if (state.SsCoopId == "027001")
                {
                    printinterest();
                }
            }
            else
            {
                HdIsPostBack.Value = "true";
                this.RestoreContextDw(DwPrintPrompt);
                this.RestoreContextDw(DwStm);
            }
            this.deptAccountNo = HdAccountNo.Value;
        }
        public void printinterest()
        {
            try
            {
                if (Request["from_dp_slip"] == "true")
                {
                    string sql2 = @"select TOP 1 
                            ' 'as slip_date, a.deptclose_status as status,
                            'เลขที่บัญชี '+a.deptaccount_no as deptaccount_no,b.deptslip_amt as deptslip_amt,'--'+ 
                            ' ' as amt_th,'ชื่อบัญชี'+a.deptaccount_name as name_account,
                            'ใบสำคัญจ่ายเงินดอกเบี้ยเงินฝาก '+c.depttype_desc as depttype,deptslip_amt as sum , 
                            'ดอกเบี้ยจ่าย ' as description
                            from dpdeptmaster a,dpdeptslip b,dpdepttype c 
                            where  a.deptaccount_no = b.deptaccount_no 
                            and a.depttype_code = c.depttype_code 
                            and a.deptclose_status = '1' 
                            and b.recppaytype_code = 'INT' 
                            and a.coop_id = '" + state.SsCoopId + @"' 
                            and b.refer_slipno is not null
                            and b.deptslip_date = CONVERT(DATETIME,'" + state.SsWorkDate.ToString("ddMMyyyy") + "')and a.deptaccount_no = '" + Request["deptAccountNo"] + "'";
                    Sdt dt1 = WebUtil.QuerySdt(sql2);
                    if (dt1.Next())
                    {
                        if (dt1.GetString("status") == "1")
                        {
                            if (HdDpSlip.Value == "true")
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('ใบสำคัญจ่าย');", true);
                                DataTable dt = WebUtil.Query(sql2);
                                System.Threading.Thread.Sleep(2000);
                                Printing.PrintApplet(this, "dept_printinterest", dt);
                                HdDpSlip.Value = "false";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postPrintBook")
            {
                try
                {
                    Int16 seq = Convert.ToInt16(DwPrintPrompt.GetItemDecimal(1, "lastrec_no_pb"));
                    Int16 page = Convert.ToInt16(DwPrintPrompt.GetItemDecimal(1, "lastpage_no_pb"));
                    Int16 line = Convert.ToInt16(DwPrintPrompt.GetItemDecimal(1, "lastline_no_pb"));

                    //XmlConfigService xml = new XmlConfigService();
                    int ai_status = 1;// xml.DepositPrintMode;
                    String as_xml_return = "";
                    string printset = state.SsPrinterSet;

                    String returnValue  = "";
                    ndept.of_print_book(state.SsWsPass, deptAccountNo, state.SsCoopId, seq, page, line, true, state.SsPrinterSet, ref returnValue, Convert.ToInt16(ai_status), ref as_xml_return);
                    
                    String[] re = returnValue.Split('@');
                    int rePage = int.Parse(re[0]);
                    int reReq = int.Parse(re[1]);

                    HdIsZeroPage.Value = rePage == 0 ? "true" : "false";
                    HdIsNewBook.Value = rePage == 1 ? "true" : "false"; //เพิ่มเพื่อรับค่าจาก pb srv ว่าขึ้นเล่มใหม่
                    //HdIsNewBook.Value = "true";

                    //if (ai_status == 1)
                    //{
                    //    Printing.Print(this, "Slip/ap_deposit/PrintBook.aspx", as_xml_return, 25);
                    //}
                    //else if (ai_status == 2)
                    //{
                    Printing.DeptPrintBook(this, as_xml_return);
                    if(state.SsCoopId=="018001"){
                        string sql = @"select deptaccount_no,
                            deptaccount_name,
                            condforwithdraw
                            from dpdeptmaster 
                            where coop_id = '" + state.SsCoopId + @"' 
                            and deptaccount_no = '" + deptAccountNo + "'";
                        DataTable dt = WebUtil.Query(sql);
                        Printing.DeptPrintBookBackPage(this, dt);                    
                    }
                    //}

                    DwPrintPrompt.SetItemDecimal(1, "lastpage_no_pb", rePage);
                    DwPrintPrompt.SetItemDecimal(1, "lastrec_no_pb", reReq);
                    DwPrintPrompt.SetItemDecimal(1, "lastline_no_pb", 1);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            DwPrintPrompt.SaveDataCache();
        }

        #endregion
    }
}
