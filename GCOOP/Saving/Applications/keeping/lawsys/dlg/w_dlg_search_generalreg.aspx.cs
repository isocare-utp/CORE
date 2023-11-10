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

namespace Saving.Applications.lawsys.dlg
{
    public partial class w_dlg_search_generalreg : PageWebDialog,WebDialog
    {
        private DwThDate tDwMain;
        protected String SearchGenRalReg;
        protected String GenRegNo;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            SearchGenRalReg = WebUtil.JsPostBack(this, "SearchGenRalReg");
            GenRegNo = WebUtil.JsPostBack(this, "GenRegNo");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("register_date_begin", "register_tdate_begin");
            tDwMain.Add("register_date_end", "register_tdate_end");
            tDwMain.Add("return_date_begin", "return_tdate_begin");
            tDwMain.Add("return_date_end", "return_tdate_end");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DateTime begin = new DateTime(2007, 1, 1);
                DateTime end = new DateTime(2057, 12, 31);
                DwMain.SetItemDateTime(1, "register_date_begin", begin);
                DwMain.SetItemDateTime(1, "register_date_end", end);
                DwMain.SetItemDateTime(1, "return_date_begin", begin);
                DwMain.SetItemDateTime(1, "return_date_end", end);
                tDwMain.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "SearchGenRalReg")
            {
                String genRegNo = DwUtil.GetString(DwMain, 1, "genreg_no", "");
                String regisTitel = DwUtil.GetString(DwMain, 1, "register_title", "");
                String regisDesc = DwUtil.GetString(DwMain, 1, "register_desc", "");
                DateTime regDateBegin = DwMain.GetItemDateTime(1, "register_date_begin");
                DateTime regDateEnd = DwMain.GetItemDateTime(1, "register_date_end");
                DateTime returnDateBegin = DwMain.GetItemDateTime(1, "return_date_begin");
                DateTime returnDateEnd = DwMain.GetItemDateTime(1, "return_date_end");
                
                String sqlTxt = @"SELECT GENREG_NO, REGISTER_TITLE, REGISTER_DESC FROM LWGENERALREG 
                                WHERE GENREG_NO LIKE '" + genRegNo + "%' and " + 
                                "REGISTER_TITLE LIKE '%" + regisTitel + "%' and " +
                                "REGISTER_DESC LIKE '%" + regisDesc + "%' and " +
                                "REGISTER_DATE BETWEEN to_date('" + regDateBegin.ToString("dd/MM/yyyy", WebUtil.EN) + "','dd/mm/yyyy') and to_date('" + regDateEnd.ToString("dd/MM/yyyy", WebUtil.EN) + "','dd/mm/yyyy') and " +
                                "RETURN_DATE BETWEEN to_date('" + returnDateBegin.ToString("dd/MM/yyyy", WebUtil.EN) + "','dd/mm/yyyy') and to_date('" + returnDateEnd.ToString("dd/MM/yyyy", WebUtil.EN) + "','dd/mm/yyyy')";
                DataTable dt = WebUtil.Query(sqlTxt);
                DwUtil.ImportData(dt, DwList, null);
            }
            else if (eventArg == "GenRegNo")
            {
                String genRegNo = "";
                try
                {
                    String regNo = "";
                    genRegNo = DwMain.GetItemString(1, "genreg_no");
                    String YY = genRegNo.Substring(0, 2);
                    if (genRegNo.Length < 8)
                    {
                        if (genRegNo.Length > 2)
                        {
                            regNo = WebUtil.Right("000000" + genRegNo.Substring(2, genRegNo.Length - 2), 6);
                            regNo = YY + regNo;
                        }
                        else
                        {
                            DwMain.SetItemString(1, "genreg_no", genRegNo);
                        }
                    }
                    else
                    {
                        regNo = genRegNo;
                    }
                    DwMain.SetItemString(1, "genreg_no", regNo);
                }
                catch
                {
                    DwMain.SetItemString(1, "genreg_no", genRegNo);
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion
    }
}
