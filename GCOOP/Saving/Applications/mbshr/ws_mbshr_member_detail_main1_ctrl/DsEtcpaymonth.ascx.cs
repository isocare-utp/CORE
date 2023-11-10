using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
{
    public partial class DsEtcpaymonth : DataSourceRepeater
    {
        public DataSet1.DT_ETCPAYMONTHDataTable DATA { get; set; }

        public void InitDsEtepaymonth(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_ETCPAYMONTH;
            this.EventItemChanged = "OnDsEtcpaymonthItemChanged";
            this.EventClicked = "OnDsEtcpaymonthClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsEtcpaymonth");
            this.Register();
        }

        public void RetrieveEtepaymonth(String ls_member_no, String ls_recvpd)
        {
            String sql = @"select kptempreceivedet.member_no,   
                kptempreceive.receipt_no,   
                kptempreceivedet.recv_period,  
                kptempreceivedet.keepitemtype_code,   
                kptempreceivedet.description,   
                kptempreceivedet.period,   
                kptempreceivedet.principal_payment,   
                kptempreceivedet.interest_payment,   
                kptempreceivedet.item_payment,   
                kptempreceivedet.loancontract_no,   
                kptempreceive.keeping_status,   
                kptempreceivedet.keepitem_status,   
                kptempreceivedet.ref_membno,   
                kptempreceive.member_no,   
                kpucfkeepitemtype.sort_in_receive,   
                kptempreceivedet.item_balance,   
                kptempreceivedet.shrlontype_code,   
                kpucfkeepitemtype.sign_flag  
                from kptempreceive,   
                kptempreceivedet,   
                kpucfkeepitemtype  
                where ( kptempreceive.coop_id = kptempreceivedet.coop_id ) 
                and ( kptempreceive.kpslip_no = kptempreceivedet.kpslip_no )   
                and ( kptempreceive.coop_id = kpucfkeepitemtype.coop_id )   
                and ( kptempreceivedet.keepitemtype_code = kpucfkeepitemtype.keepitemtype_code )   
                and ( ( kptempreceive.coop_id = {0} )
                and ( kptempreceive.recv_period = {1} )
                and ( kptempreceive.member_no = {2} )   
                and ( kptempreceivedet.keepitemtype_code <> 'ETN' ) ) 
                order by kptempreceive.kpslip_no, kptempreceivedet.seq_no";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_recvpd, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public TextBox TContract
        {
            get { return this.cp_sum_principal_payment; }
        }
        public TextBox TContract2
        {
            get { return this.cp_sum_interest_payment; }
        }
        public TextBox TContract3
        {
            get { return this.cp_sum_item_payment; }
        }
        public TextBox TContract4
        {
            get { return this.cp_sum_balance; }
        }
    }
}