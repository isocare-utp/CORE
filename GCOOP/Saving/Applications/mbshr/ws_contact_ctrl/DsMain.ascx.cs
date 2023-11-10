using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_contact_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable2DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable2;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }
        public void RetrieveMain(String member_no)
        {
            String sql = @"select mbmembmaster.coop_id,   
                             mbmembmaster.member_no,   
                             mbmembmaster.prename_code,        
                             mbmembmaster.member_type,     
                          	isnull(ltrim(rtrim(mbmembmaster.membgroup_code)),'')+''+'-'+isnull(ltrim(rtrim(mbucfmembgroup.membgroup_desc)),'') as membgroup_desc,
						mbmembmthexpense.mthexpense_amt,
						mbmembmthexpense.mthexpense_desc,
						mbmembmthexpense.sign_flag,
                    isnull(ltrim(rtrim(mbucfprename.prename_desc)),'')+ isnull(ltrim(rtrim(mbmembmaster.memb_name)),'')   
                             +' '+isnull(ltrim(rtrim(mbmembmaster.memb_surname)),'' ) as MEMB_NAME,
						mbmembmthexpense.seq_no
                        from mbmembmaster
            left join mbmembmthexpense on mbmembmthexpense.coop_id = mbmembmaster.coop_id and mbmembmaster.member_no = mbmembmthexpense.member_no
        left join mbucfmembgroup on mbmembmaster.coop_id = mbucfmembgroup.coop_id and mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code
        left join mbucfprename on mbmembmaster.prename_code = mbucfprename.prename_code                                  
                       where (mbmembmaster.member_no = {1}) and ( mbmembmaster.coop_id = {0} )";
            sql = WebUtil.SQLFormat(sql,state.SsCoopControl,member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}