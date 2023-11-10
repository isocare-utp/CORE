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
using CoreSavingLibrary.WcfNAccount;
using DataLibrary;
using System.Data.OracleClient;
using System.Web.Services.Protocols;


namespace Saving.Applications.account
{

    public partial class w_acc_accmaster : PageWebSheet, WebSheet
    {

        private n_accountClient accService;
        //==========================
        protected String postNewClear;
        protected String postShowTreeviewAll;
        protected String postNoShowTreeviewAll;
        protected String postEditData;
        protected String postDeleteAccountId;
        protected String postFindShow;


        //  public int status;

        //========================================

        private void JspostFindShow()
        {
            String Acc_Id = "";
            Acc_Id = HdAccNo.Value.Trim();
            // Hd_accid_master.Value = Acc_Id;
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
                //LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
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
                n_accountClient accService = wcf.NAccount;
                String account_id;
                account_id = Hd_accid_master.Value;
                String wsPass = state.SsWsPass;

                String sqldelete = "DELETE FROM finucfitemtype where account_id = '" + account_id + "' and coop_id = '" + state.SsCoopControl + "'";
                WebUtil.Query(sqldelete);

                int result = wcf.NAccount.of_delete_accountid(wsPass, account_id, state.SsCoopId);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลรหัสบัญชีเรียบร้อยแล้ว");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JspostNewClear()
        {
            Dw_master.Reset();
            Dw_master.InsertRow(0);
            SetDwMasterEnable(1);
            Dw_master.SaveDataCache();
            Panel2.Visible = false;
            //status = 1; 
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

            WebUtil.RetrieveDDDW(Dw_master, "account_group_id", "accmaster.pbl", state.SsCoopId);
            WebUtil.RetrieveDDDW(Dw_master, "account_rev_group", "accmaster.pbl", state.SsCoopId);
        }


        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            accService = wcf.NAccount;//ประกาศ new
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
                n_accountClient accService = wcf.NAccount;
                Int16 account_status = Convert.ToInt16(Hd_aistatus.Value);
                String xmlDw_main = Dw_master.Describe("Datawindow.Data.Xml");
                String wsPass = state.SsWsPass;
                
                String account_id_text = Dw_master.GetItemString(1, "account_id");
                String account_name_text = Dw_master.GetItemString(1, "account_name");
                // int genvc_flag_text = 1;
                int accnature_flag_text = 1;
                String idcoop = state.SsCoopId;

                 if (account_status == 1)
                {

                    String sql = "INSERT INTO finucfitemtype(slipitemtype_code,item_desc,accnature_flag,accmap_code,genvc_flag,account_id,coop_id) VALUES('" + account_id_text + "','" + account_name_text + "'," + accnature_flag_text + ",'AID',1,'" + account_id_text + "','" + idcoop + "')";
                    WebUtil.Query(sql);
                }
                else
                {
                    String sql2 = "UPDATE finucfitemtype SET item_desc = '" + account_name_text + "' WHERE slipitemtype_code='" + account_id_text + "'";
                    WebUtil.Query(sql2);
                }
                    int result = wcf.NAccount.of_add_newaccount_id(wsPass, xmlDw_main, account_status);
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเลขที่บัญชีเรียบร้อยแล้ว");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
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
                String sql = @"select account_id, account_name, (select count(*) from accmaster where account_control_id = ac.account_id) childnodecount from accmaster ac where  account_level =  1 and coop_id = '" + state.SsCoopId + "' order  by account_id";
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
                String sql = "select account_id, account_name, (select count(*) from accmaster where account_control_id = ac.account_id) childnodecount from accmaster ac where  ac.account_control_id = '" + acc_control_id + "' and ac.coop_id = '" + state.SsCoopId + "'order by account_id";
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
                tn.Text = (dr["account_id"].ToString() + "   " + dr["account_name"].ToString());
                tn.Value = dr["account_id"].ToString();
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
            //String  Acc_Id = TreeView1.SelectedNode.Value.Trim();
            String Acc_Id = Dw_master.GetItemString(1, "account_id");
            Dw_master.Reset();
            Dw_master.Retrieve(Acc_Id, state.SsCoopId);
        }

        protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            JspostNewClear();
        }

        protected void B_add_Click(object sender, EventArgs e)
        {

            // ถ้ากเพิ่มข้อมูล ให้ Get ค่าก่อนหน้านั้นมา

            String type_id = "";
            String group_id = "";
            String acc_nature = "";
            Decimal acc_level = 0;
            String acc_control = "";
            String rev_group = "";
            String section_id = "";
            Decimal on_report = 0;

            try
            {
                type_id = Dw_master.GetItemString(1, "account_type_id");
            }
            catch
            {
                type_id = "";
            }

            try
            {
                group_id = Dw_master.GetItemString(1, "account_group_id");
            }
            catch
            {
                group_id = "";
            }
            try
            {
                acc_nature = Dw_master.GetItemString(1, "account_nature");
            }
            catch
            {
                acc_nature = "";
            }
            try
            {
                acc_level = Dw_master.GetItemDecimal(1, "account_level");
            }
            catch
            {
                acc_level = 0;
            }
            try
            {
                acc_control = Dw_master.GetItemString(1, "account_control_id");
            }
            catch
            {
                acc_control = "";
             }
            try
            {
                rev_group = Dw_master.GetItemString(1, "account_rev_group");
            }
            catch
            {
                rev_group = "";
            }
            try
            {
                section_id = Dw_master.GetItemString(1, "section_id");
            }
            catch
            {
                section_id = "";
            }
            try
            {
                on_report = Dw_master.GetItemDecimal(1, "on_report");
            }
            catch
            {
                on_report = 0;
            }
                 
            //status = 1;
            JspostAddNew();

            Dw_master.SetItemString(1, "account_type_id", type_id);
            Dw_master.SetItemString(1, "account_group_id", group_id);
            Dw_master.SetItemString(1, "account_nature", acc_nature);
           //Dw_master.SetItemString(1, "account_activity", Hd_activity.Value);
            Dw_master.SetItemDecimal(1, "account_level", acc_level);
            Dw_master.SetItemString(1, "account_control_id", acc_control);
            Dw_master.SetItemString(1, "account_rev_group", rev_group);
            Dw_master.SetItemString(1, "section_id", section_id);
            Dw_master.SetItemDecimal(1, "on_report", on_report);
        }
    }
}
