using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;



namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.ASSREQHISTORYDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQHISTORY;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void RetrieveHistory(string as_memno, string as_asscode)
        {
            string sql = @"
                        select tp.assistreq_docno,tp.itemcode, tp.asscontract_no, tp.assist_date, tp.assisttype_code, tp.approve_amt , case when tp.itemcode='CON' then 'จ่ายแล้ว' else 'รอจ่าย' end as status_desc 
                        from	(
		                        select
				                        'CON' as itemcode,
				                        mas.asscontract_no as asscontract_no,
				                        mas.approve_date as assist_date,
				                        mas.assisttype_code + ' ' + ucftype.assisttype_desc + ' : ' + paytype.assistpay_desc as assisttype_code,
				                        mas.approve_amt as approve_amt,
                                        mas.assistreq_docno as assistreq_docno
		                        from	asscontmaster mas
				                        join assucfassisttype ucftype on mas.assisttype_code = ucftype.assisttype_code
				                        join assucfassisttypepay paytype on mas.assisttype_code = paytype.assisttype_code and mas.assistpay_code = paytype.assistpay_code
		                        where mas.asscont_status <> -9
                                and mas.coop_id = {0}
                                and mas.member_no = {1}
                                and mas.assisttype_code = {2}
		                        union
		                        select
				                        'REQ' as itemcode,
				                        rtrim(ltrim(req.assist_docno)) as asscontract_no,
				                        req.req_date as assist_date,
				                        req.assisttype_code + ' ' + ucftype.assisttype_desc + ' : ' + paytype.assistpay_desc as assisttype_code,
				                        req.assistnet_amt as approve_amt,
                                        rtrim(ltrim(req.assist_docno)) as assistreq_docno
		                        from	assreqmaster req
				                        join assucfassisttype ucftype on req.assisttype_code = ucftype.assisttype_code
				                        join assucfassisttypepay paytype on req.assisttype_code = paytype.assisttype_code and req.assistpay_code = paytype.assistpay_code
		                        where req.req_status = 8
                                and req.coop_id = {0}
                                and req.member_no = {1}
                                and req.assisttype_code = {2} ) as tp
                        order by  tp.itemcode , tp.asscontract_no ,  tp.assistreq_docno , tp.assist_date desc";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_memno, as_asscode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            
            /***/
        }
    }
}