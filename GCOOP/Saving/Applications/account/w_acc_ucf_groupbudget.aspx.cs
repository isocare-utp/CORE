using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;

namespace Saving.Applications.account
{
    public partial class w_acc_ucf_groupbudget : PageWebSheet , WebSheet
    {
        //==========================
        protected String postNewClear;
        protected String postShowTreeviewAll;
        protected String postNoShowTreeviewAll;
        protected String postEditData;
        protected String postDeleteAccountId;
        protected String postFindShow;
        //========================================
        String pbl = "cm_constant_config.pbl";

        private void JspostFindShow()
        {
            String Acc_Id = "";
            Acc_Id = HdAccNo.Value.Trim();
            Dw_master.ClearDataCache();
            Dw_master.Reset();
            Dw_master.SetTransaction(sqlca);
            Dw_master.Retrieve(Acc_Id, state.SsCoopId);
            SetDwMasterEnable(1);

            Hd_checkclear.Value = "true";
            Panel2.Visible = true;
        }

        public void SetDwMasterEnable(int protect)
        {
            try
            {
                if (protect == 1)
                {
                    Dw_master.Enabled = false;
                }
                else
                {
                    Dw_master.Enabled = true;
                }
                int RowAll = int.Parse(Dw_master.Describe("Datawindow.Column.Count"));
                for (int li_index = 1; li_index <= RowAll; li_index++)
                {
                    Dw_master.Modify("#" + li_index.ToString() + ".protect= " + protect.ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        private void JspostAddNew()
        {
            Dw_master.Reset();
            Dw_master.InsertRow(0);
            Dw_master.SetItemString(Dw_master.RowCount, "coop_id", state.SsCoopId);
            Dw_master.Enabled = true;
            Dw_master.SaveDataCache();
            Panel2.Visible = false;
            Hd_aistatus.Value = "1";
        }

        private void JspostDeleteAccountId()
        {
            try
            {
                String accbudgetgroup_typ = Dw_master.GetItemString(1, "accbudgetgroup_typ");
                String sqldelete = @"DELETE FROM accbudget_group WHERE accbudgetgroup_typ = '" + accbudgetgroup_typ + "' and coop_id = " + state.SsCoopId;
                Sdt delete = WebUtil.QuerySdt(sqldelete);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเร็จ");
            }
        }

        private void JspostNewClear()
        {
            Dw_master.Reset();
            Dw_master.InsertRow(0);
            SetDwMasterEnable(1);
            Dw_master.SaveDataCache();
            Panel2.Visible = false;
            Hd_aistatus.Value = "1";
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postNoShowTreeviewAll = WebUtil.JsPostBack(this, "postNoShowTreeviewAll");
            postShowTreeviewAll = WebUtil.JsPostBack(this, "postShowTreeviewAll");
            postDeleteAccountId = WebUtil.JsPostBack(this, "postDeleteAccountId");
            postFindShow = WebUtil.JsPostBack(this, "postFindShow");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_master.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                lbl_coopname.Text = state.SsCoopName;
                Dw_master.InsertRow(0);
                SetDwMasterEnable(1);
                PopulateRootLevel();
                Panel2.Visible = false;
            }
            else
            {
                this.RestoreContextDw(Dw_master);
            }
            if (Dw_master.RowCount > 1)
            {
                Dw_master.DeleteRow(Dw_master.RowCount);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postNoShowTreeviewAll")
            {
                TreeView1.CollapseAll();
            }
            else if (eventArg == "postShowTreeviewAll")
            {
                TreeView1.ExpandAll();
            }

            else if (eventArg == "postDeleteAccountId")
            {
                JspostDeleteAccountId();
            }
            else if (eventArg == "postFindShow")
            {
                JspostFindShow();
            }

        }

        public void SaveWebSheet()
        {
            try
            {
                if (Hd_aistatus.Value == "2")
                {
                    DwUtil.UpdateDataWindow(Dw_master, pbl, "ACCBUDGET_GROUP");
                }
                else
                {
                    String accrcvpay = Dw_master.GetItemString(1, "accrcvpay");
                    String accbudgetgroup_typ = Dw_master.GetItemString(1, "accbudgetgroup_typ");
                    String budget_type = Dw_master.GetItemString(1, "budget_type");
                    String accbudgetgroup_des = Dw_master.GetItemString(1, "accbudgetgroup_des");
                    String budget_level = Dw_master.GetItemString(1, "budget_level");
                    String budget_supergrp = Dw_master.GetItemString(1, "budget_supergrp");
                    Decimal sort_seq = Dw_master.GetItemDecimal(1, "sort_seq");

                    String sqlinsert = @"INSERT INTO accbudget_group VALUES('" + accbudgetgroup_typ + "','" + state.SsCoopId + "','" + accbudgetgroup_des +
                        "','" + accrcvpay + "','" + budget_supergrp + "','" + budget_type + "','" + budget_level + "','" + sort_seq + "')";
                    Sdt insert = WebUtil.QuerySdt(sqlinsert);
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกข้อมูลไม่สำเร็จ");
            }

        }

        public void WebSheetLoadEnd()
        {
            Dw_master.SaveDataCache();
        }

        private void PopulateRootLevel()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                String sql = @"select accbudgetgroup_typ, accbudgetgroup_des, (select count(*) from accbudget_group where budget_supergrp = bg.accbudgetgroup_typ)" +
                    " childnodecount from accbudget_group bg where budget_level = 1 and coop_id = '" + state.SsCoopId + "' order by sort_seq";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    PopulateNodes(dt, TreeView1.Nodes);
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
            ta.Close();
        }

        private void PopulateSubLevel(String acc_control_id, TreeNode parentNode)
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                String sql = "select accbudgetgroup_typ, accbudgetgroup_des, (select count(*) from accbudget_group where budget_supergrp = bg.accbudgetgroup_typ)" +
                    " childnodecount from accbudget_group bg where budget_supergrp = '" + acc_control_id + "' and coop_id = '" + state.SsCoopId + "' order by sort_seq";
                Sdt dt = ta.Query(sql);
                PopulateNodes(dt, parentNode.ChildNodes);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
            ta.Close();
        }

        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
        {
            foreach (DataRow dr in dt.Rows)
            {
                String child = dr["childnodecount"].ToString();
                TreeNode tn = new TreeNode();
                tn.Text = (dr["accbudgetgroup_typ"].ToString() + "   " + dr["accbudgetgroup_des"].ToString());
                tn.Value = dr["accbudgetgroup_typ"].ToString();
                nodes.Add(tn);
                tn.PopulateOnDemand = (bool)(Int32.Parse(dr["childnodecount"].ToString()) > 0);
            }
        }

        #endregion

        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(e.Node.Value, e.Node);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            String Acc_Id = "";
            Acc_Id = TreeView1.SelectedNode.Value.Trim();
            Hd_accid_master.Value = Acc_Id;

            Dw_master.ClearDataCache();
            Dw_master.Reset();
            Dw_master.SetTransaction(sqlca);
            Dw_master.Retrieve(Acc_Id, state.SsCoopId);
            SetDwMasterEnable(1);

            Hd_checkclear.Value = "true";
            Panel2.Visible = true;
        }

        protected void B_edit_Click(object sender, EventArgs e)
        {
            SetDwMasterEnable(0);
            Hd_aistatus.Value = "2";
            String Acc_Id = Dw_master.GetItemString(1, "accbudgetgroup_typ");
            Dw_master.Reset();
            Dw_master.Retrieve(Acc_Id, state.SsCoopId);
        }

        protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            JspostNewClear();
        }

        protected void B_add_Click(object sender, EventArgs e)
        {
            JspostAddNew();
        }
    }
}
