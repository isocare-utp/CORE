using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary;
using DBAccess;

namespace Saving.Applications.lawsys
{
    public partial class w_sheet_sl_member_detail : PageWebSheet, WebSheet
    {
        protected WebState state;
        protected DwTrans sqlca;
        protected String chkTrue;
        protected String chkFalse;
        protected String postNew;
        protected String tabLoancheckbox;
        protected String sslcontno = "";
        String req = "";


        #region WebSheet Members

        public void InitJsPostBack()
        {
            tabLoancheckbox = WebUtil.JsPostBack(this, "tabLoancheckbox");
            sslcontno = WebUtil.JsPostBack(this, "sslcontno");
            chkTrue = WebUtil.JsPostBack(this, "chkTrue");
            chkFalse = WebUtil.JsPostBack(this, "chkFalse");
            postNew = WebUtil.JsPostBack(this, "postNew");
        }

        public void WebSheetLoadBegin()
        {
            state = new WebState(Session, Request);
            Hloancheck.Value = "true";
            //เช็ค ReadAble
            if (!state.IsReadable)
            { LtServerMessage.Text = WebUtil.PermissionDeny(PermissType.ReadDeny); return; }

            try
            {
                sqlca = new DwTrans();
                sqlca.Connect();

                dw_main.SetTransaction(sqlca);
                dw_data.SetTransaction(sqlca);
                dw_bank.SetTransaction(sqlca);
                dw_data2.SetTransaction(sqlca);
                dw_coll.SetTransaction(sqlca);
                dw_coll2.SetTransaction(sqlca);
                dw_data3.SetTransaction(sqlca);
                dw_data4.SetTransaction(sqlca);
                dw_data5.SetTransaction(sqlca);
                dw_data_1.SetTransaction(sqlca);
                dw_data_2.SetTransaction(sqlca);
                dw_data_3.SetTransaction(sqlca);
                dw_data_4.SetTransaction(sqlca);
                dw_remarkstat.SetTransaction(sqlca);
            }
            catch { LtServerMessage.Text = WebUtil.ErrorMessage("[Cannot Connect Database...]"); }





            try
            {
                req = Request["strvalue"];

            }
            catch { req = ""; }

            if (req != null && req != "")
            {
                String RECV_PERIOD = "";
                int chk = dw_main.Retrieve(req);
                if (chk != 1)
                {
                    dw_main.InsertRow(0);
                    dw_data.InsertRow(0);
                    dw_bank.InsertRow(0);
                    dw_data2.InsertRow(0);
                    dw_coll.InsertRow(0);
                    dw_coll2.InsertRow(0);
                    dw_data3.InsertRow(0);
                    dw_data4.InsertRow(0);
                    dw_data5.InsertRow(0);
                    dw_data_1.InsertRow(0);
                    dw_data_2.InsertRow(0);
                    dw_data_3.InsertRow(0);
                    dw_data_4.InsertRow(0);
                    dw_remarkstat.InsertRow(0);

                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบเลขทะเบียน " + req);
                }
                else
                {
                    dw_data.Retrieve(req);
                    dw_bank.Retrieve(req);
                    dw_data2.Retrieve(req);
                    dw_coll.Retrieve(req);
                    dw_coll2.Retrieve(req);
                    Sta ta = new Sta(sqlca.ConnectionString);
                    String sql = "  SELECT distinct  KPTEMPRECEIVEDET.RECV_PERIOD    FROM KPTEMPRECEIVEDET  ";
                    Sdt dt = ta.Query(sql);
                    if (dt.Next()) { RECV_PERIOD = dt.GetString("RECV_PERIOD"); }
                    dw_data3.Retrieve(req, RECV_PERIOD.Trim());
                    dw_data4.Retrieve(req);
                    dw_data5.Retrieve(req);
                    dw_data_1.Retrieve(req);

                    dw_data_2.Retrieve(req);
                    dw_data_3.Retrieve(req);
                    dw_data_4.Retrieve(req);
                    dw_remarkstat.Retrieve(req);
                    try
                    {

                        if (req != "")
                        {
                            // String imageUrl = "C:\\GCOOP\\Saving\\Applications\\keeping\\dlg\\member_picture\\" + req + ".jpg";
                            String imageUrl = state.Url + "filecommon/picture_memberno/" + req + ".jpg";
                            Image1.ImageUrl = imageUrl;
                        }
                        else
                        {
                            Image1.ImageUrl = "";

                        }

                    }
                    catch (Exception ex)
                    {

                        Label2.Text = ex.ToString();
                        req = "";

                    }


                }
            }
            else
            {
                if (!IsPostBack)
                {
                    dw_main.InsertRow(0);
                    dw_data.InsertRow(0);
                    dw_bank.InsertRow(0);
                    dw_data2.InsertRow(0);
                    dw_coll.InsertRow(0);
                    dw_coll2.InsertRow(0);
                    dw_data3.InsertRow(0);
                    dw_data4.InsertRow(0);
                    dw_data5.InsertRow(0);
                    dw_data_1.InsertRow(0);
                    dw_data_2.InsertRow(0);
                    dw_data_3.InsertRow(0);
                    dw_data_4.InsertRow(0);
                    dw_remarkstat.InsertRow(0);
                }
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            HfOpenLnContDlg.Value = "false";
            if (eventArg == "sslcontno")
            {
                Session["sslncontno"] = HfLncontno.Value;
                HfOpenLnContDlg.Value = "true";
            }
            else if (eventArg == "chkTrue")
            {
                JschkTrue();
            }
            else if (eventArg == "chkFalse")
            {
                JschkFalse();
            }
            else if (eventArg == "postNew")
            {
                JspostNew();
            }
        }

        private void JspostNew()
        {
            dw_main.Reset();
            dw_data.Reset();
            dw_bank.Reset();
            dw_data2.Reset();
            dw_coll.Reset();
            dw_coll2.Reset();
            dw_data3.Reset();
            dw_data4.Reset();
            dw_data5.Reset();
            dw_data_1.Reset();
            dw_data_2.Reset();
            dw_data_3.Reset();
            dw_data_4.Reset();
            dw_remarkstat.Reset();
            dw_main.InsertRow(0);
            dw_data.InsertRow(0);
            dw_bank.InsertRow(0);
            dw_data2.InsertRow(0);
            dw_coll.InsertRow(0);
            dw_coll2.InsertRow(0);
            dw_data3.InsertRow(0);
            dw_data4.InsertRow(0);
            dw_data5.InsertRow(0);
            dw_data_1.InsertRow(0);
            dw_data_2.InsertRow(0);
            dw_data_3.InsertRow(0);
            dw_data_4.InsertRow(0);
            dw_remarkstat.InsertRow(0);
        }

        private void JschkFalse()
        {
            try
            {
                Hloancheck.Value = "false";
                dw_data_2.SetFilter("contract_status  < 0");
                dw_data_2.Filter();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JschkTrue()
        {
            try
            {
                Hloancheck.Value = "true";
                dw_data_2.SetFilter("contract_status  > 0");
                dw_data_2.Filter();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void SaveWebSheet()
        {
            throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            sqlca.Disconnect();
        }

        #endregion
    }
}
