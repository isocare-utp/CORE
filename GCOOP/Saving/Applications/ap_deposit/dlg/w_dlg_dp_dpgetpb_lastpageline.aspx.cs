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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;
using System.IO;
namespace Saving.Applications.ap_deposit
{
    public partial class w_dlg_dp_dpgetpb_lastpageline : System.Web.UI.Page
    {
        private DwTrans sqlca;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlca = new DwTrans();
            sqlca.Connect();
            DwMain.SetTransaction(sqlca);
            String coopid = Request["coopid"].ToString();
            String accNo = Request["accno"].ToString();
            String seqNoStart = Request["seqno"].ToString();
            String seqNoEnd = Request["seqnoend"].ToString();
            Double start = Double.Parse(seqNoStart);
            Double end = Double.Parse(seqNoEnd);
            DwMain.Retrieve(accNo,coopid);
            DwMain.SetItemDouble(1, "lastrec_no_pb", start);
            DwMain.SetItemString(1, "deptpassbook_no", accNo);
            DwMain.SetItemDouble(1, "prnpbkto", end);


            
            //Int32 rowSeqNo = 0;
            //rowSeqNo = Int32.Parse(SeqNoEnd) - Int32.Parse(seqNoStart) + 1;
            //accdeptno.Text = accNo;
            //seqstart.Text = seqNoStart;
            //seqend.Text = SeqNoEnd;
            //detail.Text = "พิมพ์ทั้งหมด " + rowSeqNo + " รายการ";       

        }
    }
}
