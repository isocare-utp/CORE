<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_collredeem_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="16%">
                    <div>
                        <span>เลขที่ไถ่ถอน:</span></div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="redeem_docno" runat="server" ReadOnly="True" Style="background-color: Yellow;
                            text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>วันที่บันทึกไถ่ถอน:</span></div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="redeem_date" runat="server" Style="width: 110px; text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนหลักทรัพย์:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="collmast_no" runat="server" Style="width: 100px; text-align: center;"></asp:TextBox>
                        <asp:Button ID="b_collmast" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                    </div>
                </td>
                <td>
                    <div>
                        <span>เลขที่หลักทรัพย์:</span></div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="collmast_refno" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>วันที่ค้ำประกัน:</span></div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="MORTGAGE_DATE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนสมาชิก:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span></div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="MEMBNAME" runat="server" Style="width: 345px"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td width="60%">
                    รายละเอียดหลักทรัพย์:
                </td>
                <td width="40%">
                    มูลค่าค้ำประกัน:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_desc" runat="server" TextMode="MultiLine" Height="75px"
                        Width="435px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="MORTGAGE_PRICE" runat="server" TextMode="MultiLine" Height="75px"
                        Width="285px" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td>
                    เหตุผลการไถ่ถอน:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="redeem_cause" runat="server" TextMode="MultiLine" Height="75px"
                        Width="725px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
