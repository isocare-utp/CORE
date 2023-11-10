using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SLSLIPPAYINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView3, this.DATA, "dsMain");
            this.Button.Add("b_memsearch");
            //this.Button.Add("b_contsearch");
            this.Register();
        }

        public void RetrieveMain(String member_no)
        {
            String sql = @"SELECT '        ' as slip_tdate,   
         '        ' as operate_tdate,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
         0 as crosscoop_flag,   
         SLSLIPPAYIN.COOP_ID,   
         SLSLIPPAYIN.PAYINSLIP_NO,   
         SLSLIPPAYIN.MEMCOOP_ID,   
         SLSLIPPAYIN.MEMBER_NO,   
         SLSLIPPAYIN.DOCUMENT_NO,   
         SLSLIPPAYIN.SLIPTYPE_CODE,   
         SLSLIPPAYIN.SLIP_DATE,   
         SLSLIPPAYIN.OPERATE_DATE,   
         SLSLIPPAYIN.PAYINORDER_NO,   
         SLSLIPPAYIN.SHARESTKBF_VALUE,   
         SLSLIPPAYIN.SHARESTK_VALUE,   
         SLSLIPPAYIN.INTACCUM_AMT,   
         SLSLIPPAYIN.MONEYTYPE_CODE,   
         SLSLIPPAYIN.EXPENSE_BANK,   
         SLSLIPPAYIN.EXPENSE_BRANCH,   
         SLSLIPPAYIN.EXPENSE_ACCID,   
         SLSLIPPAYIN.ACCID_FLAG,   
         SLSLIPPAYIN.TOFROM_ACCID,   
         SLSLIPPAYIN.REF_OPEDOCNO,   
         SLSLIPPAYIN.REF_NEWCONTNO,   
         SLSLIPPAYIN.REF_SHARETYPE,   
         SLSLIPPAYIN.REF_SYSTEM,   
         SLSLIPPAYIN.REF_SLIPNO,   
         SLSLIPPAYIN.REF_SLIPAMT,   
         SLSLIPPAYIN.SLIP_AMT,   
         SLSLIPPAYIN.SLIP_STATUS,   
         SLSLIPPAYIN.MEMBGROUP_CODE,   
         SLSLIPPAYIN.SUBGROUP_CODE,   
         SLSLIPPAYIN.ENTRY_ID,   
         SLSLIPPAYIN.ENTRY_DATE,   
         SLSLIPPAYIN.CANCEL_ID,   
         SLSLIPPAYIN.CANCEL_DATE,   
         SLSLIPPAYIN.POSTTOVC_FLAG,   
         SLSLIPPAYIN.VOUCHER_NO,   
         SLSLIPPAYIN.CANCELTOVC_FLAG,   
         SLSLIPPAYIN.CANCELVC_NO,   
         SLSLIPPAYIN.PAYINTRETURN_STATUS,   
         SLSLIPPAYIN.POST_TOFIN,   
         SLSLIPPAYIN.FORWORD_FLAG  
    FROM SLSLIPPAYIN,   
         MBMEMBMASTER,   
         MBUCFMEMBGROUP,   
         MBUCFPRENAME  
   WHERE ( SLSLIPPAYIN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( SLSLIPPAYIN.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
         ( ( SLSLIPPAYIN.COOP_ID  =  {0} ) and
           (MBMEMBMASTER.MEMBER_NO = {1})) ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}