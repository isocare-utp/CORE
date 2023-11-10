<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_constant.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_constant_ctrl.w_acc_ucf_constant" %>
<%@ Register Src="wd_main.ascx" TagName="wd_main" TagPrefix="uc1" %>
<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_main = new DataSourceTool;
        var wd_list = new DataSourceTool;
      

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnWdlistClicked(s, r, c , v) {
            
          
            if (c == "account_name") {
                Postaccdetail();
                
            }
        }
        function OnWdmainClicked(s, r, c, v) {
           // alert(c);
            if (c == "bt_delete") {

                PostDeleteRow();
            }
            else if (c == "bt_edit") {
               // wd_list.GetElement(0, "account_name").enabled = false;
                wd_list.GetElement(0, "account_name").style.color = "#990000";
                PosteditRow();

               
                
            }
            else if (c == "bt_addaccid") {

                PostaddRow();
            }
        }

        function SheetLoadComplete() {
            var row = wd_list.GetRowCount();
            for (var i = 0; i < row; i++) {
                var account_type_id = wd_list.GetItem(i, "account_type_id");
                if (account_type_id == 1) {
                    wd_list.GetElement(i, "account_name").style.color = "#990000";
                    wd_list.GetElement(i, "account_name").style.fontWeight = "bold";
                   
                   
                } else if (account_type_id == 2) {
                    wd_list.GetElement(i, "account_name").style.color = "#00d900";
                    wd_list.GetElement(i, "account_name").style.fontWeight = "bold";
                } else {
                    wd_list.GetElement(i, "account_name").style.color = "#0000a7";
                }
              }

        }

        
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="center" style="margin-right: 1px; width: 100%">
        <table width="100%" >
     
        <td width="50%" > 
         <uc1:wd_main ID="wd_main" runat="server" /></td>
       
        
      
       <td  width="50%"> 
      
      <uc2:wd_list ID="wd_list" runat="server" /> </td>
     
        </table>

    </div>
</asp:Content>
