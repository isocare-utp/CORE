<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width:100%;">
    <EditItemTemplate>
        <span class="TitleSpan">ข้อมูลสมาชิก</span>
        <table class="DataSourceFormView" >
           <%-- <tr>
                <td>
                    <asp:Button ID="b_print" runat="server" Text="พิมพ์รายงานตรวจสอบสิทธิ์" />
                </td>
                <td colspan="3">
                    <asp:Button ID="b_pbreport" runat="server" Text="พิมพ์รายงานคุณสมบัติ" />
                </td>
            </tr>--%>
            <tr>
                <td width="13%">
                     <div>
                        <span>เลขที่บัญชี:</span>
                     </div>
                </td>
                <td width="18%">
                         <asp:TextBox ID="DEPTACCOUNT_NO" runat="server" Style="width:65%;text-align:center;"  ForeColor="Red" BackColor="Yellow" onfocus="this.select()" TabIndex="1"></asp:TextBox>                         
                         <asp:TextBox ID="DEPTTYPE_CODE" runat="server" Style="width:17%;text-align:center;" ReadOnly="true"  ></asp:TextBox>
                         <asp:Button ID="b_searchdeptno" Text="..." runat="server" Style="text-align:center;width:10%;height:24px" />
                </td>
                <td width="14%">
                    <div>
                        <span> ชื่อบัญชี: <span>
                   </div>
                </td>
                <td width="25%">
                        <asp:TextBox ID="DEPTACCOUNT_NAME" runat="server"  ReadOnly="true" ></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span>ประเภท:</span>
                    </div>
                </td>
                <td width="20%">
                        <asp:TextBox ID="DEPTTYPE_DESC" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td >
                    
                        <asp:TextBox ID="member_no" runat="server" Style="text-align:center;" ReadOnly="True" ></asp:TextBox>
                   
                </td>
                <td >
                   
                        <span>ชื่อ-สกุล:</span>
                    
                </td>
                <td >
                    
                        <asp:TextBox ID="member_name" runat="server" Style="width:98%" ReadOnly="true" ></asp:TextBox>
                   
                </td>
                <td >
                    <span>บุคคล:</span>
                   
                </td>
                <td>
                    <asp:TextBox ID="GROUP_DESC" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td >
                    <div>
                        <span>เลขสมุด:</span>
                    </div>
                </td>
                <td >
                        <asp:TextBox ID="DEPTPASSBOOK_NO" runat="server" Style="text-align:center;" ReadOnly="True" ></asp:TextBox>
                </td>
                <td >
                    <div>
                        <span>เงื่อนไขการถอน:</span>
                    </div>
                </td>
                <td >
                        <asp:TextBox ID="CONDFORWITHDRAW" runat="server" Style="width:98%" ReadOnly="true" ></asp:TextBox>
                </td>
                <td >
                    <div>
                        <span>เลขที่บัตร:</span>
                    </div>
                </td>
                <td >
                    
                        <asp:TextBox ID="CARD_PERSON" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                <td >
                    <div>
                        <span>วัตถุประสงค์:</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="dept_objective" runat="server" Style="width:99%" ReadOnly="True" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                    <td colspan="6">
                    <div style="background-color:Black; height:3px;">
                         
                    </div>
                    </td>
            </tr>
    </table>
    <table class="DataSourceFormView" >
            <tr>
                <td width="10%">
                    <div>
                        <span>วันเปิดบ/ช:</span>
                    </div>
                </td >
                <td width="10%">
                        <asp:TextBox ID="DEPTOPEN_DATE" runat="server" Style="text-align:center;" ReadOnly="True" ></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span>ต้นเงินล่าสุด:</span>
                    </div>
                </td>
                <td width="12%" >
                        <asp:TextBox ID="PRNC_NO" runat="server" Style="text-align: right;" ReadOnly="true" ></asp:TextBox>
                </td>
                <td width="10%"" >
                    <div>
                        <span>จำนวนรายการ:</span>
                    </div>
                </td>
                <td   width="5%"">
                    
                        <asp:TextBox ID="LASTSTMSEQ_NO" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                </td  >
                <td  width="7%"> 
                        <asp:TextBox ID="cp_sequest" runat="server" Style="text-align:center;" ReadOnly="true" ></asp:TextBox>
                </td  >
                <td width="10%">
                        <asp:TextBox ID="SEQUEST_AMOUNT" runat="server"  Style="text-align: right;"  ToolTip="#,##0.00" ReadOnly="true" ></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                <td  >
                    <div>
                        <span>สถานะบัญชี:</span>
                    </div>
                </td>
                <td >
                        <asp:TextBox ID="cp_deptclose" runat="server" Style="text-align:center;" ReadOnly="True" ></asp:TextBox>
                </td>
                <td Style="width:80px;" >
                    <div>
                        <asp:CheckBox ID="DEPTMONTH_STATUS" runat="server" Text="ฝากรายเดือน" Enabled="false"> </asp:CheckBox>
                    </div>
                </td>
                <td Style="width:80px;" >
                        <asp:TextBox ID="DEPTMONTH_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td >
                    <div>
                        <span>จำนวนครั้งถอน:</span>
                    </div>
                </td>
                <td Style="width:70px;" >
                    
                        <asp:TextBox ID="WITHDRAW_COUNT" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>

                </td>
                <td >
                    <div>
                        <span>ธนาคาร:</span>
                    </div>
                </td>
                <td  >
                    
                        <asp:TextBox ID="BRANCH_NAME" runat="server"  ReadOnly="true" ></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td  >
                    <div>
                        <span>การรับ Int.Div.:</span>
                    </div>
                </td>
                <td>
                        <asp:TextBox ID="cp_intpaymeth" runat="server" Style="text-align:center;" ReadOnly="True" ></asp:TextBox>
                </td>
                <td >
                    <div>
                        <span>บัญชีเลขที่:</span>
                    </div>
                </td>
                <td  colspan="2">
                        <asp:TextBox ID="TRAN_BANKACC_NO" runat="server" Style="text-align: center; width:98%"  ReadOnly="true" ></asp:TextBox>
                </td>
                
                <td  colspan="3">
                        <asp:TextBox ID="TRAN_DEPTACC_NO" runat="server" Style="text-align: center; width:98%" ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                    <td colspan="8">
                    <div style="background-color:Black; height:3px;">
                         
                    </div>
                    </td>
            </tr>
        </table>
        <table class="DataSourceFormView" >
            <tr>
                <td width="13%" >
                    <span> ยอดยกมา</span>
                </td>
                <td width="13%">
                    <span> ยอดคงเหลือ</span>
                </td>
                <td width="13%">
                    <span> ยอดถอนได้</span>
                </td>
                <td width="13%" >
                    <span>ยอดในสมุด</span>
                </td>
                <td width="10%">
                    <span> เช็คเรียกเก็บ</span>
                </td>
                 <td width="10%">
                    <span> Int./Div. สะสม</span>
                </td>
                 <td width="9%">
                    <span> คิด Int./Div. ล่าสุด</span>
                </td>
                <td width="10%">
                    <span>คำนวนดอกเบี้ย</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="BEGINBAL" runat="server" Style="text-align: right;" ToolTip="#,##.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="PRNCBAL" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="sum_withdraw" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="BOOK_BALANCE" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="CHECKPEND_AMT" runat="server" Style="text-align: right;" ToolTip="#,##.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ACCUINT_AMT" runat="server" Style="text-align: right;" ToolTip="#,##.00" ReadOnly="true" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="LASTCALINT_DATE" runat="server" Style="text-align: center;" ReadOnly="true" ></asp:TextBox>
                </td>
                 <td>
                    <asp:Button ID="b_calaccuint" Text="คำนวณ" runat="server" Style="text-align:center;width:100%;" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
