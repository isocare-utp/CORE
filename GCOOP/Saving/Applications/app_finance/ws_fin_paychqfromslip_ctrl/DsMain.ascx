<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_paychqfromslip_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width: 100%;">
    <EditItemTemplate>       
        <table class="DataSourceFormView">
            <tr>
                 <td width="20%">
                    <div>
                        <span>วันที่จ่ายเช็ค :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="onchq_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>เล่มที่เช็ค :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="as_chqbookno" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ธนาคาร :<span>
                    </div>
                </td>
                 <td>
                    <asp:DropDownList ID="as_bank" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>เลขที่เช็ค :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="as_chqstartno" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>                            
                <td>
                    <div>
                        <span>สาขา :<span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="as_bankbranch" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>เลขบัญชี :<span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="as_fromaccno" runat="server" Style="text-align: center;" Width="87%"></asp:TextBox>
                    <asp:Button ID="b_accno" runat="server" Text="..." Width="10%" />               
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                 <td width="20%">
                    <div>
                        <span>ประเภท :<span>
                    </div>
                </td>
                <td width="30%">
                     <asp:DropDownList ID="as_chqtype" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <div>
                        <span>วันที่เช็ค :<span>
                    </div>
                    
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ai_prndate" runat="server">
                        <asp:ListItem Value="1" Selected="True">พิมพ์</asp:ListItem>
                        <asp:ListItem Value="0">ไม่พิมพ์</asp:ListItem>                        
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะ :<span>
                    </div>
                </td>
                <td>
                     <asp:DropDownList ID="ai_chqstatus" runat="server">
                        <asp:ListItem Value="0">ยังไม่ได้รับเช็ค</asp:ListItem><%--ค้างจ่าย--%>
                        <asp:ListItem Value="8">รับเช็คแต่ยังไม่ได้ขึ้นเงิน</asp:ListItem><%--ระหว่างทาง--%>
                        <%--<asp:ListItem Value="2">เช็คล่วงหน้า</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ผู้ถือ :<span>
                    </div>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ai_killer" runat="server" >
                        <asp:ListItem Value="1" Selected="True">ขีดฆ่า</asp:ListItem>
                        <asp:ListItem Value="0">ไม่ขีดฆ่า</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>                     
                </td>
                <td>                    
                </td>
                <td>
                    <div>
                        <span>A/C PAYEE :<span>
                    </div>                    
                </td>
                <td>
                    <asp:DropDownList ID="ai_payee" runat="server">
                        <asp:ListItem Value="1">พิมพ์</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">ไม่พิมพ์</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
       </table>
       <table  class="DataSourceFormView" style="width:350px">
            <tr>
                <td>
                    <div style="font-size: 14px; text-decoration: underline; color: #333399; font-weight: bolder;"  >รายการ</div>
                </td>
            </tr>
            <tr>
                <td width="100px">
                    <span>วันที่ :</span>
                </td>
                <td width="150px">
                    <asp:TextBox ID="entry_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="100px">
                     <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" />
                </td>
            </tr>   
            <tr></tr>     
            <%--<tr>
                <td colspan="2">
                    <asp:CheckBox ID="all_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>
            </tr> --%>          
       </table>
    </EditItemTemplate>
</asp:FormView>
