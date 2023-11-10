<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_ucf_accyear.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accyear_ctrl.w_acc_ucf_accyear" %>

<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_list = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnWdListClicked(s, r, c, v) {
            if (c == "b_delete") {
                wd_list.SetRowFocus(r);
                PostDeleteRow();
            }
        }

     //   function OnDwmasterClick(s, r, c) {
       //     Gcoop.CheckDw(s, r, c, "close_account_stat", 1, 0);
     //   }

        function SheetLoadComplete() {


        }

  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc1:wd_list ID="wd_list" runat="server" />
    </div>
</asp:Content>
