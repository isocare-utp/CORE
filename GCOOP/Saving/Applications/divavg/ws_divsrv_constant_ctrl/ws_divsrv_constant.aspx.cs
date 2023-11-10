using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.ws_divsrv_constant_ctrl
{
    public partial class ws_divsrv_constant : PageWebSheet, WebSheet
    {

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.RetrieveMain();
                dsList.RetrieveList();
                
                for(int i=0;i<dsList.RowCount;i++){
                    decimal ldc_divpercent_rate = dsList.DATA[i].DIVPERCENT_RATE;
                    dsList.DATA[i].DIVPERCENT_RATE = ldc_divpercent_rate * 100;
                    decimal ldc_avgpercent_rate = dsList.DATA[i].AVGPERCENT_RATE;
                    dsList.DATA[i].AVGPERCENT_RATE = ldc_avgpercent_rate * 100;
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            try
            {
                if (dsMain.DATA[0].DIVTYPE_CODE == "DAY")
                {
                    if (dsMain.DATA[0].DIV_DAYCALTYPE_CODE == "" || dsMain.DATA[0].DIV_DAYCALTYPE_CODE == null)
                    {
                        dsMain.DATA[0].DIV_DAYCALTYPE_CODE = "AMT";
                    }
                }
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal ldc_divpercent_rate = dsList.DATA[i].DIVPERCENT_RATE;
                    dsList.DATA[i].DIVPERCENT_RATE = ldc_divpercent_rate / 100;
                    decimal ldc_avgpercent_rate = dsList.DATA[i].AVGPERCENT_RATE;
                    dsList.DATA[i].AVGPERCENT_RATE = ldc_avgpercent_rate / 100;
                }
                ExecuteDataSource exed1 = new ExecuteDataSource(this);
                exed1.AddFormView(dsMain,ExecuteType.Update);
                exed1.AddRepeater(dsList);
                exed1.Execute();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                dsMain.ResetRow();
                dsList.ResetRow();
                dsMain.RetrieveMain();
                dsList.RetrieveList();
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal ldc_divpercent_rate = dsList.DATA[i].DIVPERCENT_RATE;
                    dsList.DATA[i].DIVPERCENT_RATE = ldc_divpercent_rate * 100;
                    decimal ldc_avgpercent_rate = dsList.DATA[i].AVGPERCENT_RATE;
                    dsList.DATA[i].AVGPERCENT_RATE = ldc_avgpercent_rate * 100;
                }
            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}