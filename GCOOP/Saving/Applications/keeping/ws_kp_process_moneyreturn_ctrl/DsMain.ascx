<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_process_moneyreturn_ctrl.DsMain" %>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>วันที่โอนเงิน : </span>
                    </div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
           
                <td width="15%">
                    <div>
                        <span>งวดเรียกเก็บล่าสุด : </span>
                    </div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="recv_period" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr><tr>
                <td width="15%">
                    <div>
                        <span>ตั้งแต่สังกัด : </span>
                    </div>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="smembgroup_code" runat="server">
                    </asp:DropDownList>
                </td>
           
                <td width="15%">
                    <div>
                        <span>ถึงสังกัด : </span>
                    </div>
                </td>
                <td width="35%">
                     <asp:DropDownList ID="emembgroup_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" />                
                </td>
                <td>
                    <asp:Button ID="b_post" runat="server" Text="ประมวลเข้าเงินฝาก" />                
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
