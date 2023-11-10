using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_mb_mthexpense_adjust_ctrl
{
    public partial class DsPlus : DataSourceRepeater
    {
        public DataSet1.MBMEMBMTHEXPENSEDataTable DATA { get; set; }

        public void InitDsPlus(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMTHEXPENSE;
            this.EventItemChanged = "OnDsPlusItemChanged";
            this.EventClicked = "OnDsPlusClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPlus");
            this.Button.Add("b_delete");
            this.Register();
        }
        public void RetrievePlus(string member_no)
        {
            String sql = @"  SELECT MBMEMBMTHEXPENSE.COOP_ID,
                                     MBMEMBMTHEXPENSE.MEMBER_NO,   
                                     MBMEMBMTHEXPENSE.SIGN_FLAG,   
                                     MBMEMBMTHEXPENSE.SEQ_NO,   
                                     MBMEMBMTHEXPENSE.MTHEXPENSE_DESC,   
                                     MBMEMBMTHEXPENSE.MTHEXPENSE_AMT  
                                FROM MBMEMBMTHEXPENSE  
                               WHERE (MBMEMBMTHEXPENSE.COOP_ID={0}) AND
                                     ( mbmembmthexpense.member_no = {1} ) AND  
                                     ( MBMEMBMTHEXPENSE.SIGN_FLAG = 1 )    

 ";
            //1
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox TContract
        {
            get { return this.cpsum_mthexpense_amt; }
        }
    }
}