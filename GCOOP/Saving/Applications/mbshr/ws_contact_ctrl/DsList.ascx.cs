using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_contact_ctrl
{
    public partial class DsList : DataSourceFormView
    {
        private object seq_no;
        private object refmember_flag;
        public DataSet1.DataTable1DataTable DATA { get;  set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, FormView2, this.DATA, "dsList");
            this.Button.Add("m_search");
            this.Register();
        }

        public void RetrieveData(string member_no)
        {
            String sql = @"    select * from mbmembcontact where coop_id={0} and member_no={1} 
                                        order by seq_no ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


        public void RetrieveMemb_no(string member_no)
        {
           String sql = @"select mbmembmaster.coop_id,  
                            mbmembmaster.member_no as REFMEMBER_NO,  
                            mbmembmaster.prename_code,        
                            mbmembmaster.member_type,
                           1 refmember_flag,
                                              ((rtrim(ltrim(mbucfprename.prename_desc)))+(rtrim(ltrim(mbmembmaster.memb_name) ))  
                            +' '+(rtrim(ltrim(mbmembmaster.memb_surname))) ) as DESCRIPTION,
            ('เลขที่'+(rtrim(ltrim(mbmembmaster.addr_no)))+' '+'ม.'+(rtrim(ltrim(mbmembmaster.addr_moo)))+' '+'อ.'+(rtrim(ltrim(mbucfdistrict.district_desc)))  
                            +' '+'จ.'+(rtrim(ltrim(mbucfprovince.province_desc)))+' '+'รหัสไปรษณีย์ '+(rtrim(ltrim(mbmembmaster.addr_postcode))) ) as REFMEMBER_ADDRESS,
               ((rtrim(ltrim(mbmembmaster.addr_phone)))+' '+(rtrim(ltrim(mbmembmaster.addr_mobilephone)))) as REFMEMBER_TEL
                       from mbmembmaster
       left join mbmembcontact on mbmembmaster.coop_id = mbmembcontact.coop_id and mbmembmaster.member_no = mbmembcontact.member_no
       left join mbucfdistrict on mbmembmaster.amphur_code = mbucfdistrict.district_code
       left join mbucfprovince on mbmembmaster.province_code = mbucfprovince.province_code
       left join mbucfmembgroup on mbmembmaster.coop_id = mbucfmembgroup.coop_id and mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code
       left join mbucfprename on mbmembmaster.prename_code = mbucfprename.prename_code                
                      where (mbmembmaster.member_no = {1}) and ( mbmembmaster.coop_id = {0} )";
           sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
           DataTable dt = WebUtil.Query(sql);
           this.ImportData(dt);
        }

        internal void RetrieveList(string memb_no)
        {
            throw new NotImplementedException();
        }

        internal void RetriveData()
        {
            throw new NotImplementedException();
        }
    
     
     
      }
}