<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_cfloanconstant.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_cfloanconstant" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=itemChanged %>

    <script type="text/javascript">
//    function OnItemChanged(s,r,c,v){
//        if(c == ""){
//            objdw_main.SetItem(r, c, v);
//            objdw_main.AccepText();
//        }
//    }
    function Validate(){
        return confirm("ยืนยันการบันทึกข้อมูล?");
    }
    function OnItemChanged(sender, rowNumber, columnName, newValue){
        objdw_main.SetItem(rowNumber, columnName, newValue);
        objdw_main.AcceptText();
            if( columnName == "rdintsatang_type" || columnName == "grtright_contflag" || columnName == "grtright_memflag" || columnName == "trncollcontno_type" || columnName == "fixpayintoverfst_type" || columnName == "fixpayintovernxt_type"){
                itemChanged();
            }
            
            //1.acceptext only 
            //2.acceptext and filter other field
        }
        function OnClickCheckbox(s,r,c){
            if(c== "grtright_contflag"){
                Gcoop.CheckDw(s, r, c, "grtright_contflag", 1, 0);
            }else if(c== "grtright_memflag"){
                Gcoop.CheckDw(s, r, c, "grtright_memflag", 1, 0);
            }else if(c== "receiptsplit_flag"){
                Gcoop.CheckDw(s, r, c, "receiptsplit_flag", 1, 0);
            }
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
    <table class="t">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_cfloanconstant"
                    LibraryList="~/DataWindow/shrlon/sl_cfloanconstant.pbl" ClientScriptable="True"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventItemChanged="OnItemChanged" ClientEventClicked="OnClickCheckbox">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
