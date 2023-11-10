using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_insurfire_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_memsearch");
           // this.Button.Add("b_contsearch");
            this.Register();
        }

        public void RetrieveMembNo(string memberNO)
        {
            string sql = @"
            SELECT 
                MBMEMBMASTER.MEMBER_NO,
                MBMEMBMASTER.PRENAME_CODE,
                MBMEMBMASTER.MEMB_NAME,
                MBMEMBMASTER.MEMB_SURNAME,
                MBUCFPRENAME.PRENAME_DESC,
                MBUCFPRENAME.PRENAME_DESC || MBMEMBMASTER.MEMB_NAME || ' ' || MBMEMBMASTER.MEMB_SURNAME as NAME,
                MBMEMBMASTER.MEMBTYPE_CODE,
                MBMEMBMASTER.MEMBER_STATUS,
                MBUCFMEMBTYPE.MEMBTYPE_DESC,
                MBUCFMEMBGROUP.MEMBGROUP_DESC,
                case when MBMEMBMASTER.MEMBER_STATUS = 1 then 'ลาออก' 
					 when MBMEMBMASTER.MEMBER_STATUS = 0 then 'ปกติ'
					 else 'ยกเลิก' end as status,
                1 as sorter
            FROM 
	            MBMEMBMASTER,
	            MBUCFPRENAME,	
	            MBUCFMEMBTYPE,
                MBUCFMEMBGROUP
            WHERE  
	            ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE )
	            AND ( MBMEMBMASTER.MEMBTYPE_CODE = MBUCFMEMBTYPE.MEMBTYPE_CODE )
                AND ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE )
	            AND ( MEMBER_NO = '" + memberNO + "' )"
            ;
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
       
        }
    }
}