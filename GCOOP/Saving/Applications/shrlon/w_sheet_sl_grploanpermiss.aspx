<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_grploanpermiss.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_grploanpermiss" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>

    <script type="text/javascript">
    function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
    function OnAddRow(){
        objDwMain.InsertRow(objDwMain.RowCount() + 1);
    }
    function OnButtonClicked(s, r, c){
        if(c == "b_delete"){
                var detail = "รหัส " + objDwMain.GetItem(r, "loanpermgrp_code");
                detail += " : " + objDwMain.GetItem(r, "loanpermgrp_desc");
                
                if(confirm("คุณต้องการลบรายการ "+ detail +" ใช่หรือไม่?")){
                    objDwMain.DeleteRow(r);
                }
            }
            return 0;
    }
    </script>

    <style type="text/css">
        table.t
        {
            border-width: medium;
            border-spacing: 2px;
            border-style: ridge;
            border-color: gray;
            border-collapse: separate;
            background-color: white;
        }
        table.t th
        {
            border-width: 1px;
            padding: 1px;
            border-style: none;
            border-color: gray;
            background-color: white;
            -moz-border-radius: 0px 0px 0px 0px;
        }
        table.t td
        {
            border-width: 1px;
            padding: 1px;
            border-style: none;
            border-color: gray;
            background-color: white;
            -moz-border-radius: 0px 0px 0px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table >
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_sl_grploanpermiss"
                    LibraryList="~/DataWindow/shrlon/sl_grploanpermiss.pbl" ClientEventButtonClicked="OnButtonClicked">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <span class="linkSpan" onclick="OnAddRow()" style="font-size: small; font-family: Tahoma;
        float: left; color: #808080;">เพิ่มแถว</span>
</asp:Content>
