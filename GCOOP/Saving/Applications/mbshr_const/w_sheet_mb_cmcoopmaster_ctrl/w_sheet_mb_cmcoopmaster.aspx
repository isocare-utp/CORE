<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_cmcoopmaster.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_cmcoopmaster_ctrl.w_sheet_mb_cmcoopmaster" %>

<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsListClicked(s, r, c) {
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
