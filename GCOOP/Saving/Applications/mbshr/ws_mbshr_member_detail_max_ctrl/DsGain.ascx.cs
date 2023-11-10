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
    public partial class DsGain : DataSourceRepeater
    {
        public DataSet1.DT_GAINDataTable DATA { get; set; }

        public void InitDsGain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_GAIN;
            this.EventItemChanged = "OnDsGainItemChanged";
            this.EventClicked = "OnDsGainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsGain");
            this.Register();
        }

        public void RetrieveGain(String ls_member_no)
        {
            String sql = @"  
                  SELECT MBGAINMASTER.COOP_ID,   
                         MBGAINMASTER.MEMBER_NO,   
                         MBGAINMASTER.SEQ_NO,   
                         MBGAINMASTER.GAIN_NAME,   
                         MBGAINMASTER.GAIN_SURNAME,   
                         MBGAINMASTER.GAIN_ADDR,   
                         MBGAINMASTER.GAIN_RELATION,   
                         MBGAINMASTER.REMARK,   
                         MBGAINMASTER.WRITE_DATE,   
                         MBGAINMASTER.WRITE_AT,   
                         MBGAINMASTER.AGE  
                    FROM MBGAINMASTER  
                   WHERE ( MBGAINMASTER.COOP_ID = {0} ) AND  
                         ( MBGAINMASTER.MEMBER_NO = {1} )  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}