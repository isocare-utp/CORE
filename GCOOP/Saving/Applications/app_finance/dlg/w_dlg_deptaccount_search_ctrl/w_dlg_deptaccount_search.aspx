<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_deptaccount_search.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_deptaccount_search_ctrl.w_dlg_deptaccount_search" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     var dsMain = new DataSourceTool();

     function OnDsMainClicked(s, r, c) {
         var deptno = dsMain.GetItem(r, "account_no");
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
<table>
        <tr>
            <td>
                <uc1:dsMain ID="dsMain" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
