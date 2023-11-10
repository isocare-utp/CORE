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

using CoreSavingLibrary.WcfNCommon;  //new common
using CoreSavingLibrary.WcfNDeposit; //new deposit
using System.Web.Services.Protocols;
using System.ServiceModel.Channels;
using System.Xml;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_conteck : PageWebSheet, WebSheet
    {

        //private DepositClient depServ;
        private n_depositClient ndept; // new deposit        
        protected String testControls;
        private DwThDate thDwMain;
        private String pblFileName = "dp_conteck.pbl";

        protected String newClear;
        protected String postAccountNo;
        protected String postPrintbookback;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            newClear = WebUtil.JsPostBack(this, "newClear");
            postAccountNo = WebUtil.JsPostBack(this, "postAccountNo");
            postPrintbookback = WebUtil.JsPostBack(this, "postPrintbookback");
            //----------------------------------------------------------------
            thDwMain = new DwThDate(DwMain, this);
            thDwMain.Add("deptopen_date", "deptopen_tdate");
            thDwMain.Add("lastcalint_date", "lastcalint_tdate");
            //----------------------------------------------------------------
        }

        public void WebSheetLoadBegin()
        {
            HdClickedSeqNo.Value = "";
            this.ConnectSQLCA();
            DwDetail.SetTransaction(sqlca);
            //depServ = wcf.Deposit;
            ndept = wcf.NDeposit;
            if (IsPostBack)
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
                HdIsPostBack.Value = "true";
            }
            else
            {
               // JsNewClear();
                DwMain.InsertRow(0);
                //DwMain.SetItemString(1, "branch_id", state.SsCoopId);
                DwMain.SetItemString(1, "coop_id", state.SsCoopControl);
               Decimal deptpassflag = Convert.ToDecimal(Session["deptpassflag"]);            
               DwMain.SetItemDecimal(1, "deptpass_flag", deptpassflag);               
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                this.ConnectSQLCA();
                DwDetail.SetTransaction(sqlca);
                DwDetail.UpdateData();
                sqlca.Commit();

                String deptAccountNo = DwMain.GetItemString(1, "deptaccount_no");
                String coop_id = DwMain.GetItemString(1, "coop_id");
                //deptAccountNo = depServ.BaseFormatAccountNo(state.SsWsPass, deptAccountNo);

                deptAccountNo = ndept.of_analizeaccno(state.SsWsPass, deptAccountNo);
                int maxSeq = WebUtil.UpdateMaxBookSeqNo(state.SsWsPass, deptAccountNo, coop_id);                

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว (พิมพ์สมุดล่าสุด = " + maxSeq + ")...");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "newClear")
            {
                JsNewClear();
            }
            else if (eventArg == "postAccountNo")
            {
                JsPostAccountNo();
            }
            else if (eventArg == "postPrintbookback")
            {
                JsPostPrintbookback();
            }
        }

        public void WebSheetLoadEnd()
        {           
            DwDetail.PageNavigationBarSettings.Visible = (DwDetail.RowCount > 10);
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();           
        }

        #endregion
        //bee
        private void JsPostPrintbookback()
        {
            Printing.DeptPrintBookBack(this, state.SsCoopControl, DwMain.GetItemString(1, "deptaccount_no").Trim());
        }
        private void JsPostAccountNo()
        {
            string sqlStr;
            DataTable dt;
            String dept_acc = "", accNo = "";
            try
            {
                //String dwAccNo = DwMain.GetItemString(1, "deptaccount_no");
                decimal deptpassflag =Convert.ToDecimal( Hdeptpass_flag.Value);
                    //DwMain.GetItemDecimal(1, "deptpass_flag");
                String dwAccNo = HdnewAcc.Value;
             

                //ใช้ interpreter เช็คว่าเป็นบาร์โค้ดหรือไม่ ถ้าเป็นบาร์โค้ดจะไป select เลขที่บัญชีกลับมา , ถ้าไม่เจออะไรจะคืนค่า dwAccNo เดิม
                //accNo = wcf.InterPreter.DeptBarcodeToDeptAccount(state.SsConnectionIndex, state.SsCoopControl, dwAccNo);
                //accNo = depServ.BaseFormatAccountNo(state.SsWsPass, accNo);
            
                String dep_No = DwMain.GetItemString(1, "deptaccount_no");
                dep_No = wcf.NDeposit.of_analizeaccno(state.SsWsPass, dep_No); //gen acc_no format
               
                String coop_id = DwMain.GetItemString(1, "coop_id");
               
                DwUtil.RetrieveDataWindow(DwMain, pblFileName, null, dep_No, state.SsCoopControl);
                DwMain.SetItemString(1, "deptaccount_no", dep_No);
                try
                {
                    DwMain.SetItemDecimal(DwMain.RowCount, "deptpass_flag", deptpassflag);
                    
                }
                catch { DwMain.SetItemDecimal(1, "deptpass_flag", deptpassflag); }
                try
                {
                    DwUtil.RetrieveDataWindow(DwDetail, pblFileName, null, dep_No, state.SsCoopControl);
                }
                catch (Exception)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลรายการเคลื่อนไหว");
                    DwDetail.Reset();
                }
                if (DwMain.RowCount < 1)
                {
                    throw new Exception("ไม่พบเลขบัญชีดังกล่าว");
                }
                else
                {
                    HdDeptAccountNo.Value = dep_No;
                    //String depFormat = depServ.ViewAccountNoFormat(state.SsWsPass, dep_No);
                    //DwMain.SetItemString(1, "deptaccount_no", depFormat);
                    String deptTypeCode = DwMain.GetItemString(1, "depttype_code");
                    HdDeptTypeCode.Value = deptTypeCode;
                }

                DataTable dtBookHis = new DataTable();
                //String BookStmbase = "", BookGroup="";
                //string sqlBookHis = "select book_stmbase ,book_group from dpdepttype where depttype_code ='" + DwMain.GetItemString(1, "depttype_code") + "'";
                //dtBookHis = WebUtil.Query(sqlBookHis);
                //BookStmbase = dtBookHis.Rows[0]["book_stmbase"].ToString();
                //BookGroup = dtBookHis.Rows[0]["book_group"].ToString();

                try
                {
                    string sqlBookHis = "";
                    sqlBookHis = " SELECT MIN(  BH.BOOK_NO ) as  BOOK_NO " +
                                 " FROM DPDEPTBOOKHIS   BH "+
                                 " WHERE ( BH.BOOK_STATUS = 8 ) "+ 
                                 "         AND exists (select 1 "+ 
                                 "                     from dpdepttype dt "+
                                 "                     where dt.book_stmbase = BOOK_TYPE "+ 
                                 "                           and dt.book_group = BH.BOOK_GRP "+ 
                                 "                           and dt.depttype_code='"+DwMain.GetItemString(1, "depttype_code")+"')";

                    dtBookHis = WebUtil.Query(sqlBookHis);
                    if (dtBookHis.Rows.Count > 0)
                    {
                        HdDeptPassNo.Value = dtBookHis.Rows[0]["BOOK_NO"].ToString();
                    }
                    else
                    {
                        HdDeptPassNo.Value = "NotFound";
                    }

                }
                catch 
                {
                    HdDeptPassNo.Value = "NotFound";
                }

            }
            catch (Exception ex)
            {
                JsNewClear();
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
            Session["deptpassflag"] = DwMain.GetItemDecimal(1, "deptpass_flag");
        }

        private void JsNewClear()
        {
            //if (IsPostBack)
            //{
            DwMain.Reset();
            DwDetail.Reset();
            //}

            DwMain.InsertRow(0);
            //DwMain.SetItemString(1, "branch_id", state.SsCoopId);
            DwMain.SetItemString(DwMain.RowCount, "coop_id", state.SsCoopControl);
            Decimal deptpassflag = Convert.ToDecimal(Session["deptpassflag"]);
            DwMain.SetItemDecimal(DwMain.RowCount, "deptpass_flag", deptpassflag);
            HdIsPostBack.Value = "false";
            HdDeptAccountNo.Value = "";
        }
    }
}
