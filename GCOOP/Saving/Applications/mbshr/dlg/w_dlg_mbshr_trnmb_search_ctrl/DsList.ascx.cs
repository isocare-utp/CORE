using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.dlg.w_dlg_mbshr_trnmb_search_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBREQTRANMBDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBREQTRANMB;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void RetrieveList(string sql_search)
        {
            string sql = @"SELECT MBREQTRANMB.COOP_ID,   
                                 MBREQTRANMB.TRNMBREQ_DOCNO,   
                                 MBREQTRANMB.MEMOLD_NO,   
                                 MBREQTRANMB.MEMNEW_NO,   
                                 MBREQTRANMB.TRNMBREQ_DATE,   
                                 MBREQTRANMB.TRNMBREQ_STATUS,   
                                 MBREQTRANMB.ENTRY_ID,   
                                 MBREQTRANMB.ENTRY_BYCOOPID,   
                                 MBREQTRANMB.ENTRY_DATE,   
                                 MBREQTRANMB.APV_ID,   
                                 MBREQTRANMB.APV_DATE,   
                                 MBREQTRANMB.REMARK,   
                                 0 as apvflag,   
                                 '         ' as trnmbreq_tdate,   
                                 ft_memname( mbreqtranmb.coop_id , mbreqtranmb.memold_no ) as memnameold  
                            FROM MBREQTRANMB,
                                 MBMEMBMASTER  
                            WHERE (MBREQTRANMB.MEMOLD_NO=MBMEMBMASTER.MEMBER_NO)and
                                  ( mbreqtranmb.coop_id = {0} ) AND  
                                  (mbreqtranmb.trnmbreq_status=8)" + sql_search + @" order by trnmbreq_docno ASC";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}