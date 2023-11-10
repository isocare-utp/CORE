using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_cfloanconstant_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNLOANCONSTANTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANCONSTANT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve()
        {
            String sql = @"SELECT COOP_NO, MAXALLLOAN_AMOUNT, DAYINYEAR, FORMATYEAR_TYPE, FORMATMONTH_TYPE, CONTRACT_FORMAT, GRTRIGHT_CONTFLAG, GRTRIGHT_MEMFLAG, GRTRIGHT_CONTRACT, GRTRIGHT_MEMBER, CALINT_FUTURE, KPPROCMRT_TYPE, FIXPAYCAL_TYPE,  RDINTSATANG_TYPE, RDINTSATANG_TABCODE, RDINTSATANGSUM_TYPE, RDINTDEC_TYPE, INTDATEVIEW_TYPE, INTDATEINC_FIRSTRCV, INTDATEINC_LASTPAY, FIXPAYINTOVERFST_TYPE, FIXPAYINTOVERFSTPRN_TYPE, FIXPAYINTOVERNXT_TYPE, FIXPAYINTOVERNXTPRN_TYPE, GRTMEMCO_CONTFLAG, GRTMEMCO_MEMFLAG, GRTMEMCO_CONTRACT, GRTMEMCO_MEMBER, LNOVERRETRY_SHRFORMAT, LNOVERRETRY_SHRRATIO, LNOVERRETRY_SHRPERC, LNOVERRETRY_CMFORMAT, LNOVERRETRY_CMRATIO, LNOVERRETRY_CMPERC, COOP_ID, CALLOANDIV_DATE FROM LNLOANCONSTANT";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


        public void DDRdSatangTabCode() {
            string sql = "SELECT LNCFROUNDINGFACTOR.FACTOR_CODE, LNCFROUNDINGFACTOR.FACTOR_NAME  FROM LNCFROUNDINGFACTOR ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "rdintsatang_tabcode", "FACTOR_NAME", "FACTOR_CODE");
        }
        
    }
}