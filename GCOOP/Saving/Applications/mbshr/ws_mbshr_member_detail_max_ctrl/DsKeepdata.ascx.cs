using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl
{
    public partial class DsKeepdata : DataSourceRepeater
    {
        public DataSet1.DT_KEEPDATADataTable DATA { get; set; }

        public void InitDsKeepdata(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_KEEPDATA;
            this.EventItemChanged = "OnDsKeepdataItemChanged";
            this.EventClicked = "OnDsKeepdataClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsKeepdata");
            this.Button.Add("b_detail");
            this.Register();
        }

        public void RetrieveKeepdata(String ls_member_no)
        {
            String sql = @"  
                      SELECT KPMASTRECEIVE.MEMBER_NO,   
                             SUBSTR( KPMASTRECEIVE.RECV_PERIOD,1,4)||'/'||SUBSTR( KPMASTRECEIVE.RECV_PERIOD,5)as RECV_PERIOD,   
                             KPMASTRECEIVE.RECV_PERIOD as recvperiod,
                             KPMASTRECEIVE.REF_MEMBNO,   
                             KPMASTRECEIVE.RECEIPT_NO,   
                             KPMASTRECEIVE.RECEIPT_DATE,   
                             KPMASTRECEIVE.MEMBGROUP_CODE,   
                             KPMASTRECEIVE.KEEPING_STATUS,   
                             KPMASTRECEIVE.RECEIVE_AMT  
                        FROM KPMASTRECEIVE  
                       WHERE ( kpmastreceive.member_no = {0} )   ";

            sql = WebUtil.SQLFormat(sql, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}