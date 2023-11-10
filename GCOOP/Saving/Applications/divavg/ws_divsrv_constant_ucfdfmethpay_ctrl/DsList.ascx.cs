using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_constant_ucfdfmethpay_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.YRUCFDFMETHPAYDataTable DATA { get; set; }

        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.YRUCFDFMETHPAY;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"   SELECT YRUCFDFMETHPAY.COOP_ID,   
                                     YRUCFDFMETHPAY.SEQ_NO,   
                                     YRUCFDFMETHPAY.START_VALUE,   
                                     YRUCFDFMETHPAY.END_VALUE,   
                                     YRUCFDFMETHPAY.METHPAYTYPE_CODE,   
                                     YRUCFDFMETHPAY.EXPENSE_BANK,
                                     YRUCFDFMETHPAY.PAYREPLACE_FLAG  
                                FROM YRUCFDFMETHPAY ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }

        public void DdMethpaytype()
        {
            string sql = @"
                      SELECT YRUCFMETHPAY.COOP_ID,   
                             YRUCFMETHPAY.METHPAYTYPE_CODE,   
                             YRUCFMETHPAY.METHPAYTYPE_DESC,   
                             YRUCFMETHPAY.METHPAYTYPE_SORT,1 as sorter  
                        FROM YRUCFMETHPAY  
                       WHERE  ( yrucfmethpay.showlist_flag = 1 )    
                       union
                    select '','','',0,0 from dual order by sorter,METHPAYTYPE_CODE ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "methpaytype_code", "METHPAYTYPE_DESC", "METHPAYTYPE_CODE");
        }

        public void DdBank()
        {
            string sql = @"
                      SELECT CMUCFBANK.BANK_CODE,   
                             CMUCFBANK.BANK_DESC,   
                             CMUCFBANK.SETSORT,   
                             CMUCFBANK.BANK_SHORTNAME  ,1 as sorter
                        FROM CMUCFBANK      
                       union
                    select '','',0,'',0 from dual order by sorter,BANK_CODE ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_bank", "BANK_DESC", "BANK_CODE");
        }
    }
}