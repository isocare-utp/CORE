using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_req_mbnew_ctrl
{
    public partial class DsMoneytr : DataSourceRepeater
    {
        public DataSet1.MBREQAPPLMONEYTRDataTable DATA { get; set; }

        public void InitDsMoneytr(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBREQAPPLMONEYTR;
            this.EventItemChanged = "OnDsMoneytrItemChanged";
            this.EventClicked = "OnDsMoneytrClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMoneytr");
            this.Button.Add("b_del");
            this.Register();
        }
        public void RetrieveMoneytr(string docno)
        {
            String sql = @"select * from mbreqappl pl,mbreqapplmoneytr tr 
                            where (pl.appl_docno=tr.appl_docno) and 
                                  (pl.coop_id={0}) and 
                                  (pl.appl_docno={1})";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, docno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdMoneyTrType()
        {
            string sql = @"
            SELECT trtype_code,   
                 trtype_code || ' - ' || trtype_desc as trtype_desc 
            FROM MBUCFMONEYTRTYPE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "trtype_code", "trtype_desc", "trtype_code");
        }

        public void DdMoneyType()
        {
            string sql = @"
            SELECT moneytype_code,   
                 moneytype_code || ' - ' || moneytype_desc as moneytype_desc,   
                 SORT_ORDER , 1 as sorter
            FROM cmucfmoneytype
            union
            select '','', 0, 0 from dual order by sorter , SORT_ORDER ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "moneytype_code", "moneytype_desc", "moneytype_code");
        }

        public void DdBank()
        {
            string sql = @"
            SELECT bank_code,   
                bank_code || ' - ' || bank_desc as bank_desc, 1 as sorter 
            FROM cmucfbank 
            union
            select '','',0 from dual order by sorter, bank_code ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }
    }
}