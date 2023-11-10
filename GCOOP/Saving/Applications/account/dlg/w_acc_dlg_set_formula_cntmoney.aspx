<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_acc_dlg_set_formula_cntmoney.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_acc_dlg_set_formula_cntmoney" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            color: #0000CC;
            width: 704px;
        }
        .style3
        {
            width: 704px;
        }
        .objDw-main3EF
        { ;background-color:transparent;OVERFLOW:hidden;VISIBILITY:hidden}
        .objDw-main3F0
        { ;background-color:#a6caf0;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3F2
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-main3F3
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3F5
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-detail229
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-detail22A
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:none}
        .style5
        {
            font-weight: bold;
            color: #0000CC;
            width: 704px;
            height: 7px;
        }
        .style6
        {
            height: 7px;
        }
        .objDw-main147A
        { ;background-color:transparent;OVERFLOW:hidden;VISIBILITY:hidden}
        .objDw-main147B
        { ;background-color:#a6caf0;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main147D
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-main147E
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main1480
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-main159B
        { ;background-color:transparent;OVERFLOW:hidden;VISIBILITY:hidden}
        .objDw-main159C
        { ;background-color:#a6caf0;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main159E
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-main159F
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main15A1
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        #B_close
        {
            width: 60px;
        }
        #B_save
        {
            width: 60px;
        }
    </style>
</head>

<%=postSaveConstant %>

<script type="text/javascript">
    function OnDwmainClick(s,r,c)
    {
        Gcoop.CheckDw(s, r, c, "flag", 1, 0);
    }
    
    function OnDwdetailClick(s,r,c)
    {
        if(c=="cnt_short") 
        {
             Gcoop.CheckDw(s, r, c, "cnt_short", 1, 0);
        }
        else if(c=="cnt_long")
        {
             Gcoop.CheckDw(s, r, c, "cnt_long", 1, 0);
        }
    }
    
    function SaveData() {
        var isConfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
            if (isConfirm) {
             postSaveConstant();        
       }
    }
    
     //ฟังก์ชันในการปิด dialog
     function OnCloseDialog() {
        if (confirm ("ยืนยันการออกจากหน้าจอ ")){
            window.parent.OnRefresh();  
            parent.RemoveIFrame();
        }
     }
     
    
     
    
</script>

<body>
    <form id="form1" runat="server">
    <div style="font-family: Tahoma; font-size: small">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td class="style1" colspan="3">
                    รายการ
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="200px" Width="531px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="dw_acc_const_code"
                            LibraryList="~/DataWindow/account/acc_set_formula.pbl" Style="top: 1px; left: 3px"
                            ClientEventClicked="OnDwmainClick" ClientFormatting="True"></dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    รายการสั้น - ยาว
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" Height="50px" Width="531px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="dw_acc_cont_typemoney" LibraryList="~/DataWindow/account/acc_set_formula.pbl"
                            ClientEventClicked="OnDwdetailClick" ClientFormatting="True"></dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3" colspan="3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style5" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;<input id="B_save" type="button" value="บันทึก" onclick="SaveData()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                        id="B_close" type="button" value="ปิด" onclick="OnCloseDialog()" />
                </td>
                <td class="style6">
                </td>
                <td class="style6">
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                    <asp:HiddenField ID="Hd_moneycode" runat="server" />
                </td>
                <td class="style3">
                    <asp:HiddenField ID="Hd_moneyseq" runat="server" />
                </td>
                <td class="style3">
                    <asp:HiddenField ID="Hd_datadesc" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
