using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.account.w_acc_ucf_constant_ctrl
{
    public partial class w_acc_ucf_constant : PageWebSheet, WebSheet
    {
       
        [JsPostBack]
        public String Postaccdetail { get; set; }
        [JsPostBack]
        public String PostaddRow { get; set; }
        [JsPostBack]
        public String PosteditRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }
       


        public void InitJsPostBack()
        {
          
            wd_list.InitList(this);
            wd_main.InitList(this);
            
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

               
               
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
           

          if (eventArg == Postaccdetail)
            {
               
                


            }
            else if (eventArg == PostaddRow)
            {
                int r = wd_main.GetRowFocus();

                
            }

            else if (eventArg == PosteditRow)
            {
                
                
               
            }

            else if (eventArg == PostDeleteRow)
            {
                
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