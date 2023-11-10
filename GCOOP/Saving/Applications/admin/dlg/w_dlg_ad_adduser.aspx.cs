using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin.dlg
{
    public partial class w_dlg_ad_adduser : PageWebDialog, WebDialog
    {

        public void InitJsPostBack()
        {
      
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwUtil.RetrieveDataWindow(DwMain, "ad_user.pbl", null);
                FilterUser();

            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
          
        }

        public void WebDialogLoadEnd()
        {

            DwMain.SaveDataCache();
        }

        public void FilterUser()
        {
            int count=0;
            string group_name = Request["group_name"];
            string sqlcount = "select count(user_name) from amsecgroupings where group_name ='" + group_name + "'";
            Sdt checkcount = WebUtil.QuerySdt(sqlcount);
            if (checkcount.Next())
                count = checkcount.GetInt32("count(user_name)");  
                string[] user_name = new string[count+1];

            string sql = "select user_name from amsecgroupings where group_name ='"+group_name+"'";
            Sdt check = WebUtil.QuerySdt(sql);
            int k = 1;
            while (check.Next())
            {
                user_name[k] = check.GetString("user_name");
                k++;
            }

            int[] rowdelete = new int[count + 1];
            string userN;
            int rows = DwMain.RowCount;
            int countdelete = 1;
            for (int i = 1; i < rows + 1; i++)
            {
                for (int j = 1; j < k; j++)
                {
                    userN = DwMain.GetItemString(i,"user_name");
                    if(userN == user_name[j])
                    {
                        rowdelete[countdelete] = i;
                        countdelete++;
                    }
                }
            }
            for (int d = countdelete-1; d >0 ; d--)
            {
                int row = rowdelete[d];
                DwMain.DeleteRow(row);
            }
            
        }
    }
}