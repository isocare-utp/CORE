<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_rdate_billpayment.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span style="width: 127px;">ตั้งแต่วันที่:</span>
                        <asp:TextBox ID="start_date" runat="server" Style="width: 130px;"></asp:TextBox>
                        <asp:TextBox runat="server" Style="width: 10px; border: 0 none black;" Text="-"></asp:TextBox>
                        <asp:TextBox ID="end_date" runat="server" Style="width: 130px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="bank_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <center>
            <div>
                <asp:CheckBox ID="chk_bankall" runat="server" /><span style="width: 127px;">&nbsp ไม่ระบุธนาคาร</span>
                <asp:HiddenField ID="reportchk" runat="server" />
            </div>
        </center>
    </EditItemTemplate>
</asp:FormView>
