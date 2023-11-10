using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_pea_ctrl
{
    public partial class DsMain1 : DataSourceFormView
    {
        public DataSet1.LNREQLOANDataTable DATA { get; set; }
        public string IsShow = "visible";

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQLOAN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");

            this.Button.Add("b_loan");
            this.Button.Add("b_show");
            //this.Button.Add("b_loop_save");

            this.Button.Add("b_receipt_pay");
            this.Button.Add("b_receipt");
            //this.Button.Add("b_print");

            this.Register();
        }


        public void TypeLoan()
        {
            string sql = @"
             SELECT DISTINCT LNLOANTYPE.LOANTYPE_CODE,
                    LNLOANTYPE.LOANTYPE_CODE ||' ('|| LNLOANTYPE.PREFIX ||') '|| LNLOANTYPE.LOANTYPE_DESC as texttype,
                    LNLOANTYPE.COOP_ID,
                    LNLOANMBTYPE.MEMBTYPE_CODE,
                    LNLOANTYPE.LOANPERMGRP_CODE,
                    LNLOANTYPE.LOANGROUP_CODE
             FROM LNLOANTYPE,
                  LNLOANMBTYPE
             WHERE ( LNLOANTYPE.COOP_ID = LNLOANMBTYPE.COOP_ID ) and
                   ( LNLOANTYPE.LOANTYPE_CODE = LNLOANMBTYPE.LOANTYPE_CODE ) and
                   ( ( LNLOANTYPE.USELNREQ_FLAG = 1 ) )
             union
            select ' ',' ',' ',' ', ' ' ,' 'from dual
                    ORDER BY LOANTYPE_CODE ASC
                ";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "loantype_code", "texttype", "LOANTYPE_CODE");

        }
        public void memgroup()
        {
            string sql = @"
             SELECT MEMBGROUP_CODE,
                    MEMBGROUP_DESC 
                 
             FROM MBUCFMEMBGROUP 
            union
            select ' ',' 'from dual ORDER BY MEMBGROUP_CODE ASC
";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MEMBGROUP_DESC", "MEMBGROUP_DESC", "MEMBGROUP_CODE");

        }

        public void RetriveEntryid()
        {
            string sql = @"SELECT USER_NAME,   
                FULL_NAME,   
                USER_TYPE,   
                USER_NAME || ' - ' || FULL_NAME  as use_fullname,
                1 as sorter
            FROM AMSECUSERS  
            WHERE ( user_type = 1 ) AND  
                ( full_name is not null )
            union
            select '','', 0 ,'', 0 from dual order by sorter, USER_NAME";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "entry_id", "use_fullname", "USER_NAME");
        }
    }
}