<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_botmap.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_botmap" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postGetAccyear%>
    <%=postDwPeriodInsertRow %>
    <%=postDeleteRow %>
    <%=postNewClear%>
    <%=postRefresh %>
    <%=postMainInsertRow%>
    <%=postDeleteMainRow %>
    <script type="text/javascript">
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }

        function OnDwPeriodClick(s, r, c) {
            if (c == "close_flag") {
                Gcoop.CheckDw(s, r, c, "close_flag", 1, 0);

            }
            return 0;
        }

        function OnDwAccyearClick(s, r, c) {
            if (c == "accbotmas_id" || c == "accbotmas_desc") {

                if (Gcoop.GetEl("Hdrow").value != r) {
                    Gcoop.GetEl("Hdrow").value = r + "";

                    var bot_id = objDw_main.GetItem(r, "accbotmas_id");
                    Gcoop.GetEl("HdBotid").value = bot_id;
                    postGetAccyear();
                }
            }
        }

        //เพิ่มแถว
        function B_DwmainInsert() {
                postMainInsertRow();
        }

        //เพิ่มแถว
        function B_Dw_periodInsert() {
            postDwPeriodInsertRow();
        }

        //ลบแถว
        function Delete_DwDetailClick(sender, row, bName) {
            if (bName == "b_del") {
                var accbotmas_id = "";
                accbotmas_id = objDw_main.GetItem(row, "accbotmas_id");

                if (accbotmas_id == "" || accbotmas_id == null) {
                    Gcoop.GetEl("HdRowDetail").value = row + "";
                    postDeleteRow();
                } else {
//                    var isConfirm = confirm("ต้องการลบข้อมูลรหัสบัญชีใช่หรือไม่ " + accbotmas_id + "ใช่หรือไม่ ?");
//                    if (isConfirm) {
                        Gcoop.GetEl("HdRowDetail").value = row + "";
                        postDeleteRow();
//                    }
                }
            }
            return 0;
        }

        //ลบแถว
        function Delete_DwMainClick(sender, row, bName) {
            if (bName == "b_del") {
                var accbotmas_id = "";
                accbotmas_id = objDw_main.GetItem(row, "accbotmas_id");

                if (accbotmas_id == "" || accbotmas_id == null) {
                    Gcoop.GetEl("Hdrow_mas").value = row + "";
                    postDeleteMainRow();
                } else {
                    //                    var isConfirm = confirm("ต้องการลบข้อมูลรหัสบัญชีใช่หรือไม่ " + accbotmas_id + "ใช่หรือไม่ ?");
                    //                    if (isConfirm) {
                    Gcoop.GetEl("Hdrow_mas").value = row + "";
                    postDeleteMainRow();
                    //                    }
                }
            }
            return 0;
        }
        

        
        function Validate() {
            try {
                var isconfirm = confirm("ยืนยันการบันทึกการข้อมูล?");

                if (!isconfirm) {
                    return false;
                }
                var alertstr = "";
                if (alertstr == "") {
                    return true;
                }
                else {
                    alert(alertstr);
                    return false;
                }
            }

            catch (err) {
                alert(err);
                return false;
            }
        }
       
    </script>

    <style type="text/css">
        .style2
        {
            font-weight: bold;
        }
        .style3
        {
            width: 388px;
        }
        .style4
        {
            font-weight: bold;
            width: 388px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
  <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 103%; font-size: small;">
             <tr>
             <td class="style3">
                <span class="linkSpan" onclick="B_DwmainInsert()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span>
                    </td>
                    <td>
                <span class="linkSpan" onclick="B_Dw_periodInsert()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
            </tr>
            <tr>
                <td class="style4">
                    ผังบัญชีแบงค์ชาติ
                </td>
                <td>
                    <span class="style2">รหัสที่เกี่ยวข้อง</span>
                </td>
            </tr>
            </table>
            <table>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="400px" Width="380px" ScrollBars="Auto" >
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_ac_botmaster" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
                        ClientEventClicked="OnDwAccyearClick" ClientEventButtonClicked="Delete_DwMainClick" 
                        ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" ScrollBars="Auto" 
                        Height="400px" Width="380px">
                        <dw:WebDataWindowControl ID="Dw_Detail" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_ac_botmaster_map" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
                        ClientEventButtonClicked="Delete_DwDetailClick" 
                        ClientFormatting="True" >
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HdAccyear" runat="server" />
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                    <asp:HiddenField ID="HdBotid" runat="server" />
                </td>
                <td valign="top">
                    <br />
                    <asp:HiddenField ID="HdRowDetail" runat="server" />
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <asp:HiddenField ID="Hdrow_mas" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <br />
    </asp:Content>
