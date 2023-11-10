using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsMain :DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetrieveMain(String ls_cont_no)
        {
            String sql = @"  
                        select lncontmaster.loancontract_no,   
                         lncontmaster.member_no,   
                         lncontmaster.loantype_code,   
                         mbucfprename.prename_desc,   
                         mbmembmaster.memb_name,   
                         mbmembmaster.memb_surname  ,
				         lnloantype.loantype_code +' '+ lnloantype.prefix+' '+lnloantype.loantype_desc as loantype_desc
                        from lncontmaster,   
                         mbmembmaster,   
                         mbucfprename,
				         lnloantype  
                        where ( lncontmaster.member_no = mbmembmaster.member_no ) and  
                         ( lncontmaster.loantype_code = lnloantype.loantype_code ) and  
                         ( mbmembmaster.prename_code = mbucfprename.prename_code ) and  
                         ( lncontmaster.memcoop_id = mbmembmaster.coop_id ) and   
                         ( ( lncontmaster.loancontract_no = {0} ) )   ";

            sql = WebUtil.SQLFormat(sql, ls_cont_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdLoanType()
        {
            string sql = @" 
                 SELECT LOANTYPE_CODE,   
                        LOANTYPE_DESC,   
                        PREFIX  ,
                        LOANTYPE_CODE+' '+PREFIX+' '+LOANTYPE_DESC as display,1 as sorter
                FROM LNLOANTYPE  
            union
            select '','','','',0 from dual order by sorter,LOANTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "display", "LOANTYPE_CODE");

        }
    }
}