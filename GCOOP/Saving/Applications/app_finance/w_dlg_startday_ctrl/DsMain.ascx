<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.w_dlg_startday_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="705px">
    <EditItemTemplate>
        <table class="DataSourceFormView" width="100%">
	        <tr>
		        <td colspan="4"><h2 style="color:#000099"><u>สหกรณ์ :</u></h2></td>
	        </tr>
	        <tr>
		        <td width="150px">
			        <span>ชื่อสหกรณ์ :</span>
		        </td>
		        <td colspan="3">
                    <asp:TextBox ID="COOP_NAME" runat="server" ReadOnly="true" width="100%" BackColor="#c1c1bd" style="font-weight:bold;font-size:16px"></asp:TextBox>
                </td>
	        </tr>
	        <tr>
		        <td>
			        <span>เปิดงานประจำวัน :</span>
		        </td>
		        <td>
                    <asp:TextBox ID="OPERATE_DATE" runat="server" ReadOnly="true" style="text-align:center;font-weight:bolder" BackColor="#c1c1bd"></asp:TextBox>
                </td>
		        <td width="110px">
			        <span>เวลา :</span>
		        </td>
		        <td >
                    <asp:TextBox ID="Full_DATENOW"  runat="server" ReadOnly="true" width="100%" style="text-align:center" BackColor="#c1c1bd"></asp:TextBox>
                </td>
	        </tr>
	        <tr>
		        <td colspan="2"><h3 style="color:#000099"><u>จำนวนเงินสดในสหกรณ์ :</u></h3></td> 
		        <td colspan="2"><h3 style="color:#000099"><u>รายละเอียดอื่นๆ :</u></h3></td>  
	        </tr>
	        <tr>
		        <td >
			        <span>เงินสดยกมา :</span>
		        </td>
		        <td>
                    <asp:TextBox ID="CASH_BEGIN" runat="server" ReadOnly="true" style="font-size:18px;text-align:right;font-weight:bolder;" ToolTip="#,##0.00" BackColor="#c1c1bd" ForeColor="DarkRed"></asp:TextBox>
                </td>	
		        <td >
			        <span>สาขา :</span>
		        </td>
		        <td >
                    <asp:TextBox ID="COOP_ID" runat="server" ReadOnly="true" width="100%" style="text-align:center" BackColor="#c1c1bd"></asp:TextBox>
                </td>
	        </tr>
	        <tr>
		        <td >
			        <span>เช็คยกมา :</span>
		        </td>
		        <td>
                    <asp:TextBox ID="CHQINSAFT_BFAMT" runat="server" ReadOnly="true" style="font-size:18px;text-align:right;font-weight:bolder;" ToolTip="#,##0.00" ForeColor="#8B008B" BackColor="#c1c1bd"></asp:TextBox>
                </td>	
		        <td >
			        <span>วันที่ทำรายการ :</span>
		        </td>
		        <td >
                    <asp:TextBox ID="ENTRY_DATE" runat="server" ReadOnly="true" width="100%" style="text-align:center" BackColor="#c1c1bd"></asp:TextBox>
                </td>
	        </tr>
	        <tr>
		        <td>
			        <span style="font-weight:bold;font-size:22px;vertical-align:middle; line-height: 35px; height: 35px;">รวม :</span>
		        </td>
		        <td>
                    <asp:TextBox ID="AMOUNT_AMT" runat="server" ReadOnly="true" style="height:35px;font-weight:bolder;font-size:22px;text-align:right" ToolTip="#,##0.00" BackColor="FloralWhite" ForeColor="DarkRed" ></asp:TextBox>
                </td>	
		        <td >
			        <span style="height:35px;font-size:16px;vertical-align:middle; line-height: 35px;">ผู้ทำรายการ :</span>
		        </td>
		        <td >
                    <asp:TextBox ID="ENTRY_ID" runat="server" ReadOnly="true" width="100%" style="height:35px;font-size:22px;text-align:center" BackColor="#c1c1bd"></asp:TextBox>
                </td>
	        </tr>
        </table>                                   
    </EditItemTemplate>
</asp:FormView>
