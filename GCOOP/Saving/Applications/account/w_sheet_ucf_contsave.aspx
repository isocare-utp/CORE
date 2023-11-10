<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ucf_contsave.aspx.cs" Inherits="Saving.Applications.account.w_sheet_ucf_contsave" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDeleteRow%>
    <%=postAddRow%>
    <%=postInit%>
    <%=postNewClear%>

<script type ="text/javascript" >
    function OnDwMainItemChange(s, r, c, v) {
        if (c == "acc_year") {
            objDw_main.SetItem(1, "acc_year", v);
            objDw_main.AcceptText();
        }
        else if (c == "acc_period") {
            objDw_main.SetItem(1, "acc_period", v);
            objDw_main.AcceptText();
        }
    }

    function MenubarNew() 
    {
        postNewClear();
    }

    function OnDwMainbuttonClick(s, r, b) {
        if (b == "b_process") 
        {
            var acc_year = objDw_main.GetItem(1, "acc_year");
            var acc_period = objDw_main.GetItem(1, "acc_period");
            if (acc_year == null) {
                alert("กรุณากรอกข้อมูลปี");
            }
            else if (acc_period == null) {
                alert("กรุณากรอกข้อมูลงวด");
            }
            else {
                postInit();
            }
        }
    }

    function OnDwdataButtonClick(sender, row, bName) {
        if (bName == "b_del") {
            var isConfirm = confirm("ต้องการลบข้อมูลแถวนี้ใช่หรือไม่ ?");
            if (isConfirm) {
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow();
            }
        }
        return 0;
    }
    // Add Row
    function DwDataAddRow() {
        var acc_year = objDw_main.GetItem(1, "acc_year");
        var acc_period = objDw_main.GetItem(1, "acc_period");
        if (acc_year == null) {
            alert("กรุณากรอกข้อมูลปี");
        }
        else if (acc_period == null) {
            alert("กรุณากรอกข้อมูลงวด");
        }
        else {
            postAddRow();
        }
    }

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
</script> 
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                <br />
    <strong><span class="style1">ระบุปีและงวด</span><br />
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_acc_criteria_year_period" 
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
                        ClientEventButtonClicked="OnDwMainbuttonClick" ClientEventItemChanged="OnDwMainItemChange">
                    </dw:WebDataWindowControl>
                </strong>
    <br />
    <strong><span class="style1">รายละเอียดค่าคงที่กระทบยอด</span></strong><br 
        class="style1" />
                <asp:Panel 
        ID="Panel1" runat="server" Height="200px" 
        ScrollBars="Auto" Width="746px" BorderStyle="Ridge">
                    <dw:WebDataWindowControl ID="Dw_data" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_acc_const_save" 
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
                        ClientEventButtonClicked="OnDwdataButtonClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
                <span class="linkSpan" onclick="DwDataAddRow()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span><br />
    <table style="width:100%;">
        <tr>
            <td>
                <asp:HiddenField ID="Hd_row" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

