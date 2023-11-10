<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsBonus.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsBonus" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="100%">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:100%">
            <tr>
                <td >
                    <div>
                        <span>ประเภทของขวัญ:</span>
                    </div>
                </td>
                <td colspan="3" >
                    <asp:DropDownList ID="bonus_type" runat="server">
                    </asp:DropDownList>
                </td>
                <td >
                    <div>
                        <span>หน่วยนับ:</span>
                    </div>
                </td>
                 <td colspan="3" >
                    <asp:DropDownList ID="bonus_unit" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td style="width: 15%">
                    <div>
                        <span>วิธีการรับของขวัญ:</span>
                    </div>
                </td>
                <td colspan="6" style="width: 85%">
                    <asp:DropDownList ID="bonus_methpay" runat="server" Width="100%">
                        <asp:ListItem Value="1">รับเอง</asp:ListItem>
                        <asp:ListItem Value="2">ส่งไปรษณีย์</asp:ListItem>
                        <asp:ListItem Value="3">รับแทน</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
           <%-- <tr>
                <td style="width: 15%">
                    <div>
                        <span>เป็นสมาชิกก่อนวันที่:</span>
                    </div>
                </td>
                <td colspan="6" style="width: 85%">
                    <asp:TextBox ID="check_date" runat="server" Style="text-align: left;" width="100%"></asp:TextBox>
                </td>
            </tr>--%>
           <%-- <tr>
            <td>
                 รับแทนสมาชิก
            </td>
            </tr>
            <tr>
                <td >
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                 <td >
                    <asp:TextBox ID="member_no_ref" runat="server" Style="width: 75%; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20%;" />
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="membname_ref" runat="server" ReadOnly="true" ></asp:TextBox>
                </td>
                <td >
                    <div>
                        <span>หน่วยนับ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="1">บาท</asp:ListItem>
                        <asp:ListItem Value="2">XXL</asp:ListItem>
                        <asp:ListItem Value="3">XL</asp:ListItem>
                        <asp:ListItem Value="4">L</asp:ListItem>
                        <asp:ListItem Value="5">M</asp:ListItem>
                        <asp:ListItem Value="6">S</asp:ListItem>
                        <asp:ListItem Value="7">SS</asp:ListItem>
                    </asp:DropDownList>
                </td>
    
            </tr>
            <tr>
            <td>
                        <div>
                            <span>เงื่อนไขการจ่าย:</span>
                        </div>
                    </td>
                    <td colspan="1">
                        <asp:DropDownList ID="assistpay_code" runat="server">
                        </asp:DropDownList>
                    </td>
            </tr>--%>


        </table>
    </EditItemTemplate>
</asp:FormView>
