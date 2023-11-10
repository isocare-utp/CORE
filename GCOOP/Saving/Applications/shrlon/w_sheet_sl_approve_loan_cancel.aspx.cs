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
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Globalization;
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_approve_loan_cancel : PageWebSheet, WebSheet
    {
         
        private DwThDate thDwMaster;


        protected String jsgenReqDocNo = "";
        #region WebSheet Members

        void WebSheet.InitJsPostBack()
        {

            jsgenReqDocNo = WebUtil.JsPostBack(this, "jsgenReqDocNo");
            //thDwMaster = new DwThDate(dw_master, this);
            //thDwMaster.Add("loanrequest_date", "loanrequest_tdate");
            //thDwMaster.Add("loanrcvfix_date", "loanrcvfix_tdate");
        }

        void WebSheet.WebSheetLoadBegin()
        {
            try
            {
                //NshrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();

            if (IsPostBack)
            {
                dw_master.RestoreContext();
            }
            if (dw_master.RowCount < 1)
            {
                this.InitLnReqList();
            }

        }

        void WebSheet.CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsgenReqDocNo")
            {
              //  this.GenReqDocNo();
            }

        }

        void WebSheet.SaveWebSheet()
        {

            Sta ta = new Sta(sqlca.ConnectionString);

            try
            {
                for (int i = 1; i <= dw_master.RowCount; i++)
                {

                    String lncont_no = "";
                    try { lncont_no = dw_master.GetItemString(i, "loancontract_no"); }
                    catch { lncont_no = ""; }
                    String lncont_num2 = WebUtil.Right(lncont_no, 6);
                    //String lncont_num1 = WebUtil.Left(lncont_no, 8);
                    //String lncont_num2 = WebUtil.Right(lncont_num1, 5);
                    String loantype_code = dw_master.GetItemString(i, "loantype_code").Trim();
                    string req_coopid = dw_master.GetItemString(i, "coop_id");
                    string document_code = "";
                    string strsql = @"  SELECT 
                                       LNLOANTYPE.DOCUMENT_CODE
                                    FROM LNLOANTYPE  
                                   WHERE LNLOANTYPE.LOANTYPE_CODE ='" + loantype_code + @"'   
                                  AND COOP_ID='" + req_coopid + @"'";
                    Sdt dt = WebUtil.QuerySdt(strsql);

                    if (dt.Next())
                    {

                        document_code = dt.GetString("DOCUMENT_CODE").Trim();
                    }

                    String ls_reqno = dw_master.GetItemString(i, "loanrequest_docno");
                    Decimal li_status = dw_master.GetItemDecimal(i, "loanrequest_status");
                    if (li_status == 8)
                    {
                        
                        String newReqDocNo = wcf.NShrlon.of_gennewcontractno(state.SsWsPass, req_coopid, loantype_code);

                        String newReqDocN_num2 = WebUtil.Right(newReqDocNo, 6);
                        //String newReqDocN_num1 = WebUtil.Left(newReqDocNo, 8);
                        //String newReqDocN_num2 = WebUtil.Right(newReqDocN_num1, 5);
                        Decimal result = Convert.ToDecimal(newReqDocN_num2) - Convert.ToDecimal(lncont_num2);
                        if (result <= 1)
                        {
                            decimal last_doc = Convert.ToDecimal(lncont_num2)-1 ;
                            try
                            {
                                String sql = @"  UPDATE CMDOCUMENTCONTROL  
                                                 SET LAST_DOCUMENTNO = " + last_doc + @"
                                               WHERE CMDOCUMENTCONTROL.DOCUMENT_CODE ='" + document_code + @"' 
                                           AND COOP_ID='" + req_coopid + @"'";
                                ta.Exe(sql);
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                            try
                            {
                                String sql = @" delete	lncontmaster                             
                            where	( loancontract_no	= '" + lncont_no + @"' ) and
                                    ( coop_id	= '" + state.SsCoopControl + "' )";
                                ta.Exe(sql);
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }


                            try
                            {
                                String sql = @" update	lnreqloan
                            set			loanrequest_status	= " + li_status + @"   
                                            ,loancontract_no='" + lncont_no + @"'        
                            where	( loanrequest_docno	= '" + ls_reqno + @"' ) and
                                    ( coop_id	= '" + state.SsCoopControl + "' )";
                                ta.Exe(sql);
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                        }
                        else
                        {
                            lncont_no = "  ";
                            try
                            {
                                String sql = @" update	lncontmaster
                            set			contract_status	= -9         
                            where	( loancontract_no	= '" + lncont_no + @"' ) and
                                    ( coop_id	= '" + state.SsCoopControl + "' )";
                                ta.Exe(sql);
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                            try
                            {
                                String sql = @" update	lnreqloan
                            set			loanrequest_status	= " + li_status + @"   
                                ,loancontract_no='" + lncont_no + @"'        
                            where	( loanrequest_docno	= '" + ls_reqno + @"' ) and
                                    ( coop_id	= '" + state.SsCoopControl + "' )";
                                ta.Exe(sql);
                            }
                            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                        }
                    }
                }
                ta.Close();
                InitLnReqList();
            }
            catch (Exception ex)
            {


            }

        }

        void WebSheet.WebSheetLoadEnd()
        {
            DwUtil.RetrieveDDDW(dw_master, "loantype_code", "sl_approve_loan.pbl", null);
            dw_master.SaveDataCache();
        }

        #endregion

        private void InitLnReqList()
        {

            try
            {
                string as_coopid = state.SsCoopControl;
                string as_endcoopid = state.SsCoopControl;
                DateTime adtm_approve = state.SsWorkDate;
                //dw_master.Retrieve(as_coopid,adtm_approve);
                String reqListXML =   wcf.NShrlon.of_initlist_lnreqapv_cancel(state.SsWsPass, as_coopid, as_endcoopid, adtm_approve);

                dw_master.Reset();
                DwUtil.ImportData(reqListXML, dw_master, null, FileSaveAsType.Xml);
                //   dw_master.SetFilter("loantype_code not in ('23','26') ");
                //dw_master.SetFilter("loantype_code <'23' and coop_id ='" + state.SsCoopId + "'");
                //dw_master.Filter();
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูลรอทำรายการ");
            }
        }

        private void GenReqDocNo()
        {
            int count = dw_master.RowCount;
            for (int i = 0; i < count; i++)
            {
                string req_coopid = dw_master.GetItemString(i + 1, "coop_id");

                String lncont_no = "";
                try { lncont_no = dw_master.GetItemString(i + 1, "loancontract_no"); }
                catch { lncont_no = ""; }

                String lncont_status = dw_master.GetItemString(i + 1, "loanrequest_status");
                if (lncont_status == "1")
                {
                    if (lncont_no == "")
                    {
                        String loantype_code = dw_master.GetItemString(i + 1, "loantype_code").Trim();
                        String newReqDocNo = wcf.NShrlon.of_gennewcontractno(state.SsWsPass, req_coopid, loantype_code);

                        dw_master.SetItemString(i + 1, "loancontract_no", newReqDocNo);
                        //  dw_master.SetItemDateTime(i + 1, "approve_date", state.SsWorkDate);
                    }
                }
            }

        }
    }
}
