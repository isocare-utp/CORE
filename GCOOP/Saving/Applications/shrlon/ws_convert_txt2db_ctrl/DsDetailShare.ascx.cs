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
    public partial class DsDetailShare : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDETDataTable DATA { get; set; }
        public void InitDsDetailShare(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYINDET;
            this.EventItemChanged = "OnDsDetailShareItemChanged";
            this.EventClicked = "OnDsDetailShareClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetailShare");
            // this.Button.Add("b_memsearch");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void RetrieveDetailLoan(string payinslip_no)
        {
            String sql = @"  SELECT SLSLIPPAYIN.COOP_ID,   
                             SLSLIPPAYIN.PAYINSLIP_NO,   
                             SLSLIPPAYIN.MEMBER_NO,   
                             SLSLIPPAYINDET.OPERATE_FLAG,   
                             SLSLIPPAYINDET.SLIPITEMTYPE_CODE,   
                             SLSLIPPAYINDET.SLIPITEM_DESC,   
                             SLSLIPPAYINDET.PERIODCOUNT_FLAG,   
                             SLSLIPPAYINDET.PERIOD,   
                             SLSLIPPAYINDET.BFSHRCONT_BALAMT,   
                             SLSLIPPAYINDET.ITEM_PAYAMT,   
                             SLSLIPPAYINDET.ITEM_BALANCE  
                        FROM SLSLIPPAYIN,   
                             SLSLIPPAYINDET  
                       WHERE ( SLSLIPPAYINDET.COOP_ID = SLSLIPPAYIN.COOP_ID ) and  
                             ( SLSLIPPAYINDET.PAYINSLIP_NO = SLSLIPPAYIN.PAYINSLIP_NO ) and  
                             (SLSLIPPAYINDET.SLIPITEMTYPE_CODE = 'SHR') AND
                             ( ( SLSLIPPAYIN.COOP_ID = {0} ) AND  
                             ( SLSLIPPAYIN.PAYINSLIP_NO = {1} ) )    ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, payinslip_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}