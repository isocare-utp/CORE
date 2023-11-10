<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_requestment_lite_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBER_NO" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อ สกุล</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่ใบขอกู้</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="LOANREQUEST_DOCNO" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>ประเภทกู้</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="LOANTYPE_CODE" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>วันที่ขอกู้</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="LOANREQUEST_DATE" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดขอกู้</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="LOANREQUEST_AMT" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
