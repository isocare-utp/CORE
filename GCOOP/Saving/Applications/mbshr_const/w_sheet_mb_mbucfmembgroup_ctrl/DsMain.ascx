<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfmembgroup_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <tr>
                    <td>
                        <u><b>รายละเอียด</b></u>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <div>
                            <span>รหัสหน่วย :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="membgroup_code" runat="server" Style="width: 185px; text-align: center;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20px;" />
                    </td>
                    <td width="20%">
                        <div>
                            <span>ชื่อหน่วย :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>หน่วยคุม :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="membgroup_control" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>หน่วยลูกหนี้ตัวแทน :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="membgroup_agentgrg" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>กลุ่มขึ้นแผ่นจัดเก็บ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="savedisk_type" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </tr>
            <tr>
                <tr>
                    <td>
                        <u><b>ที่อยู่</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>เลขที่ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>หมู่ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_moo" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ถนน :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_road" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>ซอย :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_soi" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ตำบล :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_tambol" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>อำเภอ :</span>
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
                            <span>จังหวัด :</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="addr_province" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div>
                            <span>รหัสไปรษณีย์ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_postcode" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>โทรศัพย์ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_phone" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>โทรสาร :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_fax" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
