<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_addassisttype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px">
            <tr>
                <td colspan="2">
                    <strong><u>โครงการสวัสดิการประเภทใหม่</u></strong>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <div>
                        <span>รหัสสวัสดิการ:</span>
                    </div>
                </td>
                <td style="width: 80%">
                    <asp:TextBox ID="assisttype_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
<%--                <td style="width: 20%">
                    <div>
                        <span>ตัวย่อสวัสดิการ:</span>
                    </div>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="ass_prefix" runat="server" Style="text-align: center"></asp:TextBox>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสวัสดิการ:</span>
                    </div>
                </td>
                <td >
                    <asp:TextBox ID="assisttype_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>กลุ่มสวัสดิการ:</span>
                    </div>
                </td>
                <td >
                    <asp:DropDownList ID="assisttype_group" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <br />
                    &nbsp;
                    <asp:Button ID="b_add" runat="server" Text="ตกลง" Width="70px" />&nbsp;
                    <asp:Button ID="b_cancel" runat="server" Text="ยกเลิก" Width="70px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
