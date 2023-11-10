using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.TableName = "LNCOLLMASTER";
            this.Button.Add("b_memsearch");
            this.Register();
        }

        public void RetrieveMain(String member_no)
        {
            String sql = @"select mbmembmaster.member_no,   
                                 mbucfprename.prename_desc||   
                                 mbmembmaster.memb_name||' '||
                                 mbmembmaster.memb_surname as cp_name,   
                                 trim(mbmembmaster.membgroup_code) ||' - '|| 
                                 mbucfmembgroup.membgroup_desc as cp_memgroup,
                                 shsharemaster.sharestk_amt * shsharetype.unitshare_value as sharestk_value 
                            from mbmembmaster,   
                                 mbucfprename,   
                                 mbucfmembgroup,   
                                 shsharemaster,   
                                 shsharetype  
                           where ( mbucfprename.prename_code = mbmembmaster.prename_code ) and  
                                 ( mbucfmembgroup.membgroup_code = mbmembmaster.membgroup_code ) and  
                                 ( shsharetype.sharetype_code = shsharemaster.sharetype_code ) and  
                                 ( mbmembmaster.member_no = shsharemaster.member_no )and  
                                 ( ( mbmembmaster.member_no = {0} ) )  ";

            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            this.DATA[0].COOP_ID = state.SsCoopControl;
            this.DATA[0].COLLMASTTYPE_GRP = "01";
            DdCollmasttypegrp();
        }

        public void DdCollmasttypegrp()
        {
            string sql = @"select collmasttype_grp,collmasttype_desc from lnucfcollmasttypegrp
                            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_grp", "collmasttype_desc", "collmasttype_grp");

        }

        public void Retrieve(String collmast_no)
        {
            String sql = @"select   lncollmaster.coop_id,
                                    lncollmaster.collmast_no,
                                    lncollmaster.collmast_desc,
                                    lncollmaster.member_no,
                                    lncollmaster.collmasttype_grp,
                                    mbucfprename.prename_desc||   
                                    mbmembmaster.memb_name||' '||
                                    mbmembmaster.memb_surname as cp_name,   
                                    trim(mbmembmaster.membgroup_code) ||' - '|| 
                                    mbucfmembgroup.membgroup_desc as cp_memgroup,
                                    shsharemaster.sharestk_amt * shsharetype.unitshare_value as sharestk_value 
                           from     lncollmaster,
                                    mbmembmaster,   
                                    mbucfprename,   
                                    mbucfmembgroup,   
                                    shsharemaster,   
                                    shsharetype
                           where    ( mbucfprename.prename_code = mbmembmaster.prename_code ) and  
                                    ( mbucfmembgroup.membgroup_code = mbmembmaster.membgroup_code ) and  
                                    ( shsharetype.sharetype_code = shsharemaster.sharetype_code ) and  
                                    ( mbmembmaster.member_no = shsharemaster.member_no )and  
                                    (  mbmembmaster.member_no = lncollmaster.member_no )  and
                                    ( lncollmaster.coop_id = {0}) and
                                    ( lncollmaster.collmast_no = {1})";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdCollmasttypegrp();
        }
    }
}