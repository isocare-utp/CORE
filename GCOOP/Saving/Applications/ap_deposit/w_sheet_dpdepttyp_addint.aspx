<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dpdepttyp_addint.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dpdepttyp_addint" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewDwHead%>
    <%=DeleteRate%>
    <%=postRate%>

    <script type="text/javascript">
    
    function OnDwHeadItemChanged(sender, rowNumber, columnName, newValue){

        if (columnName == "depttype_group") {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            objDwHead.AcceptText();
            var deptType = "";
            try {
                deptType = objDwHead.GetItem(rowNumber, "depttype_group");
                if (deptType == null) throw "";
            } catch (Err) { deptType = ""; }

            

            if (deptType != "" ) {
                postRate();
            }
        }

        else if (columnName == "date_teffect") {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            sender.AcceptText();
            objDwHead.SetItem(1, "date_effect", Gcoop.ToEngDate(newValue));
            sender.AcceptText();
//            postRate();
        }

        else if (columnName == "useall_flag") {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            objDwHead.AcceptText();
            postNewDwHead();
        }

        else if (columnName == "exprie_tdate") {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            objDwHead.AcceptText();
            // return 0;  
        }
        return 0;
    }
    function OnDwHeadClicked(sender, rowNumber, objectName){
        
        Gcoop.CheckDw(sender,rowNumber,objectName,"pro_flag",1,0);
        if(objectName == "pro_flag"){
        postNewDwHead();
        }
    }
    
    function OnDwIntRateButtonClicked(sender, rowNumber, buttonName){
        if (buttonName == "cb_delete"){
            Gcoop.GetEl("HdRow").value = rowNumber;
            DeleteRate();
        }
        else if (buttonName == "cb_insert"){
            objDwIntRate.InsertRow(objDwIntRate.RowCount() + 1);
        }
    }
    
    function Validate(){
        return confirm("บันทึกข้อมูลใช่ หรือไม่?");
    }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwHead" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_depttype_headadd_int"
        LibraryList="~/DataWindow/ap_deposit/dp_depttype_addint.pbl" ClientEventItemChanged="OnDwHeadItemChanged"
        ClientEventClicked="OnDwHeadClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwIntRate" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        DataWindowObject="d_dp_depttype_addinterest" LibraryList="~/DataWindow/ap_deposit/dp_depttype_addint.pbl"
        ClientFormatting="True" ClientEventButtonClicked="OnDwIntRateButtonClicked">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content>

