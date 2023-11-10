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
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_install_ins_mu : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {
            
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_main.SetTransaction(sqlca);

            if (IsPostBack)
            {
                this.RestoreContextDw(Dw_main);
            }
            else
            {
                JsNewClear();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            
        }

        private void JsNewClear()
        {
            Dw_main.Reset();
            Dw_main.InsertRow(0);

            Dw_list.Reset();
            Dw_list.InsertRow(0);

           // Dw_main.SetItemDate(1, "redeem_date", state.SsWorkDate);
            //tdwhead.Eng2ThaiAllRow();
        }
        private void JsRetrieveData()
        {
            DateTime start_date = Dw_main.GetItemDateTime(1, "start_date");
            DateTime end_date = Dw_main.GetItemDateTime(1, "end_date");
            string itemtype_code = Dw_main.GetItemString(1, "itemtype_code");
            Dw_list.SetTransaction(sqlca);
            double rowcount = Dw_list.Retrieve(start_date, end_date, itemtype_code);


        
        }
        private void JsPosttoMutual()
        { 
        //ติดตั้งกองทุน
             int post_flag , check_ins = 0 ;
            string shrlon_type = "61", itemtype_code = "";
            string insgroupdoc_no = "",member_no = "", sql_chk= "", sql_exc = "";
            Double inspayment_amt = 0;
            int rowcount = Dw_list.RowCount ;
                for ( int i = 1 ;i<= rowcount;i++)
                {
                    post_flag = Convert.ToInt16(Dw_list.GetItemDouble(i, "postetcmast_flag"));
                    shrlon_type = Dw_list.GetItemString(i, "shrlontype_code");
                    itemtype_code = Dw_list.GetItemString(i, "slipitemtype_code");
                    member_no = Dw_list.GetItemString(i, "member_no");
                    inspayment_amt = Dw_list.GetItemDouble(i, "item_payamt");
                    sql_chk = "select member_no, mumembtype_code from mumembmaster  where member_no = '" + member_no + "' and mumembtype_code = '" + shrlon_type + "'";
                    Sdt dt = WebUtil.QuerySdt(sql_chk);
                    if (dt.Next())
                    {
                        sql_exc = "";

                    }
                    else
                    {


                    }
                }   
        
        }
        private void JsPosttoinsurancegroup() 
        { 
          //ติดตั้งประกัน
            int post_flag = 0, check_ins = 0 ;
            string shrlon_type = "61", itemtype_code = "";
            string insgroupdoc_no = "",member_no = "", sql_chk= "", sql_exc = "";
            Double inspayment_amt = 0;
            int rowcount = Dw_list.RowCount ;
                for ( int i = 1 ;i<= rowcount;i++)
                {
                    post_flag = Convert.ToInt16(Dw_list.GetItemDouble(i, "postetcmast_flag"));
                    shrlon_type = Dw_list.GetItemString(i, "shrlontype_code");
                    itemtype_code = Dw_list.GetItemString(i, "slipitemtype_code");
                    member_no = Dw_list.GetItemString(i, "member_no");
                    inspayment_amt = Dw_list.GetItemDouble(i, "item_payamt");
                    sql_chk = "select member_no, insgroupdoc_no from insgroupmaster  where member_no = '" + member_no + "' and instype_code = '" + shrlon_type + "'";
                    Sdt dt = WebUtil.QuerySdt(sql_chk);
                    if (dt.Next())
                    {
                        sql_exc = "";

                    }
                    else { 
                    
                    //select max( insgroup_id )
                    //into :ll_insgroupid
                    //from insgroupmaster using itr_sqlca  ;
	
                    //if isnull( ll_insgroupid ) then ll_insgroupid = 0
                    //insert into  insgroupmaster 
                    //(	member_no,		instype_code,		level_code,				insgroupdoc_no,			insgroup_date,	insgroup_id, period,
                    //    insreq_date,		inscost_blance,		insperod_payment,		last_period,				insmemb_status,	
                    //    last_stm_no,		inspayment_status,	loan_amt,					share_amt,				remark,
                    //    process_date,		misspay_amt,		insreqdoc_no,			loanreq_amt,				startsafe_date	,	endsafe_date,
                    //    marrige_name	,	insgroupno_ref,	insmemb_type	,		person_card	,			inspayment_arrear,	inspayment_amt,	expense_code	) 
                    //values 
                    }
                        //INSERT INTO INSGROUPSTATEMENT  
                        //( 	MEMBER_NO,  	 	instype_code,   			SEQ_NO,						INSITEMTYPE_CODE,   insgroup_id,
                        //    INSPERIOD_AMT,  INSPERIOD_PAYMENT,  	insprince_balance, 
                        //    OPERATE_DATE,  	INSGROUPSLIP_DATE,    	ENTRY_DATE,  	 			insgroupdoc_no ,
                        //    ENTRY_ID,			refdoc_no )  
                        //VALUES 
                }
        
        
        }
    }
}