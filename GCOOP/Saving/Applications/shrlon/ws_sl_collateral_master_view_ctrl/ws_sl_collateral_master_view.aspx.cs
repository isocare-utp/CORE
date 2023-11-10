using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Drawing;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl
{
    public partial class ws_sl_collateral_master_view : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostPanel { get; set; }
        [JsPostBack]
        public String PostCollmastNo { get; set; }
        [JsPostBack]
        public String PostInsertRowBdingdet { get; set; }
        [JsPostBack]
        public String PostDeleteRowBdingdet { get; set; }
        [JsPostBack]
        public String PostInsertRowCollprop { get; set; }
        [JsPostBack]
        public String PostDeleteRowCollprop { get; set; }
        [JsPostBack]
        public String PostCollNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsLanduse.InitDsLanduse(this);
            dsLand.InitDsLand(this);
            dsCondo.InitDsCondo(this);
            dsBdingdet.InitDsBdingdet(this);
            dsBding.InitDsBding(this);
            dsCollprop.InitDsCollprop(this);
            dsRate.InitDsRate(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = true;
                Panel5.Visible = true;
                dsMain.DATA[0].COLLMASTTYPE_GRP = "01";
                dsRate.DATA[0].txt1 = "ราคากรมที่ดิน";
            }

            dsMain.DdCollmasttypegrp();
            dsBding.DdBdtype();
            dsBdingdet.Ddfloor();
            dsLand.DdCollmasttype();
            dsBding.DdCollmasttype();
        }

        public void Collmastgrp_Changed()
        {
            string type_grp = dsMain.DATA[0].COLLMASTTYPE_GRP;

            switch (type_grp)
            {
                case "01":
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = true;
                    Panel5.Visible = true;
                    dsRate.DATA[0].txt1 = "ราคากรมที่ดิน";
                    break;
                case "02":
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    dsRate.DATA[0].txt1 = "ราคารวม";
                    break;
                case "03":
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    Panel4.Visible = true;
                    Panel5.Visible = true;
                    dsRate.DATA[0].txt1 = "ราคารวม";
                    break;
                default:
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    break;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostPanel)
            {
                Collmastgrp_Changed();
            }
            else if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(member_no);
                dsLand.ResetRow();
                dsLanduse.ResetRow();
                dsRate.ResetRow();
                dsCondo.ResetRow();
                dsCollprop.ResetRow();
                dsBdingdet.ResetRow();
                dsBding.ResetRow();
                dsMain.DdCollmasttypegrp();
                dsBding.DdBdtype();
                dsBdingdet.Ddfloor();
                dsLand.DdCollmasttype();
                dsBding.DdCollmasttype();
                dsList.Retrieve(member_no);
                Collmastgrp_Changed();
            }
            else if (eventArg == PostCollmastNo)
            {

                string collmast_no = dsMain.DATA[0].COLLMAST_NO;
                dsMain.Retrieve(collmast_no);
                dsLanduse.Retrieve(collmast_no);
                dsLand.Retrieve(collmast_no);
                dsCondo.Retrieve(collmast_no);
                dsBdingdet.Retrieve(collmast_no);
                dsBding.Retrieve(collmast_no);
                dsCollprop.Retrieve(collmast_no);
                dsRate.Retrieve(collmast_no);
                Collmastgrp_Changed();

                dsMain.FindDropDownList(0, "collmasttype_grp").Enabled = false;
            }
            else if (eventArg == PostInsertRowBdingdet)
            {
                dsBdingdet.InsertLastRow();
                int r = dsBdingdet.RowCount - 1;
                dsBdingdet.Ddfloor();
            }
            else if (eventArg == PostDeleteRowBdingdet)
            {
                int r = dsBdingdet.GetRowFocus();
                dsBdingdet.DeleteRow(r);
            }
            else if (eventArg == PostInsertRowCollprop)
            {
                dsCollprop.InsertLastRow();
            }
            else if (eventArg == PostDeleteRowCollprop)
            {
                int r = dsCollprop.GetRowFocus();
                dsCollprop.DeleteRow(r);
            }
            else if (eventArg == PostCollNo)
            {
                int row = dsList.GetRowFocus();
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    dsList.FindTextBox(i, "collmast_no").BackColor = Color.White;
                    dsList.FindTextBox(i, "collmasttype_desc").BackColor = Color.White;
                    dsList.FindTextBox(i, "dol_prince").BackColor = Color.White;
                    dsList.FindTextBox(i, "est_price").BackColor = Color.White;
                }
                dsList.FindTextBox(row, "collmast_no").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "collmasttype_desc").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "dol_prince").BackColor = Color.SkyBlue;
                dsList.FindTextBox(row, "est_price").BackColor = Color.SkyBlue;

                string collmast_no = dsList.DATA[row].COLLMAST_NO; ;
                dsMain.Retrieve(collmast_no);
                dsLanduse.Retrieve(collmast_no);
                dsLand.Retrieve(collmast_no);
                dsCondo.Retrieve(collmast_no);
                dsBdingdet.Retrieve(collmast_no);
                dsBding.Retrieve(collmast_no);
                dsCollprop.Retrieve(collmast_no);
                dsRate.Retrieve(collmast_no);
                Collmastgrp_Changed();
                dsMain.FindDropDownList(0, "collmasttype_grp").Enabled = false;
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