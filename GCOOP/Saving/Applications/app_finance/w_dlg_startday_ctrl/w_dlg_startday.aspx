<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_startday.aspx.cs" Inherits="Saving.Applications.app_finance.w_dlg_startday_ctrl.w_dlg_startday" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
      
     function Validate() {
         return confirm("ยืนยันการบันทึกข้อมูล");                                   
     }

     function SheetLoadComplete() {
     }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <span id="F_dsMain" style="display:none"><uc1:DsMain ID="dsMain" runat="server" /></span>
</asp:Content>