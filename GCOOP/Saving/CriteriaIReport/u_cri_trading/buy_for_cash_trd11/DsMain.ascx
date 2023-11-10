<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_trading.buy_for_cash_trd11.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<center>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium">	[TRADING11] - รายงานขายเชื่อ</asp:Label>
</center>
<br />
<script type="text/javascript">
      $(function () {
        $('input[name="ctl00$ContentPlace$dsMain$FormView1$memb_no1"]').attr("maxlength", 6);
        $('input[name="ctl00$ContentPlace$dsMain$FormView1$memb_no2"]').attr("maxlength", 6);        
    });
</script>
</script>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="30%">
                    <div align="right">
                        <span>ตั้งแต่วันที่ : </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="date_start" runat="server"></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="date_end" runat="server"></asp:TextBox>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div align="right">
                        <span>ตั้งแต่รหัสลูกค้า : </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_no1" runat="server"></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="memb_no2" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
