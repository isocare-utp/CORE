using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_req_mbnew_gain_btg_ctrl
{
    public partial class DsGain : DataSourceRepeater
    {
        public DataSet1.MBREQAPPLGAINDataTable DATA { get; set; }

        public void InitDsGaing(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBREQAPPLGAIN;
            this.EventItemChanged = "OnDsGainItemChanged";
            this.EventClicked = "OnDsGainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsGain");
            this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveGain(string docno)
        {
            String sql = @"select * from mbreqappl pl,mbreqapplgain mg 
                            where (pl.appl_docno=mg.appl_docno) and 
                                  (pl.coop_id={0}) and 
                                  (pl.appl_docno={1})";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, docno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdPrenameGain()
        {
            string sql = @"
            SELECT PRENAME_CODE,   
                PRENAME_DESC
            FROM MBUCFPRENAME
            order by PRENAME_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "PRENAME_CODE", "PRENAME_DESC", "PRENAME_CODE");
        }

        public void DdRelationGain()
        {
            string sql = @"
            SELECT CONCERN_CODE,   
                GAIN_CONCERN
            FROM MBUCFGAINCONCERN
            order by CONCERN_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "gain_relation", "GAIN_CONCERN", "CONCERN_CODE");
        }
    }
}