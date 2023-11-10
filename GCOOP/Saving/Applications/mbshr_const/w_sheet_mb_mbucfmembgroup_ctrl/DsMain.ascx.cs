using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBUCFMEMBGROUPDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBGROUP;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void RetrieveMembGroup(string membgroup_code)
        {
            string sql = @"
                         select coop_id,
                                membgroup_code,
                                membgroup_control,
                                membgroup_desc,
                                membgroup_agentgrg,
                                addr_no,
                                addr_moo,
                                addr_soi,
                                addr_road,
                                addr_tambol,
                                addr_amphur,
                                trim(addr_province) as addr_province,
                                addr_phone,
                                addr_fax,
                                savedisk_type
                           from mbucfmembgroup
                          where (coop_id={0}) and (membgroup_code ={1})";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, membgroup_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);


        }
        public void DdDistrict(string province_code)
        {
            string sql = @"
              SELECT DISTRICT_CODE,   
                     PROVINCE_CODE,   
                     DISTRICT_DESC,   
                     POSTCODE  ,1 as sorter
                FROM MBUCFDISTRICT 
              where (PROVINCE_CODE={0}) 
            union
            select '','','','',0 from dual ORDER BY sorter,DISTRICT_DESC ASC,   
                     DISTRICT_CODE ASC  ";
            sql = WebUtil.SQLFormat(sql, province_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_amphur", "DISTRICT_DESC", "DISTRICT_CODE");
        }
        public void DdProvince()
        {
            string sql = @"
              SELECT PROVINCE_CODE,   
                     PROVINCE_DESC  ,1 as sorter
                FROM MBUCFPROVINCE 
            union 
            select '','',0 from dual order by sorter,PROVINCE_DESC ASC";
            //sql = WebUtil.SQLFormat(sql, province_code);
            //DataTable dt = WebUtil.Query(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "addr_province", "PROVINCE_DESC", "PROVINCE_CODE");
        }
    }
}