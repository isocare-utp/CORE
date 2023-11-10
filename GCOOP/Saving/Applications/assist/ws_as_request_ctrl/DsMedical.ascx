<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMedical.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsMedical" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>สาเหตุการป่วย:</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="med_cause" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>รักษาที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="med_hpname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 13%">
                    <div>
                        <span>วันที่รักษา:</span>
                    </div>
                </td>
                <td style="width: 13%">
                    <asp:TextBox ID="med_startdate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="width: 3%">
                    <span style="text-align: center;">-</span>
                </td>
                <td style="width: 13%">
                    <asp:TextBox ID="med_enddate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td style="width: 10%">
                    <asp:TextBox ID="med_day" runat="server" Style="width: 50%; text-align: center;"
                        ReadOnly="true" BackColor="#DDDDDD"></asp:TextBox>
                    <div>
                        <span style="width: 65%; text-align: center;">คืน</span>
                    </div>
                </td>
                <td style="width: 13%">
                    <div>
                        <span>ประเภทการจ่าย:</span>
                    </div>
                </td>
                <td style="width: 37%">
                    <asp:DropDownList ID="assistpay_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
