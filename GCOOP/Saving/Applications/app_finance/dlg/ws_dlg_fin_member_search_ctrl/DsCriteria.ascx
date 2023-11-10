﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.ws_dlg_fin_member_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 560px;">
            <tr>
                <td width="12%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>   
                 <td width="15%">
                    <div>
                        <span>รหัสพนักงาน:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                    </div>
                </td>
             </tr>
             <tr>                          
                <td>
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>นามสกุล:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                    </div>
                </td>                
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสสังกัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="membgroup_nodd" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>                
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
