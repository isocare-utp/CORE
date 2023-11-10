<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_education_to_promote_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr align="center">  
        <th width="3%">
        </th>
        <th width="10%">
            <span>วันที่ขอทุน</span>
        </th> 
         
        <th width="10%">
            <span>ทะเบียน</span>
        </th>
        <th width="34%">
            <span>ชื่อ สกุล</span>
        </th>
         
        <th width="34%">
            <span>ชื่อ สกุล บุตร</span>
        </th>
        <th width="10%">
            <span>เกรดเฉลี่ย</span>
        </th>
<%--        <th width="20%">
            <span>ระดับชั้น</span>
        </th>--%>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Auto">
    <table class="DataSourceRepeater">      
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>    
                <tr>
                    <td width="3%" align="center">
                        <asp:CheckBox ID="choose_flag" runat="server" />
                    </td> 
                     <td width="10%">
                        <asp:TextBox ID="req_date" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>                   
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="34%">
                        <asp:TextBox ID="full_name" runat="server" Style="text-align:left;" ReadOnly="true"></asp:TextBox>
                    </td>
                   
                    <td width="34%">
                        <asp:TextBox ID="child_name" runat="server" Style="text-align:left;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="gpa" runat="server" Style="text-align:right;" ReadOnly="true"></asp:TextBox>
                    </td>
                  <%--  <td width="20%">
                        <asp:TextBox ID="education_desc" runat="server" Style="text-align:left;" ReadOnly="true"></asp:TextBox>
                    </td>--%>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
