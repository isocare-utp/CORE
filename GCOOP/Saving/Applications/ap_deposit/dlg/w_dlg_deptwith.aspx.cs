using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_deptwith : PageWebDialog, WebDialog
    {
        protected int iRow;
        protected String postSubmit;
        protected String postUp;
        protected String postDown;

        protected int iGetRow
        {
            get { return iRow; }
        }

        protected int iSetRow
        {
            get { return iRow++; }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSubmit = WebUtil.JsPostBack(this, "postSubmit");
            postUp = WebUtil.JsPostBack(this, "postUp");
            postDown = WebUtil.JsPostBack(this, "postDown");
        }

        public void WebDialogLoadBegin()
        {
            iRow = 1;
            try
            {
                if (!IsPostBack)
                {
                    String reqItemType = Request["itemDeptWith"].Trim();
                    if (reqItemType == "d" || reqItemType == "w" || reqItemType == "c")
                    {
                        if (reqItemType == "d")
                        {
                            reqItemType = "+";
                        }
                        else if (reqItemType == "w")
                        {
                            reqItemType = "-";
                        }
                        else
                        {
                            reqItemType = "/";
                        }
                        DdDeptWith.Text = reqItemType;
                    }
                    DataTable dt = null;// wcf.Deposit.GetChildDeptWith(state.SsWsPass, itemType);
                    try
                    {
                        dt = WebUtil.GetChildDeptWith(state.SsWsPass, reqItemType);
                    }
                    catch { }
                    if (dt != null)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }

                    try
                    {
                       LbAccountName.Text = Request["accname"].Trim();
                    }
                    catch { }
                }
                else
                {
                }
            }
            catch { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSubmit")
            {
                JsPostSubmit();

            }
            else if (eventArg == "postUp")
            {
                int currentRow = GridView1.SelectedIndex;
                if (currentRow > 0)
                {
                    GridView1.SelectedIndex = currentRow - 1;
                }
                else if (currentRow == -1 && GridView1.Rows.Count > 0)
                {
                    GridView1.SelectedIndex = GridView1.Rows.Count - 1;
                }
                else if (currentRow == 0)
                {
                    GridView1.SelectedIndex = -1;
                }
            }
            else if (eventArg == "postDown")
            {
                int currentRow = GridView1.SelectedIndex;
                if (GridView1.Rows.Count > currentRow + 1)
                {
                    GridView1.SelectedIndex = currentRow + 1;
                }
                else
                {
                    GridView1.SelectedIndex = -1;
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            try
            {
                HdCurrentRow.Value = GridView1.SelectedIndex + "";
            }
            catch
            {
                HdCurrentRow.Value = "-1";
            }
        }

        #endregion

        protected void DdDeptWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridView1.SelectedIndex = -1;
            JsPostSubmit();
        }

        private void JsPostSubmit()
        {
            String reqItemType = DdDeptWith.SelectedValue;
            DataTable dt = null;
            try
            {
                dt = WebUtil.GetChildDeptWith(state.SsWsPass, reqItemType);
            }
            catch { }
            if (dt != null)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            GridView1.SelectedIndex = -1;
        }
    }
}