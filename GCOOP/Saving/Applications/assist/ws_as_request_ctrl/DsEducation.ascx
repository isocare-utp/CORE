<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsEducation.ascx.cs"
    Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsEducation" %>
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
                <td>
                    <span>ชื่อบุตร:</span>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="ass_rcvname" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เลขที่บัตรบุตร:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ass_rcvcardid" runat="server" OnKeyPress="return chkNumber(this)" MaxLength="13"></asp:TextBox>
                </td>
                <tr>
                    <td>
                        <div>
                            <span>วันเกิดบุตร:</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="edu_childbirthdate" runat="server" Style="width: 45%; text-align: center;"></asp:TextBox>
                        <asp:TextBox ID="childage_th" runat="server" Style="width: 50%; text-align: center;"
                            ReadOnly="true" BackColor="#DDDDDD"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <div>
                            <span>เลขบัตรผู้ปกครอง(ที่ไม่ใช่ผู้ขอทุน):</span>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="ass_prcardid" runat="server" OnKeyPress="return chkNumber(this)"
                                Style="text-align: center;" MaxLength="13"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ชื่อสถานศึกษา:</span>
                        </div>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="edu_school" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>ระดับชั้น:</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="edu_levelcode" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>เกรดเฉลี่ย:</span>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="edu_gpa" runat="server" ToolTip="#.00" OnKeyPress="return chkNumber(this)"
                                Style="text-align: center;"></asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <div>
                            <span>เงื่อนไขการจ่าย:</span>
                        </div>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="assistpay_code" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 12%">
                    </td>
                    <td style="width: 25%">
                    </td>
                    <td style="width: 24%">
                    </td>
                    <td style="width: 12%">
                    </td>
                    <td style="width: 25%">
                    </td>
                </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
