using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsOtherkeep : DataSourceRepeater
    {
        public DataSet1.DT_OTHERKEEPDataTable DATA { get; set; }

        public void InitDsOtherkeep(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_OTHERKEEP;
            this.EventItemChanged = "OnDsOtherkeepItemChanged";
            this.EventClicked = "OnDsOtherkeepClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsOtherkeep");
            this.Register();
        }

        public void RetrieveOtherkeep(String ls_member_no)
        {
            String sql = @"  
                              SELECT KPUCFKEEPOTHITEMTYPE.KEEPOTHITEMTYPE_CODE,   
                                     KPUCFKEEPOTHITEMTYPE.KEEPOTHITEMTYPE_DESC,   
                                     KPRCVKEEPOTHER.KEEPOTHER_TYPE,   
                                     KPRCVKEEPOTHER.STARTKEEP_PERIOD,   
                                     KPRCVKEEPOTHER.LASTKEEP_PERIOD,   
                                     KPRCVKEEPOTHER.DESCRIPTION,   
                                     KPRCVKEEPOTHER.ITEM_PAYMENT  
                                FROM KPRCVKEEPOTHER,   
                                     KPUCFKEEPOTHITEMTYPE  
                               WHERE ( KPRCVKEEPOTHER.COOP_ID = KPUCFKEEPOTHITEMTYPE.COOP_ID ) and  
                                     ( ( kprcvkeepother.coop_id = {0} ) AND  
                                     ( kprcvkeepother.member_no = {1} ) )    
  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox TContract
        {
            get { return this.cp_sum_item_payment; }
        }
    }
}