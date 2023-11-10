using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications._global.w_dlg_sl_detail_contract_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ContractMainDataTable DATA { get; set; }


        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ContractMain;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";

            this.Register();
        }

        public void RetrieveMembNo(String member_no, string loancontract_no)
        {
            string sql = @"
        SELECT LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.LOANTYPE_CODE,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME ,
 LNLOANTYPE.LOANTYPE_CODE,   
         LNLOANTYPE.LOANTYPE_DESC,   
         LNLOANTYPE.PREFIX
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,
         LNLOANTYPE
   WHERE ( LNCONTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.MEMCOOP_ID = MBMEMBMASTER.COOP_ID ) and  
        ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( ( trim(lncontmaster.loancontract_no) = '" + loancontract_no.Trim() + @"' ) ) and
            ( (  MBMEMBMASTER.MEMBER_NO = '" + member_no + "' ) )  ";

        
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}
