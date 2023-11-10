<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_acc_dlg_setformula_det_sm.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_acc_dlg_setformula_det_sm" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 411px;
        }
        #B_save
        {
            width: 76px;
        }
        .objDw-masterF8
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-masterF9
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-masterFC
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:underline;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-masterFE
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:14pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:underline;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-master100
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master102
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master103
        { ;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master104
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:none}
        .objDw-master106
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-master107
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master10A
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:underline;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master10C
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:14pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:underline;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-master10E
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master110
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master111
        { ;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-master112
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:none}
.objDw-masterE82{;background-color:transparent;OVERFLOW:hidden}
.objDw-masterE83{;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-masterE86{;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:underline;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-masterE88{;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:14pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:underline;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
.objDw-masterE8A{;background-color:#e6e6e6;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-masterE8C{;background-color:#e6e6e6;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-masterE8D{;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-masterE8E{;background-color:transparent;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:none}
    </style>
</head>
<%=postDwSheetChoose%>
<%=postDeleteRowDwData %>
<%=postSaveData %>

<script type="text/javascript">
    
     function OnDwmasterClick(s,r,c)
     {
        Gcoop.CheckDw(s, r, c, "choose_flag",1 ,0);
     }
     
    
    function OnDwDataButtonClick(s,r,bName)
    {
        if(bName=="b_del")
        {
            if (confirm ("ต้องการลบข้อมูลแถวนี้ใช่หรือไม่ ? "))
            {
                Gcoop.GetEl("Hd_RowDwdata").value = r +"";
                postDeleteRowDwData();
            }   
        }
        return 0;
    }
    
    function OnDwSheetChooseItemChange(s, r, c, v){
        if(c == "section_id"){
            objDw_sheetchoose.SetItem(r, c, v);
            objDw_sheetchoose.AcceptText();
            Gcoop.GetEl("Hd_sectionid").value = v;
            postDwSheetChoose();
        }
        return 0;
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
     
</script>

<body>
    <form id="form1" runat="server">
    <div style="font-family: Tahoma; font-size: small">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr valign="top">
                <td valign="top" class="style1" colspan="4">
                    <asp:Panel ID="Panel1" runat="server" Height="424px" ScrollBars="Horizontal" BorderStyle="Inset"
                        Width="400px">
                        <dw:WebDataWindowControl ID="Dw_data" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_set_formula_det_sm"
                            LibraryList="~/DataWindow/account/acc_set_formula.pbl" 
                            ClientEventButtonClicked="OnDwDataButtonClick" RowsPerPage="15"><pagenavigationbarsettings 
                            navigatortype="NumericWithQuickGo" visible="True">
                        </pagenavigationbarsettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top" height="20px">
                    <asp:Panel ID="Panel2" runat="server" Height="30px" BorderStyle="Inset" 
                        Width="470px">
                        <dw:WebDataWindowControl ID="Dw_sheetchoose" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_acc_section_sheetchoose1" LibraryList="~/DataWindow/account/acc_set_formula.pbl"
                            Style="top: 0px; left: 0px" ClientEventItemChanged="OnDwSheetChooseItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Height="390px" BorderStyle="Inset" ScrollBars="Horizontal"
                        Width="470px">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" 
                            AutoRestoreContext="False" AutoRestoreDataCache="True" 
                            AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnDwmasterClick" 
                            ClientFormatting="True" ClientScriptable="True" 
                            DataWindowObject="d_acc_set_formula_det_sm_choose" 
                            LibraryList="~/DataWindow/account/acc_set_formula.pbl" RowsPerPage="11" 
                            style="top: 0px; left: 0px; height: 112px">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;<asp:HiddenField ID="Hd_RowDwdata" runat="server" />
                </td>
                <td class="style1">
                    <asp:HiddenField ID="Hd_moneyseq" runat="server" />
                </td>
                <td class="style1">
                    <asp:HiddenField ID="Hd_moneycode" runat="server" />
                </td>
                <td class="style1">
                    <asp:HiddenField ID="Hd_sectionid" runat="server" />
                </td>
                <td valign="top">
                    &nbsp;<asp:Button ID="B_back" runat="server" Text="&lt; &lt;" UseSubmitBehavior="False"
                        Width="60px" OnClick="B_back_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                        id="B_save" type="button" value="บันทึกข้อมูล" onclick="SaveData()" />&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_close" type="button" value="ปิดหน้าจอ" onclick="OnCloseDialog()" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
