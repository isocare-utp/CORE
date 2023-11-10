using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl
{
    public partial class DsCodept : DataSourceRepeater
    {
        public DataSet1.DT_CODEPTDataTable DATA { get; set; }

        public void InitDsCodept(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_CODEPT;
            this.EventItemChanged = "OnDsDsCodeptItemChanged";
            this.EventClicked = "OnDsDsCodeptClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCodept");
            this.Register();
        }

        public void RetrieveCodept(String ls_dept_no)
        {
            String sql = @"    SELECT DPCODEPOSIT.DEPTACCOUNT_NO,   
                                DPCODEPOSIT.SEQ_NO,   
                                DPCODEPOSIT.REF_NO,
                                DPCODEPOSIT.NAME,   
                                DPCODEPOSIT.SURNAME,   
                                DPCODEPOSIT.PRENAME_CODE,
		                    MBUCFPRENAME.PRENAME_DESC,   
                                DPCODEPOSIT.HOUSE_ID,   
                                DPCODEPOSIT.ROAD,   
                                DPCODEPOSIT.SOI,   
                                DPCODEPOSIT.GROUP_ID,   
                                DPCODEPOSIT.TUMBOL,   
                                DPCODEPOSIT.DISTRICT,   
                                DPCODEPOSIT.PROVINCE,   
                                DPCODEPOSIT.POST_CODE,   
                                DPCODEPOSIT.PHONE_NO,   
                                DPCODEPOSIT.FAX_NO,   
                                DPCODEPOSIT.REF_TYPE,   
                                DPCODEPOSIT.COOP_ID  
                        FROM DPCODEPOSIT  ,
                    MBUCFPRENAME
                        WHERE
                        (DPCODEPOSIT.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE) AND
                    ( DPCODEPOSIT.DEPTACCOUNT_NO = {1} ) AND
                        ( DPCODEPOSIT.COOP_ID = {0} )   ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_dept_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


    }
}