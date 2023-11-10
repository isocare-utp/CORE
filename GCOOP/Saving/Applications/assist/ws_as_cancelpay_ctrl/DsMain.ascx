<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_cancelpay_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" valign="top">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="20%" valign="top">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox> 
                </td>
                <td width="3%" valign="top">
                    <asp:Button ID="b_searchMem" runat="server" Text="..." Width="25px" />
                </td>
                <td width="17%" valign="top">
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td width="5%" valign="top">
                    <asp:TextBox ID="assist_code" runat="server"  Style="text-align: center" MaxLength="2"></asp:TextBox>
                </td>
                <td width="25%" valign="top">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td  valign="top">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Width="100px" />   
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div>
                        <span>วันที่จ่าย:</span>
                    </div>
                </td>
                <td valign="top">
                    <asp:TextBox ID="start_date" runat="server" Style="text-align: center"></asp:TextBox> 
                </td>
                <td colspan="2" valign="top">
                    <div>
                        <span>ถึงวันที่จ่าย:</span>
                    </div>
                </td>
                <td colspan="2" valign="top">
                    <asp:TextBox ID="end_date" runat="server" Style="text-align: center" Width="215px" ></asp:TextBox> 
                </td>
                <td> 
                    <asp:Button ID="b_clear" runat="server" Text="ล้างข้อมูล" Width="100px"/>
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" colspan="2">
                    <asp:CheckBox ID="select_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>                             
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
