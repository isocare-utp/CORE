using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class DsDetailEtc : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDETDataTable DATA { get; set; }
        public void InitDsDetailEtc(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET;
            this.EventItemChanged = "OnDsDetailEtcItemChanged";
            this.EventClicked = "OnDsDetailEtcClicked";
            this.InitDataSource(pw, Repeater3, this.DATA, "dsDetailEtc");
            // this.Button.Add("b_memsearch");
            this.Button.Add("b_del");
            this.Register();
        }
        public void RetrieveDetailEtc(string payinslip_no)
        {
            String sql = @"  SELECT SLSLIPPAYIN.COOP_ID,   
                             SLSLIPPAYIN.PAYINSLIP_NO,   
                             SLSLIPPAYIN.MEMBER_NO,   
                             SLSLIPPAYINDET.OPERATE_FLAG,   
                             SLSLIPPAYINDET.SLIPITEMTYPE_CODE,   
                             SLSLIPPAYINDET.SLIPITEM_DESC,   
                             SLSLIPPAYINDET.ITEM_PAYAMT,   
                             SLSLIPPAYINDET.PRNCALINT_AMT  
                        FROM SLSLIPPAYIN,   
                             SLSLIPPAYINDET  
                       WHERE ( SLSLIPPAYINDET.COOP_ID = SLSLIPPAYIN.COOP_ID ) and  
                             ( SLSLIPPAYINDET.PAYINSLIP_NO = SLSLIPPAYIN.PAYINSLIP_NO ) and  
                              (SLSLIPPAYINDET.SLIPITEMTYPE_CODE NOT IN ('LON','SHR') ) AND
                             ( ( SLSLIPPAYIN.COOP_ID = {0} ) AND  
                             ( SLSLIPPAYIN.PAYINSLIP_NO = {1} ) )    
";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, payinslip_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdLoanType()
        {
            string sql = @" 
                SELECT slipitemtype_code,
	                ( slipitemtype_code||' : '|| slipitemtype_desc )   as slipitemtype_desc, 1 as sorter
                FROM slucfslipitemtype  
                WHERE ( itemslipetc_flag = 1 )
                union
                select '','', 0 from dual order by sorter, slipitemtype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "SLIPITEMTYPE_CODE", "SLIPITEMTYPE_DESC", "SLIPITEMTYPE_CODE");

        }

    }
}