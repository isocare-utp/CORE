using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.w_dlg_mb_detail_keepdatadet_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.DT_DETAILDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAIL;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Register();
        }

        public void Retrieve(String ls_member_no, String ls_recvperiod)
        {
            String sql = @"  select  kpmastreceivedet.member_no ,
kpmastreceive.receipt_no ,
kpmastreceivedet.recv_period , 
kpmastreceivedet.keepitemtype_code ,
kpmastreceivedet.description ,
kpmastreceivedet.period ,
kpmastreceivedet.principal_payment ,
kpmastreceivedet.interest_payment ,
kpmastreceivedet.item_payment ,
kpmastreceivedet.loancontract_no ,
kpmastreceive.keeping_status ,
kpmastreceivedet.keepitem_status ,
kpmastreceivedet.ref_membno ,
kpmastreceive.member_no ,
kpucfkeepitemtype.sort_in_receive ,
kpmastreceivedet.item_balance 
,kpmastreceivedet.shrlontype_code 
,kpucfkeepitemtype.sign_flag 
from kpmastreceive ,
kpmastreceivedet ,
kpucfkeepitemtype
where  ( KPMASTRECEIVE.coop_id = KPMASTRECEIVEDET.coop_id ) and
( KPMASTRECEIVE.kpslip_no = KPMASTRECEIVEDET.kpslip_no ) and
( KPMASTRECEIVEDET.KEEPITEMTYPE_CODE = KPUCFKEEPITEMTYPE.KEEPITEMTYPE_CODE ) and
( ( kpmastreceive.member_no = {0} ) and
( kpmastreceive.recv_period = {1}) and
( kpmastreceivedet.keepitemtype_code <> 'ETN' ) )
order by kpmastreceivedet.member_no,kpmastreceivedet.recv_period,kpmastreceivedet.ref_membno,kpucfkeepitemtype.sort_in_receive,kpmastreceivedet.keepitemtype_code
";

            sql = WebUtil.SQLFormat(sql, ls_member_no, ls_recvperiod);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}