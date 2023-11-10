<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_lw_cancel_master.aspx.cs"
    Inherits="Saving.Applications.lawsys.w_sheet_lw_cancel_master" Title="Untitled Page"
    ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript"></script>

    <%=initJavaScript%>
    <%=postMemberNo%>
    <%=postLoanContractNo%>
    <%=postLoanContractDialog%>

    <script type="text/javascript">
    function MenubarNew(){
        var urls = Gcoop.GetUrl() + "Applications/" + Gcoop.GetApplication() + "/" + Gcoop.GetCurrentPage();
        if(confirm("ยืนยันการล้างหน้าจอ")){
            window.location = urls;
        }
    }
    
    function OnDwLoanAllClicked(s, r, c){
        if(r <= 0){
            return ;
        }
        if(c != "datawindow"){
            Gcoop.GetEl("HdOpenLoanDialog").value = s.GetItem(r, "loancontract_no");
            postLoanContractDialog();
        }
        return 0;
    }
    
    function OnDwLoanItemChanged(s, r, c, v){
        if(c == "loancontract_no"){
            s.SetItem(r, c, v);
            s.AcceptText();
            postLoanContractNo();
        }
    }
    
    function OnDwMainItemChanged(s, r, c, v){
        if(c == "member_no"){
            s.SetItem(r, c, v);
            s.AcceptText();
            postMemberNo();
        }
        return 0;
    }
    
    function OnDwShareClicked(s, r, c){
        if(r <= 0){
            return ;
        }
        if(c != "datawindow"){
            var memno = objDwMain.GetItem(1, "member_no");
            var shr_tcode = s.GetItem(r, "sharetype_code");
            Gcoop.OpenIFrame("650", "560", "w_dlg_sl_detail_share.aspx", "?memno=" + memno + "&shrtype=" + shr_tcode);
        }
        return 0;
    }
    
    function SheetLoadComplete(){
        ShowTabPage2(Gcoop.ParseInt(Gcoop.GetEl("HdTabIndex").value));
        if(Gcoop.GetEl("HdIsPostBack").value == "false"){
            Gcoop.Focus("member_no_0");
        }
        if(Gcoop.GetEl("HdIsOpenLoanDialog").value == "true"){
            Gcoop.OpenIFrame("640", "570", "w_dlg_sl_detail_contract.aspx", "");
        }
    }
    
    function ShowTabPage2(tab) {
        var i = 1;
        var tabamount = 5;
        for (i = 1; i <= tabamount; i++) {
            if (i == tab) {
                document.getElementById("tab_" + i).style.visibility = "visible";
                document.getElementById("stab_" + i).className = "tabTypeTdSelected";
            } else {
                document.getElementById("tab_" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).className = "tabTypeTdDefault";
            }
        }
        Gcoop.GetEl("HdTabIndex").value = tab + "";
    }
    
    function Validate(){
        return confirm("ยืนยันการบันทึกข้อมูบ");
    }
    </script>

    <style type="text/css">
        .tabTypeDefault
        {
            width: 743px;
            border-spacing: 2px;
            margin-left: 6px;
        }
        .tabTypeTdDefault
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #777777;
            border: solid 1px #55A9CD;
            background-color: rgb(200,235,255);
            cursor: pointer;
        }
        .tabTypeTdSelected
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #660066;
            border: solid 1px #77CBEF;
            background-color: #76EFFF;
            cursor: pointer;
            text-decoration: underline;
        }
        .tabTypeTdDefault:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTypeTdSelected:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTableDetail
        {
            width: 743px;
            margin-left: 6px;
        }
        .tabTableDetail td
        {
        }
        .tableMessage
        {
            border: solid 1px #77771;
            width: 743px;
            margin-left: 6px;
        }
        .tableMessage td
        {
            font-family: Tahoma, Sans-Serif, Serif;
            font-size: 14px;
            font-weight: bold;
            border: solid 1px #EE0022;
            text-align: center;
            vertical-align: middle;
            margin-top: 15px;
            margin-bottom: 15px;
            background-color: #FDDDAA;
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div id="divTableMessage" runat="server">
        <table cellpadding="0" cellspacing="0" class="tableMessage">
            <tr>
                <td>
                    <asp:Label ID="LbLoanNumberMessage" runat="server" Text="Label"></asp:Label>
                    ยังไม่ได้ลงทะเบียนยกเลิกสัญญา
                </td>
            </tr>
        </table>
        <br />
    </div>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_lw_member_detail"
        LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        ClientScriptable="True" ClientEventItemChanged="OnDwMainItemChanged">
    </dw:WebDataWindowControl>
    <hr />
    <dw:WebDataWindowControl ID="DwLoan" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
        DataWindowObject="d_lw_contract_detail" LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl"
        ClientEventItemChanged="OnDwLoanItemChanged">
    </dw:WebDataWindowControl>
    <table class="tabTypeDefault">
        <tr>
            <td class="tabTypeTdSelected" id="stab_1" onclick="ShowTabPage2(1);">
                รายการเงินกู้
            </td>
            <td class="tabTypeTdDefault" id="stab_2" onclick="ShowTabPage2(2);">
                รายการหุ้น
            </td>
            <td class="tabTypeTdDefault" id="stab_3" onclick="ShowTabPage2(3);">
                ใครค้ำให้เรา
            </td>
            <td class="tabTypeTdDefault" id="stab_4" onclick="ShowTabPage2(4);">
                เราค้ำให้ใคร
            </td>
            <td class="tabTypeTdDefault" id="stab_5" onclick="ShowTabPage2(5);">
                ทะเบียนบอกเลิกสัญญา
            </td>
        </tr>
    </table>
    <table class="tabTableDetail">
        <tr>
            <td style="height: 700px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <asp:Label ID="Label3" runat="server" Text="สัญญาเงินกู้" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwLoanAll" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_lw_contract_all" LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl"
                        ClientEventClicked="OnDwLoanAllClicked">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <asp:Label ID="Label2" runat="server" Text="รายการหุ้น" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwShare" runat="server" DataWindowObject="d_lw_share"
                        LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" ClientEventClicked="OnDwShareClicked">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <asp:Label ID="Label1" runat="server" Text="หลักประกัน" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwCollAll" runat="server" DataWindowObject="d_lw_collall"
                        LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <asp:Label ID="Label4" runat="server" Text="สัญญาเงินกู้ที่ค้ำ" Font-Bold="True"
                        Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwCollWho" runat="server" DataWindowObject="d_lw_collwho"
                        LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_5" style="visibility: hidden; position: absolute;">
                    <asp:Label ID="Label5" runat="server" Text="ทะเบียนบอกเลิกสัญญา" Font-Bold="True"
                        Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwCancel" runat="server" DataWindowObject="d_lw_canceldetail"
                        LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="300">
                    </dw:WebDataWindowControl>
                    <asp:Label ID="Label6" runat="server" Text="ผู้ค้ำสัญญาเงินกู้" Font-Bold="True"
                        Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwCollDetail" runat="server" DataWindowObject="d_lw_colldetail"
                        LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True" TabIndex="300">
                    </dw:WebDataWindowControl>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="หนี้อื่นๆ" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwLoanOther" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_lw_contract_other" LibraryList="~/DataWindow/lawsys/lw_cancel_master.pbl"
                        ClientEventClicked="OnDwLoanAllClicked">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdTabIndex" runat="server" Value="1" />
    <asp:HiddenField ID="HdOpenLoanDialog" runat="server" />
    <asp:HiddenField ID="HdIsOpenLoanDialog" runat="server" Value="false" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
</asp:Content>
