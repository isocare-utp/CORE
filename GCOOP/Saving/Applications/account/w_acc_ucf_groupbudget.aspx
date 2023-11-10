<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_groupbudget.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_ucf_groupbudget" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>
    <%=postShowTreeviewAll%>
    <%=postNoShowTreeviewAll%>
    <%=postEditData %>
    <%=postDeleteAccountId%>
    <%=postFindShow%>
    <style type="text/css">
        .style5
        {
            font-size: small;
            font-weight: bold;
        }
        .style6
        {
            font-size: small;
        }
    </style>
    <script type="text/javascript">

        function DeleteAccountId() {
            var accbudgetgroup_typ = "";
            accbudgetgroup_typ = objDw_master.GetItem(1, "accbudgetgroup_typ");
            if (confirm("ยืนยันการลบข้อมูลรหัสงบประมาณ : " + accbudgetgroup_typ)) {
                postDeleteAccountId();
            }
            return 0;
        }

        //Clear หน้าจอ
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }
        //แก้ไขข้อมูล
        function EditData() {
            postEditData();
        }
        // แสดง Tree ทั้งหมด
        function ShowTreeview() {
            postShowTreeviewAll();
        }
        // ย่อ Tree ทั้งหมด
        function NoShowTreeview() {
            postNoShowTreeviewAll();
        }

        //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");

            if (!isconfirm) {
                return false;
            }

            var accbudgetgroup_typ = objDw_master.GetItem(1, "accbudgetgroup_typ");
            var accbudgetgroup_des = objDw_master.GetItem(1, "accbudgetgroup_des");
            var budget_level = objDw_master.GetItem(1, "budget_level");
            var budget_supergrp = objDw_master.GetItem(1, "budget_supergrp");
            var sort_seq = objDw_master.GetItem(1, "sort_seq");

            if (accbudgetgroup_typ != "" && accbudgetgroup_typ != null && accbudgetgroup_des != "" && accbudgetgroup_des != null && budget_level != "" && budget_level != null &&
            budget_supergrp != "" && budget_supergrp != null && sort_seq != "" && sort_seq != null) {
                return true;
            }
            else {
                confirm("กรุณาระบุข้อมูลให้ครบถ้วน");
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td class="style5" colspan="2">
                    รายละเอียดผังงบประมาณ
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="150px" BorderStyle="None">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_acc_budget_group_2" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
                            ClientEventClicked="OnDwMainClick" ClientFormatting="True" Height="155px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="B_add" runat="server" OnClick="B_add_Click" Text="เพิ่มข้อมูล" Width="70px"
                        UseSubmitBehavior="False" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Width="496px">
                        <asp:Button ID="B_edit" runat="server" Text="แก้ไขข้อมูล" Width="70px" OnClick="B_edit_Click"
                            UseSubmitBehavior="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="ButtonDelete" onclick="DeleteAccountId()" type="button" value="ลบข้อมูล" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <span class="linkSpan" onclick="ShowTreeview()" style="font-family: Tahoma; font-size: small;
                        float: left; color: #0000CC;">แสดงผังงบประมาณทั้งหมด&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </span><span class="linkSpan" onclick="NoShowTreeview()" style="font-family: Tahoma;
                        font-size: small; float: left; color: #0000CC;">ย่อผังงบประมาณทั้งหมด</span>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    ผังงบประมาณ :
                    <asp:Label ID="lbl_coopname" runat="server" Text="coopname"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" EnableClientScript="False"
                        ImageSet="Arrows" OnTreeNodePopulate="TreeView1_TreeNodePopulate" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                        OnTreeNodeExpanded="TreeView1_TreeNodeExpanded">
                        <ParentNodeStyle Font-Bold="False" ForeColor="#FF0066" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <RootNodeStyle ForeColor="#0000CC" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                        <LeafNodeStyle ForeColor="#009900" />
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hd_aistatus" runat="server" />
                    <asp:HiddenField ID="Hd_checkclear" runat="server" Value="false" />
                    <asp:HiddenField ID="Hd_accid_master" runat="server" />
                    <asp:HiddenField ID="HdAccNo" runat="server" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
