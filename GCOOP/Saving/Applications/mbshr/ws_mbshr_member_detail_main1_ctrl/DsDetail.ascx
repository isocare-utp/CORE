<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span style="font-size: 12px;"><font color="#cc0000"><u><strong>ที่อยู่</strong></u></font></span>
        <table class="FormStyle">
            <tr>
                <td width="15%">
                    วันเกิด:
                </td>
                <td width="35%">
                    <asp:TextBox ID="birth_date" runat="server" Style="text-align: center; width: 160px;"
                        ReadOnly="true" CssClass="FormStyle"></asp:TextBox>
                    <asp:TextBox ID="age" runat="server" ReadOnly="true" Style="width: 79px; text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    บัตรประชาชน:
                </td>
                <td width="35%">
                    <asp:TextBox ID="card_person" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ที่อยู่ตามทะเบียน:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_addr" runat="server" ReadOnly="true" TextMode="MultiLine" Height="50px"
                        Width="612px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ที่อยู่ปัจจุบัน:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_curraddr" runat="server" ReadOnly="true" TextMode="MultiLine"
                        Height="50px" Width="612px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="FormStyle">
            <tr>
                <td colspan="2">
                    <span style="font-size: 13px;"><font color="#cc0000"><u><strong>ข้อมูลสมาชิกสหกรณ์</strong></u></font></span>
                </td>
                <td colspan="2">
                    <span style="font-size: 13px;"><font color="#cc0000"><u><strong>ข้อมูลการทำงาน</strong></u></font></span>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    วันเป็นสมาชิก:
                </td>
                <td width="35%">
                    <asp:TextBox ID="member_date" runat="server" Style="text-align: center; width: 160px;"
                        ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="memb_age" runat="server" ReadOnly="true" Style="width: 79px; text-align: center;"></asp:TextBox>
                </td>
                <td width="15%">
                    วันบรรจุ:
                </td>
                <td width="35%">
                    <asp:TextBox ID="work_date" runat="server" Style="text-align: center; width: 160px;"
                        ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="work_age" runat="server" ReadOnly="true" Style="width: 79px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    วันที่ลาออก:
                </td>
                <td>
                    <asp:TextBox ID="resign_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    ตำแหน่ง:
                </td>
                <td>
                    <asp:TextBox ID="position_desc" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    วันที่ปิดบัญชี:
                </td>
                <td>
                    <asp:TextBox ID="close_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    เลขที่พนักงาน:
                </td>
                <td>
                    <asp:TextBox ID="salary_id" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    เลขสมาชิกเดิม:
                </td>
                <td>
                    <asp:TextBox ID="member_ref" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    เงินเดือน:
                </td>
                <td>
                    <asp:TextBox ID="salary_amount" runat="server" ReadOnly="true" Style="text-align: center;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    สาเหตุลาออก:
                </td>
                <td>
                    <asp:TextBox ID="resigncause_code" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    ค่าไฟ:
                </td>
                <td>
                    <asp:TextBox ID="incomeetc_amt" runat="server" ReadOnly="true" Style="text-align: center;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-size: 13px;"><font color="#cc0000"><u><strong>ข้อมูลการสมรส</strong></u></font></span>
                </td>
                <td>
                    วันเกษียณ:
                </td>
                <td>
                    <asp:TextBox ID="retry_date" runat="server" Style="text-align: center; width: 160px;"
                        ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="retry_age" runat="server" ReadOnly="true" Style="width: 79px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    สถานภาพสมรส:
                </td>
                <td>
                    <asp:TextBox ID="cp_mariage_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    เกษียณแบบ:
                </td>
                <td>
                    <asp:TextBox ID="cp_retry_status" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ชื่อคู่สมรส:
                </td>
                <td>
                    <asp:TextBox ID="mate_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    เบอร์โทรศัพท์:
                </td>
                <td>
                    <asp:TextBox ID="phone" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    เลขอ้างอิงคู่สมรส:
                </td>
                <td>
                    <asp:TextBox ID="mate_salaryid" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    E-mail:
                </td>
                <td>
                    <asp:TextBox ID="addr_email" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    เงินบำเหน็จ:
                </td>
                <td>
                    <asp:TextBox ID="bumned_amt" runat="server" ReadOnly="true" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    เงินสะสม:
                </td>
                <td>
                    <asp:TextBox ID="sasom_amt" runat="server" ReadOnly="true" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
