using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_insurfire_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNINSURANCEFIREDataTable DATA { get;  set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNINSURANCEFIRE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            //this.Button.Add("b_memsearch");
            this.Button.Add("b_contsearch");
            this.Register();
        }

        public void RetrieveInsurance(string ins_no)
        {
            //LNINSURANCEFIRE.MEMCOOP_ID, LNINSURANCEFIRE.CONCOOP_ID, LNINSURANCEFIRE.ENTRY_BYCOOPID,
            string sql = @"select 
                lninsurancefire.coop_id,   
                lninsurancefire.insurance_no,                 
                lninsurancefire.member_no,                   
                lninsurancefire.loancontract_no,   
                lninsurancefire.operate_date,   
                lninsurancefire.principal_balance,   
                lninsurancefire.startinsure_date,  
                lninsurancefire.expireinsure_date,   
                lninsurancefire.insurance_amt,   
                lninsurancefire.stampduty_amt,   
                lninsurancefire.vat_percent,   
                lninsurancefire.discount_percent,   
                lninsurancefire.coordinate_percent,   
                lninsurancefire.insurenet_amt,   
                lninsurancefire.insurepay_status,
                case when lninsurancefire.insurepay_status = 1 then 'ชำระแล้ว' 
					 when lninsurancefire.insurepay_status = 8 then ''
                end as pay_status,   
                lninsurancefire.entry_id,   
                lninsurancefire.entry_date,                 
                lninsurancefire.loantype_code,   
                lninsurancefire.floodinsure_amt,
                lninsurancefire.startinsure_date || ' - ' || lninsurancefire.expireinsure_date  as insure_date,
                lninsurancefire.mthkeep_status,
                lninsurancefire.collmast_no
            from lninsurancefire 
            where ( lninsurancefire.coop_id = {0} ) and  
                ( trim(lninsurancefire.insurance_no) = {1} )
            order by startinsure_date";
            //TO_DATE(LNINSURANCEFIRE.STARTINSURE_DATE,'dd/mm/yyyy')||' - '||TO_DATE(LNINSURANCEFIRE.EXPIREINSURE_DATE,'dd/mm/yyyy')
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ins_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}