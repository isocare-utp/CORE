<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_ln_nplsue_moneyadv_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
        <EditItemTemplate>
            <table class="DataSourceFormView" style="width: 400px;">
                <tr>
                    <td colspan="2">
                        <strong style="font-size: 14px;">รายการตั้งลูกหนี้มีปัญหา</strong>
                    </td>
                </tr>
                <tr>
                    <td width="40%">
                        <div>
                            <span>ตั้งเป็นประเภทลูกหนี้:</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="contlaw_status" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ตั้งเงินยืมทดรองคดี:</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="mavset_bal" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
    </asp:FormView>
</div>
