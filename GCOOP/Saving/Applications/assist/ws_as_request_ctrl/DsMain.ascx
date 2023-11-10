<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="800px">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 12%">
                    <div>
                        <span>เลขที่ใบคำขอ:</span>
                    </div>
                </td>
                <td style="width: 23%">
                    <asp:TextBox ID="assist_docno" runat="server" Style="text-align: center; background-color: #FFFF99;" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="width: 10%">
                    <div>
                        <span>สวัสดิการ:</span>
                    </div>
                </td>
                <td style="width: 28%">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 12%">
                    <div>
                        <span>ประจำปี:</span>
                    </div>
                </td>
                <td style="width: 15%">
                    <asp:DropDownList ID="assist_year" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="width: 75%; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20%;" />
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mbname" runat="server" ReadOnly="true" BackColor=" #FFFF99"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่ขอ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="REQ_DATE" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันเกิด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="birth_date" runat="server" Style="width: 45%; text-align: center;"
                        BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                        <asp:TextBox ID="birthdate_th" runat="server" Style="width: 50%; text-align: center;"
                        BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mbgroup" runat="server" ReadOnly="true" BackColor=" #FFFF99"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่นับอายุ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="calage_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เป็นสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_date" runat="server" Style="width: 45%; text-align: center;"
                        BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="membdate_th" runat="server" Style="width: 50%; text-align: center;"
                        BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mbtypedesc" runat="server" ReadOnly="true" BackColor=" #FFFF99"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เลขบัตร ปชช:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="card_person" runat="server" Style="text-align: center;" BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
             <td>
                    <div>
                        <span>วันรับโอนย้าย:</span>
                    </div>
              </td>
              <td>
                    <asp:TextBox ID="tranmem_date" runat="server" Style="width: 45%; text-align: center;"
                        BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="tranmem_th" runat="server" Style="width: 50%; text-align: center;"
                        BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
                 <td>
                    <div>
                        <span>ที่อยู่:</span>
                    </div>
                </td>
                <td colspan="3" style="width:100%">
                    <asp:TextBox ID="mbaddr" runat="server" BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
               
                <td>
                    <div>
                        <span>สถานภาพ:</span>
                    </div>
                </td>
                 <td>
                    <asp:TextBox ID="mariage_desc" runat="server" Style="text-align: center;" BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
                     <td>
                    <div>
                        <span>สถานะคำขอ:</span>
                    </div>
                </td>
                    <td>
                    <asp:TextBox ID="reqstatus_desc" runat="server" Style="text-align: center;" BackColor=" #FFFF99" ReadOnly="True"></asp:TextBox>
                </td>
                 <%--<td colspan="2">
                        <asp:Button ID="b_add" runat="server" Text="บันทึกเอกสารการขอสวัสดิการ"/>
                </td>--%>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
