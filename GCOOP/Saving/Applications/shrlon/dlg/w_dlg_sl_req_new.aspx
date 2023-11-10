<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_req_new.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_req_new" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<%--<%@ Register Src="../../../CustomControl/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เพิ่มการลงรับหนังสือกู้เงิน</title>
    <%=jsPostMember%>
    <%=jsSavesheet%>
    <%=jsCheckduplicate%>
    <script type="text/javascript">

        function OnDwDetailChanged(s, r, c, v) {
          
            if (c == "member_no") {
                Gcoop.GetEl("Hfmember_no").value = s.GetItem(r, "member_no");
                jsPostMember();
            }else if (c == "loantype_code" || c == "loantype_code_1") {
               // alert("vv" + V);
                objDw_detail.SetItem(r, c, v);
                objDw_detail.AcceptText();
               
                jsCheckduplicate();

            }else if (c == "loanentry_id" || c == "loanentry_id_1") {
                // alert("vv" + V);
                objDw_detail.SetItem(r, c, v);
                objDw_detail.AcceptText();

                jsCheckduplicate();

            }
        }

        function OnDwDetailClick(s, r, c) {
            if (c == "save") {
                objDw_detail.AcceptText();
                jsSavesheet();
            }
           
        }

    </script>
</head>
<body>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <form id="form1" runat="server">
    <%--<uc1:DatePicker ID="datePicker" runat="server" />--%>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_sl_lnreqregister_update"
                    LibraryList="~/DataWindow/shrlon/sl_req_loanregister.pbl" ClientScriptable="True"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventItemChanged="OnDwDetailChanged" ClientFormatting="True" TabIndex="100"
                    ClientEventClicked="OnDwDetailClick" ClientEvents="True">
                </dw:WebDataWindowControl>
                <asp:Panel ID="Panel3" runat="server" Height="200px" Width="500px">
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hflnentry_id" runat="server" />
    </form>
</body>
</html>
