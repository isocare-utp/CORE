<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurfire_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="6">
                    <b><u>บันทึกเบี้ยประกันอัคคีภัย</u></b>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 90px;"></asp:TextBox>
                        <asp:Button ID="b_memsearch" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="NAME" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBTYPE_DESC" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด:</span></div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สถานะ:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="status" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />     
    </EditItemTemplate>
</asp:FormView>
