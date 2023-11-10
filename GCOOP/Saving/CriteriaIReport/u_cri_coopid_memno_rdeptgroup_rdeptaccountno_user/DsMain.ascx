<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" >
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>  
                    <div>
                        <span>รับรองเงินฝาก:</span>
                    </div>       
                </td>
                <td>  
                    <asp:CheckBox ID="deptgrp_code1" runat="server"/>รับรองเงินฝาก
                   <%-- <asp:CheckBox ID="deptgrp_code1" runat="server"/>ออมทรัพย์     
                    <asp:CheckBox ID="deptgrp_code2" runat="server"/>ออมทรัพย์พิเศษ                         
                    <asp:CheckBox ID="deptgrp_code3" runat="server"/>ประจำ      --%>                            
                </td>           
            </tr> 
                <td width="30%">  
                    <div>
                            <span>รับรองทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td>   
                    <asp:CheckBox ID="c_share" runat="server" />รับรองทุนเรือนหุ้น                         
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>อัตราแลกเปลี่ยน:</span></div>
                </td>
                <td>
                    <div>
                        <span>1.00 $</span></div>
                </td>
                <th>
                    <div>
                        ::</div>
                </th>
                
                <td colspan="3">
                    <asp:TextBox ID="us" runat="server"  Style="text-align: center" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>฿</span></div>
                </td>
            </tr>
            <tr>
                <td width="40%">  
                    <div>
                            <span>ประเภทหนังสือรับรอง:</span>
                        </div>
                </td>
                <td> 
                    <asp:DropDownList ID="guarantee" runat="server" >                      
                        <asp:ListItem Value="0" Selected="True">รับรองผู้อื่น</asp:ListItem>                    
                        <asp:ListItem Value="1">รับรองตัวเอง</asp:ListItem>                     
                    </asp:DropDownList>
                </td>
            </tr>        
            <tr>            
                <td>
                    <div>
                            <span>ภาษา:</span>
                      </div>
                 </td>
                 <td>  
                   <asp:DropDownList ID="language" runat="server" ClientIDMode=Static> 
                   <asp:ListItem Value="eng" Selected="True">ภาษาอังกฤษ</asp:ListItem>                       
                       <asp:ListItem Value="thai" Selected="True">ภาษาไทย</asp:ListItem>             
                   </asp:DropDownList>
               </td>
            </tr>
           
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนสมาชิก:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="memno" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่บัญชีเงินฝาก:</span>
                    </div>
                </td>
                <td >
                    <asp:TextBox ID="deptaccount_no" runat="server" Width="70%" ReadOnly="true"></asp:TextBox><asp:Button ID="b_deptno" runat="server" Text="..." Width="20%"/>  
                </td>
            </tr>            
            <tr>
                <td colspan="2">
                    <div style="font-weight:bolder">ผู้ลงนามท้ายหนังสือ</div>
                </td>
            </tr>
            <tr>
                <td>
                    <div><span>ชื่อผู้รับรอง:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="as_manager" Width="185%" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="font-weight:bolder">ข้อมูลในจดหมาย(ภาษาอังกฤษ)</div>
                </td>
            </tr>
            <tr>
                <td>
                    <div><span>ชื่อผู้ขอรับรอง:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="seconder" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div><span>รับรองให้:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="user_name" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div><span>ประเทศที่รับรอง:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="country" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>

    
