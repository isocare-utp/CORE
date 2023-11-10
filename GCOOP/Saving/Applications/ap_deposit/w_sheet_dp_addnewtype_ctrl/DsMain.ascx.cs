using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_addnewtype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DPDEPTTYPEDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTTYPE;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChange";
            this.EventClicked = "OnDsMainClick";
            this.Register();
        }

        public void Retrieve(string deptType)
        {
            string sql = @"
                    SELECT   *     FROM     DPDEPTTYPE
                    WHERE  DPDEPTTYPE.COOP_ID = '" + state.SsCoopControl + @"'   and DPDEPTTYPE.depttype_code ='" + deptType + "'  ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        /// <summary>
        /// dropdown กลุ่มประเภทเงินฝาก
        /// </summary>
        public void DropDownDeptType()
        {
            string sql = @"
                    SELECT depttype_code , depttype_desc  , 1 as sorter     FROM     DPDEPTTYPE
                    WHERE  DPDEPTTYPE.COOP_ID = '" + state.SsCoopControl + @"'  
                    union 
                    select '','',0 from dual
                    ORDER BY sorter,depttype_code ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "depttype_group", "depttype_desc", "depttype_code");
        }

        /// <summary>
        ///  dropdown บุคคล
        /// </summary>
        /// <param name="persongrp_code">เลขโค้ดบุคคล</param>
        public void DropDownPersonGrp(String persongrp_code)
        {
            string sql = @"
                    SELECT PERSONGRP_CODE, GROUP_DESC 
                    FROM DPUCFACCTYPEGRP WHERE PERSONGRP_CODE = '" + persongrp_code + @"'
                    ORDER BY PERSONGRP_CODE ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "persongrp_code", "GROUP_DESC", "persongrp_code");
        }

        /// <summary>
        ///  dropdown กลุ่มเงินฝาก
        /// </summary>
        /// <param name="deptgrp_code">เลขโค้ดกลุ่มเงินฝาก</param>
        public void DropDownDeptGrp()
        {
            string sql = @"
        SELECT  DEPTGROUP_CODE ,DEPTGROUP_CODE || ' ' || DEPTGROUP_DESC   as DEPTGROUP_DESC
        FROM    DPUCFDEPTGROUP   
                    ORDER BY DEPTGROUP_CODE ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "deptgroup_code", "DEPTGROUP_DESC", "deptgroup_code");
        }

        /// <summary>
        /// dropdown รหัสคู่บัญชี
        /// </summary>
        /// <param name="acc_id">เลขโค้ดรหัสคู่บัญชี</param>
        public void DropDownAccID(String acc_id)
        {
            string sql = @"
        SELECT      ACCOUNT_ID, ACCOUNT_ID || ' ' || ACCOUNT_NAME as account_name
        FROM        ACCMASTER  
        WHERE       ACCOUNT_LEVEL > 3  and ACCOUNT_ID = '" + acc_id + @"'
        ORDER BY    ACCOUNT_ID ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "account_id", "account_name", "account_id");
        }

        /// <summary>
        /// dropdown กลุ่มลง acc
        /// </summary>
        /// <param name="acc_id"></param>
        public void DropDownAccGrp()
        {
            string sql = @"
        SELECT      DEPTTYPE_ACCGRP,   DEPTTYPE_ACCGRP || ' ' || DEPTTYPE_ACCDESC as depttype_accdesc
        FROM        DPUCFDEPTACCGROUP   
        ORDER BY    DEPTTYPE_ACCGRP ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "depttype_accgrp", "depttype_accdesc", "depttype_accgrp");
        }

    }
}