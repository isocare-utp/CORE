using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl
{
    public partial class DsRecieveList : DataSourceRepeater
    {
        public DataSet1.KPTEMPRECEIVEDETDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.KPTEMPRECEIVEDET;
            this.EventItemChanged = "OnDsRecieveListItemChanged";
            this.EventClicked = "OnDsRecieveListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsRecieveList");
            this.Register();
        }

        /// <summary>
        /// ดึงรายละเอียด slip
        /// </summary>
        /// <param name="member_no"></param>
        public void Retrieve(String member_no, String recv_period, String kpslip_no)
        {
            string sql = @" SELECT  kpdet.kpslip_no, kpdet.seq_no,  kpdet.keepitemtype_code, 
                                    isnull(kpdet.description,kpdet.loancontract_no) as description , 
                                    kpdet.period, kpdet.principal_payment, kpdet.interest_payment,
                                    kpdet.item_payment, kpdet.principal_balance, kpdet.keepitem_status, 
                                    kpdet.posting_flag, kpdet.bfprinbalance_amt
                            FROM    kptempreceivedet kpdet
                            where   kpdet.coop_id ='" + state.SsCoopControl + @"' and 
                                    kpdet.member_no='" + member_no + @"' and 
                                    kpdet.recv_period = '" + recv_period + @"' and 
                                    kpdet.kpslip_no = '" + kpslip_no + "' ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}