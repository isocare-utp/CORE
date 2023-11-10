<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_contact_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
 <asp:FormView ID="FormView2" runat="server" DefaultMode="Edit">
     <EditItemTemplate>
         <table class="DataSourceFormView">
         <tr>
        
              <td width="10%">
                  <div>
                        <span>ประเภทสมาชิก :</span></div>
              </td>
              <td width="10%">
                  <asp:DropDownList ID="REFMEMBER_FLAG" runat="server" Width="285px" >
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">สมาชิก</asp:ListItem>
                    <asp:ListItem Value="0">บุคคลภายนอก</asp:ListItem>
                  </asp:DropDownList>
            </td>
            <td width="10%">
                 <div>
                        <span>เลขสมาชิก :</span></div>
            </td>
            <td width="35%">
                <asp:TextBox ID="REFMEMBER_NO" runat="server" Width="70px" Style="text-align: right"  ></asp:TextBox>   
             </td>
              <td width="10%">
                        <asp:Button ID="m_search" runat="server" Text="..." Width="25px" />
              </td>
         </tr>   
         <tr>
             <td width="10%">
                  <div>
                        <span>ชื่อ-สกุล :</span></div> 
            </td>
             <td width="20%">
                 <asp:TextBox ID="DESCRIPTION" runat="server" Width="285px"></asp:TextBox>
          </td>
          
         </tr>
   
            <tr>
               <td width="10%">
                <div>
                        <span>ความสัมพันธ์กับผู้กู้ :</span></div>  
                 </td>
              <td width="10%">
                  <asp:TextBox ID="REFMEMBER_RELATION" runat="server" Width="285px" OnKeyPess="return chkNumber(this)"></asp:TextBox>
              </td>
                <td width="10%">
                 <div>
                        <span>โทรศัพท์ :</span></div>  
                </td>
                 <td width="10%">
                  <asp:TextBox ID="REFMEMBER_TEL" runat="server" Width="70px" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>
          </tr>
            <tr>
                <td width="40%">
                 <div>
                        <span>ที่อยู่ :</span></div> 
                 </td>
                 <td width="40%">
                     <asp:TextBox ID="REFMEMBER_ADDRESS" runat="server" Width="285px" Style="text-align: right" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                 <div>
                        <span>กรณีติดต่อไม่ได้ให้ติดต่อไปยังสถานที่ต่อไปนี้ </span></div> 
                 </td>
            </tr>
            <tr>
               <td width="10%">
                <div>
                        <span>สถานที่อยู่ปัจจุบัน :</span></div>  
                 </td>
              <td width="10%">
                  <asp:TextBox ID="MEMBER_ADDRESS" runat="server" Width="285px"  OnKeyPess="return chkNumber(this)"></asp:TextBox>
              </td>
                <td width="10%">
                 <div>
                        <span>โทรศัพท์ :</span></div>  
                </td>
                 <td width="10%">
                  <asp:TextBox ID="MEMBER_TEL" runat="server" Width="70px"  OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>
          </tr>
           <tr>
               <td width="10%">
                <div>
                        <span>สถานที่ทำงาน :</span></div>  
                 </td>
              <td width="10%">
                  <asp:TextBox ID="MEMBER_WORKADDRESS" runat="server" Width="285px"  OnKeyPess="return chkNumber(this)"></asp:TextBox>
              </td>
                <td width="10%">
                 <div>
                        <span>โทรศัพท์ :</span></div>  
                </td>
                 <td width="10%">
                  <asp:TextBox ID="MEMBER_WORKTEL" runat="server" Width="70px" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>
          </tr>
</table>
</EditItemTemplate>
</asp:FormView>