<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl.w_dlg_mb_editmembgroup_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0" style="width: 600px;">
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="b_save" runat="server" Text="บันทึก" Style="width: 50px;" />
                </td>
            </tr>
            <tr>
                <tr>
                    <td>
                        <u><b>รายละเอียด</b></u>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <div>
                            <span>รหัสสังกัด :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="membgroup_code" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <div>
                            <span>ชื่อสังกัด :</span>
                        </div>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>หน่วยคุม :</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="membgroup_control" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div>
                            <span>หน่วยลูกหนี้ตัวแทน :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="membgroup_agentgrg" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ภูมิภาคสังกัด :</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="membgrptype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <%--<td>
                        <div>
                            <span>กลุ่มขึ้นแผ่นจัดเก็บ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="savedisk_type" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>--%>
                </tr>
            </tr>
            <tr>
                <tr>
                    <td>
                        <u><b>ที่อยู่</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>เลขที่ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_no" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>หมู่ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_moo" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ถนน :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_road" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>ซอย :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_soi" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>จังหวัด :</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="addr_province" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div>
                            <span>อำเภอ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="addr_amphur" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>ตำบล :</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="addr_tambol" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div>
                            <span>รหัสไปรษณีย์ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_postcode" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span>โทรศัพท์ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_phone" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <div>
                            <span>โทรสาร :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="addr_fax" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
