<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_reqchgdept_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" >
    <EditItemTemplate>
        <table class="DataSourceFormView"  >
            <tr>
                <td width="20%">
                    <div>
                        <span>เลขที่คำร้อง:</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="dpreqchg_doc" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>                    
                </td>
                <td width="20%">
                    <div>
                        <span>วันที่ใบคำขอ: <span>
                    </div>
                </td>
                <td  width="15%">
                    <asp:TextBox ID="reqchg_date" runat="server" Style="text-align: center;"  ReadOnly="true"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>วันที่มีผล: <span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="deptmontchg_date" runat="server"  Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาขา/เลขที่บัญชี:</span>
                    </div>
                </td>
                <td >
                    <asp:TextBox ID="coop_id" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center;" BackColor="#ffffcc"  onfocus="this.select()" TabIndex="1"></asp:TextBox>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="deptaccount_name" runat="server" Style="width:99%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" style="text-align:center" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                         <span>ชื่อสมาชิก: <span>
                    </div>
                </td>
                <td colspan="3">
                     <asp:TextBox ID="fullname" runat="server" Style="width:99%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:21%">
                    <div>
                        <span>จำนวนเงินส่งรายเดือนเก่า:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="deptmonth_oldamt" runat="server" ToolTip="#,##0.00" style="text-align:right" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width:21%">
                    <div>
                        <span>จำนวนเงินส่งต่อเดือนใหม่:  <span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="deptmonth_newamt" runat="server"   ToolTip="#,##0.00" style="text-align:right" onfocus="this.select()"  BackColor="#ffffcc" ></asp:TextBox>
                </td> 
                 <td>
                    <div>
                        <span>จำนวนเงินที่อนุมัติ:  <span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="deptmonth_appamt" runat="server"  ToolTip="#,##0.00" style="text-align:right" ReadOnly="true"></asp:TextBox>
                </td>               
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงินคงเหลือ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="prncbal" runat="server" ToolTip="#,##0.00" style="text-align:right" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>วันที่ทำรายการ:  <span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="entry_date" runat="server"  style="text-align:center" ReadOnly="true"></asp:TextBox>
                </td> 
                 <td>
                    <div>
                        <span>ผู้บันทึก:  <span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="entry_id" runat="server" ReadOnly="true"></asp:TextBox>
                </td>               
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมายเหตุ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="remark" runat="server" Style="width:98%"  BackColor="#ffffcc" ></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หน่วย:  <span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="MEMBGROUP_FULLDESC" runat="server" ReadOnly="true" Style="width:98%"></asp:TextBox>
                </td> 
                 <td>              
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
