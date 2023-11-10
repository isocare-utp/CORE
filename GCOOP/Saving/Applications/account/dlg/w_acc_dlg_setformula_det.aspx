<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_acc_dlg_setformula_det.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_acc_dlg_setformula_det" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        #B_close
        {
            width: 105px;
        }
        #B_save
        {
            width: 105px;
        }
        .linkSpan
        {
            float: right;
            cursor: pointer;
        }
        *
        {
            padding: 0;
            margin: 0;
            top: 0px;
            left: 0px;
        }
        .style1
        {
        }
        .style2
        {
            width: 49px;
        }
        .style3
        {
            width: 602px;
        }
    </style>
</head>
<%=postSaveData%>
<%=postDeleteRow%>
<%=jsPostSearch%>
<script type="text/javascript">

     function OnDwmasterClick(s,r,c) {
        Gcoop.CheckDw(s, r, c, "choose_flag", 1, 0);
     }
     
     //ฟังก์ชันการส่ง currentrow ให้ Hiden Field
     function Dw_dataItemFocusChanged(s,r,c) {
        Gcoop.GetEl("HdCurrentrow").value = r + "";
     }
     
    function OnDwdataClick(s,r,c){
        if(c == "show_status"){
             Gcoop.CheckDw(s, r, c, "show_status", 1, 0);
        }else if(c=="b_del"){
            Gcoop.GetEl("HdRowDelete").value = r + "";
            postDeleteRow();
        }
    }
   
   function SaveData()
   {
        if (confirm ("ยืนยันการบันทึกข้อมูล")){
            postSaveData();
        }
   }
     //ฟังก์ชันในการปิด dialog
     function OnCloseDialog() {
        if (confirm ("ยืนยันการออกจากหน้าจอ ")){
            parent.RemoveIFrame();
        }
    }

    function OnClickSearchButton(sender, row, bName) {
        jsPostSearch();
    }

    function OnDwSearchItemChange(s, r, c, v) {
        objDwsearch.SetItem(r, c, v);
        objDwsearch.AcceptText();
        jsPostSearch();
    }

</script>

<body>
    <form id="form1" runat="server">
    <div style="font-family: Tahoma; font-size: small">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">

        <tr><td valign="top" align="right" colspan="5">
                            <dw:WebDataWindowControl ID="Dwsearch" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_dlg_acc_search_account_id" LibraryList="~/DataWindow/account/acc_set_formula.pbl"
                        ClientEventButtonClicked="OnClickSearchButton" ClientEventItemChanged="OnDwSearchItemChange" > 
                    </dw:WebDataWindowControl>
        
        
        </td></tr>

            <tr>
                <td valign="top" colspan="3">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" BorderStyle="Ridge"
                        Height="430px" Width="430px">
                        <dw:WebDataWindowControl ID="Dw_data" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_set_formula_det"
                            LibraryList="~/DataWindow/account/acc_set_formula.pbl" ClientEventClicked="OnDwdataClick"
                            ClientEventItemFocusChanged="Dw_dataItemFocusChanged" RowsPerPage="15"><pagenavigationbarsettings 
                            navigatortype="NumericWithQuickGo" visible="True">
                        </pagenavigationbarsettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                

                <td valign="top" class="style3">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" Height="430px" 
                        Width="458px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" 
                            AutoRestoreContext="False" AutoRestoreDataCache="True" 
                            AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnDwmasterClick" 
                            ClientFormatting="True" ClientScriptable="True" 
                            DataWindowObject="d_acc_set_formula_det_accmaster_new" 
                            LibraryList="~/DataWindow/account/acc_set_formula.pbl" RowsPerPage="15">
                            <pagenavigationbarsettings navigatortype="NumericWithQuickGo" visible="True">
                            </pagenavigationbarsettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style1" valign="top">
                    <asp:HiddenField ID="HdCurrentrow" runat="server" />
        <asp:HiddenField ID="Hd_account_id" runat="server" />
                </td>
                <td class="style1" valign="top">
                    <asp:HiddenField ID="HdRowDelete" runat="server" />
                </td>
                <td class="style2" valign="top">
                    <asp:HiddenField ID="Hd_moneycode" runat="server" />
                </td>
                <td valign="top" class="style3">
                    &nbsp;<asp:Button ID="B_back" runat="server" Text="&lt; &lt;" UseSubmitBehavior="False"
                        Width="60px" OnClick="B_back_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_save" type="button" value="บันทึก" onclick="SaveData()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                        id="B_close" type="button" value="ยกเลิก/ปิดหน้าจอ" onclick="OnCloseDialog()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HiddenField
                            ID="Hd_moneyseq" runat="server" />
                    <asp:HiddenField ID="Hd_account_name" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
