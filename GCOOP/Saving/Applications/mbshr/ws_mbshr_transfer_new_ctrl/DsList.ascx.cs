using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_transfer_new_ctrl
{
    public partial class DsList : DataSourceRepeater
    {

        public DataSet1.DataTable1DataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            //this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveData(string member_no)
        {
            String sql = @"select tran_date,tran_period,tran_share,tran_loan,tran_detail from mbmembtransfer where coop_id={0} and member_no={1} 
                                        order by member_no "; 
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


        public void RetrieveMemb_no(string member_no)
        {
            String sql = @" select 
		                    coop_id,   
		                    member_no ,   
		                    tran_date,        
		                    tran_period,
		                    tran_share,
		                    tran_loan,
		                    tran_detail
		                    from mbmembtransfer
		                    where   (mbmembtransfer.member_no = {1}) and 
		                            ( mbmembtransfer.coop_id = {0} )";
           sql = WebUtil.SQLFormat(sql, state.SsCoopId ,member_no);
           DataTable dt = WebUtil.Query(sql);
           this.ImportData(dt);
        }

        public void CMTType()
        {
//            string sql = @"select 
//	                        mbucfcremation.cmttype_code cmttype_code, 
//	                        cmttype_code || ' - ' || cmttype_desc display, 
//	                        1 as sorter from mbucfcremation 
//                        where mbucfcremation.coop_id= {0} 
//                        union
//                        select '','',0 from dual order by sorter, cmttype_code";
//            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
//            DataTable dt = WebUtil.Query(sql);
//            this.DropDownDataBind(dt, "cmttype_code", "display", "cmttype_code");
        }

        internal void RetrieveList()
        {
            throw new NotImplementedException();
        }

        internal void RetriveData(string memb_no)
        {
            throw new NotImplementedException();
        }
    
     
     
      }
}