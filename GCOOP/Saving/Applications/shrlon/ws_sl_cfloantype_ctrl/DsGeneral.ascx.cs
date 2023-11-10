using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_cfloantype_ctrl
{
    public partial class DsGeneral : DataSourceFormView
    {
        public DataSet1.LNLOANTYPEDataTable DATA { get; set; }

        public void InitDsGeneral(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNLOANTYPE;
            this.EventItemChanged = "OnDsGeneralItemChanged";
            this.EventClicked = "OnDsGeneralClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsGeneral");
            this.Button.Add("b_searchdoc");
            this.Register();
        }

        public void DdSalarybal()
        {
            string sql = @" 
                select salarybal_code,
                       salarybal_code+' '+salarybal_desc as display,
                       1 as sorter
                  from cmucfsalarybalance
                  union
                  select '','',0 from dual order by sorter, salarybal_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "salarybal_code", "display", "salarybal_code");

        }

        public void DdLoanobjective(string loantype_code)
        {
            string sql = @" 
                select loanobjective_code,
                        loanobjective_code+' '+loanobjective_desc as display,
                        1 as sorter
                        from lnucfloanobjective
                        where loantype_code = {1} and coop_id = {0}
                        union
                        select '','',0 from dual order by sorter, loanobjective_code
                ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, loantype_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "defaultobj_code", "display", "loanobjective_code");


        }

        public void Retrieve(string loantype_code)
        {
            String sqllnloantype = @"select coop_id,
                                        loantype_code,
                                        loantype_desc,
                                        loangroup_code,
                                        prefix, member_time,
                                        payspec_method,
                                        document_code,
                                        approve_flag,
                                        payround_type,
                                        payround_factor,
                                        reqround_type,
                                        reqround_factor,
                                        contalert_time,
                                        counttimecont_type,
                                        defaultpay_type,
                                        od_flag,
                                        defaultobj_code,
                                        salarybal_status,
                                        salarybal_code,
                                        contract_time,
                                        member_type,
                                        intcertificate_status,
                                        contnosplitbranch_flag,
                                        pxaftermthkeep_type
                                        from lnloantype
                                   WHERE loantype_code = {0}   ";
            sqllnloantype = WebUtil.SQLFormat(sqllnloantype, loantype_code);
            DataTable dtlnloantype = WebUtil.Query(sqllnloantype);

            this.ImportData(dtlnloantype);
        }
    }
}