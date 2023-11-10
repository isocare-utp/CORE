<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_booknew.aspx.cs"
    Inherits="Saving.Applications.mbshr.dlg.w_dlg_dp_booknew" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ระบบเงินฝาก</title>
    <%=newClear%>
    <%=postSubmit%>
    <style type="text/css">
        .buttonWidth
        {
            width: 60px;
        }
    </style>
    <script type="text/javascript">
        function DialogLoadComplete() {

            try {
                var closeDlg = Gcoop.GetEl("HdCloseDlg").value;
                if (closeDlg == "true") {
                    window.close();
                    //parent.RemoveIFrame();
                    var newBookNo = objDwMain.GetItem(1, "as_bookno"); //Gcoop.GetEl("").value;
                    parent.UpNewBookFinish(newBookNo);
                }
            } catch (err) { }
            //Gcoop.GetEl("ok").focus();
            //Gcoop.GetEl("as_bookno").focus();//.SetFocus("as_bookno");
            //Gcoop.SetLastFocus("as_bookno_0");
            Gcoop.Focus("as_bookno_0");
        }

        function ok_onclick() {
            postSubmit();
        }

        function cancel_onclick() {
            window.close();
            parent.RemoveIFrame();
        }

        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "as_bookreson" || columnName == "as_bookno") {
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.AcceptText();
                //filterBookNO();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table>
            <tr>
                <td valign="top">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_dp_booknew"
                        LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" HorizontalScrollBar="NoneAndClip"
                        VerticalScrollBar="NoneAndClip" UseCurrentCulture="True" AutoRestoreContext="False"
                        AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientFormatting="True"
                        ClientEventItemChanged="OnDwMainItemChanged">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <input id="ok" class="buttonWidth" type="button" value="พิมพ์" onclick="return ok_onclick()" />
                    &nbsp;
                    <input id="cancel" class="buttonWidth" type="button" value="ยกเลิก" onclick="return cancel_onclick()" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:HiddenField ID="HdDeptAccountNo" runat="server" />
    <asp:HiddenField ID="HdCloseDlg" runat="server" />
    <asp:HiddenField ID="HdLastSeqNo" Value="" runat="server" />
    </form>
</body>
</html>
