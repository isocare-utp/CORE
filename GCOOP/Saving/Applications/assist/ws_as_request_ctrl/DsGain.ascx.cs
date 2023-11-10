using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsGain : DataSourceRepeater
    {
        public DataSet1.ASSREQMASTERGAINDataTable DATA { get; set; }
        public void InitDsGain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTERGAIN;
            this.EventItemChanged = "OnDsGainItemChanged";
            this.EventClicked = "OnDsGainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsGain");
            this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveGainMb(String memno)
        {
            string sql = @"select
        	                seq_no,
        	                gain_name,
        	                gain_relation as gainrelation_code
                        from mbgainmaster
                        where coop_id = {0} and member_no = {1}
                        order by seq_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveGainAss(String as_assdocno)
        {
            string sql = @"select
                            b.coop_id,
                            b.assist_docno,
	                        b.seq_no,
	                        b.gain_name,
                            b.gainassist_amt,
	                        b.gainrelation_code
                        from assreqmaster a
                        inner join assreqmastergain b on a.assist_docno = b.assist_docno
                        where b.coop_id = {0} and b.assist_docno = {1}
                        order by b.seq_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_assdocno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdGainRelation(ref String ls_mingaincode)
        {
            string sql = @"
            SELECT rtrim(ltrim( CONCERN_CODE )) as gainrelation_code,
                GAIN_CONCERN
            FROM MBUCFGAINCONCERN
            WHERE GAIN_CONCERN <> 'ตนเอง'
            order by CONCERN_CODE";
            DataTable dt = WebUtil.Query(sql);
            ls_mingaincode = dt.Rows[0].Field<string>("gainrelation_code");
            this.DropDownDataBind(dt, "gainrelation_code", "GAIN_CONCERN", "gainrelation_code");
        }

        public void DdGainRelationRow(int row , ref String ls_mingaincode)
        {
            string sql = @"
            SELECT rtrim(ltrim(CONCERN_CODE)) as gainrelation_code,
                GAIN_CONCERN
            FROM MBUCFGAINCONCERN
            WHERE GAIN_CONCERN <> 'ตนเอง'
            order by CONCERN_CODE";
            DataTable dt = WebUtil.Query(sql);
            ls_mingaincode = dt.Rows[0].Field<string>("gainrelation_code");
            this.DropDownDataBind(dt, "gainrelation_code", "GAIN_CONCERN", "gainrelation_code", row);
        }
    }
}