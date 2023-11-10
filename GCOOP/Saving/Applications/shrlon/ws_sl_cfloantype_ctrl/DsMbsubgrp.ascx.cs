using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsMbsubgrp : DataSourceRepeater
    {
        public DataSet1.MBUCFMEMBTYPEDataTable DATA { get; set; }

        public void InitDsMbsubgrp(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFMEMBTYPE;
            this.EventItemChanged = "OnDsMbsubgrpItemChanged";
            this.EventClicked = "OnDsMbsubgrpClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMbsubgrp");
            this.Register();
        }

        //        public void DdGrploanpermiss()
        //        {
        //            string sql = @" 
        //                select
        //                        membtype_code, membtype_code||'  '||membtype_desc as display, 1 as sorter
        //                        from mbucfmembtype
        //                        union
        //                        select '','',0 from dual order by sorter, membtype_code
        //                ";
        //            DataTable dt = WebUtil.Query(sql);
        //            this.DropDownDataBind(dt, "membtype_code", "display", "membtype_code");

        //        }
        public void RetrieveMembtype()
        {
            String sql = @"  SELECT  MEMBTYPE_CODE, MEMBTYPE_DESC, COOP_ID FROM mbucfmembtype order by MEMBTYPE_CODE  ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}