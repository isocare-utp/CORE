<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_ass_deptaccountno_search.aspx.cs" Inherits="Saving.Applications.assist.dlg.w_dlg_ass_deptaccountno_search_ctrl.w_dlg_ass_deptaccountno_search" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
       
       function OnDsListClicked(s, r, c) {
           var deptno = dsList.GetItem(r, "deptaccount_no");
            try {
                window.opener.GetDeptNoFromDlg(deptno);
                window.close();
            } catch (err) {
                parent.GetDeptNoFromDlg(deptno);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   
    <uc1:DsList ID="dsList" runat="server" />
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <br />
</asp:Content>