using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_insurance_type_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.INSURENCETYPEDataTable DATA { get; set; }

        public void InitDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.INSURENCETYPE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");

            this.Register();
        }

        public void RetrieveDetail(string instype_code )
        {
            string sql = @"select 
insurencetype.instype_code , 
insurencetype.inscompany_name , 
insurencetype.max_inscost , 
insurencetype.min_inscost , 
insurencetype.instype_desc ,
insurencetype.fee_amt ,
insurencetype.insurencedoc_no , 
insurencetype.aproveins_code ,
insurencetype.start_date , 
insurencetype.end_date , 
insurencetype.paymentlevel_flag ,
insurencetype.period_payment ,
insurencetype.keeping_flag ,
insurencetype.min_age , 
insurencetype.max_age 

from insurencetype  
            where ( insurencetype.instype_code  = {0} )

	           ";

            sql = WebUtil.SQLFormat(sql, instype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void Ddloantype()
        {
            string sql = @"select loantype_code, 
                loantype_code +' - '+loantype_desc as display, 1 as sorter
                from lnloantype
                where coop_id = {0}
                union
                select '','',0 from dual order by sorter, loantype_code";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "display", "loantype_code");
        }

    }
}