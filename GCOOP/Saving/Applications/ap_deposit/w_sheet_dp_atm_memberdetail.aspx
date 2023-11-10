<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_atm_memberdetail.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_atm_memberdetail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsinitAccNo %>
    <%=postPost %>
    <%=postProvince%>
    <%=other_postPost%>
    <%=other_postProvince%>
    <%=jsAddRelateRow %>
    <%=jsDeleteRelateRow %>
    <%=jsRetive%>
    <script type="text/javascript">
      
        
       
        function OnDwMainItemChanged(s, r, c, v) {
            switch (c) {
                case "atmmember_member_no":
                    s.SetItem(r, c, v);
                    s.AcceptText();
                    jsRetive();
                    break;
               

            }
        }
        function OnDwMainButtonClicked(s, r, c) {
            switch (c) {
                case "acoount_no_search":
                    jsinitAccNo();
                    break;
            }
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame("690", "690", "w_dlg_wc_deptedit.aspx");
        }
        function setdeptNo(deptaccount_no) {
            objDwMain.SetItem(1, "deptaccount_no", deptaccount_no);
            jsinitAccNo();
        }

        function ShowTabPage2(tab) {
            var i = 1;
            var tabamount =5;
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

        function SheetLoadComplete() {
            ShowTabPage2(Gcoop.ParseInt(Gcoop.GetEl("HdTabIndex").value));
        }

       
    </script>
    <style type="text/css">
        .tabTypeDefault
        {
            width: 700px;
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
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_member_detail"
        LibraryList="~/DataWindow/ap_deposit/dp_atm_memberdetail.pbl" ClientScriptable="True"
        AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientEventItemChanged="OnDwMainItemChanged"
        AutoRestoreContext="False" ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <table class="tabTypeDefault">
        <tr>
            <td class="tabTypeTdSelected" id="stab_1" onclick="ShowTabPage2(1);">
                เงินฝาก
            </td>
            <td class="tabTypeTdDefault" id="stab_2" onclick="ShowTabPage2(2);">
                เงินกู้
            </td>
            <td class="tabTypeTdDefault" id="stab_3" onclick="ShowTabPage2(3);">
                ข้อมูลทำรายการ
            </td>
        </tr>
    </table>
    <table class="tabTableDetail">
        <tr>
            <td style="height: 470px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <asp:Label ID="Label1" runat="server" Text="เงินฝาก" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    &nbsp; &nbsp; &nbsp; 
                   
                    <dw:WebDataWindowControl ID="DwAtmdept" runat="server" DataWindowObject="d_member_atmdept"
                        LibraryList="~/DataWindow/ap_deposit/dp_atm_memberdetail.pbl" ClientScriptable="True"
                        AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientEventItemChanged="OnDwMainItemChanged"
                        AutoRestoreContext="False" ClientFormatting="True" ClientEventButtonClicked="OnDeleteRow" Width="742px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: visible; position: absolute;">
                    <asp:Label ID="Label2" runat="server" Text="เงินกู้" Font-Bold="True"
                        Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwAtmloan" runat="server" DataWindowObject="d_member_atmloan"
                        LibraryList="~/DataWindow/ap_deposit/dp_atm_memberdetail.pbl" ClientScriptable="True"
                        AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientEventItemChanged="OnDwMainItemChanged"
                        AutoRestoreContext="False" ClientFormatting="True" Width="742px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: visible; position: absolute;">
                    <asp:Label ID="Label3" runat="server" Text="ข้อมูลทำรายการ" Font-Bold="True" Font-Names="Tahoma"
                        Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
                    <br />
                    <dw:WebDataWindowControl ID="DwAtmtrans" runat="server" DataWindowObject="d_member_atmtrans"
                        LibraryList="~/DataWindow/ap_deposit/dp_atm_memberdetail.pbl" ClientScriptable="True"
                        AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" AutoRestoreContext="False"
                        ClientFormatting="True" Width="742px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdTabIndex" runat="server" Value="1" />
    <asp:HiddenField ID="HdActionStatus" runat="server" Value="" />
    <asp:HiddenField ID="HdRow" runat="server" Value="0" />
    <asp:HiddenField ID="HdSeq_no" runat="server" Value="0" />
    <asp:HiddenField ID="Hdrowcount" runat="server" Value="0" />
</asp:Content>
