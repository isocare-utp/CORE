using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_assistpay_ctrl
{
    public partial class DsPayto : DataSourceRepeater
    {
        public DataSet1.ASSSLIPPAYOUTDETDataTable DATA { get; set; }
        public void InitDsPayto(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSSLIPPAYOUTDET;
            this.EventItemChanged = "OnDsPaytoItemChanged";
            this.EventClicked = "OnDsPaytoClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPayto");
            this.Button.Add("b_searchbank");
            this.Button.Add("b_delpayto");
            this.Register();
        }
        public void RetrieveDetail(String ls_assistdocno)
        {
            string sql = @"
	                select 
		                distinct
		                req.moneytype_code,
		                case when gain.gain_name is not null then 
			                case 
			                when req.moneytype_code ='CSH' then ' | | |' + pre.prename_desc + mb.memb_name + ' ' + mb.memb_surname + '| ' 
			                when req.moneytype_code ='CHQ' then req.expense_bank + '|' + req.expense_branch + '|' + rtrim(ltrim(req.expense_accid)) + '|' + pre.prename_desc + gain.gain_name + '  ' + gain.gain_surname + '| '
			                when req.moneytype_code ='CBT' then req.expense_bank + '|' + req.expense_branch + '|' + rtrim(ltrim(req.expense_accid)) + '|' + pre.prename_desc + gain.gain_name + '  ' + gain.gain_surname + '| '
			                when req.moneytype_code ='TRN' then ' | |' + rtrim(ltrim(req.deptaccount_no)) + '|' + pre.prename_desc + gain.gain_name + '  ' + gain.gain_surname + '| '
			                else '' end 
		                else
			                case  
                            when req.moneytype_code='CSH' then ' | | |' + pre.prename_desc + mb.memb_name + ' ' + mb.memb_surname + '| ' 
			                when req.moneytype_code='CHQ' then req.expense_bank + '|' + req.expense_branch + '|' + rtrim(ltrim(req.expense_accid)) +  '| | '
			                when req.moneytype_code='CBT' then req.expense_bank + '|' + req.expense_branch + '|' + rtrim(ltrim(req.expense_accid)) + '| | '
			                when req.moneytype_code='TRN' then ' | |' + rtrim(ltrim(req.deptaccount_no)) + '| | '
			                else '' end 
		                end payto_detail,
		                case when gain.gain_name is null then req.assist_amt else 0 end assist_amt
	                from assreqmaster req
	                left join assreqgain gain on req.assist_docno = gain.assist_docno
	                inner join mbmembmaster mb on req.member_no = mb.member_no
	                inner join mbucfprename pre on mb.prename_code = pre.prename_code
	                left join mbucfprename pregain on gain.gainprename_code = pregain.prename_code
	                left join cmucfbank bank on req.expense_bank = bank.bank_code
	                left join cmucfbankbranch branch on req.expense_bank = branch.bank_code and req.expense_branch = branch.branch_id
	                where req.coop_id = {0} and req.assist_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_assistdocno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdMoneyType()
        {
            string sql = @" select  MONEYTYPE_CODE,  
		                    MONEYTYPE_GROUP, 
                            MONEYTYPE_DESC,   
                            SORT_ORDER  ,
                            MONEYTYPE_CODE + ' - ' + MONEYTYPE_DESC as MONEYTYPE_DISPLAY ,1 as sorter
                            FROM CMUCFMONEYTYPE  WHERE   MONEYTYPE_CODE in ('CSH','CHQ','CBT','CBO','TRN')  
                            union
                            select '', '','',0,'---กรุณาเลือกประเภทเงิน---', 0  order by sorter, SORT_ORDER ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "moneytype_code", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");
        }

          public void DdToFromAccID()
        {
            string sql = @" select tf.account_id, tf.account_id + ':' + ac.account_name as display
                            from	( select distinct account_id from cmucftofromaccid where applgroup_code = 'SLN' and sliptype_code = 'LWD' and moneytype_code = 'TRN') tf 
		                            join accmaster ac on tf.account_id = ac.account_id
                            order by tf.account_id ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "display", "account_id");
        }

          public void DdToFromAccIDRow(string moneytype, int row, ref string min_tofromaccid)
        {
            string sql = @" select tf.account_id, tf.account_id + ':' + ac.account_name as display
                            from	( select distinct account_id from cmucftofromaccid where applgroup_code = 'SLN' and sliptype_code = 'LWD' and moneytype_code = {0}) tf 
		                            join accmaster ac on tf.account_id = ac.account_id
                            order by tf.account_id ";
            sql = WebUtil.SQLFormat(sql, moneytype);
            DataTable dt = WebUtil.Query(sql);
            if (dt.Rows.Count > 0) {
                min_tofromaccid = dt.Rows[0].Field<string>("account_id");
            }
            this.DropDownDataBind(dt, "tofrom_accid", "display", "account_id", row);
        }
    }
}