using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.dlg.w_dlg_mbshr_mbgain_history_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBREQGAINDETAILDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBREQGAINDETAIL;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }

        public void RetrieveList(string member_no)
        {
            string sql = @"
                                SELECT 
                                    MBREQGAIN.GAIN_DOCNO,
                                    MBREQGAIN.MEMBER_NO,
                                    MBREQGAIN.ENTRY_DATE,
									MBMEMBMASTER.MEMB_NAME,
									MBMEMBMASTER.MEMB_SURNAME
                                        
                                FROM 
                                    MBREQGAIN, MBMEMBMASTER
                                WHERE 
                                    MBREQGAIN.COOP_ID = '" + state.SsCoopControl + @"' AND
                                    MBREQGAIN.MEMBER_NO = '" + member_no + @"' AND
                                    MBREQGAIN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO
                           ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}