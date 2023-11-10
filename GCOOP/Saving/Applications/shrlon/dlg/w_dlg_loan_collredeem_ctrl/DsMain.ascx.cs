using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg.w_dlg_loan_collredeem_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DATAMASTERDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DATAMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            //this.Button.Add("b_contsearch");
            this.Register();
        }

        public void RetrieveMemberName(String member_no)
        {
            string sql = @" SELECT '        ' as collmast_tdate,   
            '        ' as expire_tdate,   
            MBMEMBMASTER.MEMBER_NO,
            MBUCFPRENAME.PRENAME_DESC,   
            MBUCFPRENAME.PRENAME_SHORT,   
            MBMEMBMASTER.MEMB_NAME,   
            {0} as COOP_ID,   
            'AUTO' as COLLMAST_NO,   
            ' ' as COLLMAST_DESC,   
            ' ' as COLLMAST_REFNO,   
            ' ' as COLLMASTTYPE_CODE,   
            ' ' as MEMBRANCH_ID,   
            ' ' as MEMBER_NO,   
            0 as COLLREAL_PRICE,   
            0 as COLLMAST_PRICE,   
            '        ' as COLLMAST_DATE,   
            '        ' as EXPIRE_DATE,   
            0 as REDEEM_FLAG,   
            '        ' as REDEEM_DATE,   
            ' ' as ASSETTYPE_CODE,   
            ' ' as BUILDINGTYPE_CODE,   
            ' ' as REMARK,   
            ' ' as ENTRY_ID,   
            '        ' as ENTRY_DATE,   
            ' ' as ENTRY_BYBRANCHID 
            FROM LNCOLLMASTER,   
            MBMEMBMASTER,   
            MBUCFPRENAME  
            WHERE  
            ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
            ( ( MBMEMBMASTER.COOP_ID = {0} ) AND  
            ( MBMEMBMASTER.MEMBER_NO = {1} ) )  ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        
        public void DdCollmasttypeCode()
        {
            string sql = @"                   
            SELECT COLLMASTTYPE_CODE, COLLMASTTYPE_DESC, 1 as sorter  
            FROM LNUCFCOLLMASTTYPE   COLLMASTTYPE_CODE
            union
            select ' ',' ',0 from dual order by sorter, COLLMASTTYPE_CODE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_code", "COLLMASTTYPE_DESC", "COLLMASTTYPE_CODE");
        }
    }
}