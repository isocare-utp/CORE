<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_search_voucher_full.aspx.cs" 
Inherits="Saving.Applications.account.dlg.w_dlg_search_voucher_full" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<%=jsPostSelectList%>
<%=jsPostVoucherDate%>
<script type="text/javascript">
    function OnDwDateItemChange(s, r, c, v)//เปลี่ยนวันที่
    {
        if (c == "voucher_tdate") {
            if (v == "" || v == null) {
                alert("กรุณากรอกข้อมูลวันที่ Voucher")
            }
            else {
                s.SetItem(1, "voucher_tdate", v);
                s.AcceptText();
                s.SetItem(1, "voucher_date", Gcoop.ToEngDate(v));
                s.AcceptText();
                jsPostVoucherDate();
            }
        }
        return 0;
    }



    function OnDwListClick(s, r, c) {
        if (c == "voucher_no" || c == "voucher_desc") {
            var vcNo = objDwList.GetItem(r, "voucher_no");
            if (vcNo != null && vcNo != "") {
                Gcoop.GetEl("HdVoucherNo").value = Gcoop.Trim(vcNo);
                Gcoop.GetEl("HdRowListClick").value = r + "";
                parent.SelectVoucher(voucher_no);
                parent.RemoveIFrame();
            }
        }
    }

    function OnClick(sender, rowNumber, objectName) {
        try {
            var voucher_no = objDwList.GetItem(rowNumber, "voucher_no");
            var isConfirm = confirm("ต้องการเลือก  " + Gcoop.Trim(voucher_no) + " ใช่หรือไม่");

            if (isConfirm) {
                window.opener.GetValueDlg(voucher_no);
                window.close();
            }

        }
        catch (ex) {
            alert("Error for get value ");
        }
    }

</script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td valign="top">
                    <%--<asp:Panel ID="Panel3" runat="server" Height="50px" Width="230px" BorderStyle="Ridge">--%>
                    <span>
                        <dw:WebDataWindowControl ID="DwDate" runat="server" DataWindowObject="d_dlg_vcdate"
                            LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwDateClicked"
                            ClientScriptable="True" ClientEventItemChanged="OnDwDateItemChange" ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </span>
                    <%--</asp:Panel>--%>
                </td>
                <td valign="top">
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <b>รายการ Voucher</b>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <span>
                        <asp:Panel ID="Panel2" runat="server" Height="510px" Width="930px" ScrollBars="Vertical"
                            BorderStyle="Ridge">
                            <span>
                                <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="dlg_vc_vcedit_vclist"
                                    LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                                    ClientEventClicked="OnClick" ClientEventButtonClicked="OnDwListEditClick"
                                    Width="900px">
                                </dw:WebDataWindowControl>
                            </span>
                        </asp:Panel>
                    </span>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HdVoucherNo" runat="server" Value="" />
        <asp:HiddenField ID="HdRowListClick" runat="server" />
    </div>
    </form>
</body>
</html>
