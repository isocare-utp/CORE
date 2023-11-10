<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_getmembno_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table style="width: 400px;">
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 300px;">
                        <tr>
                            <td colspan="3">
                                <b><u>แก้ไขทะเบียนล่าสุด</u></b>
                            </td>
                        </tr>
                        <tr>
                            <th width="40%">
                            </th>
                            <th width="30%">
                                เลขล่าสุด
                            </th>
                            <th width="30%">
                                ทะเบียนล่าสุด
                            </th>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 300px;">
                        <tr>
                            <td width="40%">
                                <div>
                                    <span>เลขสมาชิกปกติ :</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="last_documentno" runat="server" Style="text-align: center"></asp:TextBox>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="last_documentno2" runat="server" ReadOnly="true" BackColor="#DDDDDD"
                                    Style="text-align: center"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <div>
                                    <span>เลขสมาชิกสมทบ :</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="last_documentno_cono" runat="server" Style="text-align: center"></asp:TextBox>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="last_documentno_cono2" runat="server" ReadOnly="true" BackColor="#DDDDDD"
                                    Style="text-align: center"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 100px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="b_save" runat="server" Text="บันทึก" Style="width: 80px;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูลใหม่" Style="width: 80px;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
