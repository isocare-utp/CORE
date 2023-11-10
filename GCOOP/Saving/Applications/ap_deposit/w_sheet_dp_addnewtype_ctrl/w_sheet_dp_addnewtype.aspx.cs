using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.ap_deposit.w_sheet_dp_addnewtype_ctrl
{
    public partial class w_sheet_dp_addnewtype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostDeptType { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DropDownDeptType();
                dsList.Retrieve();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostDeptType")
            {
                String person = "", acc_id = "";
                String sql = @"
                SELECT   persongrp_code  , account_id 
                FROM    DPDEPTTYPE WHERE depttype_code = '" + HdDeptType.Value + "' ";

                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    person = dt.GetString("persongrp_code");
                    acc_id = dt.GetString("account_id");
                }

                dsMain.Retrieve(HdDeptType.Value);
                dsMain.DropDownPersonGrp(person);
                dsMain.DropDownDeptGrp();
                dsMain.DropDownAccID(acc_id);
                dsMain.DropDownAccGrp();
                dsMain.DropDownDeptType();
                dsMain.DATA[0].DEPTTYPE_CODE = HdDeptType.Value;
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}