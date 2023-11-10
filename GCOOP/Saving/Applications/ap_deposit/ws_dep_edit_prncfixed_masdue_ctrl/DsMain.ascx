<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>เลขที่บัญชี :<span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="deptaccount_no" runat="server" Style="width:80%;text-align:center;"  ForeColor="Red" BackColor="Yellow"  onfocus="this.select()" 
                        TabIndex="1"></asp:TextBox>
                    <asp:Button ID="b_searchdeptno" Text="..." runat="server" Style="float:left;text-align:center;width:15%;height:24px" />
                </td> 
                <td width="10%">
                    <asp:TextBox ID="depttype_code" runat="server" Style="text-align:center;" ReadOnly="true" BackColor="#EEEEEE"></asp:TextBox>
                </td> 
                <td width="20%">
                    <div>
                        <span>ประเภทบัญชี :<span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="depttype_desc" runat="server" Style="width:97%;" ReadOnly="true" BackColor="#EEEEEE"></asp:TextBox>
                </td> 
            </tr>  
             <tr>
                <td>
                    <div>
                        <span>ชื่อบัญชี (TH) :<span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="deptaccount_name" runat="server" Style="width:99%;" ReadOnly="true" BackColor="#EEEEEE"></asp:TextBox>
                </td>   
                <td>
                    <div>
                        <span>ชื่อบัญชี (EN) :<span>
                    </div>
                </td>
                <td >
                    <asp:TextBox ID="DEPTACCOUNT_ENAME" runat="server" Style="width:97%;" ReadOnly="true" BackColor="#EEEEEE"></asp:TextBox>
                </td>                 
            </tr> 
            <tr>
                <td>
                    <div>
                        <span>เพื่อ :<span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="DEPT_OBJECTIVE" runat="server" Style="width:99%;" ReadOnly="true" BackColor="#EEEEEE"></asp:TextBox>
                </td>   
            </tr>          
        </table>
    </EditItemTemplate>
</asp:FormView>