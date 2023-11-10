<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_accperiod.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accperiod_ctrl.w_acc_ucf_accperiod" %>
<%@ Register Src="wd_main.ascx" TagName="wd_main" TagPrefix="uc1" %>
<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_main = new DataSourceTool;
        var wd_list = new DataSourceTool;
      

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnWdlistClicked(s, r, c, v) {
            if (c == "b_delete") {
                // alert(c);
                PostDeleteRow();
            }
        }
        function OnWdListClicked(s, r, c) {
        }
        function SheetLoadComplete() {
        }
        function OnWdmainClicked(s, r, c ,v) {
            if (c == "account_year" || c == "beginning_of_accou" || c == "ending_of_account") {
               // alert(c);
            Postperiod();
        } 
        }
        
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="center" style="margin-right: 1px; width: 100%">
        <table width="100%" >
     
        <td width="50%" > 
        <span class="NewRowLink" >ปีบัญชี</span>
        <uc1:wd_main ID="wd_main" runat="server" /></td>
      
       <td  width="50%"> 
       <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
       <uc2:wd_list ID="wd_list" runat="server" /> </td>
     
        </table>
    </div>
</asp:Content>
