<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_cbt.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_cbt" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

.objDw-main1466{;background-color:transparent;OVERFLOW:hidden}
.objDw-main1467{;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:12pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
.objDw-main1468{;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-main146B{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-main146D{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-main146E{;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
    </style>
</head>

<script type ="text/javascript" >
    function OnB_OKClick()
    {
        var bank_code = objDw_main.GetItem(1,"expense_bank");
        var bank_branch = objDw_main.GetItem(1,"expense_branch");
        var expense_accid = objDw_main.GetItem(1,"expense_accid");
        window.opener.TypeCBT(bank_code,bank_branch,expense_accid);
        window.close(); 
    }
</script> 
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
    
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Width="350px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                            ClientFormatting="True" ClientScriptable="True" 
                            DataWindowObject="d_divavgsrv_dlg_methodpayment_cbt" 
                            LibraryList="~/DataWindow/shrlon/div_avg.pbl">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="B_ok" type="button" value="ตกลง" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
