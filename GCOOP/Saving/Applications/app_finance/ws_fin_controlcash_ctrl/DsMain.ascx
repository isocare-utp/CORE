<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_controlcash_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Height="316px"   Width="550px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:100%">
	        <tr>
		        <td colspan="4" ><span style="text-align:center;width:94%;font-size:32px;padding:18px">การเงิน(ควบคุมเงินสด)</span></td>
	        </tr>
	        <tr>
		        <td style="width:25%">
			        <span>วันที่ทำรายการ</span>
		        </td>
		        <td>
                    <asp:TextBox ID="OPERATE_DATE" runat="server" ReadOnly="true" style="text-align:center;" BackColor="WhiteSmoke"></asp:TextBox>
                </td>
	            <td>
			        <span>สาขา</span>
		        </td>
		        <td>
                    <asp:TextBox ID="COOP_ID" runat="server" ReadOnly="true" style="text-align:center;width:100%;" BackColor="WhiteSmoke"></asp:TextBox>
                </td>		        
	        </tr>
	        <tr>
		       <td >
			        <span>ผู้ร้องขอ</span>
		        </td>
		        <td>
                    <asp:TextBox ID="ENTRY_ID" runat="server" style="width:85%;text-align:center" BackColor="Yellow" ForeColor="Red" TabIndex="1"></asp:TextBox>
					<asp:Button ID="b_user" runat="server" Text="..." Width="10%" />  
                </td>                
		        <td colspan="2">
                    <asp:TextBox ID="FULL_NAME" runat="server" ReadOnly="true" style="width:100%;" BackColor="WhiteSmoke"></asp:TextBox>
                </td>	
			</tr>
			<tr>
		        <td >
			        <span>เงินสดคงเหลือ</span>
		        </td>
		        <td >
                    <asp:TextBox ID="MONEY_REMAIN" runat="server" ReadOnly="true" BackColor="WhiteSmoke" ForeColor="#8B008B" ToolTip="#,##0.00" style="font-weight:bolder;font-size:14px;text-align:right"></asp:TextBox>
                </td>	        
		        <td >
			        <span>สถานะ</span>
		        </td>			
		        <td>
                    <asp:TextBox ID="T_STATUS" runat="server" ReadOnly="true" style="width:100%;" BackColor="WhiteSmoke"></asp:TextBox>
                </td>
			</tr>
			<tr>
		        <td >
			        <span>ประเภทรายการ</span>
		        </td>
		        <td >
                    <asp:DropDownList ID="ITEM_TYPE" runat="server" TabIndex="2">
                    </asp:DropDownList>
                </td>
		        <td colspan="2">
					<asp:TextBox ID="T_MONEYRETURN" runat="server" ReadOnly="true" style="width:100%;" BackColor="WhiteSmoke"></asp:TextBox>			        
		        </td>
			</tr>            
	        <tr>		        	
		        <td>
			        <span style="height:45px; vertical-align: middle; line-height: 45px;">จำนวนเงินในครั้งนี้</span>
		        </td>
		        <td colspan="3">
                    <asp:TextBox ID="OPERATE_AMT" runat="server" onkeypress="check_number();" ForeColor="#00FF00" BackColor="Black" onfocus="this.select()"  ToolTip="#,##0.00" style="height:45px;font-size:32px;font-weight:bold;text-align:right;width:100%;" TabIndex="3"></asp:TextBox>                                 
                </td>
	        </tr>
			<tr>		        	
		        <td>
			        <span style="height:45px; vertical-align: middle; line-height: 45px;">เงินคงเหลือสหกรณ์</span>
		        </td>
		        <td colspan="3">
                    <asp:TextBox ID="CASH_AMT" runat="server" ForeColor="#8B008B" ToolTip="#,##0.00" style="height:45px;font-size:32px;font-weight:bold;text-align:right;width:100%;" ReadOnly="True"></asp:TextBox>
                </td>
	        </tr>            
        </table>                                     
    </EditItemTemplate>
</asp:FormView>
