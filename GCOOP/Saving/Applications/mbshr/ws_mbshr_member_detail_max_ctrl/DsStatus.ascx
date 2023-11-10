<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsStatus.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsStatus" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span style="font-size: 13px;"><font color="#cc0000"><u><strong>สถานะ</strong></u></font></span>
        <table class="FormStyle">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td width="30%">
                                ประเภทการสมัคร:
                            </td>
                            <td width="70%">
                                <asp:TextBox ID="appltype_desc" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                สถานะออกใบเสร็จ:
                            </td>
                            <td>
                                <asp:TextBox ID="cp_pausekeep_flag" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ตั้งแต่วันที่:
                            </td>
                            <td>
                                <asp:TextBox ID="pausekeep_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%" style="border: 1px solid #C4C4C4">
                        <tr>
                            <td width="50%">
                                สถานะการกู้/ค้ำ
                            </td>
                            <td width="50%">
                                สถานะประกอบการกู้
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="droploanall_flag" runat="server" Text=" งดกู้ทุกประเภท" Enabled="false" />
                            </td>
                            <td>
                                <asp:CheckBox ID="klongtoon_flag" runat="server" Text=" กองทุนสำรองเลี้ยงซีพ" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="dropgurantee_flag" runat="server" Text=" งดค้ำทุกประเภท" Enabled="false" />
                            </td>
                            <td>
                                <asp:CheckBox ID="lntransright_flag" runat="server" Text=" หนังสือโอนสิทธิเรียกร้อง"
                                    Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="lnallowloan_flag" runat="server" Text=" ใบยินยอมคู่สมรส" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="have_gain" runat="server" Text=" มีผู้รับโอนประโยชน์" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
