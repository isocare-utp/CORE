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
    public partial class DsRightdet : DataSourceFormView
    {
        public DataSet1.LNLOANTYPE1DataTable DATA { get; set; }

        public void InitDsRightdet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPE1;
            this.EventItemChanged = "OnDsRightdetItemChanged";
            this.EventClicked = "OnDsRightdetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsRightdet");
            this.TableName = "LNLOANTYPE";
            this.Register();

        }

        public void DdGrploanpermiss()
        {
            string sql = @" 
                select loanpermgrp_code,
                        loanpermgrp_code+' '+loanpermgrp_desc as display,
                         1 as sorter
                        from lngrploanpermiss
                        union
                        select '','',0 from dual order by sorter, loanpermgrp_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loanpermgrp_code", "display", "loanpermgrp_code");

        }

        public void Retrieve(string loantype_code)
        {
            String sql = @"select loantype_code,
                                loanpermgrp_code,
                                maxloan_amt,
                                loanright_type,
                                customtime_type,
                                showright_flag,
                                lngrpcutright_flag,
                                notmoreshare_flag,
                                lngrpkeepsum_flag,
                                coop_id ,
                                collmastprice_type 
                            from lnloantype
                            where loantype_code = {0}   ";
            sql = WebUtil.SQLFormat(sql, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}