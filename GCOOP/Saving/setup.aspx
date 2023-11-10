<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setup.aspx.cs" Inherits="Saving.setup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table border=1 cellpadding=2 cellspacing=2 align="center" style="display:<%=(!AuthenFlag)?"":"none"%>;">
    <tr><td bgcolor="#FFFFCC" colspan=2>Windows Server Authentication</td></tr>
    <tr><td colspan=2 style="color:red">
        <asp:Literal ID="LoginLtl" runat="server"></asp:Literal></td></tr>
    <tr><td bgcolor="White">User</td><td>
        <asp:TextBox ID="UserTbx" runat="server"></asp:TextBox>
        </td></tr>
    <tr><td bgcolor="White">Password</td><td>
        <asp:TextBox ID="PwdTbx" TextMode="Password" runat="server" ty></asp:TextBox>
        </td></tr>
    <tr><td bgcolor="#FFFFCC" colspan=2 align="center">
        <asp:Button ID="LoginBtn" runat="server" Text="Login" 
            onclick="LoginBtn_Click" />
        </td></tr>
    </table>
    <table border=0 cellpadding=2 cellspacing=2 align="center" style="display:<%=(AuthenFlag)?"":"none"%>;">
    <tr><td bgcolor="#66CCFF">
    Deploy Path = <%=ROOT_PATH%>Extend : 
        <asp:DropDownList ID="ExtendsListDDW" runat="server" 
            onselectedindexchanged="ExtendsListDDW_SelectedIndexChanged" 
            ontextchanged="ExtendsListDDW_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="ExtendsListBtn" runat="server" Text="ดึง" 
            onclick="ExtendsListBtn_Click" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"/>
        <asp:Button ID="LogoutBtn" runat="server" Text="ออกจากระบบ" 
            onclick="LogoutBtn_Click" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"/>
    </td></tr>
    <tr><td style="display:<%=(selectedExtendsFlag?"":"none")%>;" bgcolor="#CCFFFF"> 
    รายการ XML :<asp:Button ID="XmlFilesListBtn" runat="server" Text="ดึง" 
            onclick="XmlFilesListBtn_Click" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')"/>
    &nbsp;<asp:DropDownList ID="XmlFilesListDDW" runat="server" 
                    onselectedindexchanged="XmlFilesListDDW_SelectedIndexChanged" 
                    ontextchanged="XmlFilesListDDW_SelectedIndexChanged">
                </asp:DropDownList> : <%=XML_SELECTED_PATH%>
     </td></tr>
     <tr><td style="display:<%=(selectedExtendsFlag?"":"none")%>;">
      <asp:Literal ID="XmlDataLtl" runat="server" Visible=false></asp:Literal>

      <asp:GridView ID="gvconstmap" runat="server" AutoGenerateColumns="false" ShowFooter="true" 
                OnRowCommand="gvconstmap_OnRowCommand"
                onrowcancelingedit="gvconstmap_RowCancelingEdit"
                onrowdeleting="gvconstmap_RowDeleting" 
                onrowediting="gvconstmap_RowEditing"
                onrowupdating="gvconstmap_RowUpdating"
                onpageindexchanging="gvconstmap_PageIndexChanging">
        <Columns>   
                
            <asp:TemplateField HeaderText="config_code" HeaderStyle-Width="100px">
                <EditItemTemplate>
                    <asp:TextBox ID="txtconfig_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"config_code") %>' Width="150" ></asp:TextBox>       
                </EditItemTemplate>
                <ItemTemplate >
                    <asp:Label ID="lblconfig_code" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"config_code") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddconfig_code" runat="server" Width="150" ></asp:TextBox>       
                </FooterTemplate>
            </asp:TemplateField> 
                
            <asp:TemplateField HeaderText="config_value" HeaderStyle-Width="250px">
                <EditItemTemplate>
                    <asp:TextBox ID="txtconfig_value" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"config_value") %>'  Width="250"></asp:TextBox>       
                </EditItemTemplate>
                <ItemTemplate >
                    <asp:Label ID="lblconfig_value" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"config_value") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddconfig_value" runat="server" Width="250"></asp:TextBox>       
                </FooterTemplate>
            </asp:TemplateField> 
                
            <asp:TemplateField HeaderText="config_name" HeaderStyle-Width="250px">
                <EditItemTemplate>
                    <asp:TextBox ID="txtconfig_name" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"config_name") %>'  Width="250"></asp:TextBox>       
                </EditItemTemplate>
                <ItemTemplate >
                    <asp:Label ID="lblconfig_name" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"config_name") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddconfig_name" runat="server" Width="250"></asp:TextBox>       
                </FooterTemplate>
            </asp:TemplateField>    

            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="150px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtnUpdate" CommandName="Update" runat="server" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Update</asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel"  CommandName="Cancel" runat="server" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Cancel</asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Delete</asp:LinkButton>
                </ItemTemplate>        
                <FooterTemplate>
                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Add</asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>  

        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" Height="30px" />
        <RowStyle  />
      </asp:GridView>


      
      <asp:GridView ID="gvconnection" runat="server" AutoGenerateColumns="false" ShowFooter="true" 
                OnRowCommand="gvconnection_OnRowCommand"
                onrowcancelingedit="gvconnection_RowCancelingEdit"
                onrowdeleting="gvconnection_RowDeleting" 
                onrowediting="gvconnection_RowEditing"
                onrowupdating="gvconnection_RowUpdating"
                onpageindexchanging="gvconnection_PageIndexChanging">
        <Columns>   
                
            <asp:TemplateField HeaderText="id" HeaderStyle-Width="30px">
                <EditItemTemplate>
                    <asp:TextBox ID="txtid" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"id") %>' Width="50" ></asp:TextBox>       
                </EditItemTemplate>
                <ItemTemplate >
                    <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"id") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddid" runat="server" Width="50" ></asp:TextBox>       
                </FooterTemplate>
            </asp:TemplateField> 
                
            <asp:TemplateField HeaderText="profile" HeaderStyle-Width="50px">
                <EditItemTemplate>
                    <asp:TextBox ID="txtprofile" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"profile") %>'  Width="150"></asp:TextBox>       
                </EditItemTemplate>
                <ItemTemplate >
                    <asp:Label ID="lblprofile" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"profile") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddprofile" runat="server" Width="150"></asp:TextBox>       
                </FooterTemplate>
            </asp:TemplateField> 
                
            <asp:TemplateField HeaderText="connection_string" HeaderStyle-Width="600px">
                <EditItemTemplate>
                    <asp:TextBox ID="txtconnection_string" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"connection_string") %>'  Width="600"></asp:TextBox>       
                </EditItemTemplate>
                <ItemTemplate >
                    <asp:Label ID="lblconnection_string" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"connection_string") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddconnection_string" runat="server" Width="600"></asp:TextBox>       
                </FooterTemplate>
            </asp:TemplateField>    

            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtnUpdate" CommandName="Update" runat="server" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Update</asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel"  CommandName="Cancel" runat="server" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Cancel</asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Delete</asp:LinkButton>
                </ItemTemplate>        
                <FooterTemplate>
                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')">Add</asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>  

        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" Height="30px" />
        <RowStyle  />
      </asp:GridView>


      </td></tr>
     
     </table>
    </form>
</body>
</html>
