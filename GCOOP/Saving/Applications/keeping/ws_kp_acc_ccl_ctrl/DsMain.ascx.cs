using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_acc_ccl_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "DsMain");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"  select mbucfmembgroup.membgroup_code,   
                                    mbucfmembgroup.membgroup_desc,   
                                    mbucfprename.prename_desc,   
                                    mbmembmaster.memb_name,   
                                    mbmembmaster.memb_ename,   
                                    mbmembmaster.member_status,   
                                    mbmembmaster.member_date,   
                                    mbmembmaster.resign_status,   
                                    mbmembmaster.resign_date,   
                                    mbucfmembtype.membtype_code,   
                                    mbucfmembtype.membtype_desc,   
                                    mbmembmaster.coop_id,   
                                    mbmembmaster.member_no,   
                                    mbmembmaster.memb_surname,   
                                    mbmembmaster.memb_esurname
                            from mbmembmaster,   
                                    mbucfmembgroup,   
                                    mbucfprename,   
                                    mbucfmembtype  
                            where
                                    ( mbmembmaster.member_no = {1}) and
                                    ( mbmembmaster.coop_id = {0}) and
                                    ( mbmembmaster.coop_id = mbucfmembgroup.coop_id (+)) and  
                                    ( mbmembmaster.coop_id = mbucfmembtype.coop_id (+)) and  
                                    ( mbmembmaster.membtype_code = mbucfmembtype.membtype_code (+)) and  
                                    ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code (+)) and  
                                    ( mbmembmaster.prename_code = mbucfprename.prename_code (+))";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, WebUtil.MemberNoFormat(this.DATA[0].MEMBER_NO));
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}