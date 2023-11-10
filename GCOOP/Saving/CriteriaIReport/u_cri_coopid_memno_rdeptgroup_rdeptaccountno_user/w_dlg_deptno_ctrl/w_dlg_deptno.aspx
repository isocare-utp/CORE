<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_deptno.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.w_dlg_deptno_ctrl.w_dlg_deptno" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       function OnDsMainItemChanged(s, r, c, v) {
           
       }
       function OnDsMainClicked(s, r, c, v) {

           if (c == "b_choose") {

               var g_deptno = "";
               for (var ii = 0; ii < dsList.GetRowCount(); ii++) {
                   if (dsList.GetItem(ii, "choose_flag") == 1) {
                       if (g_deptno != "") {
                           g_deptno = g_deptno + ",'" + dsList.GetItem(ii, "deptaccount_no") + "'";
                       } else {
                           g_deptno = "'" + dsList.GetItem(ii, "deptaccount_no") + "'";
                       }
                   }
               }
               if (g_deptno != "") {
                   dsMain.SetItem(0, "deptno", g_deptno);
               }

           } else if (c == "b_confrim") {
               var deptno = dsMain.GetItem(r, "deptno");
               try {
                   window.opener.GetDepnoFromDlg(deptno);
                   window.close();
               } catch (err) {
                   parent.GetDepnoFromDlg(deptno);
                   parent.RemoveIFrame();
               }
           } else if (c == "b_cancel") {
               parent.RemoveIFrame();
           }
       }
       function OnDsListClicked(s, r, c) {                
       }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">   
    <table>
        <tr>
            <td>
                <uc2:DsList ID="dsList" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:DsMain ID="dsMain" runat="server" />
            <//td>
        </tr>
    </table>
</asp:Content>

