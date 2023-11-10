<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_coopno.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_coopno_ctrl.w_acc_ucf_coopno" %>
<%@ Register Src="wd_list.ascx" TagName="wd_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var wd_list = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnWdListClicked(s, r, c, v) {

        }
        function OnWdListItemChanged(s, r, c, v) {

        }
        function SheetLoadComplete() {


        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">        
        <uc1:wd_list ID="wd_list" runat="server" />
    </div>
</asp:Content>