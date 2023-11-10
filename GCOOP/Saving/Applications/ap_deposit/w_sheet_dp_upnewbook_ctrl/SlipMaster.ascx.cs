using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_upnewbook_ctrl
{
    public partial class SlipMaster : ControlDataWeb, IDataWeb
    {
        //Meta data
        public string meta_control_id = "FormView1";
        public string meta_control_type = "FormView";
        public string meta_tablename = "dpdeptmaster";
        public string meta_pk1 = "coop_od";
        public string meta_pk2 = "deptaccount_no";
        public string meta_sql = @"
        select 
            '' as deptslip_no,
	        (select workdate from amappstatus where application = :arg_application and coop_id = :arg_coop_id ) as deptslip_date,
	        '' as deptslip_amt,
	        'CSH' as cash_type,
	        d.prncbal as prncbal,
	        d.withdrawable_amt as withdrawable_amt,
	        d.checkpend_amt as checkpend_amt,
	        d.loangarantee_amt as loangarantee_amt,
	        d.accuint_amt as accuint_amt,
	        0.00 as fee_amt,
	        0.00 as preprncbal,
	        0.00 as preaccuint_amt,
	        '' as entry_id,
	        sysdate as entry_date,
	        d.intarrear_amt as intarrear_amt,
	        '' as dpstm_no,
	        sysdate as calint_from,
	        sysdate as calint_to,
	        0.00 as int_amt1,
	        0.00 as int_return ,
	        1 as item_status,
	        (select closeday_status from amappstatus where application = :arg_application and coop_id = :arg_coop_id )  closeday_status,
	        '' as recppaytype_code,
	        0.00 as other_amt,
	        d.prnc_no as prnc_no,
	        d.depttype_code as depttype_code,
	        '00' as deptgroup_code,
	        0 as count_wtd,
	        d.deptaccount_no as deptaccount_no,
	        '127.0.0.1' as machine_id,
	        0 as nobook_flag,
	        '' as authorize_id,
	        0 as cheque_send_flag,
	        d.remark as remark,
	        '11010100' as tofrom_accid,
	        '' as refer_slipno,
	        'DEP' as refer_app,
	        '' as posttovc_flag,
	        0.00 as tax_amt,
	        0.00 as int_bfyear,
	        1 as showfor_dept,
	        1 as accid_flag,
	        0 as transec_no,
	        '' as preadjdoc_no,
	        sysdate as preadjdoc_date,
	        '' as preadjitem_type,
	        '' as voucher_no,
	        0 as int_curyear,
	        0 as genvc_flag,
	        1 as payfee_meth,
	        0.00 as book_balance,
	        0 as peroid_dept,
	        1 as payfee_flag,
	        0 as due_flag,
	        0.00 as deptamt_other,
	        0.00 as deptslip_netamt,
	        0 as deptwith_flag,
	        sysdate as operate_time,
	        1 as teller_flag,
	        '' as check_no,
	        '' as bank_code,
	        '' as bankbranch_code,
	        d.deptaccount_name,
	        '' as deptformat,
	        0.00 as spint,
	        0 as spint_status,
	        0 as send_gov,
	        d.coop_id as coop_id,
	        :arg_coop_id as deptcoop_id,
	        '' as group_itemtpe,
	        0.00 as tax_return,
            d.deptaccount_ename as deptaccount_ename,
            d.member_no as member_no
        from dpdeptmaster d
        where 
	        d.coop_id = :arg_coop_id and 
	        d.deptaccount_no = :arg_deptaccount_no
        ";

        ////Meta Checkbox
        //public string meta_cdb_used_flag = "1:0:f";

        //Argument
        public string arg_application;
        public string arg_coop_id;
        public string arg_deptaccount_no;

        //Field Database
        private string fdb_deptslip_no;
        public string Fdb_deptslip_no
        {
            get { return fdb_deptslip_no; }
            set { fdb_deptslip_no = value; }
        }

        private DateTime fdb_deptslip_date;
        public DateTime Fdb_deptslip_date
        {
            get { return fdb_deptslip_date; }
            set { fdb_deptslip_date = value; }
        }

        private string fdb_deptitemtype_code;
        public string Fdb_deptitemtype_code
        {
            get { return fdb_deptitemtype_code; }
            set { fdb_deptitemtype_code = value; }
        }

        private string fdb_deptslip_amt;
        public string Fdb_deptslip_amt
        {
            get { return fdb_deptslip_amt; }
            set { fdb_deptslip_amt = value; }
        }

        private string fdb_cash_type;
        public string Fdb_cash_type
        {
            get { return fdb_cash_type; }
            set { fdb_cash_type = value; }
        }

        private decimal fdb_prncbal;
        public decimal Fdb_prncbal
        {
            get { return fdb_prncbal; }
            set { fdb_prncbal = value; }
        }

        private decimal fdb_withdrawable_amt;
        public decimal Fdb_withdrawable_amt
        {
            get { return fdb_withdrawable_amt; }
            set { fdb_withdrawable_amt = value; }
        }

        private decimal fdb_checkpend_amt;
        public decimal Fdb_checkpend_amt
        {
            get { return fdb_checkpend_amt; }
            set { fdb_checkpend_amt = value; }
        }

        private decimal fdb_loangarantee_amt;
        public decimal Fdb_loangarantee_amt
        {
            get { return fdb_loangarantee_amt; }
            set { fdb_loangarantee_amt = value; }
        }

        private decimal fdb_accuint_amt;
        public decimal Fdb_accuint_amt
        {
            get { return fdb_accuint_amt; }
            set { fdb_accuint_amt = value; }
        }

        private decimal fdb_fee_amt;
        public decimal Fdb_fee_amt
        {
            get { return fdb_fee_amt; }
            set { fdb_fee_amt = value; }
        }

        private decimal fdb_preprncbal;
        public decimal Fdb_preprncbal
        {
            get { return fdb_preprncbal; }
            set { fdb_preprncbal = value; }
        }

        private decimal fdb_preaccuint_amt;
        public decimal Fdb_preaccuint_amt
        {
            get { return fdb_preaccuint_amt; }
            set { fdb_preaccuint_amt = value; }
        }

        private string fdb_entry_id;
        public string Fdb_entry_id
        {
            get { return fdb_entry_id; }
            set { fdb_entry_id = value; }
        }

        private DateTime fdb_entry_date;
        public DateTime Fdb_entry_date
        {
            get { return fdb_entry_date; }
            set { fdb_entry_date = value; }
        }

        private decimal fdb_intarrear_amt;
        public decimal Fdb_intarrear_amt
        {
            get { return fdb_intarrear_amt; }
            set { fdb_intarrear_amt = value; }
        }

        private decimal fdb_dpstm_no;
        public decimal Fdb_dpstm_no
        {
            get { return fdb_dpstm_no; }
            set { fdb_dpstm_no = value; }
        }

        private DateTime fdb_calint_from;
        public DateTime Fdb_calint_from
        {
            get { return fdb_calint_from; }
            set { fdb_calint_from = value; }
        }

        private DateTime fdb_calint_to;
        public DateTime Fdb_calint_to
        {
            get { return fdb_calint_to; }
            set { fdb_calint_to = value; }
        }

        private decimal fdb_int_amt1;
        public decimal Fdb_int_amt1
        {
            get { return fdb_int_amt1; }
            set { fdb_int_amt1 = value; }
        }

        private decimal fdb_int_return;
        public decimal Fdb_int_return
        {
            get { return fdb_int_return; }
            set { fdb_int_return = value; }
        }

        private decimal fdb_item_status;
        public decimal Fdb_item_status
        {
            get { return fdb_item_status; }
            set { fdb_item_status = value; }
        }

        private decimal fdb_closeday_status;
        public decimal Fdb_closeday_status
        {
            get { return fdb_closeday_status; }
            set { fdb_closeday_status = value; }
        }

        private string fdb_recppaytype_code;
        public string Fdb_recppaytype_code
        {
            get { return fdb_recppaytype_code; }
            set { fdb_recppaytype_code = value; }
        }

        private decimal fdb_other_amt;
        public decimal Fdb_other_amt
        {
            get { return fdb_other_amt; }
            set { fdb_other_amt = value; }
        }

        private decimal fdb_prnc_no;
        public decimal Fdb_prnc_no
        {
            get { return fdb_prnc_no; }
            set { fdb_prnc_no = value; }
        }

        private string fdb_depttype_code;
        public string Fdb_depttype_code
        {
            get { return fdb_depttype_code; }
            set { fdb_depttype_code = value; }
        }

        private string fdb_deptgroup_code;
        public string Fdb_deptgroup_code
        {
            get { return fdb_deptgroup_code; }
            set { fdb_deptgroup_code = value; }
        }

        private decimal fdb_count_wtd;
        public decimal Fdb_count_wtd
        {
            get { return fdb_count_wtd; }
            set { fdb_count_wtd = value; }
        }

        private string fdb_deptaccount_no;
        public string Fdb_deptaccount_no
        {
            get { return fdb_deptaccount_no; }
            set { fdb_deptaccount_no = value; }
        }

        private string fdb_machine_id;
        public string Fdb_machine_id
        {
            get { return fdb_machine_id; }
            set { fdb_machine_id = value; }
        }

        private decimal fdb_nobook_flag;
        public decimal Fdb_nobook_flag
        {
            get { return fdb_nobook_flag; }
            set { fdb_nobook_flag = value; }
        }

        private string fdb_authorize_id;
        public string Fdb_authorize_id
        {
            get { return fdb_authorize_id; }
            set { fdb_authorize_id = value; }
        }

        private decimal fdb_cheque_send_flag;
        public decimal Fdb_cheque_send_flag
        {
            get { return fdb_cheque_send_flag; }
            set { fdb_cheque_send_flag = value; }
        }

        private string fdb_remark;
        public string Fdb_remark
        {
            get { return fdb_remark; }
            set { fdb_remark = value; }
        }

        private string fdb_tofrom_accid;
        public string Fdb_tofrom_accid
        {
            get { return fdb_tofrom_accid; }
            set { fdb_tofrom_accid = value; }
        }

        private string fdb_refer_slipno;
        public string Fdb_refer_slipno
        {
            get { return fdb_refer_slipno; }
            set { fdb_refer_slipno = value; }
        }

        private string fdb_refer_app;
        public string Fdb_refer_app
        {
            get { return fdb_refer_app; }
            set { fdb_refer_app = value; }
        }

        private decimal fdb_posttovc_flag;
        public decimal Fdb_posttovc_flag
        {
            get { return fdb_posttovc_flag; }
            set { fdb_posttovc_flag = value; }
        }

        private decimal fdb_tax_amt;
        public decimal Fdb_tax_amt
        {
            get { return fdb_tax_amt; }
            set { fdb_tax_amt = value; }
        }

        private decimal fdb_int_bfyear;
        public decimal Fdb_int_bfyear
        {
            get { return fdb_int_bfyear; }
            set { fdb_int_bfyear = value; }
        }

        private decimal fdb_showfor_dept;
        public decimal Fdb_showfor_dept
        {
            get { return fdb_showfor_dept; }
            set { fdb_showfor_dept = value; }
        }

        private decimal fdb_accid_flag;
        public decimal Fdb_accid_flag
        {
            get { return fdb_accid_flag; }
            set { fdb_accid_flag = value; }
        }

        private decimal fdb_transec_no;
        public decimal Fdb_transec_no
        {
            get { return fdb_transec_no; }
            set { fdb_transec_no = value; }
        }

        private string fdb_preadjdoc_no;
        public string Fdb_preadjdoc_no
        {
            get { return fdb_preadjdoc_no; }
            set { fdb_preadjdoc_no = value; }
        }

        private DateTime fdb_preadjdoc_date;
        public DateTime Fdb_preadjdoc_date
        {
            get { return fdb_preadjdoc_date; }
            set { fdb_preadjdoc_date = value; }
        }

        private string fdb_preadjitem_type;
        public string Fdb_preadjitem_type
        {
            get { return fdb_preadjitem_type; }
            set { fdb_preadjitem_type = value; }
        }

        private string fdb_voucher_no;
        public string Fdb_voucher_no
        {
            get { return fdb_voucher_no; }
            set { fdb_voucher_no = value; }
        }

        private decimal fdb_int_curyear;
        public decimal Fdb_int_curyear
        {
            get { return fdb_int_curyear; }
            set { fdb_int_curyear = value; }
        }

        private decimal fdb_genvc_flag;
        public decimal Fdb_genvc_flag
        {
            get { return fdb_genvc_flag; }
            set { fdb_genvc_flag = value; }
        }

        private decimal fdb_payfee_meth;
        public decimal Fdb_payfee_meth
        {
            get { return fdb_payfee_meth; }
            set { fdb_payfee_meth = value; }
        }

        private int fdb_book_balance;
        public int Fdb_book_balance
        {
            get { return fdb_book_balance; }
            set { fdb_book_balance = value; }
        }

        private decimal fdb_peroid_dept;
        public decimal Fdb_peroid_dept
        {
            get { return fdb_peroid_dept; }
            set { fdb_peroid_dept = value; }
        }

        private int fdb_payfee_flag;
        public int Fdb_payfee_flag
        {
            get { return fdb_payfee_flag; }
            set { fdb_payfee_flag = value; }
        }

        private decimal fdb_due_flag;
        public decimal Fdb_due_flag
        {
            get { return fdb_due_flag; }
            set { fdb_due_flag = value; }
        }

        private decimal fdb_deptamt_other;
        public decimal Fdb_deptamt_other
        {
            get { return fdb_deptamt_other; }
            set { fdb_deptamt_other = value; }
        }

        private decimal fdb_deptslip_netamt;
        public decimal Fdb_deptslip_netamt
        {
            get { return fdb_deptslip_netamt; }
            set { fdb_deptslip_netamt = value; }
        }

        private string fdb_deptwith_flag;
        public string Fdb_deptwith_flag
        {
            get { return fdb_deptwith_flag; }
            set { fdb_deptwith_flag = value; }
        }

        private DateTime fdb_operate_time;
        public DateTime Fdb_operate_time
        {
            get { return fdb_operate_time; }
            set { fdb_operate_time = value; }
        }

        private decimal fdb_teller_flag;
        public decimal Fdb_teller_flag
        {
            get { return fdb_teller_flag; }
            set { fdb_teller_flag = value; }
        }

        private string fdb_check_no;
        public string Fdb_check_no
        {
            get { return fdb_check_no; }
            set { fdb_check_no = value; }
        }

        private string fdb_bank_code;
        public string Fdb_bank_code
        {
            get { return fdb_bank_code; }
            set { fdb_bank_code = value; }
        }

        private string fdb_bankbranch_code;
        public string Fdb_bankbranch_code
        {
            get { return fdb_bankbranch_code; }
            set { fdb_bankbranch_code = value; }
        }

        private string fdb_deptaccount_name;
        public string Fdb_deptaccount_name
        {
            get { return fdb_deptaccount_name; }
            set { fdb_deptaccount_name = value; }
        }

        private string fdb_deptformat;
        public string Fdb_deptformat
        {
            get { return fdb_deptformat; }
            set { fdb_deptformat = value; }
        }

        private int fdb_spint;
        public int Fdb_spint
        {
            get { return fdb_spint; }
            set { fdb_spint = value; }
        }

        private int fdb_spint_status;
        public int Fdb_spint_status
        {
            get { return fdb_spint_status; }
            set { fdb_spint_status = value; }
        }

        private decimal fdb_send_gov;
        public decimal Fdb_send_gov
        {
            get { return fdb_send_gov; }
            set { fdb_send_gov = value; }
        }

        private string fdb_coop_id;
        public string Fdb_coop_id
        {
            get { return fdb_coop_id; }
            set { fdb_coop_id = value; }
        }

        private string fdb_deptcoop_id;
        public string Fdb_deptcoop_id
        {
            get { return fdb_deptcoop_id; }
            set { fdb_deptcoop_id = value; }
        }

        private string fdb_group_itemtpe;
        public string Fdb_group_itemtpe
        {
            get { return fdb_group_itemtpe; }
            set { fdb_group_itemtpe = value; }
        }

        private decimal fdb_tax_return;
        public decimal Fdb_tax_return
        {
            get { return fdb_tax_return; }
            set { fdb_tax_return = value; }
        }

        private string fdb_member_no;
        public string Fdb_member_no
        {
            get { return fdb_member_no; }
            set { fdb_member_no = value; }
        }

        private string fdb_deptaccount_ename;
        public string Fdb_deptaccount_ename
        {
            get { return fdb_deptaccount_ename; }
            set { fdb_deptaccount_ename = value; }
        }

        public DataTable DdCoopId()
        {
            DataTable dt = null;
            try
            {
                string sql = @"
                SELECT 
                    coop_id,   
                    coop_name
                FROM cmcoopmaster order by coop_id asc
                ";
                dt = WebUtil.Query(sql);
            }
            catch { }
            return dt;
        }

        public Control CreateInstance()
        {
            return new SlipMaster();
        }
    }
}