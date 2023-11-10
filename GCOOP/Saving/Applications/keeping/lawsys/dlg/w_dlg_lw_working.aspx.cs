using System;
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
using DataLibrary;

namespace Saving.Applications.lawsys.dlg
{
    public partial class w_dlg_lw_working : PageWebDialog, WebDialog
    {
        private string pbl = "lw_lawmaster.pbl";
        protected string postSave;
        private DwThDate tDwWorking;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSave = WebUtil.JsPostBack(this, "postSave");
            tDwWorking = new DwThDate(DwWorking, this);
            tDwWorking.Add("working_date", "working_tdate");
        }

        public void WebDialogLoadBegin()
        {
            HdIsFinish.Value = "false";
            if (!IsPostBack)
            {
                DwWorking.InsertRow(0);
                int seqNo = 0;
                try
                {
                    HdSeqNo.Value = Request["seqno"].ToString();
                    seqNo = int.Parse(Request["seqno"].ToString());
                }
                catch { }
                if (seqNo == 0)
                {
                    LbSaveCommand.Text = "เพิ่มแถว";
                    DwWorking.SetItemDecimal(1, "working_status", 0);
                }
                else
                {
                    string loancontractNo = Session["sslncontno"].ToString();
                    LbSaveCommand.Text = "แก้ไขข้อมูล";
                    DwUtil.RetrieveDataWindow(DwWorking, pbl, tDwWorking, new object[] { loancontractNo, seqNo });
                }
            }
            else
            {
                this.RestoreContextDw(DwWorking);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSave")
            {
                Sta ta = new Sta(state.SsConnectionString);
                try
                {
                    int seqNoRequest = int.Parse(HdSeqNo.Value);
                    if (seqNoRequest == 0)
                    {
                        string loancontractNo = Session["sslncontno"].ToString();
                        string sqlMax = "select max(seq_no) as maxseqno from LWWORKING where loancontract_no='" + loancontractNo + "'";
                        Sdt dt = ta.Query(sqlMax);
                        if (dt.Next())
                        {
                            int maxSeq = dt.GetInt32(0);
                            maxSeq += 1;
                            string workingDate = OraDT(DwUtil.GetDateTime(DwWorking, 1, "working_date"));// "null";
                            string sqlInsert = "insert into LWWORKING(LOANCONTRACT_NO, SEQ_NO, WORKING_STATUS, WORKING_DATE, WORKING_TITLE, WORKING_DESC)values('" + loancontractNo + "', " + maxSeq + ", " + DwUtil.GetInt(DwWorking, 1, "WORKING_STATUS") + ", " + workingDate + ", '" + DwUtil.GetString(DwWorking, 1, "working_title", "") + "', '" + DwUtil.GetString(DwWorking, 1, "working_desc", "") + "')";
                            int resu = ta.Exe(sqlInsert);
                            if (resu > 0)
                            {
                                HdIsFinish.Value = "true";
                            }
                            else
                            {
                                throw new Exception("Update not complete successfull");
                            }
                        }
                        else
                        {
                            throw new Exception("Cristical error : no row");
                        }
                    }
                    else
                    {
                        int resu = UpdateWorking(ta);
                        if (resu > 0)
                        {
                            HdIsFinish.Value = "true";
                        }
                        else
                        {
                            throw new Exception("Update not complete successfull");
                        }
                    }

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
                ta.Close();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwWorking.SaveDataCache();
        }

        #endregion

        private int UpdateWorking(Sta ta)
        {
            string loancontractNo = Session["sslncontno"].ToString();
            int working_status = DwUtil.GetInt(DwWorking, 1, "working_status", 0);
            string working_date = OraDT(DwUtil.GetDateTime(DwWorking, 1, "working_date"));
            string working_title = DwUtil.GetString(DwWorking, 1, "working_title", "");
            string working_desc = DwUtil.GetString(DwWorking, 1, "working_desc", "");
            string sql = "update LWWORKING set working_status = " + working_status + ", working_date = " + working_date + ", working_title = '" + working_title + "', working_desc = '" + working_desc + "' where loancontract_no = '" + loancontractNo + "' and seq_no = " + HdSeqNo.Value;
            return ta.Exe(sql);
        }

        private String OraDT(DateTime values)
        {
            return "to_date('" + values.ToString("yyyy-MM-dd", WebUtil.EN) + "', 'yyyy-mm-dd')";
        }
    }
}