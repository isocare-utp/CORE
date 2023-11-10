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
using DataLibrary;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_slipbank : System.Web.UI.Page
    {
        WebState state;
        DwTrans SQLCA;
        String strdocno = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            SQLCA = new DwTrans();
            SQLCA.Connect();
            d_slipbank.SetTransaction(SQLCA);

            String bank_code, bank_branch, dlg, account_no, cmd;
            try
            {
                bank_code = Request["bank_code"];
                bank_branch = Request["bankbranch_code"];
                dlg = Request["dlg"];
                cmd = Request["cmd"];
                account_no = Request["account_no"];

            }
            catch { bank_code = ""; bank_branch = ""; dlg = ""; account_no = ""; cmd = ""; }
            d_slipbank.InsertRow(0);



            if (cmd == "save")
            {

            }
            else if (cmd == null)
            {
                if (dlg == "account")
                {
                    if (bank_branch == "")
                    {
                        d_slipbank.SetItemString(1, "bank_code", bank_code);
                        DataWindowChild child = d_slipbank.GetChild("bank_branch");
                        child.SetTransaction(SQLCA);
                        child.Retrieve();
                        child.SetFilter("bank_code='" + bank_code + "'");
                        child.Filter();
                        child.Retrieve();
                    }
                    else
                    {

                        d_slipbank.SetItemString(1, "bank_code", bank_code);
                        DataWindowChild child = d_slipbank.GetChild("bank_branch");
                        child.SetTransaction(SQLCA);
                        child.Retrieve();
                        child.SetFilter("bank_code='" + bank_code + "'");
                        child.Filter();
                        child.Retrieve();
                        d_slipbank.SetItemString(1, "bank_branch", bank_branch);

                    }
                }
                else if (dlg == "deposit")
                {
                        String sqltemp = "";
                        sqltemp = sqltemp + d_slipbank.GetSqlSelect().ToString() + "where account_no = '" + account_no + "'";
                        d_slipbank.SetSqlSelect(sqltemp);
                        d_slipbank.Retrieve();
                        d_slipbank.SetItemString(1, "item_code", "DCA");
                        d_slipbank.SetItemString(1, "slip_no", "Auto");

                }
                else if (dlg == "withdrawal")
                {
                    String sqltemp = "";
                    d_slipbank.Retrieve();
                    sqltemp = sqltemp + d_slipbank.GetSqlSelect().ToString() + "where account_no = '" + account_no + "'";
                    d_slipbank.SetSqlSelect(sqltemp);
                    d_slipbank.Retrieve();
                    d_slipbank.SetItemString(1, "item_code", "WCA");
                    d_slipbank.SetItemString(1, "slip_no", "Auto");

                        //d_slipbank.SetItemDecimal(1, "item_amt", 0);

                }
            }

        }

        protected void Page_LoadComplete()
        {
            SQLCA.Disconnect();
        }

        protected void d_slipbank_BeginUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //n_cst_doccontrolservice docno = new n_cst_doccontrolservice();
            //docno.of_connect();
            //strdocno = docno.of_getnewdocno("FNRECEIPTBANK");
            //docno.of_disconnect();
            //d_slipbank.SetItemString(1, "slip_no", strdocno);
            //String item_code = d_slipbank.GetItemString(1, "item_code");
            //if (item_code == "OCA")
            //{
            //    of_openacc(strdocno);

            //}
            //else
            //{
            //    of_postother(strdocno);
            //}

        }




        protected void d_slipbank_EndUpdate(object sender, EndUpdateEventArgs e)
        {

        }




        private int of_openacc(String as_slipno)
        {
            string ls_accno, ls_bankcode, ls_bankbranch, ls_accname, ls_entryid, ls_bookno;
            string ls_acctype, ls_machineid, ls_coopbranch, ls_slipno;
            decimal ldc_beginbal, ldc_mbginbal, ldc_balance, ldc_withamt, ldc_deptamt, ldc_scobal;
            DateTime ldtm_opendate, ldtm_closedate, ldtm_entry_date, ldtm_booklastupdate;
            int li_seqno, li_sign_operate = 0;
            string ls_remark, sql = "";


            ls_accno = d_slipbank.GetItemString(1, "account_no");
            ls_bankcode = d_slipbank.GetItemString(1, "bank_code");
            ls_acctype = d_slipbank.GetItemString(1, "account_type");
            ls_bankbranch = d_slipbank.GetItemString(1, "bank_branch");
            ls_accname = d_slipbank.GetItemString(1, "account_name");
            ls_entryid = d_slipbank.GetItemString(1, "entry_id");
            ls_bookno = d_slipbank.GetItemString(1, "book_no");
            ls_remark = "";
            ls_coopbranch = "001";

            ldc_balance = d_slipbank.GetItemDecimal(1, "item_amt");
            if (ldc_balance == null) { ldc_balance = 0; }

            ldc_beginbal = ldc_balance;
            ldc_mbginbal = ldc_balance;

            ldc_deptamt = d_slipbank.GetItemDecimal(1, "item_amt");
            ldc_scobal = d_slipbank.GetItemDecimal(1, "item_amt");
            ldtm_opendate = d_slipbank.GetItemDateTime(1, "open_date");

            ldtm_entry_date = d_slipbank.GetItemDateTime(1, "entry_date");
            ldtm_booklastupdate = d_slipbank.GetItemDateTime(1, "entry_date");
            li_seqno = 1;
            li_sign_operate = 1;
            ldc_withamt = 0;
            //ldtm_closedate = null;

            ls_machineid = d_slipbank.GetItemString(1, "machine_id");
            ls_coopbranch = d_slipbank.GetItemString(1, "coopbranch_id");
            ls_slipno = d_slipbank.GetItemString(1, "slip_no");

            sql = @"INSERT INTO	FINBANKACCOUNT
                 (	ACCOUNT_NO,	BANK_CODE,	BANKBRANCH_CODE,
	                ACCOUNT_NAME, BEGINBAL,	MBEGINBAL,			
	                BALANCE, CLOSE_STATUS,	CLOSE_DATE,		
	                OPEN_DATE,	LASTSTM_SEQ, ENTRY_ID,	
	                ENTRY_DATE,	ACCOUNT_TYPE, DEPT_AMT,	
	                WITH_AMT, BOOK_LASTUPDATE, BOOK_NO,
	                LASTACCESS_DATE, SCO_BALANCE, coopbranch_id,
	                remark
                )  
                VALUES
                (	" + ls_accno + "," + ls_bankcode + "," + ls_bankbranch + @",
	                " + ls_accname + "," + ldc_beginbal + "," + ldc_mbginbal + @",
	                " + ldc_balance + @",0,	null,
	                " + ldtm_opendate + "," + li_seqno + "," + ls_entryid + @",
	                " + ldtm_entry_date + "," + ls_acctype + "," + ldc_deptamt + @",
	                " + ldc_withamt + "," + ldtm_booklastupdate + "," + ls_bookno + @",
	                " + ldtm_opendate + "," + ldc_scobal + "," + ls_coopbranch + @",
	                " + ls_remark + ")";


            //if sqlca.sqlcode <> 0 then
            //    messagebox( this.title, "ไม่สามารถบันทึกข้อมูลลง FINBANKACCOUNT ",StopSign! )
            //    rollback;
            //    return Failure
            //end if
            Sta ta1 = new Sta(new DwTrans().ConnectionString);
            int rs1 = ta1.Exe(sql);
            if (rs1 < 0)
            {
                //ทำไร?
            }

            sql = @"INSERT INTO	FINBANKSTATEMENT
                (	SEQ_NO,	ACCOUNT_NO,	BANK_CODE,
	                BANKBRANCH_CODE, DETAIL_DESC, ENTRY_ID,
	                ENTRY_DATE,	OPERATE_DATE, REF_SEQ,
	                ITEM_STATUS, CANCEL_ID,	CANCEL_DATE,
	                BALANCE, BALANCE_BEGIN,	MACHINE_ID,
	                COOPBRANCH_ID, refer_slipno, item_amt,
	                sign_operate
                )
                  VALUES
               ( 	" + li_seqno + "," + ls_accno + "," + ls_bankcode + @",
	                " + ls_bankbranch + ",'เปิดบัญชี',	" + ls_entryid + @",
	                " + ldtm_entry_date + "," + ldtm_entry_date + @",null,
	                1,null,null,
	                " + ldc_balance + ",0," + ls_machineid + @",
	                " + ls_coopbranch + "," + ls_slipno + "," + ldc_deptamt + @",
	                " + li_sign_operate + ")";

            //if sqlca.sqlcode <> 0 then
            //    messagebox( this.title, "ไม่สามารถบันทึกข้อมูลลง FINBANKSTATEMENT " + sqlca.sqlerrtext ,StopSign! )
            //    rollback;
            //    return Failure
            //end if
            Sta ta2 = new Sta(new DwTrans().ConnectionString);
            int rs2 = ta2.Exe(sql);
            if (rs2 < 0)
            {
                //ทำไร?
            }

            return 0;
        }







        private int of_postother(String as_slipno)
        {

            string ls_accno, ls_bankcode, ls_bankbranch, ls_accname, ls_entryid, ls_bookno;
            string ls_acctype, ls_machineid, ls_coopbranch, ls_slipno, ls_itemtype;
            decimal ldc_balance, ldc_itemamt, ldc_with = 0, ldc_dept = 0, idc_scobal;
            DateTime ldtm_opendate, ldtm_entry_date, ldtm_booklastupdate;
            int li_seqno, li_closestatus, li_sign_operate = 0;
            string ls_desc = "", sql = "";
            Nullable<DateTime> ldtm_closedate = null;

            //ldtm_closedate = null;

            ls_accno = d_slipbank.GetItemString(1, "account_no");
            ls_bankcode = d_slipbank.GetItemString(1, "bank_code");
            ls_acctype = d_slipbank.GetItemString(1, "account_type");
            ls_bankbranch = d_slipbank.GetItemString(1, "bank_branch");
            ls_accname = d_slipbank.GetItemString(1, "account_name");
            ls_entryid = d_slipbank.GetItemString(1, "entry_id");
            ls_bookno = d_slipbank.GetItemString(1, "book_no");
            ls_itemtype = d_slipbank.GetItemString(1, "item_code");
            ldc_balance = d_slipbank.GetItemDecimal(1, "balance");
            ldc_itemamt = d_slipbank.GetItemDecimal(1, "item_amt");

            ldtm_entry_date = d_slipbank.GetItemDateTime(1, "entry_date");
            ldtm_booklastupdate = d_slipbank.GetItemDateTime(1, "entry_date");
            li_seqno = Convert.ToInt32(d_slipbank.GetItemString(1, "last_stm"));

            ls_machineid = d_slipbank.GetItemString(1, "machine_id");
            ls_coopbranch = d_slipbank.GetItemString(1, "coopbranch_id");
            ls_slipno = d_slipbank.GetItemString(1, "slip_no");
            li_closestatus = 0;


            sql = @"select	BALANCE, sco_balance, LASTSTM_SEQ
            from FINBANKACCOUNT
            where ACCOUNT_NO	= " + ls_accno + @"and 
			BANK_CODE			= " + ls_bankcode + @" and
			BANKBRANCH_CODE	    = " + ls_bankbranch + @" and
			coopbranch_id		= " + ls_coopbranch;

            //into		:ldc_balance, :idc_scobal, :li_seqno
            Sta ta3 = new Sta(new DwTrans().ConnectionString);
            Sdt dt3 = ta3.Query(sql);

            ldc_balance = Convert.ToDecimal(dt3.Rows[0]["BALANCE"]);
            idc_scobal = Convert.ToDecimal(dt3.Rows[0]["sco_balance"]);
            li_seqno = Convert.ToInt32(dt3.Rows[0]["LASTSTM_SEQ"]);

            //if sqlca.sqlcode <> 0 then
            //    messagebox( this.title, "ไม่สามารถดึงข้อมูลจาก FINBANKACCOUNT ",StopSign! )
            //    rollback;
            //    return Failure
            //end if

            if (ls_itemtype == "DCA")
            {

                ldc_with = 0;
                ldc_dept = ldc_itemamt;
                ldc_balance = ldc_balance + ldc_itemamt;
                idc_scobal = idc_scobal + ldc_itemamt;
                ls_desc = "ฝากเงิน";
                li_sign_operate = 1;
            }
            else if (ls_itemtype == "WCA")
            {
                ldc_with = ldc_itemamt;
                ldc_dept = 0;
                ldc_balance = ldc_balance - ldc_itemamt;
                idc_scobal = idc_scobal - ldc_itemamt;
                ls_desc = "ถอนเงิน";
                li_sign_operate = -1;
            }
            else if (ls_itemtype == "CCA")
            {
                ldtm_closedate = d_slipbank.GetItemDateTime(1, "entry_date");
                li_closestatus = 1;
                ldc_balance = 0;
                idc_scobal = 0;
                ls_desc = "ปิดบัญชี";
                li_sign_operate = -1;
            }


            li_seqno++;

            sql = @"update	FINBANKACCOUNT
                set	CLOSE_STATUS	= " + li_closestatus + @",
			    CLOSE_DATE			= " + ldtm_closedate + @",
			    BALANCE				= " + ldc_balance + @",
			    LASTSTM_SEQ			= " + li_seqno + @",
			    WITH_AMT			= WITH_AMT - " + ldc_with + @",
			    DEPT_AMT			= DEPT_AMT + " + ldc_dept + @",
			    LASTACCESS_DATE	    = " + ldtm_entry_date + @",
			    BOOK_LASTUPDATE	    = " + ldtm_entry_date + @",
			    sco_balance			= " + idc_scobal + @"
                where ACCOUNT_NO	= " + ls_accno + @" and
			    BANK_CODE			= " + ls_bankcode + @"and
			    BANKBRANCH_CODE	    = " + ls_bankbranch + @"and
			    coopbranch_id		= " + ls_coopbranch;

            Sta ta4 = new Sta(new DwTrans().ConnectionString);
            int rs4 = ta4.Exe(sql);
            if (rs4 < 0)
            {
                //ทำไร?
            }

            //if sqlca.sqlcode <> 0 then
            //    messagebox( this.title, "ไม่สามารถบันทึกข้อมูลลง FINBANKACCOUNT ",StopSign! )
            //    rollback;
            //    return Failure
            //end if

            sql = @"INSERT INTO	FINBANKSTATEMENT
                 (	SEQ_NO,	ACCOUNT_NO,	BANK_CODE,
	                BANKBRANCH_CODE, DETAIL_DESC, ENTRY_ID,
	                ENTRY_DATE,	OPERATE_DATE, REF_SEQ,
	                ITEM_STATUS, CANCEL_ID,	CANCEL_DATE,
	                BALANCE, BALANCE_BEGIN,	MACHINE_ID,
	                COOPBRANCH_ID,	refer_slipno, item_amt,
	                sign_operate
                 )
                 VALUES
                (   " + li_seqno + "," + ls_accno + "," + ls_bankcode + @",
	                " + ls_bankbranch + "," + ls_desc + "," + ls_entryid + @",
	                " + ldtm_entry_date + "," + ldtm_entry_date + @",null,
	                1,null,null,
	                " + ldc_balance + ",0," + ls_machineid + @",
	                " + ls_coopbranch + "," + ls_slipno + "," + ldc_itemamt + @",
	                " + li_sign_operate + ")";

            //if sqlca.sqlcode <> 0 then
            //    messagebox( this.title, "ไม่สามารถบันทึกข้อมูลลง FINBANKSTATEMENT ",StopSign! )
            //    rollback;
            //    return Failure
            //end if

            Sta ta5 = new Sta(new DwTrans().ConnectionString);
            int rs5 = ta5.Exe(sql);
            if (rs5 < 0)
            {
                //ทำไร?
            }


            return 0;
        }
    }
}
