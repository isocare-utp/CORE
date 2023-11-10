<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>รหัสสังกัด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center" ReadOnly=true></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อสังกัด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_desc" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>รหัสคุมหุ้นหนี้ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBGROUP_CONTROL" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ส่วน/ชื่อย่อ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBGROUP_AGENTGRG" runat="server" Style="text-align: center" Width="133px"></asp:TextBox>
                        <asp:TextBox ID="MANAGER_GROUP" runat="server" Style="text-align: center" Width="214px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>รหัสคุมกองทุน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="DISTRICTGROUP_CODE" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>รหัสหน่วยคุม :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="FUNDGROUP_CODE" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_no" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หมู่ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_moo" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ซอย :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_soi" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ถนน :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_road" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จังหวัด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="addr_province" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>เขต/อำเภอ :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="addr_amphur" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>แขวง/ตำบล :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="addr_tambol" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>รหัสไปรษณีย์ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_postcode" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>โทรศัพท์ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_phone" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>FAX :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="addr_fax" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสภูมิภาค :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="area_code" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ค่าพาหนะ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="money_etc" runat="server" Style="text-align: left"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
