<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dsmain.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_close_month_ctrl.Ds" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
    <table class="DataSourceFormView">
    <tr>
        <td width="10%">
            <div>
                <span>เดือน:</span>
            </div>
        </td>
        <td width="20%">
            <div align="center">
                <asp:TextBox ID="month" runat="server" style="text-align:center;"></asp:TextBox>
            </div>
        </td>
        <td width="10%">
            <div>
                <span>ปี:</span>
            </div>
        </td>
        <td width="20%">
            <div>
                <asp:TextBox ID="year" runat="server" style="text-align:center;"></asp:TextBox>
            </div>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div>
                <span>ตั้งแต่วันที่:</span>
            </div>
        </td>
        <td>
            <div>
                <asp:TextBox ID="start_date" runat="server" style="text-align:center;"></asp:TextBox>
            </div>
        </td>
        <td>
            <div>
                <span>ถึงวันที่:</span>
            </div>
        </td>
        <td>
            <div>
                <asp:TextBox ID="end_date" runat="server" style="text-align:center;"></asp:TextBox>
            </div>
        </td>
        <td>
            <div>
                <asp:Button ID="Button1" runat="server" Text="ประมวลผล" style="width:100px;" />
            </div>
        </td>
    </tr>
</table>
    </EditItemTemplate>
</asp:FormView>
