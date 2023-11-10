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
    public partial class DsColldet : DataSourceFormView
    {
        public DataSet1.LNLOANTYPE3DataTable DATA { get; set; }

        public void InitDsColldet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPE3;
            this.EventItemChanged = "OnDsColldetItemChanged";
            this.EventClicked = "OnDsColldetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsColldet");
            this.TableName = "LNLOANTYPE";
            this.Register();
        }

        public void Ddmangrtpermgrp()
        {
            string sql = @" 
                select
                        mangrtpermgrp_code, mangrtpermgrp_code+'  '+mangrtpermgrp_desc as display, 1 as sorter
                        from lngrpmangrtperm
                        union
                        select '','',0 from dual order by sorter, mangrtpermgrp_code

                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "mangrtpermgrp_code", "display", "mangrtpermgrp_code");

        }

        public void Ddmangrtpermgrpco()
        {
            string sql = @" 
                select
                        mangrtpermgrp_code, mangrtpermgrp_code+'  '+mangrtpermgrp_desc as display, 1 as sorter
                        from lngrpmangrtperm
                        union
                        select '','',0 from dual order by sorter, mangrtpermgrp_code

                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "mangrtpermgrpco_code", "display", "mangrtpermgrp_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"  SELECT COOP_ID,
                                 LOANTYPE_CODE,   
                                 CNTMANGRTNUM_FLAG,   
                                 CNTMANGRTVAL_FLAG,   
                                 MANGRTPERMGRP_CODE,   
                                 GRTNEED_FLAG,   
                                 USEMANGRT_STATUS,   
                                 CHKLOCKSHARE_FLAG,   
                                 LOCKSHARE_FLAG,   
                                 RETRYCOLLCHK_FLAG,   
                                 USEMANGRT_MAINMAXVALUE,   
                                 USEMANGRT_COMAXVALUE,   
                                 MANGRTPERMGRPCO_CODE  
                            FROM LNLOANTYPE
                               WHERE loantype_code = {0}   ";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}