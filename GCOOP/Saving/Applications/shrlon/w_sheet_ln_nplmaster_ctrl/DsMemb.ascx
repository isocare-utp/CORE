<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMemb.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsMemb" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span>เลขทะเบียน</span>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 66px;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Style="width: 27px;" Text="..." />
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อ - นามสกุล</span>
                    </div>
                </td>
                <td width="22%">
                    <div>
                        <asp:TextBox ID="compute_1" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>เลขประจำตัว ปช.</span>
                    </div>
                </td>
                <td width="22%">
                    <div>
                        <asp:TextBox ID="card_person" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="compute_2" runat="server" ReadOnly="true" Style="background-color: #E7E7E7;
                            width: 98%;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>จังหวัด</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="province_desc" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ไปรษณีย์</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="postcode" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>โทรศัพท์(มือถือ)</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mem_telmobile" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>โทรศัพท์(บ้าน)</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mem_tel" runat="server" ReadOnly="true" Style="background-color: #E7E7E7"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
