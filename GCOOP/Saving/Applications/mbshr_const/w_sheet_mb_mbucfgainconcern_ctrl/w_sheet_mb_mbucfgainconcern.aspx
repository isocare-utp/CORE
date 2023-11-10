﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_mb_mbucfgainconcern.aspx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfgainconcern_ctrl.w_sheet_mb_mbucfgainconcern" %>

<%@ Register Src="DsList.ascx" TagName="dsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r); //ให้รู้ว่ากดแถวไหนในลิส
                PostdeleteRow(); //PostDeleteRow(); ไว้ลบแถวที่เลือก
            }
        }

        function OnListItemChanged(s, r, c, v) {
            if (c == "concern_code") {
                var ls_row = dsList.GetRowCount() - 1;
                var ls_concern = dsList.GetItem(r, "concern_code");
                for (var i = 0; i < ls_row; i++) {
                    var ls_concern_code = dsList.GetItem(i, "concern_code");
                    if (ls_concern == ls_concern_code) {
                        alert("รหัสความสัมพันธ์ผู้รับโอนผลประโยชน์ซ้ำ");
                        dsList.SetItem(r, "concern_code", "");
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="margin-right: 1px; width: 720px;">
        <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
        <uc1:dsList ID="dsList" runat="server" />
    </div>
</asp:Content>
