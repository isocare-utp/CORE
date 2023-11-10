using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.ws_as_cancelass_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ASSCONTMASTERDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSCONTMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView2, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void InitAssContno(String ls_asscontno)
        {
            string sql = @"
                        select 
		                        dbo.ft_getmemname(mb.coop_id,mb.member_no) as mbname,
		                        rtrim(ltrim( mb.membgroup_code)) +' - '+mgrp.membgroup_desc as mbgroup,
		                        rtrim(ltrim(mb.membtype_code))+ ':' + mt.membtype_desc  as mbtype,
		                        ast.assisttype_code+ ':' + ast.assisttype_desc asstypedesc,	
		                        ass.assistpay_code + ':' + asp.assistpay_desc asspaydesc,
                                ast.assisttype_group,
		                        ass.*
                        from	asscontmaster ass
		                        join mbmembmaster mb on ass.member_no = mb.member_no
		                        join mbucfprename mpre on mb.prename_code = mpre.prename_code
		                        join mbucfmembgroup mgrp on mb.membgroup_code = mgrp.membgroup_code
		                        join mbucfmembtype mt on mb.membtype_code = mt.membtype_code
		                        join assucfassisttype ast on ass.assisttype_code = ast.assisttype_code
		                        join assucfassisttypepay asp on ass.assisttype_code = asp.assisttype_code and ass.assistpay_code = asp.assistpay_code
                        where ass.coop_id = {0} 
                        and ass.asscontract_no = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_asscontno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdAssContNo(string ls_memno)
        {
            string sql = @"
                        select  99 as ordercode, 
		                        ass.asscontract_no,
                                ass.asscontract_no+ '('+ast.assisttype_desc+ ':' +asp.assistpay_desc+')' as assdisplay
                        from	asscontmaster ass
		                        join assucfassisttype ast on ass.assisttype_code = ast.assisttype_code
		                        join assucfassisttypepay asp on ass.assisttype_code = asp.assisttype_code and ass.assistpay_code = asp.assistpay_code
                        where ass.coop_id = {0} and ass.member_no = {1} and ass.withdrawable_amt > 0 and ast.assisttype_group <> '07' and ass.pay_balance = 0 and ass.asscont_status = 1 
                        union
                        select    0 as ordercode,'' as asscontract_no, 'กรุณาเลือกรายการ' as assdisplay 
                        union
                        select  99 as ordercode, 
		                        ass.asscontract_no,
                                ass.asscontract_no+'('+ast.assisttype_desc+ ':' +asp.assistpay_desc+')' as assdisplay
                        from	asscontmaster ass
		                        join assucfassisttype ast on ass.assisttype_code = ast.assisttype_code
		                        join assucfassisttypepay asp on ass.assisttype_code = asp.assisttype_code and ass.assistpay_code = asp.assistpay_code
                        where ass.coop_id = {0} and ass.member_no = {1} and ass.withdrawable_amt = 0 and ast.assisttype_group = '07' and ass.asscont_status = 1 
                        order by ordercode, asscontract_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_memno);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "asscontract_no", "assdisplay", "asscontract_no");
        }

    }
}