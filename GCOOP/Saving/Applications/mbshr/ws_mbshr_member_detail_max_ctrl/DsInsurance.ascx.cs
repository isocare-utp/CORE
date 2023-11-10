using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl
{
    public partial class DsInsurance : DataSourceRepeater
    {
        public DataSet1.DT_INSURANCEDataTable DATA { get; set; }

        public void InitDsInsurance(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_INSURANCE;
            this.EventItemChanged = "OnDsInsuranceItemChanged";
            this.EventClicked = "OnDsInsuranceClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsInsurance");
            this.Register();
        }

        public void RetrieveInsurance(String ls_member_no)
        {
            String sql = @"  
                  SELECT LNINSURANCEFIRE.INSURANCE_NO,   
                         LNINSURANCEFIRE.MEMBER_NO,   
                         LNINSURANCEFIRE.LOANCONTRACT_NO,   
                         LNINSURANCEFIRE.PRINCIPAL_BALANCE,   
                         LNINSURANCEFIRE.STARTINSURE_DATE,   
                         LNINSURANCEFIRE.EXPIREINSURE_DATE,   
                         LNINSURANCEFIRE.INSURANCE_AMT,   
                         LNINSURANCEFIRE.INSURENET_AMT,   
                         LNINSURANCEFIRE.INSUREPAY_STATUS  
                    FROM LNINSURANCEFIRE  
                   WHERE
                         ( LNINSURANCEFIRE.COOP_ID = {0} )  AND   
                         ( lninsurancefire.member_no = {1} )  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}