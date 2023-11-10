<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.admin_const.ws_am_cmcoop_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<!--
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
-->
        <table class ="DataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>Coop_No</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="Coop_No" runat="server"></asp:TextBox>
                    </div>
                </td>
                 <td>
                    <div>
                        
                    </div>
                </td>
                <td>
                    <div>
                        <span>Coop_Name</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="Coop_Name" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            
             <tr>
                <td width ="10%">
                    <div>
                        <span>Coop_Addr</span>
                    </div>
                </td>
                <td colspan ="7">
                    <div>
                        <asp:TextBox ID="Coop_Addr" runat="server" style ="width:99%"></asp:TextBox>
                    </div>
                </td>
              </tr>

              <tr>
                <td width ="10%">
                    <div>
                        <span>Tambol</span>
                    </div>
                </td>
                <td width ="15%">
                    <div>
                        <asp:DropDownList ID="Tambol" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width ="10%">
                    <div>
                        <span>District_Code</span>
                    </div>
                </td>
                <td width ="15%">
                    <div>
                        <asp:DropDownList ID="District_Code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width ="10%">
                    <div>
                        <span>Province_Code</span>
                    </div>
                </td>
                <td width ="15%">
                    <div>
                        <asp:DropDownList ID="Province_Code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width ="10%">
                    <div>
                        <span>postcode</span>
                    </div>
                </td>
                <td width ="15%">
                    <div>
                        <asp:TextBox ID="postcode" runat="server"></asp:TextBox>
                    </div>
                </td>
              </tr>

              <tr>
                <td>
                    <div>
                        <span>Chairman</span>
                    </div>
                </td>
                <td colspan ="3">
                    <div>
                        <asp:TextBox ID="Chairman" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>Coop_Id</span>
                    </div>
                </td>
                 <td>
                    <div>
                        <asp:TextBox ID="Coop_Id" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>Coop_Fax</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="Coop_Fax" runat="server"></asp:TextBox>
                    </div>
                </td>
              </tr>

              <tr>
                <td>
                    <div>
                        <span>Manager</span>
                    </div>
                </td>
                <td colspan ="3">
                    <div>
                        <asp:TextBox ID="Manager" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>Assistant</span>
                    </div>
                </td>
                <td colspan ="3">
                    <div>
                        <asp:TextBox ID="Assistant" runat="server"></asp:TextBox>
                    </div>
                </td>
              </tr>

              <tr>
                <td>
                    <div>
                        <span>Vicemanager</span>
                    </div>
                </td>
                <td colspan ="3">
                    <div>
                        <asp:TextBox ID="Vicemanager" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>Office_Finance</span>
                    </div>
                </td>
                <td colspan ="3">
                    <div>
                        <asp:TextBox ID="Office_Finance" runat="server"></asp:TextBox>
                    </div>
                </td>
              </tr>
        </table>
<!--    
    </EditItemTemplate>
</asp:FormView>
-->
