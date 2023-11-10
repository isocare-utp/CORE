<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_adjmth_ccl_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขที่สมาชิก :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="member_no" runat="server" Width="178px" Style="text-align: center"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Width="22px" />
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
                <td width="20%">
                    <asp:TextBox ID="member_date" runat="server" Style="background: #CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server" Style="background: #CCCCCC"></asp:TextBox>
                </td>
                <td bgcolor="#CCCCCC">
                    <asp:CheckBox ID="resign_status" runat="server" />
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
                        <span>สังกัด :</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="cp_membgrp" runat="server" Width="355px" Style="background: #CCCCCC"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ปรเภทสมาชิก :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_membtyp" runat="server" Style="background: #CCCCCC"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
