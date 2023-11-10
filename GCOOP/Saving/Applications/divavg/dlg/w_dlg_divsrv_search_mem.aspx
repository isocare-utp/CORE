<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_divsrv_search_mem.aspx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_divsrv_search_mem" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
    <%=postSearch %>
    <%=postNewClear %>
     <script type="text/javascript">

         function OnDwMainItemChange(s, r, c, v) {
             if (c == "branch_id") {
                 objDw_main.SetItem(r, "branch_id", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hdbranch_id").value = v;
             }
             else if (c == "smembtype_code") {
                 objDw_main.SetItem(r, "smembtype_code", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("hd_smembtype_code").value = v;
             }
             else if (c == "emembtype_code") {
                 objDw_main.SetItem(r, "emembtype_code", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("hd_emembtype_code").value = v;
             }
             else if (c == "smembgroup_code") {
                 objDw_main.SetItem(r, "smembgroup_code", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hd_smembgroup_code").value = v;
             }
             else if (c == "emembgroup_code") {
                 objDw_main.SetItem(r, "emembgroup_code", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hd_emembgroup_code").value = v;
             }
             else if (c == "smember_no") {
                 objDw_main.SetItem(r, "smember_no", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hd_smember_no").value = v;
             }
             else if (c == "emember_no") {
                 objDw_main.SetItem(r, "emember_no", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hd_emember_no").value = v;
             }
             else if (c == "memb_name") {
                 objDw_main.SetItem(r, "memb_name", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hd_memb_name").value = v;
             }
             else if (c == "memb_ename") {
                 objDw_main.SetItem(r, "memb_ename", v);
                 objDw_main.AcceptText();
                 Gcoop.GetEl("Hd_memb_ename").value = v;
             }
         }

         function OnDwMainButtonClick(s, r, b) {
             if (b == "b_ok") {
                 objDw_main.AcceptText();
                 postSearch();
             }
             else if (b == "b_cancel") {
                 postNewClear();
             }
         }

         function OnDwDetailClick(s, r, c) {
             var member_no = objDw_detail.GetItem(r, "member_no");
             parent.GetMemberNoFromDialog(member_no);
             parent.RemoveIFrame();
         }
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_divsrv_search_mem_option" 
                        LibraryList="~/DataWindow/divavg/divsrv_search_mem.pbl" 
                        ClientEventButtonClicked="OnDwMainButtonClick" ClientEventItemChanged="OnDwMainItemChange">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_divsrv_search_mem_list" 
                        
    LibraryList="~/DataWindow/divavg/divsrv_search_mem.pbl" ClientEventClicked="OnDwDetailClick" 
                            RowsPerPage="10">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hidden_search" runat="server"/>
                    <asp:HiddenField ID="Hdbranch_id" runat="server" />
                    <asp:HiddenField ID="hd_smembtype_code" runat="server" />
                    <asp:HiddenField ID="hd_emembtype_code" runat="server" />
                    <asp:HiddenField ID="Hd_smembgroup_code" runat="server" />
                    <asp:HiddenField ID="Hd_emembgroup_code" runat="server" />
                    <asp:HiddenField ID="Hd_smember_no" runat="server" />
                    <asp:HiddenField ID="Hd_emember_no" runat="server" />
                    <asp:HiddenField ID="Hd_memb_name" runat="server" />
                    <asp:HiddenField ID="Hd_memb_ename" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

