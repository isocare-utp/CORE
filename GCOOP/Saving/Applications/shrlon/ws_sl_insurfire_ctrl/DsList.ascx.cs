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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNINSURANCEFIREDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNINSURANCEFIRE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            //this.Button.Add("b_memsearch");
            // this.Button.Add("b_contsearch");
            this.Register();
        }

        public void RetrieveInsurfire(string member_no)
        {
            //LNINSURANCEFIRE.MEMCOOP_ID, LNINSURANCEFIRE.CONCOOP_ID, LNINSURANCEFIRE.ENTRY_BYCOOPID,
            string sql = @"
            SELECT LNINSURANCEFIRE.COOP_ID,   
                LNINSURANCEFIRE.INSURANCE_NO,                 
                LNINSURANCEFIRE.MEMBER_NO,                   
                LNINSURANCEFIRE.LOANCONTRACT_NO,   
                LNINSURANCEFIRE.OPERATE_DATE,   
                LNINSURANCEFIRE.PRINCIPAL_BALANCE,   
                LNINSURANCEFIRE.STARTINSURE_DATE,  
                LNINSURANCEFIRE.EXPIREINSURE_DATE,   
                LNINSURANCEFIRE.INSURANCE_AMT,   
                LNINSURANCEFIRE.STAMPDUTY_AMT,   
                LNINSURANCEFIRE.VAT_PERCENT,   
                LNINSURANCEFIRE.DISCOUNT_PERCENT,   
                LNINSURANCEFIRE.COORDINATE_PERCENT,   
                LNINSURANCEFIRE.INSURENET_AMT,   
                LNINSURANCEFIRE.INSUREPAY_STATUS,
                case when LNINSURANCEFIRE.INSUREPAY_STATUS = 1 then 'ชำระแล้ว' 
					 when LNINSURANCEFIRE.INSUREPAY_STATUS = 0 then ''
                end as PAY_STATUS,   
                LNINSURANCEFIRE.ENTRY_ID,   
                LNINSURANCEFIRE.ENTRY_DATE,                 
                LNINSURANCEFIRE.LOANTYPE_CODE,   
                LNINSURANCEFIRE.FLOODINSURE_AMT,
                LNINSURANCEFIRE.STARTINSURE_DATE || ' - ' || LNINSURANCEFIRE.EXPIREINSURE_DATE  AS INSURE_DATE,
                LNINSURANCEFIRE.collmast_no
            FROM LNINSURANCEFIRE 
            where ( LNINSURANCEFIRE.coop_id = {0} ) AND  
                ( LNINSURANCEFIRE.member_no = {1} )
            order by STARTINSURE_DATE"
            ;
            //TO_DATE(LNINSURANCEFIRE.STARTINSURE_DATE,'dd/mm/yyyy')||' - '||TO_DATE(LNINSURANCEFIRE.EXPIREINSURE_DATE,'dd/mm/yyyy')
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}