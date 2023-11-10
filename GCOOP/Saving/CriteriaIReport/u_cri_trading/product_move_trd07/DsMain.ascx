<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_trading.product_move_trd07.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<center>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium">[TRADING07] - รายการเคลื่อนไหวของสินค้า</asp:Label>
</center>
<br />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="30%">
                    <div align="right">
                        <span>ตั้งแต่รหัสสินค้า : </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="as_product_no_start" runat="server"></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="as_product_no_end" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div align="right">
                        <span>ตั้งแต่วันที่ : </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="sdate" runat="server"></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="edate" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
