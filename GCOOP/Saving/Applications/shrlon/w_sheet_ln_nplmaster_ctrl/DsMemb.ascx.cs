using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl
{
    public partial class DsMemb : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMemb");
            this.EventItemChanged = "OnDsMembItemChanged";
            this.EventClicked = "OnDsMembClicked";
            this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(String memberNo)
        {
            string sql = @"
                SELECT 
                  MBMEMBMASTER.MEMBER_NO,   
                  MBMEMBMASTER.MEMB_NAME,   
                  MBMEMBMASTER.MEMB_SURNAME,   
                  MBMEMBMASTER.MEMBGROUP_CODE,   
                  MBMEMBMASTER.ADDR_NO,   
                  MBMEMBMASTER.ADDR_MOO,   
                  MBMEMBMASTER.ADDR_SOI,   
                  MBMEMBMASTER.ADDR_VILLAGE,
                  MBMEMBMASTER.ADDR_ROAD,
                  MBMEMBMASTER.TAMBOL_CODE,
                  MBMEMBMASTER.AMPHUR_CODE,
                  MBMEMBMASTER.PROVINCE_CODE,
                  MBMEMBMASTER.ADDR_POSTCODE,
                  MBMEMBMASTER.ADDR_PHONE,
                  MBMEMBMASTER.ADDR_MOBILEPHONE,
                  MBMEMBMASTER.CARD_PERSON,
                  MBUCFPROVINCE.PROVINCE_DESC,
                  MBUCFPRENAME.PRENAME_DESC,
                  MBUCFTAMBOL.TAMBOL_DESC,
                  MBUCFDISTRICT.DISTRICT_DESC
                FROM 
                  MBMEMBMASTER,   
                  MBUCFPROVINCE,
                  MBUCFTAMBOL,
                  MBUCFDISTRICT,
                  MBUCFPRENAME  
                WHERE
                  MBMEMBMASTER.PROVINCE_CODE = MBUCFPROVINCE.PROVINCE_CODE and
                  MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE and
                  MBMEMBMASTER.TAMBOL_CODE = MBUCFTAMBOL.TAMBOL_CODE(+) AND
                  MBMEMBMASTER.AMPHUR_CODE = MBUCFDISTRICT.DISTRICT_CODE(+) AND
                  MBMEMBMASTER.COOP_ID = {0} AND
                  MBMEMBMASTER.MEMBER_NO = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}