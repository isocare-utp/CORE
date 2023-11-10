<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsFamilydecease.ascx.cs"
    Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsFamilydecease" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<script language="JavaScript">
        function chkNumber(ele) {
            var vchar = String.fromCharCode(event.keyCode);
            if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
            ele.onKeyPress = vchar;
        }
</script>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 20%">
                    <span>วันที่ตามเอกสารราชการ:</span>
                </td>
                <td style="width: 20%">
                    <asp:TextBox ID="fam_docdate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="width: 20%">
                    <div>
                        <span>ชื่อสมาชิกครอบครัว:</span>
                    </div>
                </td>
                <td style="width: 40%">
                    <asp:TextBox ID="ass_rcvname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขที่บัตรประชาชน:</span>
                </td>
                <td>
                    <asp:TextBox ID="ass_rcvcardid" runat="server" OnKeyPress="return chkNumber(this)" MaxLength="13"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เงื่อนไขการจ่าย:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="assistpay_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
