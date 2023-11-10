<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_printbook_new.aspx.cs"
    Inherits="Saving.Applications.mbshr.dlg.w_dlg_dp_printbook_new" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Book</title>
    <%=postPrintBook%>

    <script type="text/javascript">
        function DialogLoadComplete() {
            try{
                var isPostBack = Gcoop.GetEl("HdIsPostBack").value == "true";
                if(isPostBack){
                    //parent.PrintBookReload();
                }
            }catch(err){}
            try{
                var isNewBook = Gcoop.GetEl("HdIsNewBook").value;
                if (isNewBook == "true") {
                    DialBookNew();
                    //ExitIFrame();
                }
            } catch (err) { }
            
            try {
                var isZero = Gcoop.GetEl("HdIsZeroPage").value;
                if (isZero == "true") {
                    ExitIFrame();
                }
            } catch (err) { }
            document.addEventListener("keyup", HandleOnKeyUp, true);
            Gcoop.GetEl("Button1").focus();
        }
        
        function OnKeyUpEnd(e){
            if(e.keyCode == "27" || e.keyCode == "109" || e.keyCode == "189"){
                ExitIFrame();
            }
        }
        // เพิ่มเพื่อให้เรียก ไดอะร็อค ออกสมุดใหม่ 
        function DialBookNew() {
            var mem_no = objDwPrintPrompt.GetItem(1, "member_no");
            var deptPassBookNo = "";
            try {
                deptPassBookNo = objDwPrintPrompt.GetItem(1, "member_no");
                deptPassBookNo = Gcoop.StringFormat(deptPassBookNo, "0000000000");
            } catch (err) { deptPassBookNo = "0000000000"; }
            if (mem_no != "") {
                var deptTypeCode = objDwPrintPrompt.GetItem(1, "depttype_code");
                Gcoop.OpenDlgIn(550, 350, "w_dlg_dp_booknew.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo + "&deptTypeCode=" + deptTypeCode);
            }
        }
        function ExitIFrame() {
            try {
                //  parent.PrintBookReload();
                parent.setcursor();
            } catch (err) { }
            try {
                //  alert("inexitframe");
                parent.setcursor();
                // alert("outexitframe")   
            } catch (err) {
//                alert("ErrorexitIfarme");
                parent.RemoveIFrame();
            }
        }

    </script>

    <style type="text/css">
        #Button1
        {
            width: 243px;
        }
        #Button2
        {
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div>
        <table style="width: 100%;" align="center">
            <tr>
                <td>
                 <dw:WebDataWindowControl ID="DwStm" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="dlg_conntack_detail"
                    LibraryList="~/DataWindow/mbshr/sl_contack.pbl" RowsPerPage="10" ClientScriptable="True"
                    ClientFormatting="True" ClientEventClicked="OnDwDetailClicked">
                    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
                    <%-- <dw:WebDataWindowControl ID="DwStm" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dp_dept_stm_prnt"
                    LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" RowsPerPage="10" ClientScriptable="True"
                    ClientFormatting="True">
                     <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
                    </PageNavigationBarSettings>
                </dw:WebDataWindowControl>--%>
                <br >
                </td>
                
            </tr>
            <tr>
                <td align="center">
        <dw:WebDataWindowControl ID="DwNewBook" runat="server" DataWindowObject="d_dp_booknew"
            LibraryList="~/DataWindow/mbshr/sl_contack.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientValidation="False">
        </dw:WebDataWindowControl>
        <br />
        <dw:WebDataWindowControl ID="DwPrintPrompt" runat="server" DataWindowObject="d_dppbk_prompt_print_share"
            LibraryList="~/DataWindow/mbshr/sl_contack.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" TabIndex="1">
        </dw:WebDataWindowControl>
        &nbsp;
        <div align="center">
            <input id="Button1" style="width:65px;" type="button" value="พิมพ์" onclick="postPrintBook()" />
            &nbsp; &nbsp;
            <input id="Button2" style="width:65px;" type="button" value="ปิด" onclick="ExitIFrame()" />
        </div>
                </td>
              
            </tr>
        </table>
     

     
        <asp:HiddenField ID="HdAccountNo" runat="server" />
        <asp:HiddenField ID="HdIsZeroPage" runat="server" />
        <asp:HiddenField ID="HdIsPostBack" runat="server" />
        <asp:HiddenField ID="HdIsNewBook" runat="server" />
    </div>
    </form>
</body>
</html>
