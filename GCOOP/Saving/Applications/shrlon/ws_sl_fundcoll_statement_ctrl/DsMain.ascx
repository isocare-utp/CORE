<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_fundcoll_statement_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span class="TitleSpan">ข้อมูลสมาชิก</span>
        <table class="DataSourceFormView" style="width: 770px;">
            <tr>
                <td colspan="2">
                   <asp:Button ID="b_printbook" runat="server" Text="พิมพ์ปกสมุดฝากกองทุน" />  
                </td>
                <td colspan="2">
                   <asp:Button ID="b_printstate" runat="server" Text="พิมพ์รายการเคลื่อนไหวกองทุน" />  
                </td>
                <td colspan="2">
                   <asp:Button ID="b_processint" runat="server" Text="ประมวลดอกเบี้ยกองทุน" />  
                </td>
            </tr>
            <tr>
                <td  width="18%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 100px; text-align:center;"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td colspan="4">
                    <div>
                        <asp:TextBox ID="fullname" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="18%">
                    <div>
                        <span>สะสมสูงสุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="maxfund_amt" runat="server" ToolTip="#,##0.00" Style="text-align:right;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                    <td width="18%">
                        <div>
                            <span>ยอดเงินกองทุน:</span>
                        </div>
                    </td>
                <td>
                    <div>
                        <asp:TextBox ID="fundbalance" runat="server" ToolTip="#,##0.00" Style="text-align:right;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                    <td width="18%">
                        <div>
                            <span>ดอกเบี้ยสะสม:</span>
                        </div>
                    </td>
                <td>
                    <div>
                        <asp:TextBox ID="interest_return" runat="server" ToolTip="#,##0.00" Style="text-align:right;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td width="18%">
                    <div>
                        <span>บรรทัดที่จะพิมพ์:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="lastrec_no_pb" runat="server" ToolTip="#,##0" Style="text-align:center;"></asp:TextBox>
                    </div>
                </td>
                    <td width="18%">
                        <div>
                            <span>บรรทัดสมุด:</span>
                        </div>
                    </td>
                <td>
                    <div>
                        <asp:TextBox ID="lastline_no_pb" runat="server" ToolTip="#,##0" Style="text-align:center;"></asp:TextBox>
                    </div>
                </td>
                    <td width="18%">
                        <div>
                            <span>หน้าสมุดที่จะพิมพ์:</span>
                        </div>
                    </td>
                <td>
                    <div>
                        <asp:TextBox ID="lastpage_no_pb" runat="server" ToolTip="#,##0" Style="text-align:center;"></asp:TextBox>
                    </div>
                </td>
            </tr>         
        </table>
    </EditItemTemplate>
</asp:FormView>

