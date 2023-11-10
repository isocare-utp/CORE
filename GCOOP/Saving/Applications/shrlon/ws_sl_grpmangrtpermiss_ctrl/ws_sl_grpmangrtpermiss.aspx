<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_grpmangrtpermiss.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_grpmangrtpermiss_ctrl.ws_sl_grpmangrtpermiss" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {

        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "mangrtpermgrp_code") {
                PostRetrieve();
            }
        }

        function OnDsDetailClicked(s, r, c) {

        }

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "grtright_contflag") {
                if (v == "1") {
                    dsDetail.GetElement(0, "grtright_contract").readOnly = false;
                    dsDetail.GetElement(0, "grtright_contract").style.background = "#FFFFFF";
                } else {
                    dsDetail.GetElement(0, "grtright_contract").readOnly = true;
                    dsDetail.GetElement(0, "grtright_contract").style.background = "#CCCCCC";
                    dsDetail.SetItem(0, "grtright_contract", 0);
                }
            } else if (c == "grtright_memflag") {
                if (v == "1") {
                    dsDetail.GetElement(0, "grtright_member").readOnly = false;
                    dsDetail.GetElement(0, "grtright_member").style.background = "#FFFFFF";
                } else {
                    dsDetail.GetElement(0, "grtright_member").readOnly = true;
                    dsDetail.GetElement(0, "grtright_member").style.background = "#CCCCCC";
                    dsDetail.SetItem(0, "grtright_member", 0);
                }
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                PostDelRow();
            }
        }

        function OnDsListItemChanged(s, r, c, v) {

        }

        function SheetLoadComplete() {
            if (dsDetail.GetItem(0, "grtright_contflag") == "0") {
                dsDetail.GetElement(0, "grtright_contract").readOnly = true;
                dsDetail.GetElement(0, "grtright_contract").style.background = "#CCCCCC";
            }

            if (dsDetail.GetItem(0, "grtright_memflag") == "0") {
                dsDetail.GetElement(0, "grtright_member").readOnly = true;
                dsDetail.GetElement(0, "grtright_member").style.background = "#CCCCCC";
            }
        }

        function AddNewGroup() {
            Gcoop.OpenIFrame2("520", "250", "ws_dlg_sl_add_grpmangrtpermiss.aspx", "");
        }

        function GetValueFromDlg(mangrtpermgrp_code) {
            dsMain.SetItem(0, "mangrtpermgrp_code", mangrtpermgrp_code);
            PostRetrieve();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <span class="NewRowLink" onclick="AddNewGroup()" style="font-size: small;">เพิ่มประเภทใหม่</span>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc3:DsDetail ID="dsDetail" runat="server" />
    <br />
    <table width="97%">
        <tr>
            <td>
                <strong style="font-size: small;"><u>วงเงินการค้ำ</u></strong>
            </td>
            <td align="right">
                <span class="NewRowLink" onclick="PostInsertRow()" style="font-size: small;">เพิ่มแถว
                </span>
            </td>
        </tr>
        <uc2:DsList ID="dsList" runat="server" />
    </table>
</asp:Content>
