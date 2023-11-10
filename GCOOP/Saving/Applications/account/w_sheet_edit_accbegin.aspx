<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_edit_accbegin.aspx.cs"
    Inherits="Saving.Applications.account.w_sheet_edit_accbegin" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postSumledger%>
    <%=postDrCr%>
    <script type="text/javascript">
    function GenerateSumleger()
    {
        postSumledger();
    }
   
   //ฟังก์ชันที่เปลี่ยน dropdownlist แล้วให้ชื่อ account_id ขึ้นอัตโนมัติ
     function OnDwmainItemChange(s,r,c,v) {
         if (c == "begin_dr_amount") {
            Gcoop.GetEl("Hd_row").value = r + "";
            objDw_main.SetItem(r,c,v);
            objDw_main.AcceptText();
            postDrCr();

        } else if (c == "begin_cr_amount") {
            Gcoop.GetEl("Hd_row").value = r + "";
            objDw_main.SetItem(r,c,v);
            objDw_main.AcceptText();
            postDrCr();
        }
     }
     
    //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
        if (!isconfirm){
            return false;        
        }
        var RowDetail = objDw_main.RowCount();
        var alertstr = "";
        
        var account_id = objDw_main.GetItem(RowDetail,"account_id");
        if (account_id == "" || account_id == null){
           alertstr = alertstr + "_กรุณากรอกรหัสบัญชี\n";
        }
      
        if (alertstr == "")
        {
            return true;
        }
        else {
            alert(alertstr);
            return false;
        }
    }
        
        
    
    </script>

    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 598px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                <b>รายการบัญชี</b>
            </td>
           <%-- <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="B_gen" type="button" value="Genarate" onclick="GenerateSumleger()" /></td>
            <td>
                &nbsp;
            </td>--%>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server" Height="420px" ScrollBars="Vertical" 
                    BorderStyle="Ridge">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_acc_editacc_begin"
                    LibraryList="~/DataWindow/account/editacc_begin.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventButtonClicked="OnDwMainButton" 
    ClientEventClicked="OnDwmainClick" ClientEventItemChanged="OnDwmainItemChange">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="3">
                &nbsp;<asp:Panel ID="Panel2" runat="server">
                    <dw:WebDataWindowControl ID="Dw_footer" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_acc_editacc_begin_footer" 
                        LibraryList="~/DataWindow/account/editacc_begin.pbl">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:HiddenField ID="HdRowDelete" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:HiddenField ID="Hd_accid" runat="server" />
                <asp:HiddenField ID="Hd_row" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
