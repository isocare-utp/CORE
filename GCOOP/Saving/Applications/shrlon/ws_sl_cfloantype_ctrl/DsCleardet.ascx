<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCleardet.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsCleardet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="2">
                    <strong><u>เงินกู้ที่ต้องหักกลบ</u></strong>
                </td>
                <td width="15%">
                    <strong><u>ซื้อหุ้น</u></strong>
                </td>
                <td width="42%">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="clcfstrcvonly_flag" runat="server" />
                    หักกลบเฉพาะการจ่ายเงินกุ้งวดแรกเท่านั้น
                </td>
                <td>
                    <div>
                        <span>หุ้นขั้นต่ำเทียบกับ:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="shrstk_buytype" runat="server">
                        <asp:ListItem Value="0">ไม่ตรวจสอบ</asp:ListItem>
                        <asp:ListItem Value="1">ยอดขอกู้</asp:ListItem>
                        <asp:ListItem Value="2">ยอดหนี้ทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="3">ยอดกลุ่มเงินกู้</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="clccclworksht_flag" runat="server" />
                    หักกลบต้องยกเลิกกระดาษทำการ
                </td>
                <td colspan="2">
                    <strong><u>การนับวงเงินกู้สำหรับเปรียบเทียบซื้อหุ้น</u></strong>
                </td>
            </tr>
            <tr>
                <td width="18%">
                    <div>
                        <span>หักดอกเบี้ยรับล่วงหน้า:</span></div>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="lnrcvclrfuture_type" runat="server">
                        <asp:ListItem Value="0">ไม่หักด/บรับล่วงหน้า</asp:ListItem>
                        <asp:ListItem Value="1">หักดอกเบี้ยรับล่วงหน้าเฉพาะงวดแรก</asp:ListItem>
                        <asp:ListItem Value="2">หักดอกเบี้ยรับล่วงหน้าทุกงวด</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>การนับวงเงินกู้:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="shrstkcount_flag" runat="server">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="0">ไม่นับ</asp:ListItem>
                        <asp:ListItem Value="1">นับ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
