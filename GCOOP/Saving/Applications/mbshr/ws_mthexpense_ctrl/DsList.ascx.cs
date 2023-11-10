using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_mthexpense_ctrl
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
            this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveData(String member_no)
        {
            String sql = @"    select mbmembmaster.coop_id,   
                             mbmembmaster.member_no,   
                             mbmembmaster.prename_code,     
                             mbmembmaster.membgroup_code,   
                             mbmembmaster.member_type,       
                         mbucfmembgroup.membgroup_desc,
						mbmembmthexpense.mthexpense_amt,
						mbmembmthexpense.mthexpense_desc,
                        mbmembmthexpense.seq_no,
						mbmembmthexpense.sign_flag,
rtrim(ltrim(mbucfprename.prename_desc))+ rtrim(ltrim(mbmembmaster.memb_name)) +' '+ rtrim(ltrim(mbmembmaster.memb_surname) ) as MEMB_NAME
 from mbmembmaster
left join mbmembmthexpense on mbmembmthexpense.coop_id = mbmembmaster.coop_id and mbmembmaster.member_no = mbmembmthexpense.member_no
left join mbucfmembgroup on mbmembmaster.coop_id = mbucfmembgroup.coop_id and mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code
left join mbucfprename on mbmembmaster.prename_code = mbucfprename.prename_code                
                       where (mbmembmaster.member_no = {1}) and ( mbmembmaster.coop_id = {0})  order by mbmembmthexpense.seq_no ";
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

        internal void Retrieve()
        {
            throw new NotImplementedException();
        }

        internal void RetriveGroup()
        {
            throw new NotImplementedException();
        }

        internal void RetriveList()
        {
            throw new NotImplementedException();
        }

        internal void RetrieveData()
        {
            throw new NotImplementedException();
        }
    }
}