<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCbt.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_opr_exp_textfile_ctrl.DsCbt" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 450px;">
            <tr>
                <td width="35%">
                    <div>
                        <span>ธนาคาร:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="bank_code" runat="server" Style="width: 280px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่บัญชีธนาคาร:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="bank_accid" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อบริษัท(ธนาคารกำหนด):</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="bank_name" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>TR Type:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="transfer_type" runat="server">
                            <asp:ListItem Value="D" Selected="True">DR</asp:ListItem>
                            <asp:ListItem Value="C">CR</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>TR Code:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="transfer_code" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="year" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เดือน:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="month" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">มกราคม</asp:ListItem>
                        <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                        <asp:ListItem Value="4">เมษายน</asp:ListItem>
                        <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="9">กันยายน</asp:ListItem>
                        <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                        <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่ทำรายการ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
