using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_crenation_ctrl
{
    public partial class DsInsurance : DataSourceRepeater
    {
        public DataSet1.MBCREMATIONTHAIDataTable DATA { get; set; }

        public void InitDsInsurance(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBCREMATIONTHAI;
            this.EventItemChanged = "OnDsInsuranceItemChanged";
            this.EventClicked = "OnDsInsuranceClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsInsurance");
            this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveData(String ls_member_no)
        {
             String sql = @"                     
                    SELECT 
                    MBUCFCREMATIONTHAI.cmttype_code,
                    MBUCFCREMATIONTHAI.CMTTYPE_DESC,
                    cmtaccount_name,
                    cremation_amt,
                    APPLY_DATE,
                    CREMATION_DATE,
                    INS_AMT
                    FROM 
                    mbcremationthai ,MBUCFCREMATIONTHAI
                    WHERE
                    mbcremationthai.cmttype_code = MBUCFCREMATIONTHAI.cmttype_code and
                    (( mbcremationthai.member_no = {1}) AND  
                    ( mbcremationthai.coop_id = {0}) AND 
                    mbcremationthai.cmtclose_status = '1')    AND mbcremationthai.cmttype_code <> '99'
                    order  by mbcremationthai.cmttype_code asc";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdCmttypeCode()
        {
            string sql = @"SELECT CMTTYPE_CODE AS VALUE_CODE,  
                    CMTTYPE_DESC AS VALUE_DESC,
                    1 AS SORT                             
                    FROM MBUCFCREMATIONTHAI WHERE CMTTYPE_CODE<>'99'";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sort,value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "cmttype_code", "display", "value_code");
        }
    }
}