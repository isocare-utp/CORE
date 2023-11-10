<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_vc_cash_loan.aspx.cs" Inherits="Saving.Applications.account.dlg.w_dlg_vc_cash_loan" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

 
    </style>
</head>


<script type="text/javascript">
    //ฟังก์ชันในการปิด dialog
    function OnCloseDialog() {
        if (confirm("ยืนยันการออกจากหน้าจอ ")) {
            // window.close();
            //            window.parent.postVoucherDate();
            parent.RemoveIFrame();
        }
    }

    function OnOkClick() {
        if (confirm("ยืนยันการเลือกสัญญาเงินกู้")) {
            var rowcount = objDw_main.RowCount();
            //            var operate_flag = objDw_main.GetItem(i, "operate_flag");
            var slip_no = "";
            for (var i = 1; i <= rowcount; i++) {
                var operate_flag = 0;
                var operate_flag = objDw_main.GetItem(i, "operate_flag");
                if (operate_flag == 1) {
                    if (i > 1 && slip_no != "") {

                        slip_no += ",";
                    }
                    slip_no += objDw_main.GetItem(i, "payoutslip_no");
                }
                else {
                }
            }
            parent.GetAccount(slip_no);
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
                    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Horizontal" BorderStyle="Inset"
                        Width="700px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_vc_trnrcvgrp_loan_csh"
                            LibraryList="~/DataWindow/account/vc_genwizard_vcdate.pbl" 
                            ClientEventButtonClicked="OnDwDataButtonClick" >
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
            <td valign="top" class="style9">
                    <%--<input id="B_refresh" type="button" value="รีเฟรช" onclick="OnRefreshClick()" />--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_save" type="button" value="ตกลง" onclick="OnOkClick()" />
                    <input id="B_cancel" type="button" value="ยกเลิก" onclick="OnCloseDialog()" />
                </td>
            </tr>
          </table>
        <br />
    </div>
    </form>
</body>
</html>

