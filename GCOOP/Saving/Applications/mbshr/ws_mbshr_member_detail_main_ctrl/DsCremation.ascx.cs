using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
{
    public partial class DsCremation : DataSourceRepeater
    {

        public DataSet1.DT_CREMATIONDataTable DATA { get; private set; }
        public void InitDsCremation(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_CREMATION;
            this.EventItemChanged = "OnDsCremationItemChanged";
            this.EventClicked = "OnDsCremationClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsCremation");
            this.Register();
        }



        public void RetrieveMemb_no(string member_no)
        {
            String sql = @" select 
		                    mbcremationdet.coop_id as coop_id,   
		                    mbcremationdet.member_no as member_no,   
		                    mbcremationdet.seq_no as seq_no,        
		                    mbcremationdet.cmttype_code as cmttype_code,
		                    mbucfcremation.cmttype_desc as description, 
		                    mbcremationdet.item_amount as item_amount
		                    from mbcremationdet , mbucfcremation
		                    where  ( mbcremationdet.cmttype_code = mbucfcremation.cmttype_code ) and
		                           (mbcremationdet.member_no = {1}) and 
		                            ( mbcremationdet.coop_id = {0} )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

     
     //   public TextBox TContract
     //   {
   //         get { return this.cp_sum_item; }
    //    }



    }
}