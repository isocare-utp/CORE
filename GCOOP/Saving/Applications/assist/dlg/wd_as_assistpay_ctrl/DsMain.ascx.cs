using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_assistpay_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ASSSLIPPAYOUTDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSSLIPPAYOUT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView2, this.DATA, "dsMain");
            this.Register();
        }

        public void InitDetail(String ls_assistdocno)
        {
            string sql = @"
                        select 
		                        mpre.prename_desc + mb.memb_name + '  ' + mb.memb_surname as mbname,
		                        rtrim(ltrim( mb.membgroup_code )) + ' - ' + mgrp.membgroup_desc as mbgroup,
		                        rtrim(ltrim(mb.membtype_code)) + ':' + mt.membtype_desc  as mbtype,
		                        ast.assisttype_code, 
                                ast.assisttype_group,                        
		                        ass.assistpay_code,
		                        ast.assisttype_code + ':' + ast.assisttype_desc assisttype_desc,	
		                        ass.assistpay_code  + ':' + asp.assistpay_desc assistpay_desc,
		                        ass.asscontract_no,
		                        ass.assistreq_docno as ref_reqdocno, 
		                        ass.member_no,
		                        ass.approve_amt as bfapv_amt,
		                        ass.withdrawable_amt as bfwtd_amt,
		                        ass.last_periodpay as bfperiod,
                                ass.lastpay_date as bflastpay_date,
                                case mb.resign_status when 1 then 'ลาออก' else 'เป็นสมาชิก' end as resign_desc,
                                case mb.dead_status when 1 then 'เสียชีวิต' else '-' end as dead_desc,
                                mb.dead_status,
                                ass.send_system
                        from	asscontmaster ass
		                        join mbmembmaster mb on ass.member_no = mb.member_no
		                        join mbucfprename mpre on mb.prename_code = mpre.prename_code
		                        join mbucfmembgroup mgrp on mb.membgroup_code = mgrp.membgroup_code
		                        join mbucfmembtype mt on mb.membtype_code = mt.membtype_code
		                        join assucfassisttype ast on ass.assisttype_code = ast.assisttype_code
		                        join assucfassisttypepay asp on ass.assisttype_code = asp.assisttype_code and ass.assistpay_code = asp.assistpay_code
                        where ass.asscont_status > 0 
                        and ass.withdrawable_amt > 0
                        and ass.coop_id = {0} 
                        and ass.asscontract_no = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_assistdocno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}