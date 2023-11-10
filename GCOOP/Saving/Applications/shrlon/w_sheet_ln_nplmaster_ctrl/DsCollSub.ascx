<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollSub.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsCollSub" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 330px;">
            <tr>
                <td width="30%">
                    <div>
                        <span>เลขที่</span>
                    </div>
                </td>
                <td width="70%">
                    <div>
                        <asp:TextBox ID="REF_COLLNO" runat="server" Style="background-color: #E7E7E7; text-align: center;"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันจำนอง</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mortgage_date" runat="server" Style="background-color: #E7E7E7;
                            text-align: center;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคาจำนอง</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mortgage_price" runat="server" Style="background-color: #E7E7E7;
                            text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สกส. ประเมิน</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="estimate_price" runat="server" Style="background-color: #E7E7E7;
                            text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จพค. ประเมิน</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="jpk_estprice" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันขาย</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="sale_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคาขาย</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="sale_price" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปัญหา/อุปสรรค์</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="obscacles" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ผู้รับผิดชอบ</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="responsible" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งานพิมพ์</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="printing" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การยึดทัพย์</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="confiscation" runat="server" Style="width: 70px;"></asp:TextBox>
                        <span style="width: 68px;">วันยึดทรัพย์</span>
                        <asp:TextBox ID="confiscation_date" runat="server" Style="width: 72px; text-align: center" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <fieldset>
            <legend style="font-family: Tahoma; font-size: 14px;">การเบิกเอกสารดำเนินคดี</legend>
            <asp:TextBox ID="withdraw_judgedoc" runat="server" TextMode="MultiLine" Style="width: 330px;
                height: 48px; border: none;"></asp:TextBox>
        </fieldset>
    </EditItemTemplate>
</asp:FormView>
