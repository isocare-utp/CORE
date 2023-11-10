<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_monthpay.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanrequest_monthpay" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เงินเดือนคงเหลือ</title>
    <%=closeWebDialog%>
    <script type="text/javascript">
        function DialogLoadComplete() {
            var chkStatus = Gcoop.GetEl("HfChkStatus").value;
            if (chkStatus == "1") {
                try {
                    Gcoop.GetEl("HfChkStatus").value = "";
                    if (confirm("ต้องการปิดหน้าจอนี้ ")) {
                        parent.CloseDLG();
                    }

                } catch (Err) {
                    alert("Error Dlg");
                    //window.close();
                    parent.RemoveIFrame();
                }
                //            window.opener.CloseDLG();
                //            window.close();
            }
        }
        function OnCloseClick() {
            //        parent.removeIFrame();
            //        alert(".........");
            //closeWebDialog();
            parent.RemoveIFrame();
        }
    </script>
    <style type="text/css">
        .newStyle1
        {
            font-family: Tahoma;
            font-size: 12px;
            color: #008080;
            float: right;
        }
        .style1
        {
            width: 151px;
        }
        .newStyle2
        {
            float: left;
            color: Red;
            font-weight: bold;
            font-family: tahoma;
            font-size: 12px;
        }
        .newStyle3
        {
            font-family: tahoma;
            font-size: 12px;
            font-weight: bold;
            float: left;
        }
        .newStyle4
        {
            font-family: tahoma;
            font-size: 12px;
            color: #008080;
            float: none;
            clip: rect(auto, 15px, auto, 15px);
        }
        .newStyle5
        {
            font-family: tahoma;
            font-size: 20px;
            background-color: Green;
            color: Yellow;
            font-weight: bold;
            right: 15px;
            left: 15px;
        }
        .newStyle6
        {
            float: left;
            color: Red;
            font-weight: bold;
            font-family: tahoma;
            font-size: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label5" Visible="false" runat="server" Text="เปอร์เซ็นเงินเดือน" CssClass="newStyle4"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label3" runat="server" Text="เงินเดือน ::" CssClass="newStyle1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="t_salaryamt" Width="90px" runat="server" CssClass="newStyle3" Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td>
                    </tr>
            </td>
            <tr>
                <td class="style1">
                    <asp:TextBox ID="TextBox1" Visible="false" Width="50px" runat="server" CssClass="newStyle5"></asp:TextBox>
                </td>
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" Text="เงินเดือนคงเหลือขั้นต่ำ ::" CssClass="newStyle1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="t_basesalbal" Width="90" runat="server" CssClass="newStyle3" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" Visible="false" runat="server" Text="คำนวน" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label4" runat="server" Text="ยอดหักจากสหกรณ์ ::" CssClass="newStyle1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="t_paymonthcoop" Width="90px" runat="server" CssClass="newStyle3" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label2" runat="server" Text="เงินเดือนคงเหลือ ::" CssClass="newStyle1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="t_salbal" Width="90px" runat="server" CssClass="newStyle6" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
    </table>
    </td> </tr>
    <tr>
        <td>
            <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_sl_loanrequest_monthpay"
                LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" AutoRestoreContext="False"
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                Style="top: 0px; left: 0px">
            </dw:WebDataWindowControl>
        </td>
    </tr>
    <tr>
        <td>
            <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_loanrequest_monthpaydet"
                LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl" AutoRestoreContext="False"
                AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                Style="top: 0px; left: 0px">
            </dw:WebDataWindowControl>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="HfChkStatus" runat="server" />
            <input id="btnClose" type="button" value="ปิด" onclick="OnCloseClick()" />
            <%--<asp:Button ID="btnClose" runat="server" Text="ปิด" onclick="btnClose_Click" />--%>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
