<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_ucf_namemanage.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_ucf_namemanage_ctrl.ws_fin_ucf_namemanage" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         var dsMain = new DataSourceTool();
         function Validate() {
             return confirm("ยืนยันการบันทึกข้อมูล");
         }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" /> 
</asp:Content>
