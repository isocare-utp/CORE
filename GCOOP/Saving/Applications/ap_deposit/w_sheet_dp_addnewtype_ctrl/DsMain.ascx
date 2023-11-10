<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_addnewtype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                    <div>
                        <span>รหัสประเภท:</span>
                    </div>
                </td>
                <td colspan="3" width="35%">
                    <div>
                        <asp:TextBox ID="depttype_code" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>กลุ่มประเภท:</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <div>
                        <asp:DropDownList ID="depttype_group" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อประเภท:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="depttype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>บุคคล:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="persongrp_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>กลุ่มเงินฝาก:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:DropDownList ID="deptgroup_code" runat="server" Width="96%">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>รหัสคู่บัญชี:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="account_id" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตัวย่อ:</span>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <asp:TextBox ID="type_prefix" runat="server" Width="90%" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>เลขที่เอกสาร:</span>
                    </div>
                </td>
                <td colspan="2">
                    <div>
                        <asp:TextBox ID="document_code" runat="server" Width="98%" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="12%">
                    <div>
                        <span>กลุ่มลง ACC:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="depttype_accgrp" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
