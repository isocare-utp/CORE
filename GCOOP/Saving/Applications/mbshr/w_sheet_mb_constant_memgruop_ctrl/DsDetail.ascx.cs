using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "DsDetail");
            this.Register();
        }

        public void RetrieveMemberGroup(String membgroup_code)
        {
            String sql = @"SELECT * FROM MBUCFMEMBGROUP
                           WHERE coop_id = '" + state.SsCoopControl + "' and membgroup_code = '" + membgroup_code + "' ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

        public void RetrieveProvince()
        {
            String sql = @"SELECT province_code , province_desc , 1 as sorter
                            FROM mbucfprovince
                            union 
                                  select '','',0 from dual order by sorter,province_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_province", "province_desc", "province_code");

        }

        public void RetrieveDistrict(String province_code)
        {
            String sql = @" SELECT district_code , district_desc , 1 as sorter
                            FROM mbucfdistrict
                            where province_code = '" + province_code + @"'
                         union 
                            select '','',0 from dual order by sorter,district_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_amphur", "district_desc", "district_code");

        }

        public void RetrieveTambol(String district_code)
        {
            String sql = @"SELECT tambol_code , tambol_desc , 1 as sorter
                            FROM mbucftambol
                            where district_code = '" + district_code + @"'
                            union 
                                  select '','',0 from dual order by sorter,tambol_code asc ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_tambol", "tambol_desc", "tambol_code");

        }
    }
}