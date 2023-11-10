<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" 
Inherits="Saving.Applications.fund.ws_fund_statement_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span class="TitleSpan">ข้อมูลสมาชิก</span>
        <table class="DataSourceFormView" style="width: 770px;">            
            <tr>
                <td  width="18%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="MEMBER_NO" runat="server" Style="width: 100px; text-align:center;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td width="56%">
                    <div>
                        <asp:TextBox ID="FULLNAME" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBTYPE_DESC" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBGROUP_DESC" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                 <td>
                    <div>
                        <span>สถานะกองทุน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="FUNDSTATUS_DISPLAY" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>คงเหลือกองทุน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="FUNDBALANCE" runat="server" ReadOnly="true" ToolTip="#,##0.00" style="text-align:right"></asp:TextBox>
                    </div>
                </td>
            </tr>            
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="REMARK" runat="server" TextMode="MultiLine" ReadOnly="true" Style="width: 754px;
                        height: 50px;"></asp:TextBox>
                </td>
            </tr>    
        </table>
    </EditItemTemplate>
</asp:FormView>

