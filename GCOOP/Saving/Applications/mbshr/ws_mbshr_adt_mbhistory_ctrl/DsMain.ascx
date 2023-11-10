<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_adt_mbhistory_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" >
            <tr>
                <td width="15%">
                    <div>
                        <span>วันทำรายการ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="start_date" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>ถึง :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="end_date" runat="server"></asp:TextBox>
                </td>
                <td rowspan="2">
                    <asp:Button ID="btn_search" runat="server" Text="ค้นหา" Height="45" Width="45" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน :
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="doc_no" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ผู้ทำรายการ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="user_id" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
