<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsHeader.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsHeader" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width='20%'>
                    <div>
                        <span>File : </span>
                    </div>
                </td>
                <td width='40%'>
                    <div>
                        <asp:HiddenField ID="ls_filename" runat="server" />                     
                    </div>
                </td>
                <td width='20%'>
                    <div>
                        <span>วันที่ทำรายการ : </span>
                    </div>
                </td>
                <td width='20%'>
                    <div>
                        <asp:TextBox ID="ls_effdate" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="ls_effdate_" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ธนาคาร : </span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="ls_bank" runat="server" ReadOnly="true" Style="width: 98%;"></asp:TextBox>
                        <asp:HiddenField ID="ls_bank_code" runat="server" />
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:TextBox ID="ls_company" runat="server" ReadOnly="true" Style="width: 98%;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
