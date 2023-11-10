<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:720px;">           
            <tr>
                <td  width="21%">
                    <span>ประเภทรายการ:</span>                    
                </td>
                <td width="25%">
                    <asp:DropDownList ID="PAY_RECV_STATUS" runat="server">
                        <asp:ListItem Value="9">กรุณาเลือกรายการ</asp:ListItem>
                        <asp:ListItem Value="1">รายการรับ</asp:ListItem>
                        <asp:ListItem Value="0">รายการจ่าย</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="17%">
                    <span>วันที่ใบเสร็จ:</span>
                </td>
                <td width="1%">
                    <asp:TextBox ID="OPERATE_DATE" runat="server"  style="text-align:center"></asp:TextBox>
                </td>
                <td width="15%">
                    <span>วันที่ทำรายการ:</span>
                </td>
                <td width="21%">
                    <asp:TextBox ID="ENTRY_DATE" runat="server" ReadOnly="true" style="text-align:center"  Width="100%"> </asp:TextBox>
                </td>                               
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="MEMBER_FLAG" runat="server" style="background-color:#ffa366">
                        <asp:ListItem Value="1">สมาชิก</asp:ListItem>
                        <asp:ListItem Value="0">บุคคลภายนอก</asp:ListItem>
                        <asp:ListItem Value="3">อื่น ๆ</asp:ListItem>
                        <asp:ListItem Value="8">ออกใบเสร็จ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <asp:TextBox ID="MEMBER_NO" runat="server" Width="80%" style="text-align:center"></asp:TextBox>                    
                    <asp:Button ID="b_member" runat="server" Text="..." Width="20px" />                    
                </td>
                <td>
                    <span>ชื่อ-สกุล:</span>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="NONMEMBER_DETAIL" runat="server" Width="100%" ></asp:TextBox>
                </td>                               
            </tr>
            <tr>
                <td>
                    <span>ทำรายการโดย:</span>                    
                </td>
                <td>
                     <asp:DropDownList ID="CASH_TYPE" runat="server"></asp:DropDownList>               
                </td>
                <td>                    
                     <span>รหัสบัญชี:</span>                    
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="TOFROM_ACCID" runat="server" Width="331px" ></asp:DropDownList>
                    <asp:Button ID="b_accid" runat="server" Text="..." Width="34" />       
                </td>                               
            </tr>
            <tr>                
                <td>
                    <span>สาขา:</span>
                </td>
                <td>
                    <asp:TextBox ID="COOP_ID" runat="server" ReadOnly="true"></asp:TextBox>
                </td>   
                <td>
                    <span>ผู้ทำรายการ:</span>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="ENTRY_ID" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                </td>                                           
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="TAX_FLAG" runat="server" Text="หักภาษี ณ ที่จ่าย:"></asp:CheckBox>
                </td>
                <td>
                    <asp:DropDownList ID="TAX_CODE" runat="server"></asp:DropDownList>               
                </td>
                <td>
                    <asp:TextBox ID="TAX_AMT" runat="server" ToolTip="#,##0.00" style="text-align:right" ReadOnly="true" ></asp:TextBox>
                </td>
                <td rowspan="2">
                    <span style="Height:50px;font-size:25px;vertical-align: middle; line-height: 50px;">สุทธิ:</span>
                </td>
                <td  colspan="2" rowspan="2">
                    <asp:TextBox ID="ITEM_AMTNET" runat="server" ReadOnly="true" ToolTip="#,##0.00" BackColor="FloralWhite" ForeColor="DarkRed" Width="100%" style="Height:50px;font-size:25px;font-weight:bolder;text-align:right"></asp:TextBox>
                </td>                                              
            </tr>    
            <tr>
                <td>
                    <asp:CheckBox ID="vat_flag" runat="server" Text="มีการคิด VAT:"></asp:CheckBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="VAT_AMT" runat="server" ToolTip="#,##0.00" style="text-align:right" Width="98%" ReadOnly="true" ></asp:TextBox>        
                </td>                                                                             
            </tr>        
            <tr>
                <td>
                    <span>รายละเอียด:</span>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="PAYMENT_DESC" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>  
            <tr>
                <td>
                    <span> สถานะการรับเช็ค :</span>
                </td>
                <td >
                    <asp:DropDownList ID="CHEQUE_STATUS" runat="server">
                        <asp:ListItem Value="0">--กรุณาเลือก---</asp:ListItem>
                        <asp:ListItem Value="1">รับทางไปรษณีย์</asp:ListItem>
                        <asp:ListItem Value="2">รับผ่านทางเจ้าหน้าที่</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span>เลขที่เช็ค:</span>
                </td>
                <td >
                    <asp:TextBox ID="ACCOUNT_NO" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <span>วันที่หน้าเช็ค:</span>
                </td>
                <td>
                    <asp:TextBox ID="DATEON_CHQ" runat="server" style="text-align:center" Width="100%"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td>
                    <span>ประเภทเช็ค :</span>
                </td>
                <td >
                    <asp:DropDownList ID="CHEQUE_TYPE" runat="server">
                        <asp:ListItem Value="0">--กรุณาเลือก---</asp:ListItem>
                        <asp:ListItem Value="1">ค่าสินไหมประกันชีวิต</asp:ListItem>
                        <asp:ListItem Value="2">เงินรับฝากจากสมาชิก</asp:ListItem>
                        <asp:ListItem Value="3">รายการเรียกเก็บประจำเดือน</asp:ListItem>
                        <asp:ListItem Value="4">รับชำระหนี้เงินกู้</asp:ListItem>
                        <asp:ListItem Value="5">อื่น ๆ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span>ธนาคาร:</span>
                </td>
                <td>
                    <asp:DropDownList ID="BANK_CODE" runat="server" ></asp:DropDownList>    
                </td>
                <td>
                    <span>สาขา:</span>
                </td>
                <td>
                    <asp:DropDownList ID="BANK_BRANCH" runat="server" Width="100%"></asp:DropDownList>    
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
