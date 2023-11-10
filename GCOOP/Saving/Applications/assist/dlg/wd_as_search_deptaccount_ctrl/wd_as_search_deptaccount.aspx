<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="wd_as_search_deptaccount.aspx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_search_deptaccount_ctrl.wd_as_search_deptaccount" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsMain = new DataSourceTool();
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            }
        }
       
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
   
    <uc2:DsMain ID="dsMain" runat="server" />
    <br />
    <uc1:DsList ID="dsList" runat="server" />
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <br />
</asp:Content>