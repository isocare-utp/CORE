using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsBonus : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsBonus(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsBonusItemChanged";
            this.EventClicked = "OnDsBonusClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsBonus");
            this.Register();
        }

        public void Retrieve(String as_reqno)
        {
            string sql = @" select distinct mt.assist_docno,
                            mg.invt_id as bonus_type,
                            mg.unit_code as bonus_unit,
                            mg.methpay_rcv as bonus_methpay,
                            mt.dis_addr,
                            mt.req_date as check_date,
                            mt.assistpay_code
                            from assreqmaster mt
                            join ASSREQMASTERGIFT mg on  mt.assist_docno=mg.assist_docno
                            where mt.coop_id={0} and mt.assist_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_reqno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdAsspaytype(String assisttype_code, ref String ls_minpaytype)
        {
            string sql = @"select
		                        assistpay_code,
		                        assistpay_desc
	                        from assucfassisttypepay
	                        where coop_id = {0} and assisttype_code = {1}
	                        order by assistpay_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, assisttype_code);
            DataTable dt = WebUtil.Query(sql);
            ls_minpaytype = dt.Rows[0].Field<string>("assistpay_code");
            this.DropDownDataBind(dt, "assistpay_code", "assistpay_desc", "assistpay_code");
        }

        public void RetrieveGiftype(Decimal ldc_year, ref string ls_invcode, ref string ls_unitcode, ref string lsinvt_id, ref Int32 rowscount)
        {
            string sql = @"select   dt.invttype_code as invttype_code ,
                                    dm.invt_id as invt_id,
                                    dm.invt_name as inv_dispaly ,
                                    ds.invtsubtype_code ,
                                    ds.invtgrp_code ,
                                    ds.invtsubtype_desc ,
                                    ds.unit_code as unit_code
                          from diucfinvttype dt, diucfinvtsubtype ds , diinvtmaster dm
                          where dt.invtgrp_code = ds.invtgrp_code and
                                dt.invttype_code = ds.invttype_code and
                                dm.invtgrp_code = dt.invtgrp_code and
                                dm.invttype_code = dt.invttype_code and
                                dm.invtsubtype_code=ds.invtsubtype_code  and 
                                dt.invtgrp_code = '03'  and dt.invtyear_acc = {0} 
                                order by ds.invtgrp_code,dt.invttype_code,dm.invt_id";

            sql = WebUtil.SQLFormat(sql, ldc_year + 543);
            DataTable dt = WebUtil.Query(sql);
            if (dt.Rows.Count > 0)
            {
                ls_invcode = dt.Rows[0].Field<string>("invttype_code");
                ls_unitcode = dt.Rows[0].Field<string>("unit_code");
                lsinvt_id = dt.Rows[0].Field<string>("invt_id");
            }
            rowscount = dt.Rows.Count;

            this.DropDownDataBind(dt, "bonus_type", "inv_dispaly", "invt_id");
        }

        public void RetrieveGifUnit(string ls_unitcode, ref string ls_minunitcode)
        {
            string sql = @"select unit_code , unit_desc  from diucfunit where unit_code = {0}";

            sql = WebUtil.SQLFormat(sql, ls_unitcode);
            DataTable dt = WebUtil.Query(sql);
            ls_minunitcode = dt.Rows[0].Field<string>("unit_code");
            this.DropDownDataBind(dt, "bonus_unit", "unit_desc", "unit_code");
        }
    }
}