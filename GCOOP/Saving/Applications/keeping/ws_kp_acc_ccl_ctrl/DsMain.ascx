<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_acc_ccl_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>งวด :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="recv_period" runat="server"></asp:TextBox>
                </td>
                <td width="20%" bgcolor="#CCCCCC">
                    <asp:CheckBox ID="member_status" runat="server" />
                    ปิดบัญชีสมาชิก
                </td>
                <td width="15%">
                    <div>
                        <span>วันที่เป็นสมาชิก :</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="member_date" runat="server" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สมาชิก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Width="146px"></asp:TextBox><asp:Button
                        ID="b_search" runat="server" Text="..." Width="20px" />
                </td>
                <td bgcolor="#CCCCCC">
                    <asp:CheckBox ID="resign_status" runat="server"/>
                    ลาออก
                </td>
                <td>
                    <div>
                        <span>วันที่ลาออก :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="resign_date" runat="server" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสมาชิก :</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="cp_name" runat="server" Width="614px" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสมชิก(eng) :</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="cp_ename" runat="server" Width="614px" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด :</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="cp_membgrp" runat="server" Width="614px" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปรเภทสมาชิก :</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="cp_membtyp" runat="server" Width="614px" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
